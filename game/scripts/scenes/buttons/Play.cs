using Godot;

public partial class Play : HoverButton
{
    [Export] public string ScenePath { get; set; } = "res://scenes/game.tscn";

    protected override void OnClicked()
    {
        SceneTransition.Instance.TransitionTo(ScenePath);
    }
}
