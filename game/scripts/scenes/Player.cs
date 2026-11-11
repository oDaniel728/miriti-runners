using Godot;
using System;

public partial class Player : CharacterBody2D
{
    [Export]
    public float Speed { get; set; } = 1000f;

    [Export]
    public float Acceleration { get; set; } = 1000f;

    [Export]
    public float Friction { get; set; } = 2000f;

    private float currentSpeedX = 0f;
    private Vector2 initialPosition;
    private bool isDead = false;

    public override void _Ready()
    {
        initialPosition = GlobalPosition;

        GameEvents.OnPlayerDied += OnPlayerDied;
        GameEvents.OnGameOverEnded += Restart;
    }

    private void OnPlayerDied(Node cause)
    {
        if (isDead) return;

        isDead = true;
        currentSpeedX = 0f;
        Velocity = Vector2.Zero;

        GD.Print($"Player died: {cause}");
    }

    public void Restart()
    {
        isDead = false;

        GlobalPosition = initialPosition;
        Velocity = Vector2.Zero;
        currentSpeedX = 0f;

        GameManager.Instance.ResetPlayerPace();

        ResetPhysicsInterpolation();

        GD.Print("Player restarted");
    }

    public void BoostPlayer(float value)
    {
        if (isDead) return;

        currentSpeedX += value;
    }

    public override void _PhysicsProcess(double delta)
    {
        if (isDead) return;

        if (Input.IsActionPressed("sprint"))
        {
            GameManager.Instance.AcceleratePlayerPace(.05f);
        }
        else
        {
            if (GameManager.Instance.GetPlayerPace() > 1)
                GameManager.Instance.AcceleratePlayerPace(-.1f);
            else
                GameManager.Instance.ResetPlayerPace();
        }

        float direction = Input.GetAxis("left", "right");
        float targetSpeed = direction * Speed;
        float deltaSpeed = (float)delta;

        if (direction != 0)
        {
            currentSpeedX = Mathf.MoveToward(
                currentSpeedX,
                targetSpeed,
                Acceleration * deltaSpeed * 2
            );
        }
        else
        {
            currentSpeedX = Mathf.MoveToward(
                currentSpeedX,
                0,
                Friction * deltaSpeed * 2
            );
        }

        var vel = Velocity;

        vel.Y = 0;
        vel.X = currentSpeedX;

        Velocity = vel;

        MoveAndSlide();
    }

    public override void _ExitTree()
    {
        GameEvents.OnPlayerDied -= OnPlayerDied;
        GameEvents.OnGameOverEnded -= Restart;
    }
}
