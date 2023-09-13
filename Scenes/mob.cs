using Godot;
using System;

public partial class mob : CharacterBody3D
{
    [Export]
    public int MinSpeed { get; set; } = 10;

    [Export]
    public int MaxSpeed { get; set; } = 18;

    public override void _PhysicsProcess(double delta){
        MoveAndSlide();
    }

    public void Initialize(Vector3 startPosition, Vector3 playerPosition) {
        int randomSpeed = GD.RandRange(MinSpeed, MaxSpeed);

        Velocity = Vector3.Forward * randomSpeed;

        Velocity = Velocity.Rotated(Vector3.Up, Rotation.Y);
    }

    private void OnVisibilityNotifierScreenExited() {
        QueueFree();
    }
}
