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

internal record struct NewStruct(object Item1, object Item2)
{
    public static implicit operator (object, object)(NewStruct value)
    {
        return (value.Item1, value.Item2);
    }

    public static implicit operator NewStruct((object, object) value)
    {
        return new NewStruct(value.Item1, value.Item2);
    }
}