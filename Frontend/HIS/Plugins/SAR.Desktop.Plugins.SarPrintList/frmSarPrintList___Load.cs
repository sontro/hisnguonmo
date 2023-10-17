using Inventec.Common.Adapter;
using Inventec.Core;
using SAR.Desktop.Plugins.SarPrintList.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SAR.Desktop.Plugins.SarPrintList
{
    public partial class frmSarPrintList : HIS.Desktop.Utility.FormBase
    {
        private void FillDataToGridControl()
        {
            try
            {

                if (!String.IsNullOrEmpty(jsonPrintId))
                {
                    prints = new List<SAR.EFMODEL.DataModels.SAR_PRINT>();
                    SAR.Filter.SarPrintFilter printFilter = new SAR.Filter.SarPrintFilter();

                    var printIds = PrintIdByJsonPrint(jsonPrintId);
                    if (printIds != null && printIds.Count > 0)
                    {
                        printFilter.IDs = printIds;
                        printFilter.ORDER_FIELD = "CREATE_TIME";
                        printFilter.ORDER_DIRECTION = "DESC";
                        prints = new BackendAdapter(new CommonParam())
                        .Get<List<SAR.EFMODEL.DataModels.SAR_PRINT>>(HisRequestUriStore.SAR_PRINT_GET, HIS.Desktop.ApiConsumer.ApiConsumers.SarConsumer, printFilter, new CommonParam());
                    }     
                }

                grdControlSarPrint.DataSource = prints;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }
    }
}
