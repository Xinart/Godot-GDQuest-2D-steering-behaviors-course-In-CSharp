using System;
using Godot;

public class CharacterFollow : KinematicBody2D
{
    private Sprite sprite;
    private Node2D target;

    private const int ARRIVE_THRESHOLD = 3;

    [Export] NodePath target_path = new NodePath();
    [Export] int follow_offset = 200;
    [Export] int max_speed = 500;
    private Vector2 _velocity = Vector2.Zero;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        target = GetNode<Node2D>(target_path);
        sprite = GetNode<Sprite>("TriangleRed");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _PhysicsProcess(float delta)
    {
        // if target_path is not set, it is the path of this node istance, and so we do not let him process correctly
        if (target == this) SetPhysicsProcess(false);
        Vector2 target_global_position = target.GlobalPosition;

        float distance_to_target = GlobalPosition.DistanceTo(target_global_position);

        if (distance_to_target >= ARRIVE_THRESHOLD)
        {
            Vector2 follow_global_position = distance_to_target > follow_offset ?
            target_global_position -
                (target_global_position - GlobalPosition).Normalized() * follow_offset
            : GlobalPosition;

            _velocity = GetNode<Steering>("/root/Steering").arrive_to(
                _velocity,
                GlobalPosition,
                follow_global_position,
                max_speed,
                200,
                20
            );

            _velocity = MoveAndSlide(_velocity);
            sprite.Rotation = _velocity.Angle();
        }
    }
}
