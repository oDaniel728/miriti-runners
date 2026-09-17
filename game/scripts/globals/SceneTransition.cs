using Godot;

public partial class SceneTransition : CanvasLayer
{
    public static SceneTransition Instance { get; private set; }

    private ColorRect _fadeRect;
    private Tween _tween;

    [Export] public float FadeDuration { get; set; } = 0.5f;

    public override void _Ready()
    {
        Instance = this;

        _fadeRect = new ColorRect
        {
            Color = new Color(0, 0, 0, 0),
            MouseFilter = Control.MouseFilterEnum.Stop
        };
        _fadeRect.SetAnchorsPreset(Control.LayoutPreset.FullRect);
        AddChild(_fadeRect);

        Layer = 10;
    }

    public void TransitionTo(string scenePath, float? duration = null)
    {
        float dur = duration ?? FadeDuration;
        StartTransition(scenePath, dur);
    }

    private async void StartTransition(string scenePath, float duration)
    {
        ProcessMode = ProcessModeEnum.Always;

        _tween?.Kill();
        _tween = CreateTween();
        _tween.TweenProperty(_fadeRect, "color:a", 1.0f, duration)
            .SetTrans(Tween.TransitionType.Sine)
            .SetEase(Tween.EaseType.InOut);
        await ToSignal(_tween, Tween.SignalName.Finished);

        GetTree().ChangeSceneToFile(scenePath);

        _tween?.Kill();
        _tween = CreateTween();
        _tween.TweenProperty(_fadeRect, "color:a", 0.0f, duration)
            .SetTrans(Tween.TransitionType.Sine)
            .SetEase(Tween.EaseType.InOut);
        await ToSignal(_tween, Tween.SignalName.Finished);

        ProcessMode = ProcessModeEnum.Inherit;
    }
}
