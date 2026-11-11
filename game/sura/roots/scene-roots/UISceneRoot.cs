using Godot;
using System;

public partial class UISceneRoot : Control, ISceneRoot
{

	[Export] public AudioStream InitialMusic { get; private set; }
	public bool hideMobileControls { get; private set; } = false;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GameServices.Current.Scene.FinishSceneChange();
		GameServices.Current.Audio.PlayMusic(InitialMusic);
	}
	public void OnSceneChangeRequest(PackedScene targetScene, Vector2I targetCoords)
	{
		GameServices.Current.Scene.SetPlayerInitialNewSceneCoords(targetCoords);
		GameServices.Current.Scene.StartSceneChange(targetScene);
	}
}
