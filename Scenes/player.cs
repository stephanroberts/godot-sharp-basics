using Godot;
using System;

public partial class player : CharacterBody3D
{
	[Export]
	public int Speed {get; set; } = 14;
	[Export]
	public int FallAcceleration { get; set; } = 75;

	private Vector3 _targetVelocity = Vector3.Zero;

    public override void _PhysicsProcess(double delta)
    {
        var direction = Vector3.Zero;
		
		if (Input.IsActionPressed("moveRight")) {
			direction.X += 1.0f;
		}
		if (Input.IsActionPressed("moveLeft")) {
			direction.X -= 1.0f;
		}
		if (Input.IsActionPressed("moveDown")) {
			direction.Z += 1.0f;
		}
		if (Input.IsActionPressed("moveUp")) {
			direction.Z -= 1.0f;
		}
		
		if (direction != Vector3.Zero) {
			direction = direction.Normalized();
			GetNode<Node3D>("Pivot").LookAt(Position + direction, Vector3.Up);
		}

		_targetVelocity.X = direction.X * Speed;
		_targetVelocity.Z = direction.Z * Speed;

		if(!IsOnFloor()) {
			_targetVelocity.Y -= FallAcceleration * Speed;
		}

		Velocity = _targetVelocity;

		MoveAndSlide();
    }
}
