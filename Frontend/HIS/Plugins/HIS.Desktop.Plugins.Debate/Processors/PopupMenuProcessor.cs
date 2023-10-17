using DevExpress.XtraBars;
using HIS.Desktop.Common;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.Debate.Processors
{
    delegate void MouseRightClick(object sender, ItemClickEventArgs e);

    class PopupMenuProcessor
    {
        BarManager _BarManager = null;
        internal PopupMenu _Menu = null;
        MouseRightClick _MouseRightClick;
        V_HIS_DEBATE _DebatePoppup;
        RefeshReference BtnRefreshPhimTat;
        internal enum ItemType
        {
            TMP
        }
        internal PopupMenuProcessor(V_HIS_DEBATE currentDebate, BarManager barManager, MouseRightClick mouseRightClick, RefeshReference _BtnRefreshPhimTat)
        {
            this._BarManager = barManager;
            this._MouseRightClick = mouseRightClick;
            this._DebatePoppup = currentDebate;
            this.BtnRefreshPhimTat = _BtnRefreshPhimTat;
        }

        internal void InitMenu(System.Drawing.Point point)
        {
            try
            {
                if (this._BarManager == null || this._MouseRightClick == null)
                    return;
                if (this._Menu == null)
                    this._Menu = new PopupMenu(this._BarManager);
                this._Menu.ItemLinks.Clear();

                if (this._DebatePoppup == null)
                    return;

                //TMP
                BarButtonItem bbtnTMP = new BarButtonItem(this._BarManager, "Gửi thông tin đến hệ thống y tế từ xa - TMP", 2);
                bbtnTMP.Tag = ItemType.TMP;
                bbtnTMP.ItemClick += new ItemClickEventHandler(this._MouseRightClick);
                this._Menu.AddItems(new BarItem[] { bbtnTMP });

                this._Menu.ShowPopup(point);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
