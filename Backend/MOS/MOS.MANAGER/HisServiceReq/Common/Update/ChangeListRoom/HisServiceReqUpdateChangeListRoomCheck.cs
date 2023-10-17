using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisServiceRoom;
using MOS.MANAGER.HisTreatment;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisServiceReq.Update.ChangeListRoom
{
    partial class HisServiceReqUpdateChangeListRoomCheck : BusinessBase
    {
        internal HisServiceReqUpdateChangeListRoomCheck()
            : base()
        {

        }

        internal HisServiceReqUpdateChangeListRoomCheck(CommonParam paramCheck)
            : base(paramCheck)
        {

        }

        /// <summary>
        /// Kiem tra xem danh sach dich vu co the duoc xu ly tai phong hay khong
        /// </summary>
        /// <param name="roomId"></param>
        /// <param name="sereServs"></param>
        /// <returns></returns>
        internal bool IsProcessable(ChangeRoomSDO data, ref List<HIS_SERE_SERV> sereServs)
        {
            bool valid = true;
            try
            {
                sereServs = new HisSereServGet().GetByServiceReqIds(data.ServiceReqIds);

                return new HisServiceReqCheck(param).IsProcessable(data.ExecuteRoomId, sereServs);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool IsAllow(WorkPlaceSDO workPlace, ChangeRoomSDO data, ref List<HIS_SERVICE_REQ> serviceReqs, ref List<HIS_TREATMENT> treatments)
        {
            try
            {
                serviceReqs = new HisServiceReqGet().GetByIds(data.ServiceReqIds);

                if (serviceReqs != null)
                {
                    List<string> invalidRooms = serviceReqs.Where(o => o.REQUEST_ROOM_ID != workPlace.RoomId && o.EXECUTE_ROOM_ID != workPlace.RoomId).Select(o => o.SERVICE_REQ_CODE).ToList();

                    if (IsNotNullOrEmpty(invalidRooms))
                    {
                        string codeStr = string.Join(",", invalidRooms);
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_KhongThuocPhongXuLyHoacChiDinh, codeStr);
                        return false;
                    }

                    List<string> invalidStatuss = serviceReqs.Where(o => o.SERVICE_REQ_STT_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__HT).Select(o => o.SERVICE_REQ_CODE).ToList();

                    if (IsNotNullOrEmpty(invalidStatuss))
                    {
                        string codeStr = string.Join(",", invalidStatuss);
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_KhongChoPhepChuyenPhongVoiYeuCauDaHoanThanh, codeStr);
                        return false;
                    }

                    List<long> treatmentIds = serviceReqs.Select(o => o.TREATMENT_ID).ToList();
                    List<HIS_TREATMENT> hisTreatments = new HisTreatmentGet().GetByIds(treatmentIds);

                    List<string> invalidTreatments = serviceReqs.Where(o => hisTreatments.Exists(t => t.ID == o.TREATMENT_ID && t.IS_PAUSE == Constant.IS_TRUE)).Select(o => o.SERVICE_REQ_CODE).ToList();
                    if (IsNotNullOrEmpty(invalidTreatments))
                    {
                        string codeStr = string.Join(",", invalidTreatments);
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_CacChiDinhCoHoSoDaKetThuc, codeStr);
                        return false;
                    }

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
