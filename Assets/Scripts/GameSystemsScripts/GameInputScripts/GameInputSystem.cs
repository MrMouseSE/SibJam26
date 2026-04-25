using ScenesOperatingScripts;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GameSystemsScripts.GameInputScripts
{
    public class GameInputSystem : IGameSystem
    {
        public GameInputMechanic Mechanic;
        public GameInputComponent Component;

        public GameInputSystem()
        {
            Component = new GameInputComponent();
            Mechanic = new GameInputMechanic(Component);
            
        }

        public void UpdateSystem(GameSystemsHandler gameSystemsHandler, float deltaTime)
        {
            if (Component.IsInputLocked) return;
            Mechanic.UpdateMechanic(gameSystemsHandler, deltaTime);
        }

        public void DisposeSystem()
        {
            Mechanic.DisposeMechanic();
        }
    }

    public class GameInputComponent
    {
        public bool IsInputLocked;
        public bool MouseWasPressedThisFrame;
        public Vector2 MousePressedPosition;
        public Collider[] MousePressedHitColliders;
        public bool MouseWasReleasedThisFrame;
        public Vector2 MouseReleasedPosition;
        public Collider[] MouseReleasedHitColliders;
    }

    public class GameInputMechanic : IGameMechanic
    {
        public GameInputComponent Component;

        public GameInputMechanic(GameInputComponent component)
        {
            Component = component;
        }
        
        public void UpdateMechanic(GameSystemsHandler gameSystemsHandler, float deltaTime)
        {
            Component.MouseWasPressedThisFrame = Mouse.current.leftButton.wasPressedThisFrame;
            if (Component.MouseWasPressedThisFrame)
            {
                Component.MousePressedPosition = Mouse.current.position.ReadValue();
            }
            Component.MouseWasReleasedThisFrame = Mouse.current.rightButton.wasPressedThisFrame;
            if (Component.MouseWasReleasedThisFrame)
            {
                Component.MouseReleasedPosition = Mouse.current.position.ReadValue();
            }
        }
        
        public void DisposeMechanic()
        {
        }
    }
}