using Godot;

public partial class TitleScreen : UISceneRoot
{
	[Export] private PackedScene targetScene;
	[Export] private MainMenu mainMenu;
	[Export] private SettingsMenu settingsMenu;
	[Export] private CreditsMenu creditsMenu;
	private MenuPageController _pages;

	public override void _Ready()
	{
		base._Ready();
		_pages = GetNode<MenuPageController>("MenuPageController");
		mainMenu.PlayRequested += OnPlayPressed;
		mainMenu.SettingsRequested += OnSettingsPressed;
		mainMenu.CreditsRequested += OnCreditsPressed;
		mainMenu.QuitRequested += OnQuitPressed;
		settingsMenu.BackRequested += OnReturnPressed;
		creditsMenu.BackRequested += OnReturnPressed;
		_pages.ShowPage(mainMenu);
	}

	private void OnPlayPressed()
	{
		GameServices.Current.Scene.SetPlayerInitialNewSceneCoords(Vector2I.Zero);
		GameServices.Current.Scene.StartSceneChange(targetScene);
	}

	private void OnSettingsPressed() => _pages.ShowPage(settingsMenu);
	private void OnCreditsPressed() => _pages.ShowPage(creditsMenu);
	private void OnReturnPressed() => _pages.ShowPage(mainMenu);
	private void OnQuitPressed() => GameServices.Current.Scene.QuitGame();
}
