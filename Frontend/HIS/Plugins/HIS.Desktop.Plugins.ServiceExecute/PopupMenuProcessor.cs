using DevExpress.XtraBars;
using HIS.Desktop.ApiConsumer;
using HIS.Desktop.Common;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.ServiceExecute
{
    delegate void ServiceMouseRightClick(object sender, ItemClickEventArgs e);
    class PopupMenuProcessor
    {
        BarManager _BarManager = null;
        PopupMenu _Menu = null;
        ServiceMouseRightClick _MouseRightClick;
        ADO.ServiceADO _ServicePoppupPrint;
        RefeshReference BtnRefreshPhimTat;
        public static HIS_SERE_SERV_EXT sereServExt;

        internal PopupMenuProcessor(ADO.ServiceADO currentServiceADO, BarManager barManager, ServiceMouseRightClick mouseRightClick, RefeshReference _BtnRefreshPhimTat)
        {
            this._BarManager = barManager;
            this._MouseRightClick = mouseRightClick;
            this._ServicePoppupPrint = currentServiceADO;
            this.BtnRefreshPhimTat = _BtnRefreshPhimTat;
        }

        internal enum ItemType
        {
            EkipExecute,
            PhieuKeKhaiThuocVatTuTieuHao,
            PhieuPTTT,
            KeThuocVatTuTieuHao,
            KhaiBaoThuocVTTH,
            HuyPhieuThuocVatTuTieuHaoDaKe,
            XacNhanKhongThucHien,
            HuyXacNhanKhongThucHien,
            HuyXuLy
        }

        internal void InitMenu()
        {
            try
            {
                if (this._BarManager == null || this._MouseRightClick == null)
                    return;
                if (this._Menu == null)
                    this._Menu = new PopupMenu(this._BarManager);
                sereServExt = new HIS_SERE_SERV_EXT();
                CommonParam param = new CommonParam();
                HisSereServExtFilter filter = new HisSereServExtFilter();
                filter.SERE_SERV_ID = this._ServicePoppupPrint.ID;
                var sereServExts = new Inventec.Common.Adapter.BackendAdapter
                    (param).Get<List<MOS.EFMODEL.DataModels.HIS_SERE_SERV_EXT>>
                    ("api/HisSereServExt/Get", ApiConsumer.ApiConsumers.MosConsumer, filter, param);
                if (sereServExts != null && sereServExts.Count > 0)
                {
                    Inventec.Common.Logging.LogSystem.Debug("sereServExts_____________ " + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => sereServExts), sereServExts));
                    sereServExt = sereServExts.Where(o => o.SUBCLINICAL_PRES_ID != null).FirstOrDefault();
                    Inventec.Common.Logging.LogSystem.Debug("sereServExt_____________ " + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => sereServExt), sereServExt));

                }
                BarButtonItem bbtnEkipExecute = new BarButtonItem(this._BarManager, ResourceMessage.ThucHienCls, 1);
                bbtnEkipExecute.Tag = ItemType.EkipExecute;
                bbtnEkipExecute.ItemClick += new ItemClickEventHandler(this._MouseRightClick);
                this._Menu.AddItems(new BarItem[] { bbtnEkipExecute });

                BarButtonItem bbtnKeThuocVatTuTieuHao = new BarButtonItem(this._BarManager, ResourceMessage.KeThuocVatTuTieuHao, 1);
                bbtnKeThuocVatTuTieuHao.Tag = ItemType.KeThuocVatTuTieuHao;
                bbtnKeThuocVatTuTieuHao.ItemClick += new ItemClickEventHandler(this._MouseRightClick);
                this._Menu.AddItems(new BarItem[] { bbtnKeThuocVatTuTieuHao });

                BarButtonItem bbtnKhaiBaoThuocVTTH = new BarButtonItem(this._BarManager, ResourceMessage.KhaiBaoThuocVTTH, 1);
                bbtnKhaiBaoThuocVTTH.Tag = ItemType.KhaiBaoThuocVTTH;
                bbtnKhaiBaoThuocVTTH.ItemClick += new ItemClickEventHandler(this._MouseRightClick);
                this._Menu.AddItems(new BarItem[] { bbtnKhaiBaoThuocVTTH });

                if (sereServExt != null && sereServExt.SUBCLINICAL_PRES_ID != null)
                {
                    BarButtonItem bbtnHuyPhieuThuocVatTuTieuHaoDaKe = new BarButtonItem(this._BarManager, ResourceMessage.HuyPhieuThuocVatTuTieuHaoDaKe, 1);
                    bbtnHuyPhieuThuocVatTuTieuHaoDaKe.Tag = ItemType.HuyPhieuThuocVatTuTieuHaoDaKe;
                    bbtnHuyPhieuThuocVatTuTieuHaoDaKe.ItemClick += new ItemClickEventHandler(this._MouseRightClick);
                    this._Menu.AddItems(new BarItem[] { bbtnHuyPhieuThuocVatTuTieuHaoDaKe });
                }

                BarButtonItem bbtnPhieuKeKhaiThuocVatTu = new BarButtonItem(this._BarManager, ResourceMessage.KeKhaiThuocHaoPhi, 1);
                bbtnPhieuKeKhaiThuocVatTu.Tag = ItemType.PhieuKeKhaiThuocVatTuTieuHao;
                bbtnPhieuKeKhaiThuocVatTu.ItemClick += new ItemClickEventHandler(this._MouseRightClick);
                this._Menu.AddItems(new BarItem[] { bbtnPhieuKeKhaiThuocVatTu });

                BarButtonItem bbtnPhieuPTTT = new BarButtonItem(this._BarManager, ResourceMessage.PhieuPhauThuatThuThuat, 1);
                bbtnPhieuPTTT.Tag = ItemType.PhieuPTTT;
                bbtnPhieuPTTT.ItemClick += new ItemClickEventHandler(this._MouseRightClick);
                this._Menu.AddItems(new BarItem[] { bbtnPhieuPTTT });

                if (!_ServicePoppupPrint.IsProcessed && _ServicePoppupPrint.IS_NO_EXECUTE != 1 && _ServicePoppupPrint.IS_CONFIRM_NO_EXCUTE != 1)
                {
                    BarButtonItem bbtnXacNhanKhongThucHien = new BarButtonItem(this._BarManager, ResourceMessage.XacNhanKhongThucHien, 1);
                    bbtnXacNhanKhongThucHien.Tag = ItemType.XacNhanKhongThucHien;
                    bbtnXacNhanKhongThucHien.ItemClick += new ItemClickEventHandler(this._MouseRightClick);
                    this._Menu.AddItems(new BarItem[] { bbtnXacNhanKhongThucHien });
                }
                if(_ServicePoppupPrint.IS_NO_EXECUTE != 1 && _ServicePoppupPrint.IS_CONFIRM_NO_EXCUTE == 1)
                {
                    BarButtonItem bbtnHuyXacNhanKhongThucHien = new BarButtonItem(this._BarManager, ResourceMessage.HuyXacNhanKhongThucHien, 1);
                    bbtnHuyXacNhanKhongThucHien.Tag = ItemType.HuyXacNhanKhongThucHien;
                    bbtnHuyXacNhanKhongThucHien.ItemClick += new ItemClickEventHandler(this._MouseRightClick);
                    this._Menu.AddItems(new BarItem[] { bbtnHuyXacNhanKhongThucHien });
                }
                if (_ServicePoppupPrint.IsProcessed && _ServicePoppupPrint.IS_NO_EXECUTE != 1)
                {
                    BarButtonItem bbtnHuyXuLy = new BarButtonItem(this._BarManager, ResourceMessage.HuyXuLy, 1);
                    bbtnHuyXuLy.Tag = ItemType.HuyXuLy;
                    bbtnHuyXuLy.ItemClick += new ItemClickEventHandler(this._MouseRightClick);
                    this._Menu.AddItems(new BarItem[] { bbtnHuyXuLy });
                }
                this._Menu.ShowPopup(Cursor.Position);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
