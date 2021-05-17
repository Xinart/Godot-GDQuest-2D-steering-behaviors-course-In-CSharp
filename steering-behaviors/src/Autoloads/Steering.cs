using System;
using Godot;

public class Steering : Node
{
    private const int DEFAULT_MASS = 2;
    private const int DEFAULT_MAX_SPEED = 2;

    public Vector2 follow(
        Vector2 velocity,
        Vector2 global_position,
        Vector2 target_position,
        int max_speed = DEFAULT_MAX_SPEED,
        int mass = DEFAULT_MASS
        )
    {
        Vector2 desired_velocity = (target_position - global_position).Normalized() * max_speed;
        Vector2 steering = (desired_velocity - velocity) / mass;
        return velocity + steering;
    }

}
