using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Inventec.Core;
using HIS.Desktop.ApiConsumer;
using Inventec.Common.Adapter;
using MOS.SDO;
using DevExpress.XtraCharts;
using System.Globalization;
using HIS.Desktop.LocalStorage.ConfigApplication;
using System.Runtime.InteropServices;
using HIS.Desktop.Plugins.Dashboard.Base;
using Inventec.Desktop.Common.Message;
using HIS.Desktop.Plugins.Dashboard.UCFilter;

namespace HIS.Desktop.Plugins.Dashboard
{
    public partial class UCDashBoard : UserControl
    {
        UCFilterByDay ucFilterByDay { get; set; }
        UCFilterByWeek ucFilterByWeek { get; set; }
        UCFilterByMonth ucFilterByMonth { get; set; }


        private void InitUcFilterByDay()
        {
            try
            {
                ucFilterByDay = new UCFilterByDay();
                panelFilter.Controls.Clear();
                ucFilterByDay.Dock = DockStyle.Fill;
                lciPanel.Size = new Size(518,24);
                panelFilter.Controls.Add(ucFilterByDay);
                btnThongKe.Visible = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void InitUcFilterByWeek()
        {
            try
            {
                ucFilterByWeek = new UCFilterByWeek();
                panelFilter.Controls.Clear();
                ucFilterByWeek.Dock = DockStyle.Fill;
                lciPanel.Size = new Size(390, 24);
                panelFilter.Controls.Add(ucFilterByWeek);
                btnThongKe.Visible = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void InitUcFilterByMonth()
        {
            try
            {
                ucFilterByMonth = new UCFilterByMonth();
                panelFilter.Controls.Clear();
                ucFilterByMonth.Dock = DockStyle.Fill;
                lciPanel.Size = new Size(390, 24);
                panelFilter.Controls.Add(ucFilterByMonth);
                btnThongKe.Visible = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }
    }
}
