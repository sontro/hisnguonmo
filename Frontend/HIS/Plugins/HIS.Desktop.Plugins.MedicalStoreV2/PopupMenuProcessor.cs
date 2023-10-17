using DevExpress.XtraBars;
using HIS.Desktop.Common;
using HIS.Desktop.Plugins.MedicalStoreV2.ADO;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.MedicalStoreV2
{
    delegate void MediRecordMouseRightClick(object sender, ItemClickEventArgs e);
    delegate void TreatmentMouseRightClick(object sender, ItemClickEventArgs e);
    class PopupMenuProcessor
    {
        BarManager _BarManager = null;
        internal PopupMenu _Menu = null;
        MediRecordMouseRightClick _MouseRightClick;
        MediRecordADO _MediRecorPoppupPrint;
        RefeshReference BtnRefreshPhimTat;
        TreatmentMouseRightClick _TreatMouseRightClick;
        TreatmentADO _TreatmentPoppupPrint;

        internal PopupMenuProcessor(MediRecordADO currentMediRecor, BarManager barManager, MediRecordMouseRightClick mouseRightClick, RefeshReference _BtnRefreshPhimTat)
        {
            this._BarManager = barManager;
            this._MouseRightClick = mouseRightClick;
            this._MediRecorPoppupPrint = currentMediRecor;
            this.BtnRefreshPhimTat = _BtnRefreshPhimTat;
        }

        internal PopupMenuProcessor(TreatmentADO currenttreatmentADO, BarManager barManager, TreatmentMouseRightClick treatMouseRightClick, RefeshReference _BtnRefreshPhimTat)
        {
            this._BarManager = barManager;
            this._TreatMouseRightClick = treatMouseRightClick;
            this._TreatmentPoppupPrint = currenttreatmentADO;
            this.BtnRefreshPhimTat = _BtnRefreshPhimTat;
        }
        internal enum ItemType
        {
            TrackingInMediRecord,
            CheckingTreatmentEmr
        }

        internal void InitMenu()
        {
            if (this._BarManager == null || this._MouseRightClick == null)
                return;
            if (this._Menu == null)
                this._Menu = new PopupMenu(this._BarManager);
            this._Menu.ItemLinks.Clear();

            BarButtonItem bbtnMediRecordt = new BarButtonItem(this._BarManager, "Tờ điều trị trong bệnh án", 0);
            bbtnMediRecordt.Tag = ItemType.TrackingInMediRecord;
            bbtnMediRecordt.ItemClick += new ItemClickEventHandler(this._MouseRightClick);
            this._Menu.AddItems(new BarItem[] { bbtnMediRecordt });

            this._Menu.ShowPopup(Cursor.Position);
        }

        internal void InitMenuTreat()
        {
            if (this._BarManager == null || this._TreatMouseRightClick == null)
                return;
            if (this._Menu == null)
                this._Menu = new PopupMenu(this._BarManager);
            this._Menu.ItemLinks.Clear();

            //tra soát hồ sơ bệnh án
            BarButtonItem bbtnCheckEMR = new BarButtonItem(this._BarManager, "Tra soát hồ sơ bệnh án", 0);
            bbtnCheckEMR.Tag = ItemType.CheckingTreatmentEmr;
            bbtnCheckEMR.ItemClick += new ItemClickEventHandler(this._TreatMouseRightClick);
            this._Menu.AddItems(new BarItem[] { bbtnCheckEMR });

            this._Menu.ShowPopup(Cursor.Position);
        }
    }
}
