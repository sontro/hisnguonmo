using DevExpress.XtraBars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars;

namespace HIS.Desktop.Plugins.BaseCompensationCreate
{
    delegate void ExpMestAggregateMouseRight_Click(object sender, ItemClickEventArgs e);

    internal class ExpMestBCSPopupMenuProcessor
    {
        MOS.EFMODEL.DataModels.V_HIS_EXP_MEST _ExpMestMouseRight;
        ExpMestAggregateMouseRight_Click expMestAggregatePrintClick;
        BarManager barManager;
        PopupMenu menu;

        internal enum PrintType
        {
            InTraDoiThuoc,
            PhieuBuCoSo
            
        }

        internal ExpMestBCSPopupMenuProcessor(MOS.EFMODEL.DataModels.V_HIS_EXP_MEST _expMest, ExpMestAggregateMouseRight_Click aggregatePrintClick, BarManager barManager)
        {
            try
            {
                this._ExpMestMouseRight = _expMest;
                this.expMestAggregatePrintClick = aggregatePrintClick;
                this.barManager = barManager;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        internal void InitMenu()
        {
            try
            {
                if (menu == null)
                    menu = new PopupMenu(barManager);

                menu.ItemLinks.Clear();

                BarButtonItem itemInTraDoiBCS = new BarButtonItem(barManager, "Phiếu tra đối bù cơ số", 2);
                itemInTraDoiBCS.Tag = PrintType.InTraDoiThuoc;
                itemInTraDoiBCS.ItemClick += new ItemClickEventHandler(expMestAggregatePrintClick);

                BarButtonItem itemInPhieuLinhThuoc = new BarButtonItem(barManager, "Phiếu bù cơ số", 4);
                itemInPhieuLinhThuoc.Tag = PrintType.PhieuBuCoSo;
                itemInPhieuLinhThuoc.ItemClick += new ItemClickEventHandler(expMestAggregatePrintClick);

                menu.AddItems(new BarItem[] {itemInTraDoiBCS, itemInPhieuLinhThuoc });
                menu.ShowPopup(Cursor.Position);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }
    }
}
