using Godot;
using System;

public partial class PauseMenu : Control
{
	[Export] private PackedScene _mainMenuScene;

	public override void _Ready()
	{
		// A cena ainda pode estar sendo inicializada; o SceneService só recebe
		// o SceneContainer depois que Main._Ready() é executado.
		Hide();
	}

	public void ShowPauseMenu()
	{
		if (Visible) return;

		GameServices.Current.Hud.SetGameplayVisible(false);
		GameServices.Current.Scene.FreezeSceneProcess(true);
		Show();
	}
	public void OnResumePressed()
	{
		HidePauseMenu();
	}
	public void OnMainMenuPressed()
	{
		HidePauseMenuForSceneChange();
		GameServices.Current.Scene.StartSceneChange(_mainMenuScene);
	}

	private void HidePauseMenuForSceneChange()
	{
		GameServices.Current.Scene.FreezeSceneProcess(false);
		Hide();
	}
	public void HidePauseMenu()
	{
		GameServices.Current.Scene.FreezeSceneProcess(false);
		GameServices.Current.Hud.SetGameplayVisible(true);
		Hide();
	}
}
