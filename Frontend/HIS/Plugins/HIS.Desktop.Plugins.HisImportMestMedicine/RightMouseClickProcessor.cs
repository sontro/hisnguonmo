using DevExpress.XtraBars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.HisImportMestMedicine
{
    delegate void MouseRight_Click(object sender, ItemClickEventArgs e);

    class RightMouseClickProcessor
    {
        MOS.EFMODEL.DataModels.V_HIS_IMP_MEST ImpMestRightClick;
        MouseRight_Click mouseRightClick;
        BarManager barManager;
        PopupMenu menu;
        long roomId;
        string loginName;
        internal enum ModuleType
        {
            ManuExpMestCreate,
            ManuImpMestEdit,
        }
        internal ModuleType moduleType { get; set; }

        internal RightMouseClickProcessor(MOS.EFMODEL.DataModels.V_HIS_IMP_MEST currentImpMest, MouseRight_Click MouseRightClick, BarManager barManager, long _roomId, string loginName)
        {
            this.ImpMestRightClick = currentImpMest;
            this.mouseRightClick = MouseRightClick;
            this.barManager = barManager;
            this.roomId = _roomId;
            this.loginName = loginName;
        }

        internal void InitMenu()
        {
            try
            {
                if (menu == null)
                    menu = new PopupMenu(barManager);
                // Add item and show
                menu.ItemLinks.Clear();

                BarButtonItem itemManuExpMestCreate = new BarButtonItem(barManager, "Tạo phiếu xuất trả NCC", 1);
                itemManuExpMestCreate.Tag = ModuleType.ManuExpMestCreate;
                itemManuExpMestCreate.ItemClick += new ItemClickEventHandler(mouseRightClick);

                BarButtonItem itemManuImpMestEdit = new BarButtonItem(barManager, "Sửa phiếu nhập NCC", 2);
                itemManuImpMestEdit.Tag = ModuleType.ManuImpMestEdit;
                itemManuImpMestEdit.ItemClick += new ItemClickEventHandler(mouseRightClick);

                if (this.ImpMestRightClick.IMP_MEST_STT_ID == IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_STT.ID__IMPORT && this.ImpMestRightClick.IMP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__NCC)
                {
                    menu.AddItem(itemManuExpMestCreate);
                }

                if (this.ImpMestRightClick.IMP_MEST_STT_ID == IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_STT.ID__REQUEST && this.ImpMestRightClick.IMP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__NCC && this.ImpMestRightClick.CREATOR == loginName)
                {
                    menu.AddItem(itemManuImpMestEdit);
                }

                menu.ShowPopup(Cursor.Position);

            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }
    }
}
