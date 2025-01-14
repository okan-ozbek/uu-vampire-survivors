using UnityEngine;

namespace Controllers.Player
{
    public static class PlayerInputController
    {
        public static Vector3 MovementDirection => new(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0.0f);
        public static Vector3 NormalizedMovementDirection => MovementDirection.normalized;
        
        public static bool DashKeyPressed => Input.GetKeyDown(KeyCode.Space);
        public static bool AttackKeyPressed => Input.GetKeyDown(KeyCode.Mouse0);
        public static bool AttackKeyHeld => Input.GetKey(KeyCode.Mouse0);
        public static bool AttackKeyReleased => Input.GetKeyUp(KeyCode.Mouse0);
    }
}