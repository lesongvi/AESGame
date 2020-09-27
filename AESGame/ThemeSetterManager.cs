using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AESGame
{
    internal static class ThemeSetterManager
    {
        private static List<IThemeSetter> _themeSetters = new List<IThemeSetter>();
        private static bool IsLight = true;

        static ThemeSetterManager() { }

        internal static void AddThemeSetter(IThemeSetter setter)
        {
            if (!_themeSetters.Contains(setter))
            {
                _themeSetters.Add(setter);
                setter.SetTheme(IsLight);
            }
        }

        internal static void RemoveThemeSetter(IThemeSetter setter)
        {
            _themeSetters.Remove(setter);
        }

        internal static void SetThemeSelectedThemes()
        {
            SetTheme(IsLight);
        }

        internal static void SetTheme(string displayTheme)
        {
            IsLight = displayTheme == "Light";
            SetTheme(IsLight);
        }

        internal static void SetTheme(bool isLight)
        {
            if (isLight)
            {
                Application.Current.Resources["ViLogo"] = Application.Current.FindResource("LogoLightBrush");

                Application.Current.Resources["BackgroundColor"] = Application.Current.FindResource("Brushes.Light.Grey.Grey4Background");
                Application.Current.Resources["BorderColor"] = Application.Current.FindResource("Brushes.Light.Border");
                Application.Current.Resources["LoginCircle"] = Application.Current.FindResource("LoginCircleLogoLightBrush");
                Application.Current.Resources["TextColorBrush"] = Application.Current.FindResource("Brushes.Light.TextColor");

                Application.Current.Resources["TextBoxBackGroundColor"] = Application.Current.FindResource("TextBoxBackGroundColor.Light");
                Application.Current.Resources["ComboBoxBackGroundColor"] = Application.Current.FindResource("ComboBoxBackGroundColor.Light");
                Application.Current.Resources["MODAL_WINDOW_BLUR_Background"] = Application.Current.FindResource("MODAL_WINDOW_BLUR_Background.Light");
            }
            else
            {
                Application.Current.Resources["ViLogo"] = Application.Current.FindResource("LogoDarkBrush");

                Application.Current.Resources["BackgroundColor"] = Application.Current.FindResource("Brushes.Dark.Grey.Grey1Background");
                Application.Current.Resources["BorderColor"] = Application.Current.FindResource("Brushes.Dark.Border");
                Application.Current.Resources["LoginCircle"] = Application.Current.FindResource("LoginCircleLogoDarkBrush");
                Application.Current.Resources["TextColorBrush"] = Application.Current.FindResource("Brushes.Dark.TextColor");

                Application.Current.Resources["TextBoxBackGroundColor"] = Application.Current.FindResource("TextBoxBackGroundColor.Dark");
                Application.Current.Resources["ComboBoxBackGroundColor"] = Application.Current.FindResource("ComboBoxBackGroundColor.Dark");
                Application.Current.Resources["MODAL_WINDOW_BLUR_Background"] = Application.Current.FindResource("MODAL_WINDOW_BLUR_Background.Dark");
            }
            foreach (var setter in _themeSetters)
            {
                setter.SetTheme(isLight);
            }
        }
    }
}
