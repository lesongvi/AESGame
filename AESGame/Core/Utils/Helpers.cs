using AESGame.Common;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace AESGame.Core.Utils
{
    public class Helpers : PInvokeHelpers
    {
        private static readonly bool Is64BitProcess = (IntPtr.Size == 8);
        public static bool Is64BitOperatingSystem = Is64BitProcess || InternalCheckIsWow64();

        public static readonly bool IsElevated;


        static Helpers()
        {
            using (var identity = WindowsIdentity.GetCurrent())
            {
                var principal = new WindowsPrincipal(identity);
                IsElevated = principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
        }

        public static bool InternalCheckIsWow64()
        {
            if ((Environment.OSVersion.Version.Major == 5 && Environment.OSVersion.Version.Minor >= 1) ||
                Environment.OSVersion.Version.Major >= 6)
            {
                using (var p = Process.GetCurrentProcess())
                {
                    return IsWow64Process(p.Handle, out var retVal) && retVal;
                }
            }
            return false;
        }

        public static uint GetIdleTime()
        {
            var lastInPut = new LASTINPUTINFO();
            lastInPut.cbSize = (uint)System.Runtime.InteropServices.Marshal.SizeOf(lastInPut);
            GetLastInputInfo(ref lastInPut);

            return ((uint)Environment.TickCount - lastInPut.dwTime);
        }

        public static void DisableWindowsErrorReporting(bool en)
        {

            Logger.Info("AESGAME", "Trying to enable/disable Windows error reporting");

            try
            {
                using (var rk = Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Windows\Windows Error Reporting"))
                {
                    if (rk != null)
                    {
                        var o = rk.GetValue("DontShowUI");
                        if (o != null)
                        {
                            var val = (int)o;
                            Logger.Info("AESGAME", $"Current DontShowUI value: {val}");

                            if (val == 0 && en)
                            {
                                Logger.Info("AESGAME", "Setting register value to 1.");
                                rk.SetValue("DontShowUI", 1);
                            }
                            else if (val == 1 && !en)
                            {
                                Logger.Info("AESGAME", "Setting register value to 0.");
                                rk.SetValue("DontShowUI", 0);
                            }
                        }
                        else
                        {
                            Logger.Info("AESGAME", "Registry key not found .. creating one..");
                            rk.CreateSubKey("DontShowUI", RegistryKeyPermissionCheck.Default);
                            Logger.Info("AESGAME", "Setting register value to 1..");
                            rk.SetValue("DontShowUI", en ? 1 : 0);
                        }
                    }
                    else
                        Logger.Info("AESGAME", "Unable to open SubKey.");
                }
            }
            catch (Exception ex)
            {
                Logger.Error("AESGAME", $"Unable to access registry. Error: {ex.Message}");
            }
        }

        private static bool Is45DotVersion(int releaseKey)
        {
            if (releaseKey >= 393295)
            {
                return true;
            }
            if ((releaseKey >= 379893))
            {
                return true;
            }
            if ((releaseKey >= 378675))
            {
                return true;
            }
            if ((releaseKey >= 378389))
            {
                return true;
            }
            return false;
        }

        public static bool Is45NetOrHigher()
        {
            using (var ndpKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32)
                .OpenSubKey("SOFTWARE\\Microsoft\\NET Framework Setup\\NDP\\v4\\Full\\"))
            {
                return ndpKey?.GetValue("Release") != null && Is45DotVersion((int)ndpKey.GetValue("Release"));
            }
        }

        public static bool IsConnectedToInternet()
        {
            bool returnValue;
            try
            {
                returnValue = InternetGetConnectedState(out _, 0);
            }
            catch
            {
                returnValue = false;
            }
            return returnValue;
        }
        public static int ParseInt(string text)
        {
            return int.TryParse(text, out var tmpVal) ? tmpVal : 0;
        }

        public static long ParseLong(string text)
        {
            return long.TryParse(text, out var tmpVal) ? tmpVal : 0;
        }

        public static double ParseDouble(string text)
        {
            try
            {
                var parseText = text.Replace(',', '.');
                return double.Parse(parseText, CultureInfo.InvariantCulture);
            }
            catch
            {
                return 0;
            }
        }

        public static void VisitUrlLink(string urlLink)
        {
            try
            {
                using (var p = Process.Start(urlLink)) { }
            }
            catch (Exception ex)
            {
                Logger.Error("AESGAME", "VisitLink error: " + ex.Message);
            }
        }

        public static async Task<bool> CreateAndUploadLogReport(string uuid)
        {
            try
            {
                var exePath = Paths.RootPath("CreateLogReport.exe");
                var startLogInfo = new ProcessStartInfo
                {
                    FileName = exePath,
                    WindowStyle = ProcessWindowStyle.Minimized,
                    UseShellExecute = true,
                    Arguments = Path.GetFileNameWithoutExtension(Paths.AppRoot),
                    CreateNoWindow = true
                };
                using (var doCreateLog = Process.Start(startLogInfo))
                {
                    doCreateLog.WaitForExit(10 * 1000);
                }

                var tmpZipPath = Paths.RootPath($"tmp._archive_logs.zip");
                var url = $"https://api.rqn9.com/data/1.0/dapp/_/182230003154961/dump/{uuid}.zip";

                using (var httpClient = new HttpClient())
                {
                    using (var stream = File.OpenRead(tmpZipPath))
                    {
                        var response = await httpClient.PutAsync(url, new StreamContent(stream));
                        response.EnsureSuccessStatusCode();
                    }
                }

                File.Delete(tmpZipPath);
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error("Log-Report", ex.Message);
                return false;
            }
        }
    }
}
