using System;
using Godot;

public class CharacterArriveTo : KinematicBody2D
{
    Sprite sprite;

    private const int DISTANCE_THRESHOLD = 3;

    [Export] private int max_speed = 500;
    [Export] private int slow_radius = 200;
    private Vector2 target_global_position = Vector2.Zero;
    private Vector2 _velocity = Vector2.Zero;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        SetPhysicsProcess(false);
        sprite = GetNode<Sprite>("TriangleRed");
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event.IsActionPressed("click"))
        {
            target_global_position = GetGlobalMousePosition();
            SetPhysicsProcess(true);
        }
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _PhysicsProcess(float delta)
    {
        if (Input.IsActionPressed("click"))
        {
            target_global_position = GetGlobalMousePosition();
        }
        // If the cursor is already on the Character follow, there is no need to make move the player
        if (GlobalPosition.DistanceTo(target_global_position) >= DISTANCE_THRESHOLD)
        {
            _velocity = GetNode<Steering>("/root/Steering").arrive_to(
                _velocity,
                GlobalPosition,
                target_global_position,
                max_speed,
                slow_radius
            );

            _velocity = MoveAndSlide(_velocity);
            sprite.Rotation = _velocity.Angle();
        }
        else SetPhysicsProcess(false);
    }
}
