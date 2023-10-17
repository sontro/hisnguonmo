using DevExpress.XtraBars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.AggrImpMestList
{
    delegate void ImpMestAggregateMouseRight_Click(object sender, ItemClickEventArgs e);

    internal class ImpMestAggregateListPopupMenuProcessor
    {
        MOS.EFMODEL.DataModels.V_HIS_IMP_MEST currentTreatmentSDO;
        ImpMestAggregateMouseRight_Click impMestAggregatePrintClick;
        BarManager barManager;
        PopupMenu menu;
        List<MOS.EFMODEL.DataModels.V_HIS_IMP_MEST> CheckedImpMest;

        internal enum PrintType
        {
            InTraDoiThuocTongHop = 7
        }

        internal ImpMestAggregateListPopupMenuProcessor(MOS.EFMODEL.DataModels.V_HIS_IMP_MEST currentTreatment, List<MOS.EFMODEL.DataModels.V_HIS_IMP_MEST> checkedImpMest, ImpMestAggregateMouseRight_Click aggregatePrintClick, BarManager barManager)
        {
            try
            {
                this.currentTreatmentSDO = currentTreatment;
                this.impMestAggregatePrintClick = aggregatePrintClick;
                this.CheckedImpMest = checkedImpMest;
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

                if (this.CheckedImpMest != null && this.CheckedImpMest.Count > 0 && this.CheckedImpMest.Exists(o => o.ID == this.currentTreatmentSDO.ID))
                {
                    BarButtonItem itemPhieuTraTongHop = new BarButtonItem(barManager, "In phiếu trả thuốc/vt tổng hợp", 1);
                    itemPhieuTraTongHop.Tag = PrintType.InTraDoiThuocTongHop;
                    itemPhieuTraTongHop.ItemClick += new ItemClickEventHandler(impMestAggregatePrintClick);

                    menu.AddItems(new BarItem[] { itemPhieuTraTongHop });
                    menu.ShowPopup(Cursor.Position);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
