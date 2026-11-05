using Godot;
using System;
using System.Collections.Generic;

public partial class SoundPool : Node //útil para gerenciar sfx
{
	private Dictionary< string, SoundQueue> _queues= new();
	private RandomNumberGenerator _rng = new RandomNumberGenerator();


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		foreach (var child in GetChildren())
		{
			if (child is SoundQueue q)
			{
				_queues[q.Name] = q; //o nome do nó agora é a categoria
			}
		}
	}

	public void Play(string category, AudioStream sound, float pitch = 1f, float volumeDb = 0f)
    {
        if (_queues.TryGetValue(category, out var q))
		{
			q.Play(sound, pitch, volumeDb);
		} 
        else
		{
			GD.PushWarning($"SoundPool: categoria '{category}' não existe.");
		} 
    }
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
