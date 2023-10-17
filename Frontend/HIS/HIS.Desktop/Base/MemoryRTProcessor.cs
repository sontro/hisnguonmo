using Inventec.Common.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Base
{
    internal class MemoryRTProcessor
    {
        internal static void FreeLargeObjectHeap()
        {
            try
            {
                string strDisposeAfterProcess = HIS.Desktop.LocalStorage.ConfigApplication.ConfigApplicationWorker.Get<string>("HIS.Desktop.DisposeAfterProcessAndClose");
                Inventec.Common.Logging.LogSystem.Info("ModuleControlDispose.strDisposeAfterProcess =" + strDisposeAfterProcess);
                if (strDisposeAfterProcess == "1" || strDisposeAfterProcess == "3")
                {                                       
                    if (String.IsNullOrEmpty(DATA.OsInfo))
                    {
                        string QueryOS = "SELECT * FROM Win32_OperatingSystem";
                        ManagementObjectSearcher searcherOS = new ManagementObjectSearcher(QueryOS);
                        foreach (ManagementObject WniPART in searcherOS.Get())
                        {
                            DATA.OsInfo = WniPART.Properties["Caption"].Value.ToString() + " - " + WniPART.Properties["OSArchitecture"].Value.ToString() + "\r\n";
                        }
                    }

                    if (DATA.OsInfo.ToLower().Contains("microsoft windows 7"))
                    {
                        Inventec.Common.Logging.LogSystem.Info("Máy trạm cài " + DATA.OsInfo + ", do phiên bản windows này không hỗ trợ các hàm giải phóng bộ nhớ lớn lên dừng xử lý ở đây");
                    }
                    else
                    {
                        Inventec.Common.Logging.LogSystem.Info("Gọi hàm giải phóng bộ nhớ lớn(LargeObjectHeapCompactionMode) cho HIS");
                        GCSettings.LargeObjectHeapCompactionMode = GCLargeObjectHeapCompactionMode.CompactOnce;
                        GC.Collect();
                    }
                }
                else if (strDisposeAfterProcess == "2")
                {
                    GCSettings.LargeObjectHeapCompactionMode = GCLargeObjectHeapCompactionMode.CompactOnce;
                    GC.Collect();
                }
            }
            catch (Exception ex)
            {
                LogSystem.Warn(ex);
            }
        }

    }
}
