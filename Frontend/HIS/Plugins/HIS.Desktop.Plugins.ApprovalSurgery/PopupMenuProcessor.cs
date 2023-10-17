using DevExpress.XtraBars;
using Inventec.Common.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.ApprovalSurgery
{
	delegate void SereServMouseRightClick(object sender, ItemClickEventArgs e);
	internal class PopupMenuProcessor
    {
		internal enum ItemType
		{
			DanhSachYLenh,
			HoSoDieuTri,
			BienBanHoiChan,
			DanhSachToDieuTri
		}
		private BarManager _BarManager = null;

		internal PopupMenu _Menu = null;

		private SereServMouseRightClick _MouseRightClick;
		internal PopupMenuProcessor(BarManager barManager, SereServMouseRightClick Click)
		{
			_BarManager = barManager;
			_MouseRightClick = Click;
		}
		internal void InitMenu(long roomId)
		{
			try
			{
				if (_BarManager != null && _MouseRightClick != null)
				{
					if (_Menu == null)
					{
						_Menu = new PopupMenu(_BarManager);
					}
					_Menu.ItemLinks.Clear();
					BarButtonItem barButtonItem = new BarButtonItem(_BarManager, "Hồ sơ điều trị", 10);
					barButtonItem.Tag = ItemType.HoSoDieuTri;
					barButtonItem.ItemClick += _MouseRightClick.Invoke;
					_Menu.AddItems(new BarItem[1] { barButtonItem });
					BarButtonItem barButtonItem2 = new BarButtonItem(_BarManager, "Danh sách tờ điều trị", 10);
					barButtonItem2.Tag = ItemType.DanhSachToDieuTri;
					barButtonItem2.ItemClick += _MouseRightClick.Invoke;
					_Menu.AddItems(new BarItem[1] { barButtonItem2 });
					BarButtonItem barButtonItem3 = new BarButtonItem(_BarManager, "Danh sách y lệnh", 10);
					barButtonItem3.Tag = ItemType.DanhSachYLenh;
					barButtonItem3.ItemClick += _MouseRightClick.Invoke;
					_Menu.AddItems(new BarItem[1] { barButtonItem3 });
					BarButtonItem barButtonItem4 = new BarButtonItem(_BarManager, "Danh sách biên bản hội chẩn", 10);
					barButtonItem4.Tag = ItemType.BienBanHoiChan;
					barButtonItem4.ItemClick += _MouseRightClick.Invoke;
					_Menu.AddItems(new BarItem[1] { barButtonItem4 });
					_Menu.ShowPopup(Cursor.Position);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
			}
		}
	}
}
