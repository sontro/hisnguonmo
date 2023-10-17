using HIS.Desktop.ApiConsumer;
using HIS.Desktop.Controls.Session;
using HIS.Desktop.LocalStorage.ConfigApplication;
using Inventec.Common.Logging;
using Inventec.Core;
using Inventec.Desktop.Common.LanguageManager;
using System;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.ReportList
{
    public partial class UCReportList : UserControl
    {
        Inventec.UC.ListReports.MainListReports MainReportList;
        UserControl ReportListControl;

        public UCReportList()
        {
            InitializeComponent();
            MeShow();
        }

        public void MeShow()
        {
            try
            {
                Inventec.UC.ListReports.Data.InitData dataInit = new Inventec.UC.ListReports.Data.InitData(ApiConsumers.SarConsumer, ApiConsumers.SdaConsumer, ApiConsumers.AcsConsumer, Inventec.UC.Login.Base.ClientTokenManagerStore.ClientTokenManager, ConfigApplications.NumPageSize, "APP.ico", LanguageManager.GetCulture());
                MainReportList = new Inventec.UC.ListReports.MainListReports();
                ReportListControl = new UserControl();
                ReportListControl = MainReportList.Init(Inventec.UC.ListReports.MainListReports.EnumTemplate.TEMPLATE2, dataInit);
                this.Controls.Add(ReportListControl);
                MainReportList.MeShowUC(ReportListControl);
                if (!MainReportList.SetDelegateProcessHasException(ReportListControl, ProcessHasException)) Inventec.Common.Logging.LogSystem.Debug("Loi setDelegateProcessHasException cho UCReportList");

                Inventec.UC.ListReports.Base.ResouceManager.ResourceLanguageManager();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void ProcessHasException(CommonParam param)
        {
            try
            {
                SessionManager.ProcessTokenLost(param);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        #region Keyboard
        public void Refesh()
        {
            try
            {
                MainReportList.ButtonRefreshClick(ReportListControl);
            }
            catch (Exception ex)
            {
                LogSystem.Warn(ex);
            }
        }

        public void Search()
        {
            try
            {
                MainReportList.ButtonSearchClick(ReportListControl);
            }
            catch (Exception ex)
            {
                LogSystem.Warn(ex);
            }
        }
        #endregion
    }
}
