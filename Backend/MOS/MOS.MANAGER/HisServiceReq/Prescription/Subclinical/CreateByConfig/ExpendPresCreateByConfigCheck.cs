using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisSereServExt;
using MOS.MANAGER.HisServiceMaty;
using MOS.MANAGER.HisServiceMety;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Prescription.Subclinical.CreateByConfig
{
    /// <summary>
    /// Xu ly ke don thuoc/vat tu tieu hao dua vao du lieu da thiet lap tu truoc
    /// </summary>
    class ExpendPresCreateByConfigCheck : BusinessBase
    {

        internal ExpendPresCreateByConfigCheck()
            : base()
        {
        }

        internal ExpendPresCreateByConfigCheck(CommonParam paramCreate)
            : base(paramCreate)
        {
        }

        private void Init()
        {
        }

        internal bool HasNoExpendPres(HIS_SERVICE_REQ serviceReq, ExpendPresSDO data, ref List<HIS_SERE_SERV> sereServs, ref List<HIS_SERE_SERV_EXT> sereServExts)
        {
            bool result = false;
            try
            {
                List<HIS_SERE_SERV_EXT> ssExts = new HisSereServExtGet().GetByServiceReqId(data.ServiceReqId);
                List<HIS_SERE_SERV> ss = new HisSereServGet().GetByServiceReqId(data.ServiceReqId);

                if (!IsNotNullOrEmpty(ss))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_KhongTonTaiDuLieuDichVu, serviceReq.SERVICE_REQ_CODE);
                    return false;
                }

                if (data.SereServId.HasValue)
                {
                    HIS_SERE_SERV sereServ = ss.Where(o => o.ID == data.SereServId.Value).FirstOrDefault();
                    if (sereServ == null)
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.DuLieuDauVaoKhongHopLe);
                        LogSystem.Warn("data.SereServId khong tuong ung voi data.ServiceReqId");
                        return false;
                    }

                    HIS_SERE_SERV_EXT ssExt = ssExts != null ? ssExts.Where(o => o.SERE_SERV_ID == data.SereServId.Value).FirstOrDefault() : null;
                    if (ssExt != null && ssExt.SUBCLINICAL_PRES_ID.HasValue)
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_DichVuDaKePhieuTieuHao, sereServ.TDL_SERVICE_NAME);
                        return false;
                    }

                    sereServs = sereServ != null ? new List<HIS_SERE_SERV>() { sereServ } : null;
                    sereServExts = ssExt != null ? new List<HIS_SERE_SERV_EXT>() { ssExt } : null;
                }
                else
                {
                    sereServExts = ssExts != null ? ssExts.Where(o => !o.SUBCLINICAL_PRES_ID.HasValue).ToList() : null;
                    sereServs = ss.Where(o => ssExts == null || !ssExts.Exists(t => t.SERE_SERV_ID == o.ID && t.SUBCLINICAL_PRES_ID.HasValue)).ToList();

                    if (!IsNotNullOrEmpty(sereServs))
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_DaKeDuPhieuTieuHao, serviceReq.SERVICE_REQ_CODE);
                        return false;
                    }
                }

                return true;

            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        internal bool HasExpendConfig(List<HIS_SERE_SERV> sereServs, ref List<HIS_SERVICE_MATY> serviceMaties, ref List<HIS_SERVICE_METY> serviceMeties)
        {
            bool result = false;
            try
            {
                List<long> serviceIds = IsNotNullOrEmpty(sereServs) ? sereServs.Select(o => o.SERVICE_ID).ToList() : null;
                serviceMaties = new HisServiceMatyGet().GetByServiceIds(serviceIds);
                serviceMeties = new HisServiceMetyGet().GetByServiceIds(serviceIds);

                if (!IsNotNullOrEmpty(serviceMeties) && !IsNotNullOrEmpty(serviceMaties))
                {
                    List<string> serviceNames = IsNotNullOrEmpty(sereServs) ? sereServs.Select(o => o.TDL_SERVICE_NAME).ToList() : null;
                    string serviceNameStr = IsNotNullOrEmpty(serviceNames) ? string.Join(",", serviceNames) : "";
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_CacDichVuChuaDuocThietLapDinhMucTieuHao, serviceNameStr);
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        internal bool IsValidMediStock(long requestRoomId, long mediStockId, ref V_HIS_MEDI_STOCK mediStock)
        {
            try
            {
                mediStock = HisMediStockCFG.DATA.Where(o => o.ID == mediStockId).FirstOrDefault();
                if (mediStock == null)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.DuLieuDauVaoKhongHopLe);
                    LogSystem.Warn("medi_stock_id ko ton tai");
                    return false;
                }
                if (mediStock.IS_ACTIVE != Constant.IS_TRUE)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisMediStock_KhoDangTamKhoa, mediStock.MEDI_STOCK_NAME);
                    return false;
                }

                //Kiem tra xem co kho khong nam trong d/s cac kho duoc cau hinh cho phep xuat tu phong dang lam viec hay khong
                if (HisMestRoomCFG.DATA == null || !HisMestRoomCFG.DATA.Exists(t => t.MEDI_STOCK_ID == mediStockId && t.IS_ACTIVE == Constant.IS_TRUE && t.ROOM_ID == requestRoomId))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_CacKhoSauKhongChoPhepXuatDenPhongDangLamViec, mediStock.MEDI_STOCK_NAME);
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return false;
            }
        }
    }
}
