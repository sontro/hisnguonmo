using DevExpress.XtraBars;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.ExportXmlQD4210
{
    delegate void MouseRightClick(object sender, ItemClickEventArgs e);
    class PopupMenuProcessor
    {
        BarManager _BarManager = null;
        PopupMenu _Menu = null;
        MouseRightClick _MouseRightClick;
        internal PopupMenuProcessor(BarManager barManager, MouseRightClick mouseRightClick)
        {
            this._BarManager = barManager;
            this._MouseRightClick = mouseRightClick;
        }

        internal enum ItemType
        {
            XuatXML,
            XuatXMLThongTuyen,
            XuatXMLGop,
            XuatDuLieuBenhNhan,
            XuatLaiFileXML4210Server,
        }

        internal void InitMenu()
        {
            try
            {
                if (this._BarManager == null || this._MouseRightClick == null)
                    return;
                if (this._Menu == null)
                    this._Menu = new PopupMenu(this._BarManager);

                BarButtonItem bbtnXuatXml = new BarButtonItem(this._BarManager, "Xuất XML", 0);
                bbtnXuatXml.Tag = ItemType.XuatXML;
                bbtnXuatXml.ItemClick += new ItemClickEventHandler(this._MouseRightClick);
                this._Menu.AddItems(new BarItem[] { bbtnXuatXml });

                BarButtonItem bbtnXuatXMLThongTuyen = new BarButtonItem(this._BarManager, "Xuất XML thông tuyến", 1);
                bbtnXuatXMLThongTuyen.Tag = ItemType.XuatXMLThongTuyen;
                bbtnXuatXMLThongTuyen.ItemClick += new ItemClickEventHandler(this._MouseRightClick);
                this._Menu.AddItems(new BarItem[] { bbtnXuatXMLThongTuyen });

                BarButtonItem bbtnXuatXMLGop = new BarButtonItem(this._BarManager, "Xuất XML gộp", 2);
                bbtnXuatXMLGop.Tag = ItemType.XuatXMLGop;
                bbtnXuatXMLGop.ItemClick += new ItemClickEventHandler(this._MouseRightClick);
                this._Menu.AddItems(new BarItem[] { bbtnXuatXMLGop });

                BarButtonItem bbtnXuatDuLieuBenhNhan = new BarButtonItem(this._BarManager, "Xuất dữ liệu bệnh nhân", 3);
                bbtnXuatDuLieuBenhNhan.Tag = ItemType.XuatDuLieuBenhNhan;
                bbtnXuatDuLieuBenhNhan.ItemClick += new ItemClickEventHandler(this._MouseRightClick);
                this._Menu.AddItems(new BarItem[] { bbtnXuatDuLieuBenhNhan });

                BarButtonItem bbtnXuatLaiFileXML4210Server = new BarButtonItem(this._BarManager, "Xuất lại file XML4210 server (file được sinh ra khi thiết lập xuất XML tự động)", 2);
                bbtnXuatLaiFileXML4210Server.Tag = ItemType.XuatLaiFileXML4210Server;
                bbtnXuatLaiFileXML4210Server.ItemClick += new ItemClickEventHandler(this._MouseRightClick);
                this._Menu.AddItems(new BarItem[] { bbtnXuatLaiFileXML4210Server });


                this._Menu.ShowPopup(Cursor.Position);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
