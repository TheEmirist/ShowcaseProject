using UnityEngine;

namespace Emirist.CharacterController
{
    public class PlayerState : MonoBehaviour
    {
        [field: SerializeField] public PlayerMovementState CurrentPlayerMovementState { get; private set; } = PlayerMovementState.Idle;

        public void SetPlayerMovementState(PlayerMovementState playerMovementState)
        {
            CurrentPlayerMovementState = playerMovementState;
        }

        public bool IsGroundedState()
        {
            return IsStateGroundedState(CurrentPlayerMovementState);
        }

        public bool IsStateGroundedState(PlayerMovementState playerMovementState)
        {
            return playerMovementState == PlayerMovementState.Idle || 
                    playerMovementState == PlayerMovementState.Walking || 
                    playerMovementState == PlayerMovementState.Running || 
                    playerMovementState == PlayerMovementState.Sprinting;
        }
    }

    public enum PlayerMovementState
    {
        Idle = 0,
        Walking = 1,
        Running = 2,
        Sprinting = 3,
        Jumping = 4,
        Falling = 5,
        Strafing = 6,
    }
}