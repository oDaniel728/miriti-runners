using Godot;

public partial class HoverButton : TextureRect
{
    [Export] public float HoverScale { get; set; } = 1.1f;
    [Export] public float AnimDuration { get; set; } = 0.15f;

    private Tween _hoverTween;
    private Vector2 _originalScale;
    private bool _isHovering;
    private bool _wasMouseDown;

    public override void _Ready()
    {
        _originalScale = Scale;
        MouseFilter = MouseFilterEnum.Stop;
    }

    public override void _Process(double delta)
    {
        bool hovering = GetGlobalRect().HasPoint(GetViewport().GetMousePosition());
        if (hovering != _isHovering)
        {
            _isHovering = hovering;
            if (hovering) OnMouseEntered();
            else OnMouseExited();
        }

        bool mouseDown = Input.IsMouseButtonPressed(MouseButton.Left);
        if (hovering && mouseDown && !_wasMouseDown)
        {
            OnClicked();
        }
        _wasMouseDown = mouseDown;
    }

    private void OnMouseEntered()
    {
        _hoverTween?.Kill();
        _hoverTween = CreateTween();
        _hoverTween.TweenProperty(this, "scale", _originalScale * HoverScale, AnimDuration)
            .SetTrans(Tween.TransitionType.Back)
            .SetEase(Tween.EaseType.Out);
    }

    private void OnMouseExited()
    {
        _hoverTween?.Kill();
        _hoverTween = CreateTween();
        _hoverTween.TweenProperty(this, "scale", _originalScale, AnimDuration)
            .SetTrans(Tween.TransitionType.Back)
            .SetEase(Tween.EaseType.Out);
    }

    public override void _GuiInput(InputEvent @event)
    {
        // Clique tratado em _Process para não depender da ordem dos nós.
    }

    protected virtual void OnClicked() { }
}
