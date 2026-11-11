using Godot;
using System;

public partial class GameOver : Control
{
    private bool shown = false;
    private Tween tween;

    [Export] private Label titleLabel = null;
    [Export] private Label subtitleLabel = null;

    public override void _Ready()
    {
        Modulate = new Color(1, 1, 1, 0);
        Visible = false;

        GameEvents.OnPlayerDied += OnPlayerDied;
    }

    private void OnPlayerDied(Node cause)
    {
        showScreen();
    }

    public void showScreen()
    {
        if (shown) return;

        shown = true;
        tween?.Kill();

        Visible = true;

        tween = CreateTween();
        tween.TweenProperty(this, "modulate:a", 1.0f, 0.5f);
    }

    public async void hideScreen()
    {
        if (!shown) return;

        shown = false;
        tween?.Kill();

        tween = CreateTween();
        tween.TweenProperty(this, "modulate:a", 0.0f, 0.5f);

        await ToSignal(tween, Tween.SignalName.Finished);

        if (!shown && IsInsideTree())
            Visible = false;
    }

    public override void _Input(InputEvent e)
    {
        bool spacePressed =
            e is InputEventKey k
            && k.Pressed
            && !k.Echo
            && k.Keycode == Key.Space
            && shown
            && tween != null
            && !tween.IsRunning();

        if (spacePressed)
        {
            GameEvents.TriggerGameOverEnded();
            hideScreen();

            GetViewport().SetInputAsHandled();
        }
    }

    public override void _ExitTree()
    {
        GameEvents.OnPlayerDied -= OnPlayerDied;
        tween?.Kill();
    }
}