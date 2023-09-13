using Godot;
using System;

public partial class main : Node
{
    [Export]
    public PackedScene MobScene {get; set;}



    public override void _Ready()
    {
        Timer myTimer = GetNode<Timer>("MobTimer");
        myTimer.Timeout += () => this.OnMobTimerTimeout();
    }

    public void OnMobTimerTimeout() {
        mob mob = MobScene.Instantiate<mob>();

        var mobSpawnLocation = GetNode<PathFollow3D>("SpawnPath/SpawnLocation");
        mobSpawnLocation.ProgressRatio = GD.Randf();
        Vector3 playerPosition = GetNode<player>("Player").Position;
        mob.Initialize(mobSpawnLocation.Position, playerPosition);

        AddChild(mob);
    }
}
