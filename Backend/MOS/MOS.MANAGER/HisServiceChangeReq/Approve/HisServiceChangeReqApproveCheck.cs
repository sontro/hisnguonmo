using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisEmployee;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisServiceReq;
using MOS.MANAGER.HisServiceReq.AssignService;
using MOS.MANAGER.HisTreatment;
using MOS.SDO;
using MOS.ServicePaty;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceChangeReq.Approve
{
    class HisServiceChangeReqApproveCheck : BusinessBase
    {
        internal HisServiceChangeReqApproveCheck()
            : base()
        {

        }

        internal HisServiceChangeReqApproveCheck(CommonParam paramCheck)
            : base(paramCheck)
        {

        }

        internal bool IsValidData(HisServiceChangeReqApproveSDO data, ref HIS_SERVICE_CHANGE_REQ serviceChangeReq, ref HIS_SERVICE_REQ serviceReq, ref HIS_SERE_SERV sereServ)
        {
            try
            {
                HIS_SERVICE_CHANGE_REQ scr = new HisServiceChangeReqGet().GetById(data.ServiceChangeReqId);

                if (scr == null)
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    LogSystem.Warn("data.ServiceChangeReqId ko hop le");
                    return false;
                }

                if (scr.APPROVAL_LOGINNAME != null || scr.APPROVAL_CASHIER_LOGINNAME != null)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceChangeReq_YeuCauDoiDichVuDaDuocDuyet);
                    return false;
                }

                HIS_SERE_SERV ss = new HisSereServGet().GetById(scr.SERE_SERV_ID);
                HIS_SERVICE_REQ sr = new HisServiceReqGet().GetById(ss.SERVICE_REQ_ID.Value);

                List<V_HIS_SERVICE_ROOM> serviceRooms = HisServiceRoomCFG.DATA_VIEW.Where(o => o.SERVICE_ID == scr.ALTER_SERVICE_ID && o.ROOM_ID == sr.EXECUTE_ROOM_ID && o.IS_ACTIVE == Constant.IS_TRUE).ToList();

                //Neu khong co thiet lap dich vu - phong tuong ung voi dich vu do
                if (!IsNotNullOrEmpty(serviceRooms))
                {
                    List<string> serviceNames = HisServiceCFG.DATA_VIEW.Where(o => o.ID == scr.ALTER_SERVICE_ID).Select(o => o.SERVICE_NAME).ToList();
                    string serviceNameStr = string.Join(",", serviceNames);

                    string roomName = HisExecuteRoomCFG.DATA.Where(o => o.ROOM_ID == sr.EXECUTE_ROOM_ID).Select(o => o.EXECUTE_ROOM_NAME).FirstOrDefault();

                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_DichVuSauKhongTheXuLyTaiPhong, serviceNameStr, roomName);

                    return false;
                }

                V_HIS_EXECUTE_ROOM executeRoom = HisExecuteRoomCFG.DATA.Where(o => o.ROOM_ID == sr.EXECUTE_ROOM_ID).FirstOrDefault();
                HIS_TREATMENT treatment = new HisTreatmentGet().GetById(sr.TREATMENT_ID);


                //Lay thong tin chinh sach gia
                //Tam thoi ko can lay chinh sach gia theo luot chi dinh va dieu kien dich vu (do chi ko ap dung voi BN BHYT)
                V_HIS_SERVICE_PATY appliedServicePaty = ServicePatyUtil.GetApplied(HisServicePatyCFG.DATA, executeRoom.BRANCH_ID, sr.EXECUTE_ROOM_ID, sr.REQUEST_ROOM_ID, sr.REQUEST_DEPARTMENT_ID, sr.INTRUCTION_TIME, treatment.IN_TIME, scr.ALTER_SERVICE_ID, scr.PATIENT_TYPE_ID, null, null, ss.PACKAGE_ID, null, treatment.TDL_PATIENT_CLASSIFY_ID, ss.TDL_RATION_TIME_ID);

                if (appliedServicePaty == null)
                {
                    HIS_PATIENT_TYPE hisPatientType = HisPatientTypeCFG.DATA.Where(o => o.ID == scr.PATIENT_TYPE_ID).FirstOrDefault();
                    V_HIS_SERVICE hisService = HisServiceCFG.DATA_VIEW.Where(o => o.ID == scr.ALTER_SERVICE_ID).FirstOrDefault();
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServicePaty_KhongTonTaiDuLieuPhuHop, hisService.SERVICE_NAME, hisService.SERVICE_CODE, hisPatientType.PATIENT_TYPE_NAME);
                    return false;
                }

                if (scr.PRIMARY_PATIENT_TYPE_ID.HasValue)
                {
                    //Lay thong tin chinh sach gia
                    //Tam thoi ko can lay chinh sach gia theo luot chi dinh va dieu kien dich vu (do chi ko ap dung voi BN BHYT)
                    V_HIS_SERVICE_PATY appliedPrimaryServicePaty = ServicePatyUtil.GetApplied(HisServicePatyCFG.DATA, executeRoom.BRANCH_ID, sr.EXECUTE_ROOM_ID, sr.REQUEST_ROOM_ID, sr.REQUEST_DEPARTMENT_ID, sr.INTRUCTION_TIME, treatment.IN_TIME, scr.ALTER_SERVICE_ID, scr.PRIMARY_PATIENT_TYPE_ID.Value, null, null, ss.PACKAGE_ID, null, treatment.TDL_PATIENT_CLASSIFY_ID, ss.TDL_RATION_TIME_ID);

                    if (appliedServicePaty == null)
                    {
                        HIS_PATIENT_TYPE hisPatientType = HisPatientTypeCFG.DATA.Where(o => o.ID == scr.PRIMARY_PATIENT_TYPE_ID.Value).FirstOrDefault();
                        V_HIS_SERVICE hisService = HisServiceCFG.DATA_VIEW.Where(o => o.ID == scr.ALTER_SERVICE_ID).FirstOrDefault();
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServicePaty_KhongTonTaiDuLieuPhuHop, hisService.SERVICE_NAME, hisService.SERVICE_CODE, hisPatientType.PATIENT_TYPE_NAME);
                        return false;
                    }
                }

                serviceReq = sr;
                sereServ = ss;
                serviceChangeReq = scr;

                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
        }

        internal bool IsValidWorkingRoom(HIS_SERVICE_REQ serviceReq, WorkPlaceSDO workPlaceSdo)
        {
            try
            {
                if (workPlaceSdo.RoomId != serviceReq.REQUEST_ROOM_ID && workPlaceSdo.RoomId != serviceReq.EXECUTE_ROOM_ID)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceChangeReq_KhongLamViecTaiPhongChiDinhVaPhongXuLy);
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
