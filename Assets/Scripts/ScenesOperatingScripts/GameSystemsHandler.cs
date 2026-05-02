using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using GameSystemsScripts;
using GameSystemsScripts.GameStateScripts;
using UnityEngine;

namespace ScenesOperatingScripts
{
    public class GameSystemsHandler
    {
        public GameStateSystem StateSystem;
        
        public Dictionary<Type, IGameSystem> GameSystems = new Dictionary<Type, IGameSystem>();

        private CancellationTokenSource _updateCancellationTokenSource = new CancellationTokenSource();
        private bool _isProcess;
        
        public GameSystemsHandler()
        {
            _isProcess = true;
            UpdateSystems(_updateCancellationTokenSource.Token).Forget();
        }

        public void AddStateSystem(GameStateSystem stateSystem)
        {
            StateSystem = stateSystem;
            AddGameSystem(stateSystem);
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

        public void InitializeSystems()
        {
            foreach (var gameSystem in GameSystems)
            {
                gameSystem.Value.Initialize(this);
            }
        }

        private async UniTask UpdateSystems(CancellationToken token)
        {
            while (_isProcess)
            {
                foreach (var gameSystem in GameSystems)
                {
                    gameSystem.Value.UpdateSystem(this, Time.deltaTime);
                }
                await UniTask.Yield();
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