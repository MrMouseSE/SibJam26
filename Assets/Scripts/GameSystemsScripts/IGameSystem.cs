namespace ScenesOperatingScripts
{
    public interface IGameSystem
    {
        public void UpdateSystem(GameSystemsHandler gameSystemsHandler, float deltaTime);
        public void DisposeSystem();
    }
}