using ScenesOperatingScripts;

namespace GameSystemsScripts
{
    public interface IGameSystem
    {
        public void Initialize(GameSystemsHandler gameSystemsHandler);
        public void UpdateSystem(GameSystemsHandler gameSystemsHandler, float deltaTime);
        public void DisposeSystem();
    }
}