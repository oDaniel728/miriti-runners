using Godot;
using System;

public partial class Main : Node
{
	[Export] public Node _sceneContainer { get; private set; }
	[Export] public Gui _GUI { get; private set; }
	[Export] private PackedScene InitialScene;
	[Export] private Vector2I initialPlayerCoords;
	[Export] private Texture2D cursorNormalSprite;
	[Export] private Texture2D cursorClickSprite;
	[Export] private AudioStream initialMusic;
	

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		TranslationServer.SetLocale("pt_BR");

		GameServices.Current.Scene.SetPlayerInitialNewSceneCoords(initialPlayerCoords);

		GameServices.Current.Scene.SetTransitionNode(_GUI.GetSceneTransition());
		GameServices.Current.Scene.SetSceneContainerNode(_sceneContainer);
		GameServices.Current.Scene.LoadInitialScene(InitialScene);
		
		SetCursorSprites(cursorNormalSprite, cursorClickSprite);

		GameServices.Current.Audio.PlayMusic(initialMusic);
	}
	private void SetCursorSprites(Texture2D normalCursor, Texture2D clickCursor)
	{
		    Input.SetCustomMouseCursor(
			normalCursor,
			Input.CursorShape.Arrow,
			new Vector2(0, 0)
		);

		Input.SetCustomMouseCursor(
			clickCursor,
			Input.CursorShape.PointingHand,
			new Vector2(0, 0)
		);
	}
}
