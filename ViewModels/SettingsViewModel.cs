using BaseMobile.Services;
using BaseMobile.Models;
using Microsoft.Maui.Platform;
using System.Globalization;

namespace BaseMobile.ViewModels;
public class SettingsViewModel : BaseViewModel
{
	public LocalizationManager LocalizationManager => LocalizationManager.Instance;

	public List<SettingTheme> Themes { get; set; }
	public SettingTheme selectedTheme;
	public SettingTheme SelectedTheme
	{
		get => selectedTheme;
		set
		{
			if (selectedTheme != value)
			{
				selectedTheme = value;
				OnPropertyChanged();
				ThemeService.Instance.Theme = selectedTheme.AppTheme;
			}
		}
	}

	public List<string> Languages { get; set; }
	public string selectedLanguage;
	public string SelectedLanguage
	{
		get => selectedLanguage;
		set
		{
			if (selectedLanguage != value)
			{
				selectedLanguage = value;
				OnPropertyChanged();
				LocalizationManager.Instance.SetLanguage(selectedLanguage);
				Preferences.Default.Set(Constants.LanguageKey, selectedLanguage);
			}
		}
	}

	public SettingsViewModel()
	{
		Themes = new()
		{
			new SettingTheme(AppTheme.Light, "LightTheme"),
			new SettingTheme(AppTheme.Dark, "DarkTheme"),
			new SettingTheme(AppTheme.Unspecified, "SystemTheme")
		};
		selectedTheme = LoadThemeFromSettings();

		Languages = new()
		{
			Constants.EnglishLang,
			Constants.RussianLang
		};
		selectedLanguage = LoadLanguageFromSettings();
	}

	private SettingTheme LoadThemeFromSettings()
	{
		SettingTheme? Find(AppTheme? appTheme)
		{
			foreach (SettingTheme theme in Themes)
			{
				if (theme.AppTheme == appTheme)
				{
					return theme;
				}
			}
			return null;
		}

		return Find(ThemeService.Instance?.Theme) ?? Themes[2];
	}

	private string LoadLanguageFromSettings()
	{
		string lang = CultureInfo.CurrentCulture.TwoLetterISOLanguageName == Constants.RussianAbrv
			? Constants.RussianLang
			: Constants.EnglishLang;
		if (Preferences.Default.ContainsKey(Constants.LanguageKey))
		{
			lang = Preferences.Default.Get(Constants.LanguageKey, Constants.EnglishLang);
		}
		return lang;
	}
}
