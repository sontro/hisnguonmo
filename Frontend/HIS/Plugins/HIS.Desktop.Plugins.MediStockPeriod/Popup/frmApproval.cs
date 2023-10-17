using HIS.Desktop.ApiConsumer;
using HIS.Desktop.Controls.Session;
using Inventec.Common.Adapter;
using Inventec.Common.Logging;
using Inventec.Core;
using Inventec.Desktop.Common.Message;
using MOS.EFMODEL.DataModels;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.MediStockPeriod.Popup
{
    public partial class frmApproval : Form
    {
        HIS.Desktop.Common.DelegateSelectData delegateSelectData = null;
        long MediStockPeriodId;
        bool IsAproval;
        long WorkingRoomId;
        public frmApproval()
        {
            InitializeComponent();
        }

        public frmApproval(long mediStockPeriodId, bool isAproval, HIS.Desktop.Common.DelegateSelectData _delegateSelectData, long workingRoomId)
        {
            InitializeComponent();
            this.delegateSelectData = _delegateSelectData;
            this.MediStockPeriodId = mediStockPeriodId;
            this.IsAproval = isAproval;
            this.WorkingRoomId = workingRoomId;
            if (IsAproval)
            {
                this.Text = "Duyệt";
            }
            else
            {
                this.Text = "Hủy duyệt";
            }
            try
            {
                string iconPath = System.IO.Path.Combine(HIS.Desktop.LocalStorage.Location.ApplicationStoreLocation.ApplicationStartupPath, System.Configuration.ConfigurationSettings.AppSettings["Inventec.Desktop.Icon"]);
                this.Icon = Icon.ExtractAssociatedIcon(iconPath);
            }
            catch (Exception ex)
            {
                LogSystem.Warn(ex);
            }
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            btnSave_Click(null, null);
        }

        private void frmApproval_Load(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {

                MOS.SDO.HisMestPeriodApproveSDO mediStockPeriodInventory = new MOS.SDO.HisMestPeriodApproveSDO();
                mediStockPeriodInventory.MediStockPeriodId = this.MediStockPeriodId;
                mediStockPeriodInventory.Description = txtDescription.Text.Trim();
                mediStockPeriodInventory.WorkingRoomId = this.WorkingRoomId;

                //TODO call api
                //............
                WaitingManager.Show();
                CommonParam param = new CommonParam();
                bool success = false;
                HisMestPeriodApproveResultSDO resultApproval = null;
                HIS_MEDI_STOCK_PERIOD resultUnApproval = null;

                if (this.IsAproval)
                {
                    resultApproval = new BackendAdapter(param).Post<HisMestPeriodApproveResultSDO>("api/HisMediStockPeriod/Approve", ApiConsumers.MosConsumer, mediStockPeriodInventory, param);
                }
                else
                {
                    resultUnApproval = new BackendAdapter(param).Post<HIS_MEDI_STOCK_PERIOD>("api/HisMediStockPeriod/Unapprove", ApiConsumers.MosConsumer, mediStockPeriodInventory, param);
                }

                if (resultApproval != null || resultUnApproval != null)
                {
                    success = true;
                }

                Inventec.Common.Logging.LogSystem.Debug(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => mediStockPeriodInventory), mediStockPeriodInventory)
                    + "____" + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => success), success));

                WaitingManager.Hide();
                if (success && delegateSelectData != null)
                {
                    delegateSelectData(success);
                    this.Close();
                }
                #region ShowMessager
                MessageManager.Show(this.ParentForm, param, success);
                SessionManager.ProcessTokenLost(param);
                #endregion
            }
            catch (Exception ex)
            {
                WaitingManager.Hide();
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }
    }
}
