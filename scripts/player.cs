using Godot;
using System;

public partial class player : CharacterBody3D
{
	[Export]
	public int Speed {get; set; } = 14;
	[Export]
	public int FallAcceleration { get; set; } = 1;
	[Export]
	public int JumpImpulse { get; set; } = 50;
	[Export]
	public int BounceImpulse { get; set; } = 30;

	[Signal]
	public delegate void HitEventHandler();

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
			GetNode<AnimationPlayer>("AnimationPlayer").SpeedScale = 4;
		} else {
			GetNode<AnimationPlayer>("AnimationPlayer").SpeedScale = 1;
		}

		_targetVelocity.X = direction.X * Speed;
		_targetVelocity.Z = direction.Z * Speed;

		if(!IsOnFloor()) {
			_targetVelocity.Y -= FallAcceleration * (Speed / 3);
		}

		if (IsOnFloor() && Input.IsActionJustPressed("jump")) {
			_targetVelocity.Y = JumpImpulse;
        }

		for (int index = 0; index < GetSlideCollisionCount(); index++)
		{
			KinematicCollision3D collision = GetSlideCollision(index);

			if (collision.GetCollider() is mob mob)
			{
				if (Vector3.Up.Dot(collision.GetNormal()) > 0.1f)
				{
					mob.Squash();
					_targetVelocity.Y = BounceImpulse;
				}
			}
		}

		Velocity = _targetVelocity;


		MoveAndSlide();

		var pivot = GetNode<Node3D>("Pivot");
		pivot.Rotation = new Vector3(Mathf.Pi / 6.0f * Velocity.Y / JumpImpulse, pivot.Rotation.Y, pivot.Rotation.Z);
    }

	private void Die() {
		EmitSignal(SignalName.Hit);
		QueueFree();
	}

	public void OnMobDetectorBodyEntered(Node3D body) {
		Die();
	}
}
