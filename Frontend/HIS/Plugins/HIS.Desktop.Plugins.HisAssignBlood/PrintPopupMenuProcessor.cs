using DevExpress.XtraBars;
using HIS.Desktop.ApiConsumer;
using HIS.Desktop.LocalStorage.BackendData;
using Inventec.Common.Adapter;
using Inventec.Core;
using Inventec.Desktop.Common.LanguageManager;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.HisAssignBlood
{
    delegate void PrintBlood_Click(object sender, ItemClickEventArgs e);

    class PrintPopupMenuProcessor
    {
        PrintBlood_Click PrintMouseClick;
        BarManager barManager;
        PopupMenu menu;
        ADO.BloodTypeADO ado;
       
        internal enum ModuleType
        {
            ChiDinhXetNghiem,
          
        }


        internal PrintPopupMenuProcessor(PrintBlood_Click PrintMouseClick, BarManager barManager, ADO.BloodTypeADO ado)
        {
            this.PrintMouseClick = PrintMouseClick;
            this.barManager = barManager;
            this.ado = ado;
        
        }

        internal void RightMenu()
        {
            try
            {
                if (menu == null)
                    menu = new PopupMenu(barManager);
                // Add item and show
                menu.ItemLinks.Clear();
                           
                BarButtonItem itemOtherV2 = new BarButtonItem(barManager, "Chỉ định xét nghiệm",1);
                itemOtherV2.Tag = ModuleType.ChiDinhXetNghiem;
                itemOtherV2.ItemClick += new ItemClickEventHandler(PrintMouseClick);
                menu.AddItems(new BarItem[] { itemOtherV2 });                         
                menu.ShowPopup(Cursor.Position);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }


    }
}
