using DevExpress.XtraBars;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.HisBidList
{
    delegate void MouseRightClick(object sender, ItemClickEventArgs e);
    class PopupMenuProcessor
    {
        BarManager barManager;
        MouseRightClick mouseRight_Click;
        PopupMenu popupMenu;
        V_HIS_BID currentBID;

        internal PopupMenuProcessor() { }

        internal PopupMenuProcessor(V_HIS_BID currentBID, BarManager barManager, MouseRightClick mouseRight_Click)
        {
            try
            {
                this.barManager = barManager;
                this.mouseRight_Click = mouseRight_Click;
                this.currentBID = currentBID;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        internal enum ItemType
        {
            EventLog,
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

                //EventLog
                BarButtonItem bbtnEventLog = new BarButtonItem(this.barManager, "Lịch sử tác động", 2);
                bbtnEventLog.Tag = ItemType.EventLog;
                bbtnEventLog.ItemClick += new ItemClickEventHandler(this.mouseRight_Click);
                this.popupMenu.AddItem(bbtnEventLog).BeginGroup = true;

                #endregion

                this.popupMenu.ShowPopup(Cursor.Position);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }
    }
}
