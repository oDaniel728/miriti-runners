using Godot;
using System.Collections.Generic;

[Tool]
public partial class SoundQueue : Node
{
    [Export] private int _playerCount = 8;
    [Export] private StringName _bus = "SFX";

    private readonly List<AudioStreamPlayer> _players = new();
    private int _next = 0;

    public override void _Ready()
    {
        if (Engine.IsEditorHint()) //nem te conto como descobri
		{
			return;
		}  

        var template = GetChild<AudioStreamPlayer>(0); //molde para os demais players
        template.Bus = _bus;
        _players.Add(template);

        for (int i = 1; i < _playerCount; i++) // começa à partir da template acima
        {
            var copy = (AudioStreamPlayer)template.Duplicate();
            AddChild(copy);
            _players.Add(copy);
        }
    }

    public void Play(AudioStream stream, float pitchScale = 1f, float volumeDb = 0f)
    {
        // procura o próximo player livre, a partir do "rodízio"
        for (int i = 0; i < _players.Count; i++)
        {
            int idx = (_next + i) % _players.Count;
            if (_players[idx].Playing) // se estiver tocando, pula para o próximo
			{
				continue;
			} 
            Fire(_players[idx], stream, pitchScale, volumeDb);
            _next = (idx + 1) % _players.Count;
            return;
        }

        // todos ocupados: reaproveita o próximo do rodízio (corta o mais antigo)
        Fire(_players[_next], stream, pitchScale, volumeDb);
        _next = (_next + 1) % _players.Count;
    }

    private static void Fire(AudioStreamPlayer p, AudioStream s, float pitch, float vol)
    { //evita repetir essas quatro linhas legais na função acima. "Dispara" sfx direto no player.
        p.Stream = s; //troca a música e reaplica os parâmetros restantes
        p.PitchScale = pitch; 
        p.VolumeDb = vol;
        p.Play();
    }

    public override string[] _GetConfigurationWarnings()
    {
        if (GetChildCount() == 0 || GetChild(0) is not AudioStreamPlayer)
            return new[] { "1st child node NEEDS to be and AudioStreamPlayer" };
        return System.Array.Empty<string>();
    }
}