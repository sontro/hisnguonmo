using DevExpress.XtraBars;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.ApprovalPrescriptionPK
{
    delegate void MouseRight_Click(object sender, ItemClickEventArgs e);
    class PopupMenuProcessor
    {
        HisExpMestGroupByTreatmentSDO pData;
        BarManager barManager;
        MouseRight_Click mouseRight_Click;
        PopupMenu popupMenu;
        long cboDonThuoc;

        internal PopupMenuProcessor() { }

        internal PopupMenuProcessor(long cboDonthuoc, HisExpMestGroupByTreatmentSDO rowData, BarManager barManager, MouseRight_Click mouseRight_Click)
        {
            try
            {
                this.pData = rowData;
                this.barManager = barManager;
                this.mouseRight_Click = mouseRight_Click;
                this.cboDonThuoc = cboDonthuoc;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);                
            }
        }

        internal enum ItemType
        {
            HuyDuyet,
            HuyThucXuat,
            InTongHop
        }

        internal void InitMenu()
        {
            try
            {
                if (this.barManager == null || this.mouseRight_Click == null)
                    return;
                if (this.popupMenu == null)
                    this.popupMenu = new PopupMenu(this.barManager);

                #region Thao tác

                if (this.cboDonThuoc == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__EXECUTE)
                {
                    BarButtonItem bbtHuyDuyet = new BarButtonItem(this.barManager, "Hủy duyệt", 1);
                    bbtHuyDuyet.Tag = ItemType.HuyDuyet;
                    bbtHuyDuyet.ItemClick += new ItemClickEventHandler(this.mouseRight_Click);
                    popupMenu.ItemLinks.Add(bbtHuyDuyet);
                }

                if (this.cboDonThuoc == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__DONE)
                {
                    BarButtonItem bbtHuyThucXuat = new BarButtonItem(this.barManager, "Hủy thực xuất", 1);
                    bbtHuyThucXuat.Tag = ItemType.HuyThucXuat;
                    bbtHuyThucXuat.ItemClick += new ItemClickEventHandler(this.mouseRight_Click);
                    popupMenu.ItemLinks.Add(bbtHuyThucXuat);
                }

                BarButtonItem bbtInTongHop = new BarButtonItem(this.barManager, "In tổng hợp", 1);
                bbtInTongHop.Tag = ItemType.InTongHop;
                bbtInTongHop.ItemClick += new ItemClickEventHandler(this.mouseRight_Click);
                popupMenu.ItemLinks.Add(bbtInTongHop);

                this.popupMenu.ShowPopup(Cursor.Position);
                #endregion
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);                
            }
        }


    }
}
