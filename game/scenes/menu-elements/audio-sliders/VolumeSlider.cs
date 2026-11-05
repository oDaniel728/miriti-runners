
using Godot;

public partial class VolumeSlider : Control
{
    public enum AudioBus
    {
        Master,
        SFX,
        Music,
        Ambience
    }

    private Label _label;
    private Label _valueLabel;
    private HSlider _hSlider;
    [Export] private string labelName = "Name";
    [Export] private AudioBus _audioBus = AudioBus.Master;

    public override void _Ready()
    {
        _label = GetNode<Label>("Label");
        _valueLabel = GetNode<Label>("Value");
        _hSlider = GetNode<HSlider>("HSlider");
        _label.Text = labelName;
        _hSlider.ValueChanged += OnValueChanged;

        // Sincroniza o slider com o volume atual do barramento.
        _hSlider.Value = GetBusVolume();
        UpdateValueLabel(_hSlider.Value);
    }

    private void OnValueChanged(double value)
    {
        GameServices.Current.Settings.SetAudioBusVolume(
            _audioBus.ToString(),
            (float)(value / 100.0)
        );
        UpdateValueLabel(value);
    }

    private void UpdateValueLabel(double value)
    {
        _valueLabel.Text = $"{Mathf.RoundToInt((float)value)}%";
    }

    private double GetBusVolume()
    {
        return GameServices.Current.Settings.GetAudioBusVolume(_audioBus.ToString()) * 100.0;
    }
}
