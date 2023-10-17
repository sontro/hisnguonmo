using HIS.Desktop.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using MOS.Filter;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Adapter;
using Inventec.Core;
using HIS.Desktop.ApiConsumer;
using DevExpress.Data;
using System.Collections;
using DevExpress.XtraGrid.Views.Base;

namespace HIS.Desktop.Plugins.HisCarerCard
{
    public partial class frmCarerCardBorrow : FormBase
    {
        long carerCardId;
        public frmCarerCardBorrow(long _carerCardId)
        {
            InitializeComponent();
            this.carerCardId = _carerCardId;
        }

        private void frmCarerCardBorrow_Load(object sender, EventArgs e)
        {
            this.Icon = Icon.ExtractAssociatedIcon(System.IO.Path.Combine(Inventec.Desktop.Common.LocalStorage.Location.ApplicationStoreLocation.ApplicationDirectory, System.Configuration.ConfigurationSettings.AppSettings["Inventec.Desktop.Icon"]));
            FillDataFormList();
        }

        private void FillDataFormList()
        {
            try
            {
                HisCarerCardBorrowViewFilter filter = new HisCarerCardBorrowViewFilter();
                filter.CARER_CARD_ID = this.carerCardId;

                var lstCarerCardBorrow = new BackendAdapter(new CommonParam()).Get<List<V_HIS_CARER_CARD_BORROW>>("api/HisCarerCardBorrow/GetView", ApiConsumers.MosConsumer, filter, null);
                grdCarerCardBorrow.DataSource = null;
                if (lstCarerCardBorrow != null && lstCarerCardBorrow.Count() > 0)
                    lstCarerCardBorrow = lstCarerCardBorrow.OrderByDescending(o => o.BORROW_TIME).ToList();

                grdCarerCardBorrow.DataSource = lstCarerCardBorrow;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void grvCarerCardBorrow_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            try
            {
                if (e.IsGetData && e.Column.UnboundType != UnboundColumnType.Bound)
                {
                    V_HIS_CARER_CARD_BORROW pData = (V_HIS_CARER_CARD_BORROW)((IList)((BaseView)sender).DataSource)[e.ListSourceRowIndex];
                    if (e.Column.FieldName == "STT")
                    {
                        e.Value = e.ListSourceRowIndex + 1;
                    }

                    else if (e.Column.FieldName == "BORROW_TIME_STR")
                    {
                        e.Value = Inventec.Common.DateTime.Convert.TimeNumberToTimeString(pData.BORROW_TIME);
                    }
                    else if (e.Column.FieldName == "GIVE_BACK_TIME_STR")
                    {
                        e.Value = Inventec.Common.DateTime.Convert.TimeNumberToTimeString(pData.GIVE_BACK_TIME ?? 0);
                    }


                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }
    }
}
