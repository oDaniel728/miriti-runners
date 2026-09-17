using Godot;

public partial class Exit : HoverButton
{
    protected override void OnClicked()
    {
        GetTree().Quit();
    }
}
