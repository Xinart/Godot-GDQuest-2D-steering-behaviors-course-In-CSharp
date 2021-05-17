using System;
using Godot;

public class Target : Area2D
{
    private AnimationPlayer anim_player;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Visible = false;
        anim_player = GetNode<AnimationPlayer>("AnimationPlayer");
        Connect("body_entered", this, "_on_body_entered");

    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event.IsActionPressed("click"))
        {
            anim_player.Play("fade_in");
        }
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _PhysicsProcess(float delta)
    {
        if (Input.IsActionPressed("click"))
        {
            GlobalPosition = GetGlobalMousePosition();
        }
    }

    public void _on_body_entered(PhysicsBody2D body)
    {
        anim_player.Play("fade_out");
    }
}
