using HIS.Desktop.ApiConsumer;
using HIS.Desktop.Controls.Session;
using Inventec.Common.Adapter;
using Inventec.Core;
using Inventec.Desktop.Common.Message;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.ListMediStock
{
    public partial class frmListMediStock : Form
    {
        int rowCount = 0;
        int dataTotal = 0;
        int numPageSize = 100;

        internal void FillDataToGridControl()
        {
            FillDataToGridMetiStock(new CommonParam(0, (int)numPageSize));

            CommonParam param = new CommonParam();
            param.Limit = rowCount;
            param.Count = dataTotal;
            ucPaging1.Init(FillDataToGridMetiStock, param);
        }

        private void FillDataToGridMetiStock(object param)
        {
            try
            {
                WaitingManager.Show();
                int start = ((CommonParam)param).Start ?? 0;
                int limit = ((CommonParam)param).Limit ?? 0;
                numPageSize = ((CommonParam)param).Limit ?? 0;
                CommonParam paramCommon = new CommonParam(start, limit);
                Inventec.Core.ApiResultObject<List<V_HIS_MEDI_STOCK>> apiResult = new ApiResultObject<List<V_HIS_MEDI_STOCK>>();
                MOS.Filter.HisMediStockViewFilter mediStockFilter = new HisMediStockViewFilter();
                mediStockFilter.KEY_WORD = txtKeyWord.Text.Trim();


                apiResult = new BackendAdapter(paramCommon)
                    .GetRO<List<V_HIS_MEDI_STOCK>>("api/HisMediStock/GetView", ApiConsumers.MosConsumer, mediStockFilter, paramCommon);

                if (apiResult != null)
                {
                    List<V_HIS_MEDI_STOCK> rs = new List<V_HIS_MEDI_STOCK>();
                    gridControlListMediStock.DataSource = rs;
                    mediStocks = (List<V_HIS_MEDI_STOCK>)apiResult.Data;
                    rowCount = (mediStocks == null ? 0 : mediStocks.Count);
                    dataTotal = (apiResult.Param == null ? 0 : apiResult.Param.Count ?? 0);
                    if (mediStocks != null)
                    {
                        gridControlListMediStock.DataSource = mediStocks;
                    }
                }

                #region Process has exception
                SessionManager.ProcessTokenLost((CommonParam)param);
                #endregion

                gridViewListMediStock.OptionsSelection.EnableAppearanceFocusedCell = false;
                gridViewListMediStock.OptionsSelection.EnableAppearanceFocusedRow = false;

                WaitingManager.Hide();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }
    }
}
