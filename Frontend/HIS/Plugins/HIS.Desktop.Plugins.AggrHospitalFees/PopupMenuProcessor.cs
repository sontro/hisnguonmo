using DevExpress.XtraBars;
using MOS.EFMODEL.DataModels;
using System;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.AggrHospitalFees
{
    delegate void TransactionMouseRightClick(object sender, ItemClickEventArgs e);
    class PopupMenuProcessor
    {
        V_HIS_TRANSACTION _Transaction = null;
        BarManager _BarManager = null;
        PopupMenu _PopupMenu = null;
        TransactionMouseRightClick _MouseRightClick;
        Inventec.Desktop.Common.Modules.Module CurrentModule;

        internal enum ItemType
        {
            
            HuyHoaDon
        }

        internal PopupMenuProcessor(V_HIS_TRANSACTION transaction, BarManager barmanager, TransactionMouseRightClick mouseRightClick, Inventec.Desktop.Common.Modules.Module currentModule)
        {
            this._Transaction = transaction;
            this._MouseRightClick = mouseRightClick;
            this._BarManager = barmanager;
            this.CurrentModule = currentModule;
        }

        internal void InitMenu()
        {
            try
            {
                if (this._Transaction == null || this._BarManager == null || this._MouseRightClick == null)
                    return;
                //if (this._Transaction.IS_CANCEL == 1)
                //{
                //    Inventec.Common.Logging.LogSystem.Info("giao dich da bi huy: " + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => this._Transaction), this._Transaction));
                //    return;
                //}
                if (this._PopupMenu == null)
                    this._PopupMenu = new PopupMenu(this._BarManager);
                this._PopupMenu.ItemLinks.Clear();


                BarButtonItem bbtnPhieuThuThanhToan = new BarButtonItem(this._BarManager, "Phiếu yêu cầu hủy hóa đơn", 0);
                bbtnPhieuThuThanhToan.Tag = ItemType.HuyHoaDon;
                bbtnPhieuThuThanhToan.ItemClick += new ItemClickEventHandler(this._MouseRightClick);
                this._PopupMenu.AddItems(new BarItem[] { bbtnPhieuThuThanhToan });

                this._PopupMenu.ShowPopup(Cursor.Position);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
