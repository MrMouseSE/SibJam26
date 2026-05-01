using AnimationDescriptionsScripts;

namespace GameSystemsScripts.GameSpeedScripts
{
    public class GameSpeedComponent
    {
        public float CurrentGameSpeed;
        public float CurrentAnimationsSpeed;
        
        public AnimationsDescription AnimationsDescription;

        public GameSpeedComponent(AnimationsDescription animationsDescription)
        {
            AnimationsDescription = animationsDescription;
        }
    }
}