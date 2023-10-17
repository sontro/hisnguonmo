using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Inventec.Common.Adapter;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using HIS.Desktop.ApiConsumer;
using DevExpress.XtraTreeList.Nodes;
using HIS.Desktop.Controls.Session;
using Inventec.Desktop.Common.Message;

namespace HIS.Desktop.Plugins.MediStockPeriod
{
    public partial class UCMediStockPeriod : UserControl
    {
        private void medicineType_NodeCellStyle(V_HIS_MEST_PERIOD_METY data, DevExpress.XtraTreeList.GetCustomNodeCellStyleEventArgs e)
        {
            try
            {
                if (data != null)
                {
                    if (data.IN_AMOUNT < 0 || data.OUT_AMOUNT < 0 || data.BEGIN_AMOUNT < 0 || data.VIR_END_AMOUNT < 0 || data.INVENTORY_AMOUNT < 0 || data.VIR_END_AMOUNT != data.INVENTORY_AMOUNT)
                    {
                        e.Appearance.ForeColor = Color.Red;
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void materialType_NodeCellStyle(V_HIS_MEST_PERIOD_MATY data, DevExpress.XtraTreeList.GetCustomNodeCellStyleEventArgs e)
        {
            try
            {
                if (data != null)
                {
                    if (data.IN_AMOUNT < 0 || data.OUT_AMOUNT < 0 || data.BEGIN_AMOUNT < 0 || data.VIR_END_AMOUNT < 0 || data.INVENTORY_AMOUNT < 0 || data.VIR_END_AMOUNT != data.INVENTORY_AMOUNT)
                    {
                        e.Appearance.ForeColor = Color.Red;
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void bltyType_NodeCellStyle(V_HIS_MEST_PERIOD_BLTY data, DevExpress.XtraTreeList.GetCustomNodeCellStyleEventArgs e)
        {
            try
            {
                if (data != null)
                {
                    if (data.IN_AMOUNT < 0 || data.OUT_AMOUNT < 0 || data.BEGIN_AMOUNT < 0 || data.VIR_END_AMOUNT < 0 || data.INVENTORY_AMOUNT < 0 || data.VIR_END_AMOUNT != data.INVENTORY_AMOUNT)
                    {
                        e.Appearance.ForeColor = Color.Red;
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
