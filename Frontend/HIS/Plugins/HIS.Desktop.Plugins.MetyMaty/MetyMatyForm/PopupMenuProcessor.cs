using DevExpress.XtraBars;
using HIS.Desktop.LocalStorage.BackendData;
using HIS.Desktop.Plugins.MetyMaty.ADO;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.MetyMaty
{
    delegate void MouseRight_Click(object sender, ItemClickEventArgs e);

    class PopupMenuProcessor
    {
        MaterialTypeADO _Data;
        MouseRight_Click _MouseRightClick;
        BarManager barManager;
        PopupMenu menu;
        internal enum ModuleType
        {
            T1,

        }
        internal ModuleType moduleType { get; set; }

        internal PopupMenuProcessor(MaterialTypeADO _data, MouseRight_Click MouseRightClick, BarManager barManager)
        {
            this._Data = _data;
            this._MouseRightClick = MouseRightClick;
            this.barManager = barManager;
        }

        internal void InitMenu()
        {
            try
            {
                if (menu == null)
                    menu = new PopupMenu(barManager);
                // Add item and show
                menu.ItemLinks.Clear();
                BarButtonItem itemT1 = new BarButtonItem(barManager, "Đổi chế phẩm", 1);
                itemT1.Tag = ModuleType.T1;
                itemT1.ItemClick += new ItemClickEventHandler(_MouseRightClick);
                menu.AddItems(new BarItem[] { itemT1 });

                menu.ShowPopup(Cursor.Position);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }
    }
}
