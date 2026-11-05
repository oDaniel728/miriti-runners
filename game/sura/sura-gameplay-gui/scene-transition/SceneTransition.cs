using Godot;
using System;

public partial class SceneTransition : Control
{
	[Export]
	private ColorRect _fade;
	[Export]
	private float fadeInDuration = .2f;
	[Export]
	private float fadeOutDuration = .2f;
	private Color fadeColor;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		fadeColor = _fade.Color;
		fadeColor.A = 0f;
		_fade.Color = fadeColor;
	}

	public Tween FadeOut()
	{
		return ChangeFadeAlpha(1f, fadeOutDuration);
	}
	public Tween FadeIn()
	{
		return ChangeFadeAlpha(0f, fadeInDuration);
	}
	private Tween ChangeFadeAlpha(float toAlpha, float changeTime)
	{
		Tween tween = CreateTween();
		tween.TweenProperty(
		_fade,
		"color:a",
		toAlpha,
		changeTime
		);

		return tween;
	}
}
