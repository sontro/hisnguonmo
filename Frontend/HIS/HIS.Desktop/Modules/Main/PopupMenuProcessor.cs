using DevExpress.XtraBars;
using DevExpress.XtraTab;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Modules.Main
{
    delegate void RightMouse_Click(object sender, ItemClickEventArgs e);

    class PopupMenuProcessor
    {
        RightMouse_Click rightMouseClick;
        BarManager barManager;
        PopupMenu menu;

        public class TabControlType
        {
            public XtraTabControl Tab { get; set; }
            public ModuleType Type { get; set; }
        }

        internal enum ModuleType
        {
            Right,
            Other,
        }

        public PopupMenuProcessor(RightMouse_Click _RightMouseClick, BarManager barManager)
        {
            this.rightMouseClick = _RightMouseClick;
            this.barManager = barManager;
        }

        internal void InitMenu(XtraTabControl _Tab)
        {
            try
            {
                if (menu == null)
                    menu = new PopupMenu(barManager);
                // Add item and show
                menu.ItemLinks.Clear();

                BarButtonItem itemCloseRight = new BarButtonItem(barManager, Resources.ResourceCommon.DongTabBenPhai, 1);
                itemCloseRight.Tag = new TabControlType() { Tab = _Tab, Type = ModuleType.Right };
                itemCloseRight.ItemClick += new ItemClickEventHandler(rightMouseClick);
                menu.AddItems(new BarItem[] { itemCloseRight });

                BarButtonItem itemCloseOther = new BarButtonItem(barManager, Resources.ResourceCommon.DongTabKhac, 1);
                itemCloseOther.Tag = new TabControlType() { Tab = _Tab, Type = ModuleType.Other };
                itemCloseOther.ItemClick += new ItemClickEventHandler(rightMouseClick);
                menu.AddItems(new BarItem[] { itemCloseOther });

                menu.ShowPopup(Cursor.Position);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
