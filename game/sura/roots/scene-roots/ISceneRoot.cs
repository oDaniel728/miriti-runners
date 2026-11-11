using Godot;
using System;

public interface ISceneRoot
{
	AudioStream InitialMusic { get; }
	bool hideMobileControls {get; }
	void OnSceneChangeRequest(PackedScene targetScene, Vector2I targetCoords);
}
