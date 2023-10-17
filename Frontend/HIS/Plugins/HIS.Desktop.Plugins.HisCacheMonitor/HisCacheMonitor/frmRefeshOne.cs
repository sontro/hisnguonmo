using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using Inventec.Desktop.Common.Message;
using HIS.Desktop.ApiConsumer;
using MOS.Filter;
using Inventec.Common.Adapter;

namespace HIS.Desktop.Plugins.HisCacheMonitor.HisCacheMonitor
{
    public partial class frmRefeshOne : DevExpress.XtraEditors.XtraForm
    {
        CacheMonitorADO currentCacheMonitorADO;
        Action actionRefesh;

        public frmRefeshOne(CacheMonitorADO cacheMonitorADO, Action actionrefesh)
        {
            InitializeComponent();
            try
            {
                this.currentCacheMonitorADO = cacheMonitorADO;
                this.actionRefesh = actionrefesh;
                string iconPath = System.IO.Path.Combine(HIS.Desktop.LocalStorage.Location.ApplicationStoreLocation.ApplicationStartupPath, System.Configuration.ConfigurationSettings.AppSettings["Inventec.Desktop.Icon"]);
                this.Icon = Icon.ExtractAssociatedIcon(iconPath);
            }
            catch (Exception ex)
            {
                LogSystem.Warn(ex);
            }
        }

        private void frmRefeshOne_Load(object sender, EventArgs e)
        {
            if (this.currentCacheMonitorADO != null)
            {
                rdReloadAllForHasDelete.Checked = (this.currentCacheMonitorADO.IS_RELOAD == 1);
                rdReloadNewOrEdit.Checked = !rdReloadAllForHasDelete.Checked;
            }
        }

        private void LoadCurrent(long currentId, ref MOS.EFMODEL.DataModels.HIS_CACHE_MONITOR currentDTO)
        {
            try
            {
                CommonParam param = new CommonParam();
                HisCacheMonitorFilter filter = new HisCacheMonitorFilter();
                filter.ID = currentId;
                currentDTO = new BackendAdapter(param).Get<List<MOS.EFMODEL.DataModels.HIS_CACHE_MONITOR>>(HisRequestUriStore.MOSHIS_CACHE_MONITOR_GET, ApiConsumers.MosConsumer, filter, param).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            CommonParam param = new CommonParam();
            HIS_CACHE_MONITOR rs = null;
            HIS_CACHE_MONITOR data1 = new HIS_CACHE_MONITOR();
            try
            {
                WaitingManager.Show();
                if (currentCacheMonitorADO.ID > 0)
                {
                    LoadCurrent(currentCacheMonitorADO.ID, ref data1);
                    data1.IS_RELOAD = (short)(rdReloadAllForHasDelete.Checked ? 1 : 0);

                    rs = new Inventec.Common.Adapter.BackendAdapter(param).Post<HIS_CACHE_MONITOR>(HisRequestUriStore.MOSHIS_CACHE_MONITOR_UPDATE, ApiConsumers.MosConsumer, data1, param);
                }
                else
                {
                    data1.IS_RELOAD = (short)(rdReloadAllForHasDelete.Checked ? 1 : 0);
                    data1.DATA_NAME = currentCacheMonitorADO.DATA_NAME;

                    rs = new Inventec.Common.Adapter.BackendAdapter(param).Post<HIS_CACHE_MONITOR>(HisRequestUriStore.MOSHIS_CACHE_MONITOR_CREATE, ApiConsumers.MosConsumer, data1, param);
                }
                WaitingManager.Hide();
                bool success = (rs != null);
                MessageManager.Show(param, success);
                if (success && this.actionRefesh != null)
                {
                    this.actionRefesh();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                WaitingManager.Hide();
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}