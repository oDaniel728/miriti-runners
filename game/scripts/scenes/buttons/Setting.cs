using Godot;

public partial class Setting : HoverButton
{
    [Export] public string ScenePath { get; set; } = "res://scenes/control/menu_opcoes.tscn";

    protected override void OnClicked()
    {
        SceneTransition.Instance.TransitionTo(ScenePath);
    }
}
