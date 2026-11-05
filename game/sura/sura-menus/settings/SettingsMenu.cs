using Godot;

public partial class SettingsMenu : Control
{
	[Signal] public delegate void BackRequestedEventHandler();

	public override void _Ready()
	{
		if (!OS.HasFeature("mobile")) return;
		TabContainer sections = GetNode<TabContainer>("MarginContainer/ConfigSections");
		sections.SetTabHidden(2, true);
		sections.CurrentTab = 0;
	}

	public void OnBackPressed() => EmitSignal(SignalName.BackRequested);
}
