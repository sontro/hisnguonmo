using DevExpress.XtraGrid.Views.Base;
using HIS.Desktop.Common;
using HIS.Desktop.LocalStorage.Location;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MOS.EFMODEL.DataModels;

namespace HIS.Desktop.Plugins.AssignService
{
    public partial class frmDetailsSereServ : Form
    {
        List<V_HIS_SERE_SERV_17> dataList { get; set; }
        RefeshReference refeshReference { get; set; }
        Action<bool> CloseForm { get; set; }
        public frmDetailsSereServ(List<V_HIS_SERE_SERV_17> data, RefeshReference refeshReference)
        {
            InitializeComponent();
            try
            {
                this.dataList = data;
                this.refeshReference = refeshReference;
                this.Icon = Icon.ExtractAssociatedIcon(System.IO.Path.Combine(ApplicationStoreLocation.ApplicationDirectory, ConfigurationSettings.AppSettings["Inventec.Desktop.Icon"]));
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void frmDetailsSereServ_Load(object sender, EventArgs e)
        {
            try
            {
                gridControl1.DataSource = dataList;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void frmDetailsSereServ_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                refeshReference();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                refeshReference();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void gridView1_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            try
            {
                if (e.IsGetData && e.Column.UnboundType != DevExpress.Data.UnboundColumnType.Bound)
                {
                    var data = (V_HIS_SERE_SERV_17)((IList)((BaseView)sender).DataSource)[e.ListSourceRowIndex];
                    if (data != null)
                    {
                        if (e.Column.FieldName == "VIR_TOTAL_PATIENT_PRICE_STR")
                        {
                            try
                            {
                                e.Value = Inventec.Common.Number.Convert.NumberToString(data.VIR_TOTAL_PATIENT_PRICE ?? 0, HIS.Desktop.LocalStorage.ConfigApplication.ConfigApplications.NumberSeperator);
                            }
                            catch (Exception ex)
                            {
                                Inventec.Common.Logging.LogSystem.Error(ex);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
