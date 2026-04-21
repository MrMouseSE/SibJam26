using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ScenesOperatingScripts
{
    public class GameSystemsHandler
    {
        public Dictionary<Type, IGameSystem> GameSystems = new Dictionary<Type, IGameSystem>();

        private CancellationTokenSource _updateCancellationTokenSource = new CancellationTokenSource();
        private bool _isProcess;
        public GameSystemsHandler()
        {
            _isProcess = true;
            UpdateSystems(_updateCancellationTokenSource.Token).Forget();
        }

        public void AddGameSystem(IGameSystem gameSystem)
        {
            GameSystems.Add(gameSystem.GetType(), gameSystem);
        }

        public void RemoveGameSystem(IGameSystem gameSystem)
        {
            GameSystems.Remove(gameSystem.GetType());
        }

        public IGameSystem GetGameSystem(Type type)
        {
            return GameSystems[type];
        }

        private async UniTask UpdateSystems(CancellationToken token)
        {
            while (_isProcess)
            {
                foreach (var gameSystem in GameSystems)
                {
                    gameSystem.Value.UpdateSystem(this, Time.deltaTime);
                }
                await UniTask.DelayFrame(1, cancellationToken: token);
            }
        }

        public void DisposeSystems()
        {
            _isProcess = false;
            _updateCancellationTokenSource.Cancel();
            foreach (var gameSystem in GameSystems)
            {
                gameSystem.Value.DisposeSystem();
            }
        }
    }
}