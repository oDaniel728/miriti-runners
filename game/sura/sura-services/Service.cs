using Godot;

/// <summary>Base for globally registered game services.</summary>
public abstract partial class Service : Node
{
	public override void _EnterTree()
	{
		GameServices.Current.Register(this);
	}
}
