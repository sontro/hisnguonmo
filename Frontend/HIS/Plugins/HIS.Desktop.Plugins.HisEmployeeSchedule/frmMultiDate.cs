using DevExpress.XtraEditors.Controls;
using Inventec.Desktop.Common.LanguageManager;
using Inventec.Desktop.Common.Message;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.HisEmployeeSchedule
{
    public partial class frmMultiDate : Form
    {
        DelegateSelectMultiDate delegateSelectData;
        List<DateTime?> oldDatas;
        public frmMultiDate()
        {
            InitializeComponent();
        }
        public frmMultiDate(List<DateTime?> datas, DelegateSelectMultiDate selectData)
        {
            try
            {
                InitializeComponent();
                this.delegateSelectData = selectData;
                this.oldDatas = datas;
                string iconPath = System.IO.Path.Combine(HIS.Desktop.LocalStorage.Location.ApplicationStoreLocation.ApplicationStartupPath, System.Configuration.ConfigurationSettings.AppSettings["Inventec.Desktop.Icon"]);
                this.Icon = Icon.ExtractAssociatedIcon(iconPath);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }
        private void frmMultiIntructonTime_Load(object sender, EventArgs e)
        {
            try
            {
                if (this.oldDatas != null && this.oldDatas.Count > 0)
                {
                    //Add datepcker with data
                    foreach (var item in this.oldDatas)
                    {
                        if (item != null && item.Value != DateTime.MinValue)
                        {
                            calendarIntructionTime.AddSelection(item.Value);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void btnChoose_Click(object sender, EventArgs e)
        {
            try
            {
                WaitingManager.Show();
                List<DateTime?> listSelected = new List<DateTime?>();
                foreach (DateRange item in calendarIntructionTime.SelectedRanges)
                {
                    if (item != null)
                    {
                        var dt = item.StartDate;
                        while (dt.Date < item.EndDate.Date)
                        {
                            listSelected.Add(dt);
                            dt = dt.AddDays(1);
                        }
                    }
                }
                WaitingManager.Hide();
                if (delegateSelectData != null)
                    delegateSelectData(listSelected);
                this.Close();

            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

    }
}
