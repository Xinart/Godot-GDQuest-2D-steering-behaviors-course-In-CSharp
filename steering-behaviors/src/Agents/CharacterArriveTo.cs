using System;
using Godot;

public class CharacterArriveTo : KinematicBody2D
{
    Sprite sprite;

    private const int DISTANCE_THRESHOLD = 3;

    [Export] private int max_speed = 500;
    [Export] private int slow_radius = 200;
    private Vector2 _velocity = Vector2.Zero;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        sprite = GetNode<Sprite>("TriangleRed");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        Vector2 target_global_position = GetGlobalMousePosition();

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
    }
}
