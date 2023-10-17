using DevExpress.XtraBars.Ribbon;
using HIS.Desktop.Base;
using HIS.Desktop.LocalStorage.ConfigApplication;
using HIS.Desktop.LocalStorage.ConfigSystem;
using HIS.Desktop.LocalStorage.LocalData;
using Inventec.Common.Logging;
using System;
using System.Diagnostics;
using System.Runtime;
using System.Threading;
using System.Windows.Forms;

namespace HIS.Desktop.Modules.Main
{
    public partial class frmMain : RibbonForm
    {
        const int timeLogActionDefault = 60000;
        System.Windows.Forms.Timer timerLogAction;

        private void RunLogAction()
        {
            try
            {
                string strTimeLogAction = ConfigApplicationWorker.Get<string>(AppConfigKeys.CONFIG_KEY__HIS_DESKTOP__TIME_LOG_ACTION);
                if (strTimeLogAction != "0")
                {
                    int timeWriteLogActionAndRam = Inventec.Common.TypeConvert.Parse.ToInt32(strTimeLogAction);
                    if (timeWriteLogActionAndRam == 0 || timeWriteLogActionAndRam <= 120000 || timeWriteLogActionAndRam < timeLogActionDefault)
                    {
                        Inventec.Common.Logging.LogSystem.Info("Cấu hình tài khoản CONFIG_KEY__HIS_DESKTOP__TIME_LOG_ACTION sẽ được hệ thống tự gán mặc định giá trị " + timeLogActionDefault + " ms để khởi chạy tiến trình đo dung lượng sử dụng RAM và các lệnh giải phóng bộ nhớ");
                        timeWriteLogActionAndRam = timeLogActionDefault;
                    }
                    Inventec.Common.Logging.LogSystem.Debug(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => timeWriteLogActionAndRam), timeWriteLogActionAndRam));
                    if (timeWriteLogActionAndRam > 0)
                    {
                        timerLogAction = new System.Windows.Forms.Timer();
                        timerLogAction.Interval = timeWriteLogActionAndRam;

                        timerLogAction.Enabled = true;
                        timerLogAction.Tick += timerProcessLogAction_Tick;
                        timerLogAction.Start();
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Warn(ex);
            }
        }

        private void timerProcessLogAction_Tick(object sender, EventArgs e)
        {
            try
            {
                HIS.Desktop.Utility.MemoryManagement.FlushMemory();
                HIS.Desktop.Utility.MemoryProcessor.CalculateMemoryRam("", "INFO");
                string strDisposeAfterProcess = HIS.Desktop.LocalStorage.ConfigApplication.ConfigApplicationWorker.Get<string>("HIS.Desktop.DisposeAfterProcessAndClose");
                if (!String.IsNullOrEmpty(strDisposeAfterProcess))
                {
                    MemoryRTProcessor.FreeLargeObjectHeap();
                }
            }
            catch (Exception ex)
            {
                LogSystem.Warn(ex);
            }
        }

    }
}