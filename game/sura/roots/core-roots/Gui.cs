using Godot;
using System;

public partial class Gui : Control
{
	[Export] public SceneTransition _sceneTransition { get; private set; }
	[Export] public PauseMenu _pauseMenu { get; private set; }
	public override void _Ready()
	{

	}
	public SceneTransition GetSceneTransition()
	{
		return _sceneTransition;
	}

	public void SetGameplayVisible(bool visible)
	{
		
	}

	public void ShowPauseMenu() => _pauseMenu.ShowPauseMenu();
	
}
