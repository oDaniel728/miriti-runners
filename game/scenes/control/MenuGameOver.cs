using Godot;
using System;

public partial class MenuGameOver : Control
{
	[Export] private Button backButton;
	
	public override void _Ready()
	{
		GameEvents.OnPlayerDied += (enemy) =>
		{
			ShowScreen();
		};
	}
	public override void _Process(double delta)
	{
	}

	public void ShowScreen()
	{
		
	}
	public void HideScreen()
	{
		
	}
}
