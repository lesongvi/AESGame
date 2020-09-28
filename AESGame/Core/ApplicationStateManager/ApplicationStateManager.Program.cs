using AESGame.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AESGame.Core.ApplicationStateManager
{
    static partial class ApplicationStateManager
    {
        public static Action ApplicationExit;

        public static void ExecuteApplicationExit()
        {
            Application.Exit();
            ApplicationExit?.Invoke();
        }

        public static bool SystemRequirementsEnsured()
        {
            if (!SystemSpecs.IsWmiEnabled())
            {
                MessageBox.Show(ViProductInfo.Name + " không thể chạy các thành phần cần thiết. Có vẻ như hệ thống của bạn đã tắt service Windows Management. Để cho " + ViProductInfo.Name + " để hoạt động bình thường, service Windows Management cần được Bật. Service này là cần thiết để phát hiện việc sử dụng RAM và thông tin về bộ điều khiển. Bật service Windows Management theo cách thủ công và bắt đầu " + ViProductInfo.Name + ".",
                        "Lỗi Windows Management",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }

            if (!Helpers.Is45NetOrHigher())
            {
                MessageBox.Show(ViProductInfo.Name + " yêu cầu .NET Framework 4.5 trở lên để hoạt động bình thường. Vui lòng cài đặt Microsoft .NET Framework 4.5",
                    "Cảnh báo!",
                    MessageBoxButtons.OK);

                return false;
            }

            if (!Helpers.Is64BitOperatingSystem)
            {
                MessageBox.Show(ViProductInfo.Name + " chỉ hỗ trợ thiết bị x64. Bạn không thể sử dụng " + ViProductInfo.Name + " với x86",
                    "Cảnh báo!",
                    MessageBoxButtons.OK);

                return false;
            }

            return true;
        }
    }
}
