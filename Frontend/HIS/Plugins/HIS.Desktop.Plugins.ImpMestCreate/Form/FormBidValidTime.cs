using HIS.Desktop.Plugins.ImpMestCreate.ADO;
using HIS.Desktop.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.ImpMestCreate.Form
{
    public partial class FormBidValidTime : FormBase
    {
        List<BidValidTimeADO> ListValidTime;

        public FormBidValidTime(List<BidValidTimeADO> listValidTime)
        {
            InitializeComponent();
            ListValidTime = listValidTime;
        }

        private void FormBidValidTime_Load(object sender, EventArgs e)
        {
            try
            {
                SetIconFrm();
                gridControl1.DataSource = ListValidTime;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "Excel (*.xlsx)|*.xlsx";
                if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    gridControl1.ExportToXlsx(saveFileDialog1.FileName);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        void SetIconFrm()
        {
            try
            {
                string iconPath = System.IO.Path.Combine(HIS.Desktop.LocalStorage.Location.ApplicationStoreLocation.ApplicationStartupPath, System.Configuration.ConfigurationSettings.AppSettings["Inventec.Desktop.Icon"]);
                this.Icon = Icon.ExtractAssociatedIcon(iconPath);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
