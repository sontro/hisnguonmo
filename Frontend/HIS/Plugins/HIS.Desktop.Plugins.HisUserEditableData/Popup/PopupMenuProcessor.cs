using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraBars;
using System.Windows.Forms;
namespace HIS.Desktop.Plugins.HisUserEditableData.Popup
{
    delegate void MouseRightClick(object sender, ItemClickEventArgs e);

    class PopupMenuProcessor
    {
        BarManager _Barmanager = null;
        DevExpress.XtraBars.PopupMenu _Menu = null;
        MouseRightClick _MouseRightClick;
        internal PopupMenuProcessor(BarManager barManager, MouseRightClick mouseRightClick)
        {
            this._Barmanager = barManager;
            this._MouseRightClick = mouseRightClick;
        }
        public enum ItemType
        { 
            Copy,Paste
        }
        internal void InitMenu()
        {
            try
            { 
                if(this._Barmanager == null || this._MouseRightClick == null)
                    return;
                if(this._Menu ==null)
                    this._Menu = new PopupMenu(this._Barmanager);

                //barBtn copy
                BarButtonItem btnCopy = new BarButtonItem(this._Barmanager,"Copy",0);
                btnCopy.Tag = ItemType.Copy;
                btnCopy.ItemClick += new ItemClickEventHandler(this._MouseRightClick);
                this._Menu.AddItems(new BarItem[]{btnCopy});

                //bar btn paste
                BarButtonItem btnPaste = new BarButtonItem(this._Barmanager,"Paste",1);
                btnPaste.Tag = ItemType.Paste;
                btnPaste.ItemClick += new ItemClickEventHandler(this._MouseRightClick);
                this._Menu.AddItems(new BarItem[] {btnPaste});

                this._Menu.ShowPopup(Cursor.Position);
            }
            catch(Exception e)
            {
                Inventec.Common.Logging.LogSystem.Error(e);
            }
        }
    }
}
