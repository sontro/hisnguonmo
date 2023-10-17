using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Inventec.Desktop.Common.Message;
using Inventec.Core;
using MOS.Filter;
using Inventec.Common.Adapter;
using System.Collections;
using DevExpress.XtraGrid.Views.Base;
using HIS.Desktop.ApiConsumer;
using HIS.Desktop.Common;
using HIS.Desktop.Controls.Session;
using HIS.Desktop.LocalStorage.ConfigApplication;
using HIS.Desktop.Plugins.ExpMestSaleCreate.ADO;
namespace HIS.Desktop.Plugins.ExpMestSaleCreate
{
    public partial class frmDetails  : HIS.Desktop.Utility.FormBase
    {
        List<PriceDetailsADO> listADO;
        bool hideColumn;
        public frmDetails(Inventec.Desktop.Common.Modules.Module module, List<PriceDetailsADO> lst, bool _hideColumn) : base(module)
        {
            InitializeComponent();
            try
            {
                if (lst != null && lst.Count > 0)
                {
                    this.listADO = lst;
                }
                this.hideColumn = _hideColumn;
                string iconPath = System.IO.Path.Combine(HIS.Desktop.LocalStorage.Location.ApplicationStoreLocation.ApplicationStartupPath, System.Configuration.ConfigurationSettings.AppSettings["Inventec.Desktop.Icon"]);
                this.Icon = Icon.ExtractAssociatedIcon(iconPath);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void frmDetails_Load(object sender, EventArgs e)
        {
            try
            {
                if (this.listADO != null && this.listADO.Count > 0)
                {
                    PriceDetailsADO ado = new PriceDetailsADO();
                    ado.PATIENT_NAME = "Tổng";                   
                    ado.PRICE = listADO.Sum(o => o.PRICE);
                    ado.DiscountRatio = listADO.Sum(o => o.DiscountRatio);
                    decimal priceRound = listADO.Sum(o => Decimal.Parse(o.PRICE_ROUND.ToString().Replace(".", "")));
                    ado.PRICE_ROUND = Inventec.Common.Number.Convert.NumberToString(priceRound, ConfigApplications.NumberSeperator); 
                    listADO.Add(ado);
                    if (!hideColumn)
                    {
                        gridColumn3.VisibleIndex = -1;
                        gridColumn3.Visible = false;
                    }
                    gridColumn4.Caption = "Chiết khấu (" + listADO.FirstOrDefault().BASE_VALUE + "%)";
                    gridControl1.BeginUpdate();
                    gridControl1.DataSource = listADO;
                    gridControl1.EndUpdate();
                }
            }
            catch (Exception ex)
            {
                
                throw;
            }
        }

        private void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            try
            {
                DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
                if (e.RowHandle >= 0)
                {
                    PriceDetailsADO data = (PriceDetailsADO)((IList)((BaseView)sender).DataSource)[e.RowHandle];
                    if (data != null)
                    {
                        if (e.Column.FieldName == "PATIENT_NAME")
                        {
                            if (data.PATIENT_NAME == "Tổng")
                            {
                                e.Appearance.FontStyleDelta = System.Drawing.FontStyle.Bold;
                                e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void gridView1_CustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
        {
            try
            {
                
                if (e.IsGetData && e.Column.UnboundType != DevExpress.Data.UnboundColumnType.Bound)
                {
                    PriceDetailsADO ado = (PriceDetailsADO)((IList)((BaseView)sender).DataSource)[e.ListSourceRowIndex];
                    if (ado != null)
                    {
                        if (e.Column.FieldName == "PRICE_str")
                        {
                            e.Value = Inventec.Common.Number.Convert.NumberToString(ado.PRICE, ConfigApplications.NumberSeperator); 
                        }
                       
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
