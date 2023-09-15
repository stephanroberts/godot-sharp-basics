using Godot;
using System;

public partial class main : Node
{
    [Export]
    public PackedScene MobScene {get; set;}



    public override void _Ready()
    {
        GetNode<Control>("UserInterface/Retry").Hide();
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event.IsActionPressed("jump") && GetNode<Control>("UserInterface/Retry").Visible) {
            GetTree().ReloadCurrentScene();
        }
    }
    public void OnMobTimerTimeout() {
        mob mob = MobScene.Instantiate<mob>();
    
        var mobSpawnLocation = GetNode<PathFollow3D>("SpawnPath/SpawnLocation");
        mobSpawnLocation.ProgressRatio = GD.Randf();
        Vector3 playerPosition = GetNode<player>("Player").Position;
        mob.Initialize(mobSpawnLocation.Position, playerPosition);

        AddChild(mob);

        mob.Squashed += GetNode<ScoreLabel>("UserInterface/ScoreLabel").OnMobSquashed;
    }

    public void OnPlayerHit() {
        GetNode<Timer>("MobTimer").Stop();
        GetNode<Control>("UserInterface/Retry").Show();
    }
}
