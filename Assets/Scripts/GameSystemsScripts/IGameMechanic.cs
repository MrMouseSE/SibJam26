using ScenesOperatingScripts;

namespace GameSystemsScripts
{
    public interface IGameMechanic
    {
        public void UpdateMechanic(GameSystemsHandler gameSystemsHandler, float deltaTime);
        public void DisposeMechanic();
    }
}