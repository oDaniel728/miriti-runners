using Godot;

public partial class AudioService : Service
{
    private AudioStreamPlayer _musicA, _musicB, _activeMusic;
    private SoundPool _sfxPool;
    private Tween _musicTween;

    public override void _Ready()
    {
        _musicA = GetNode<AudioStreamPlayer>("MusicPlayerA");
        _musicB = GetNode<AudioStreamPlayer>("MusicPlayerB");
        _sfxPool = GetNode<SoundPool>("SoundPool");
        _activeMusic = _musicA;
    }
    public void PlayMusic(AudioStream music, float fadeTime = 1f)
    {
        if (music == null)
        {
            StopMusic(fadeTime);
            return;
        }

        GD.Print($"AudioService.PlayMusic received '{music.ResourcePath}'.");

        if (_activeMusic.Playing && _activeMusic.Stream == music)
		{
			return; // (fix para o bug do aúdio não mantendo quando uma cena passa o mesmo aúdio da anterior)
		} 

        var old = _activeMusic;
        _activeMusic = (old == _musicA) ? _musicB : _musicA; // alterna entre os dois players de música

        _activeMusic.Stream = music;
        _activeMusic.VolumeDb = -80f;
        _activeMusic.Play();
        GD.Print($"Assigned '{music.ResourcePath}' to '{_activeMusic.GetPath()}'; playing={_activeMusic.Playing}.");

        _musicTween?.Kill(); // corta qualquer tween de fade anterior
        _musicTween = CreateTween().SetParallel();
        _musicTween.TweenProperty(_activeMusic, "volume_db", 0f, fadeTime);
        _musicTween.TweenProperty(old, "volume_db", -80f, fadeTime);
        _musicTween.Chain().TweenCallback(Callable.From(old.Stop));
    }

    public void StopMusic(float fadeTime = 1f)
    {
        _musicTween?.Kill();
        _musicTween = CreateTween();
        _musicTween.TweenProperty(_activeMusic, "volume_db", -80f, fadeTime);
        _musicTween.TweenCallback(Callable.From(_activeMusic.Stop));
    }

	// a categoria passada abaixo como string deve ser o nome de um dos nós dentro do SoundPool (SoundQueue's)
	// justamente para diferenciar o tipo de sfx (como UI, Passos, Interações, etc)
    public void PlaySFX(AudioStream sound, string category = "Default",
                        float pitch = 1f, float volumeDb = 0f)
        => _sfxPool.Play(category, sound, pitch, volumeDb);
	// o padrão agora é Default, mas se precisar diferenciar, siga os comentários acima.
	
	// Para o slider de volume das configurações
    public void SetBusVolume(string bus, float linear)
	{
	    int busIndex = AudioServer.GetBusIndex(bus);
	    float clampedVolume = Mathf.Clamp(linear, 0f, 1f); 
	    float volumeDb = clampedVolume == 0f ? -80f : Mathf.LinearToDb(clampedVolume);
	    AudioServer.SetBusVolumeDb(busIndex, volumeDb);
	}
}
