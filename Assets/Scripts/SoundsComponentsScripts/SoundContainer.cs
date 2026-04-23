namespace SoundsComponentsScripts
{
    public class SoundContainer : AudioContainer
    {
        public SoundPair[] AppearClips;
        public SoundPair[] ActionClips;
        public SoundPair[] DeathClips;

        public override void Play(SoundType soundType)
        {
            switch (soundType)
            {
                case SoundType.AppearSound:
                    PlayRandomSound(AppearClips);
                    break;
                case SoundType.ActionSound:
                    PlayRandomSound(ActionClips);
                    break;
                case SoundType.DeathSound:
                    PlayRandomSound(DeathClips);
                    break;
            }
        }
    }
}