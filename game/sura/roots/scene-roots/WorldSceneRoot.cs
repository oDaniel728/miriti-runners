using Godot;
using System;

public partial class WorldSceneRoot : Node2D, ISceneRoot
{
	[Export] public AudioStream InitialMusic {get; private set;}
	[Export] public AudioStream AmbienceMusic {get; private set;}
	[Export] public Player _player;
	public bool hideMobileControls { get; private set; } = true;
	

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//GameServices.Current.Hud.SetGameplayVisible(hideMobileControls);
		//_player.SetNewPosition(GameServices.Current.Scene.playerInitialNewSceneCoords);
		
		GameServices.Current.Scene.FinishSceneChange();
		GameServices.Current.Audio.PlayMusic(InitialMusic);
		GameServices.Current.Audio.PlaySFX(AmbienceMusic, "Ambience", 1f, -10f);
		//_doorsContainer.SceneChangeRequest += OnSceneChangeRequest;
	}

	public void OnSceneChangeRequest(PackedScene targetScene, Vector2I targetCoords)
	{
		GameServices.Current.Scene.SetPlayerInitialNewSceneCoords(targetCoords);
		GameServices.Current.Scene.StartSceneChange(targetScene);
	}
}
