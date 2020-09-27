using AESGame.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;

namespace AESGame.Views.Common
{
    public static class WindowUtils
    {
        private const int GwlStyle = -16;
        private const int WsDisabled = 0x08000000;

        private const string UserDll = "user32.dll";

        [DllImport(UserDll)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport(UserDll)]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        public static bool TrySetNativeEnabled(bool enabled, IntPtr winHandle)
        {
            try
            {
                var current = GetWindowLong(winHandle, GwlStyle);
                SetWindowLong(winHandle, GwlStyle, current & ~WsDisabled | (enabled ? 0 : WsDisabled));

                return true;
            }
            catch (Exception e)
            {
                Logger.Warn("WindowUtils", $"{e.Message}");

                return false;
            }
        }

        internal static bool ForceSoftwareRendering { get; set; } = true;

        internal static void SetForceSoftwareRendering(Window w)
        {
            if (ForceSoftwareRendering)
            {
                HwndSource hwndSource = PresentationSource.FromVisual(w) as HwndSource;
                HwndTarget hwndTarget = hwndSource.CompositionTarget;
                hwndTarget.RenderMode = RenderMode.SoftwareOnly;
            }
        }

        internal static double? Top = null;
        internal static double? Left = null;

        internal static void InitWindow(Window w)
        {
            if (Top.HasValue && Left.HasValue)
            {
                w.Top = Top.Value;
                w.Left = Left.Value;
            }
            else
            {
                w.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            }
        }

        internal static void Window_OnClosing(Window w)
        {
            Top = w.Top;
            Left = w.Left;
        }

        public static T AssertViewModel<T>(this FrameworkElement fe) where T : class, new()
        {
            if (!(fe.DataContext is T vm))
            {
                vm = new T();
                fe.DataContext = vm;
            }

            return vm;
        }
    }
    }
