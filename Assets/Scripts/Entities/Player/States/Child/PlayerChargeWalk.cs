namespace Entities.Player.States.Child;

public class PlayerChargeWalk : PlayerState
{
    public PlayerChargeWalk(PlayerCore core) : base(core)
    {
    }

    protected override void OnUpdate()
    {
        Movement.Accelerate(Core.Data.chargeSpeed);
        Movement.Decelerate();
        Movement.Brake();
    }

    protected override void SetTransitions()
    {
        AddTransition(typeof(PlayerChargeIdle), () => Core.Body.linearVelocity == Vector2.zero && PlayerInput.MovementDirection == Vector3.zero);
        AddTransition(typeof(PlayerChargeWalk), () => PlayerInput.MovementDirection != Vector3.zero);
    }
}