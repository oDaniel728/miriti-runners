using Godot;

public partial class Play : HoverButton
{
    [Export] public string ScenePath { get; set; } = "res://scenes/game.tscn";

    protected override void OnClicked()
    {
        OldSceneTransition.Instance.TransitionTo(ScenePath);
    }
}
