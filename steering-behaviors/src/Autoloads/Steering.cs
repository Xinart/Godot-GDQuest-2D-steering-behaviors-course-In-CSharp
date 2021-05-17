using System;
using Godot;

public class Steering : Node
{
    private const int DEFAULT_MASS = 2;
    private const int DEFAULT_MAX_SPEED = 400;
    private const int DEFAULT_SLOW_RADIUS = 200;

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

    public Vector2 arrive_to(
       Vector2 velocity,
       Vector2 global_position,
       Vector2 target_position,
       int max_speed = DEFAULT_MAX_SPEED,
       int slow_radius = DEFAULT_SLOW_RADIUS,
       int mass = DEFAULT_MASS
       )
    {
        float distance_to_target = global_position.DistanceTo(target_position);
        Vector2 desired_velocity = (target_position - global_position).Normalized() * max_speed;
        if (distance_to_target < slow_radius)
        {
            desired_velocity *= (float)((distance_to_target / slow_radius) * 0.8 + 0.2);
        }

        Vector2 steering = (desired_velocity - velocity) / mass;
        return velocity + steering;
    }

}
