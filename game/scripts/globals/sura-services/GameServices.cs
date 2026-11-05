using Godot;
using System;

public partial class GameServices : Node
{
	public static GameServices Current { get; private set; }
	public SceneService Scene { get; private set; }
	public AudioService Audio { get; private set; }
	public DialogService Dialog { get; private set; }
	public DataService Data { get; private set; }
	public SettingsService Settings { get; private set; }
	public Gui Hud { get; private set; }
    // Called when the node enters the scene tree for the first time.
    public override void _EnterTree()
    {
        Current = this;
    }

	public void RegisterHud(Gui hud) => Hud = hud;

	public void Register(Service service)
	{
		switch (service)
		{
			case SceneService scene: Scene = scene; break;
			case AudioService audio: Audio = audio; break;
			case DialogService dialog: Dialog = dialog; break;
			case DataService data: Data = data; break;
			case SettingsService settings: Settings = settings; break;
		}
	}
}
