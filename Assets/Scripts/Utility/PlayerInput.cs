using UnityEngine;

namespace Utility
{
    public static class PlayerInput
    {
        public static Vector2 MovementDirection => new(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        public static Vector2 NormalizedMovementDirection => MovementDirection.normalized;
        
        public static bool DashKeyPressed => Input.GetKeyDown(KeyCode.Space);
        public static bool AttackKeyPressed => Input.GetKeyDown(KeyCode.Mouse0);
        public static bool HurtKeyPressed => Input.GetKeyDown(KeyCode.Mouse1);
        public static bool RunKeyHeld => Input.GetKey(KeyCode.LeftShift);
    }
}