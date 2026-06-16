using System;
using ScenesOperatingScripts;

namespace GameSystemsScripts.ScoreViewScripts.GameTimerScripts
{
    public class GameTimerMechanic : IGameMechanic
    {
        public GameTimerComponent Component;

        public GameTimerMechanic(GameTimerComponent component)
        {
            Component = component;
        }

        public void SetContainer(GameTimerContainer container)
        {
            Component.Container = container;
            Component.ElapsedTime = 0f;
            if (Component.Container == null || Component.Container.TimerText == null) return;
            UpdateView();
        }

        public void UpdateMechanic(GameSystemsHandler gameSystemsHandler, float deltaTime)
        {
            if (Component.Container == null || Component.Container.TimerText == null) return;
            Component.ElapsedTime += deltaTime;
            UpdateView();
        }

        public void DisposeMechanic()
        {
        }

        private void UpdateView()
        {
            var timeSpan = TimeSpan.FromSeconds(Component.ElapsedTime);
            Component.Container.TimerText.text = $"{timeSpan.Minutes:00}:{timeSpan.Seconds:00}";
        }
    }
}
