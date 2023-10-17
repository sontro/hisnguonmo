using HIS.Desktop.ADO;
using Inventec.Core;
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
        public List<long> PrintIdByJsonPrint(string json_Print_Id)
        {
            List<long> result = new List<long>();
            try
            {
                var arrIds = json_Print_Id.Split(',', ';');
                if (arrIds != null && arrIds.Length > 0)
                {
                    foreach (var id in arrIds)
                    {
                        long printId = Inventec.Common.TypeConvert.Parse.ToInt64(id);
                        if (printId > 0)
                        {
                            result.Add(printId);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
            return result;
        }
        bool DeleteJsonPrint(SAR.EFMODEL.DataModels.SAR_PRINT sarDelete)
        {
            bool success = false;
            try
            {
                prints.Remove(sarDelete);
                grdControlSarPrint.RefreshDataSource();
                var listPrintIds = prints.Select(o => o.ID).ToList();
                if (delegateSarPrintResult != null)
                {
                    string jsonPrintResult = "";
                    foreach (var id in listPrintIds)
                    {
                        jsonPrintResult += id + ",";
                    }
                    delegateSarPrintResult(jsonPrintResult);
                }
                gridViewSarPrint.FocusedColumn = gridViewSarPrint.Columns[0];
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
            return success;
        }

        List<long> GetListPrintIdByAfterDelete(SAR.EFMODEL.DataModels.SAR_PRINT sarDelete)
        {
            List<long> result = new List<long>();
            try
            {
                if (!String.IsNullOrEmpty(jsonPrintId))
                {
                    var arrIds = jsonPrintId.Split(',', ';');
                    if (arrIds != null && arrIds.Length > 0)
                    {
                        foreach (var id in arrIds)
                        {
                            long printId = Inventec.Common.TypeConvert.Parse.ToInt64(id);
                            if (printId > 0 && printId != sarDelete.ID)
                            {
                                result.Add(printId);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
            return result;
        }
    }
}
