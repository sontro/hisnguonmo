using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisServiceReq;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisSereServ.Update.AcceptNoExecute
{
    internal partial class HisSereServAcceptNoExecuteCheck : BusinessBase
    {
        public HisSereServAcceptNoExecuteCheck()
            : base()
        {
        }

        public HisSereServAcceptNoExecuteCheck(CommonParam param)
            : base(param)
        {
        }

        /// <summary>
        /// + Phòng chỉ định là phòng mà người dùng đang làm việc
        /// + Không phải là thuốc/vật tư/máu
        /// + Phòng chỉ định phải là phòng khám
        /// + Dịch vụ chưa bị check "không thực hiện" (his_sere_serv có IS_NO_EXECUTE khác 1)
        /// + Dịch vụ chưa được check "cho phép không thực hiện" (his_sere_serv có IS_ACCEPTING_NO_EXECUTE khác 1)
        /// </summary>
        public bool IsValidData(HisSereServAcceptNoExecuteSDO sdo, WorkPlaceSDO workPlaceSdo, ref HIS_SERE_SERV sereServ)
        {
            try
            {
                HIS_SERE_SERV s = new HisSereServGet().GetById(sdo.SereServId);
                V_HIS_ROOM requestRoom = HisRoomCFG.DATA.Where(o => o.ID == s.TDL_REQUEST_ROOM_ID).FirstOrDefault();

                if (workPlaceSdo.RoomId != s.TDL_REQUEST_ROOM_ID)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.BanDangKhongLamViecTaiPhong, requestRoom.ROOM_NAME);
                    return false;
                }


                if (requestRoom.IS_EXAM != Constant.IS_TRUE)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisSereServ_KhongPhaiLaPhongKham, requestRoom.ROOM_NAME);
                    return false;
                }

                if (s.TDL_SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__MAU
                    || s.TDL_SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__THUOC
                    || s.TDL_SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__VT)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.KhongChoPhepThucHienVoiThuocMauVatTu);
                    return false;
                }

                if (s.IS_NO_EXECUTE == Constant.IS_TRUE)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisSereServ_DichVuKhongThucHien, s.TDL_SERVICE_NAME);
                    return false;
                }

                sereServ = s;

                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
        }

        /// <summary>
        /// + Phòng chỉ định là phòng mà người dùng đang làm việc
        /// + Không phải là thuốc/vật tư/máu
        /// + Phòng chỉ định phải là phòng khám
        /// + Y lenh chưa bị check "không thực hiện" (his_service_req có IS_NO_EXECUTE khác 1)
        /// </summary>
        public bool IsValidData(HisServiceReqAcceptNoExecuteSDO sdo, WorkPlaceSDO workPlaceSdo, ref HIS_SERVICE_REQ serviceReq)
        {
            try
            {
                HIS_SERVICE_REQ s = new HisServiceReqGet().GetById(sdo.ServiceReqId);
                V_HIS_ROOM requestRoom = HisRoomCFG.DATA.Where(o => o.ID == s.REQUEST_ROOM_ID).FirstOrDefault();

                if (workPlaceSdo.RoomId != s.REQUEST_ROOM_ID)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.BanDangKhongLamViecTaiPhong, requestRoom.ROOM_NAME);
                    return false;
                }

                // Neu loai y lenh la don thuoc hoac vat tu thi check xem loai phong lam viec phai "Buong benh" ko
                if (s.SERVICE_REQ_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__DONK
                    || s.SERVICE_REQ_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__DONDT
                    || s.SERVICE_REQ_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__DONTT)
                {
                    if (requestRoom.ROOM_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_ROOM_TYPE.ID__BUONG)
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.KhongChoPhepThucHienTaiBuongBenh);
                        return false;
                    }
                }
                else
                {
                    if (requestRoom.IS_EXAM != Constant.IS_TRUE)
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisSereServ_KhongPhaiLaPhongKham, requestRoom.ROOM_NAME);
                        return false;
                    }

                    if (s.SERVICE_REQ_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__DONM)
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.KhongChoPhepThucHienVoiThuocMauVatTu);
                        return false;
                    }
                }

                if (s.IS_NO_EXECUTE == Constant.IS_TRUE)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_YeuCauKhongThucHien, s.SERVICE_REQ_CODE);
                    return false;
                }

                serviceReq = s;

                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
        }

        public bool IsNotAccepted(HIS_SERE_SERV sereServ)
        {
            try
            {
                if (sereServ != null && sereServ.IS_ACCEPTING_NO_EXECUTE == Constant.IS_TRUE)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisSereServ_DichVuDaDuocXacNhanChoPhepKhongThucHien, sereServ.TDL_SERVICE_NAME);
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
        }

        public bool IsAccepted(HIS_SERE_SERV sereServ)
        {
            try
            {
                if (sereServ != null && sereServ.IS_ACCEPTING_NO_EXECUTE != Constant.IS_TRUE)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisSereServ_DichVuChuaDuocXacNhanChoPhepKhongThucHien, sereServ.TDL_SERVICE_NAME);
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
        }

        public bool IsNotConfirmNoExcute(HIS_SERVICE_REQ serviceReq, HIS_SERE_SERV sereServ)
        {
            try
            {
                if (serviceReq != null 
                    && serviceReq.SERVICE_REQ_STT_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__HT 
                    && sereServ != null && sereServ.IS_CONFIRM_NO_EXCUTE != Constant.IS_TRUE)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_ChiChoPhepThucHienKhiChuaBatDau);
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
        }
    }
}
