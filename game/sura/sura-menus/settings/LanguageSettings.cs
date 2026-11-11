using Godot;

public partial class LanguageSettings : Control
{
	public void OnPortuguesePressed() => GameServices.Current.Settings.SetLocale("pt_BR");
	public void OnSpanishPressed() => GameServices.Current.Settings.SetLocale("es");
	public void OnEnglishPressed() => GameServices.Current.Settings.SetLocale("en_US");
	public void OnJapanesePressed() => GameServices.Current.Settings.SetLocale("ja");
	public void OnRussianPressed() => GameServices.Current.Settings.SetLocale("ru");
}
