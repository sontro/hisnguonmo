using HIS.Desktop.ApiConsumer;
using HIS.Desktop.LocalStorage.Location;
using Inventec.Common.Adapter;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.SurgServiceReqExecute
{
    public partial class frmMediMate : Form
    {
        long parentId { get; set; }
        List<V_HIS_SERE_SERV> ListData = new List<V_HIS_SERE_SERV>();
        Action<string> ContentResult { get; set; }
        public frmMediMate(long parentId, Action<string> ContentResult)
        {
            InitializeComponent();
            try
            {
                SetIcon();
                this.ContentResult = ContentResult;
                this.parentId = parentId;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
        private void SetIcon()
        {
            try
            {
                this.Icon = Icon.ExtractAssociatedIcon(System.IO.Path.Combine(ApplicationStoreLocation.ApplicationDirectory, ConfigurationSettings.AppSettings["Inventec.Desktop.Icon"]));
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void frmMediMate_Load(object sender, EventArgs e)
        {
            try
            {
                CommonParam param = new CommonParam();
                HisSereServViewFilter filter = new HisSereServViewFilter();
                filter.PARENT_ID = parentId;
                filter.SERVICE_TYPE_IDs = new List<long>()
                {
                    IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__THUOC,
                    IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__VT,
                };
                ListData = new BackendAdapter(param)
        .Get<List<V_HIS_SERE_SERV>>("api/HisSereServ/GetView", ApiConsumers.MosConsumer, filter, param);
                if(ListData != null && ListData.Count > 0)
                {
                    ListData = ListData.OrderBy(o => o.TDL_SERVICE_TYPE_ID).OrderBy(o => o.TDL_SERVICE_NAME).ToList();
                }
                gridControl1.DataSource = ListData;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void txtFind_TextChanged(object sender, EventArgs e)
        {
            try
            {
                var data = ListData;
                string key = txtFind.Text.Trim();
                if (!string.IsNullOrEmpty(key))
                {
                    key = key.ToLower();
                    data = data.Where(o => o.TDL_SERVICE_CODE.ToLower().Contains(key) || o.TDL_SERVICE_NAME.ToLower().Contains(key) || o.SERVICE_UNIT_NAME.ToLower().Contains(key)).ToList();
                }
                gridControl1.DataSource = null;
                gridControl1.DataSource = data;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void gridView1_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            try
            {
                if (gridView1.SelectedRowsCount > 0)
                {
                    List<string> lst = new List<string>();
                    for (int i = 0; i < gridView1.SelectedRowsCount; i++)
                    {
                        lst.Add(((V_HIS_SERE_SERV)gridView1.GetRow(gridView1.GetSelectedRows()[i])).TDL_SERVICE_NAME);
                    }
                    txtContent.Text = String.Join("; ", lst);
                }
                else
                    txtContent.Text = null;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ContentResult != null)
                    ContentResult(txtContent.Text.Trim());
                this.Close();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
