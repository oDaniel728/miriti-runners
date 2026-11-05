using Godot;

public partial class CreditsMenu : Control
{
	[Signal] public delegate void BackRequestedEventHandler();
	public void OnBackPressed() => EmitSignal(SignalName.BackRequested);
}
