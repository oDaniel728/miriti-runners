using Godot;
using System;

public partial class CutsceneSceneRoot : Node, ISceneRoot
{
	[Export] public AudioStream InitialMusic {get; private set;}
	public bool hideMobileControls { get; private set; } = true;

	public override void _Ready()
	{
		GameServices.Current.Hud.SetGameplayVisible(hideMobileControls);
		GameServices.Current.Scene.FinishSceneChange();
		GameServices.Current.Audio.PlayMusic(InitialMusic);
		
	}
	public void OnSceneChangeRequest(PackedScene targetScene, Vector2I targetCoords)
	{
		GameServices.Current.Scene.SetPlayerInitialNewSceneCoords(targetCoords);
		GameServices.Current.Scene.StartSceneChange(targetScene);
	}
}
