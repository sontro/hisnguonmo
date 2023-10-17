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

namespace MOS.MANAGER.HisServiceChangeReq.Create
{
    class HisServiceChangeReqCreateSdoCheck : BusinessBase
    {
        internal HisServiceChangeReqCreateSdoCheck()
            : base()
        {

        }

        internal HisServiceChangeReqCreateSdoCheck(CommonParam paramCheck)
            : base(paramCheck)
        {

        }

        internal bool IsValidData(HisServiceChangeReqSDO data, ref HIS_SERVICE_REQ serviceReq, ref HIS_SERE_SERV sereServ, ref V_HIS_SERVICE service)
        {
            try
            {
                HIS_SERE_SERV ss = new HisSereServGet().GetById(data.SereServId);
                HIS_SERVICE_REQ sr = new HisServiceReqGet().GetById(ss.SERVICE_REQ_ID.Value);
                V_HIS_SERVICE s = HisServiceCFG.DATA_VIEW.Where(o => o.ID == data.AlterServiceId).FirstOrDefault();

                List<V_HIS_SERVICE_ROOM> serviceRooms = HisServiceRoomCFG.DATA_VIEW.Where(o => o.SERVICE_ID == data.AlterServiceId && o.ROOM_ID == sr.EXECUTE_ROOM_ID && o.IS_ACTIVE == Constant.IS_TRUE).ToList();

                //Neu khong co thiet lap dich vu - phong tuong ung voi dich vu do
                if (!IsNotNullOrEmpty(serviceRooms))
                {
                    string roomName = HisExecuteRoomCFG.DATA.Where(o => o.ROOM_ID == sr.EXECUTE_ROOM_ID).Select(o => o.EXECUTE_ROOM_NAME).FirstOrDefault();

                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_DichVuSauKhongTheXuLyTaiPhong, s.SERVICE_NAME, roomName);

                    return false;
                }

                V_HIS_EXECUTE_ROOM executeRoom = HisExecuteRoomCFG.DATA.Where(o => o.ROOM_ID == sr.EXECUTE_ROOM_ID).FirstOrDefault();
                HIS_TREATMENT treatment = new HisTreatmentGet().GetById(sr.TREATMENT_ID);
                
                
                //Lay thong tin chinh sach gia
                //Tam thoi ko can lay chinh sach gia theo luot chi dinh va dieu kien dich vu (do chi ko ap dung voi BN BHYT)
                V_HIS_SERVICE_PATY appliedServicePaty = ServicePatyUtil.GetApplied(HisServicePatyCFG.DATA, executeRoom.BRANCH_ID, sr.EXECUTE_ROOM_ID, sr.REQUEST_ROOM_ID, sr.REQUEST_DEPARTMENT_ID, sr.INTRUCTION_TIME, treatment.IN_TIME, data.AlterServiceId, data.PatientTypeId, null, null, ss.PACKAGE_ID, null, treatment.TDL_PATIENT_CLASSIFY_ID, ss.TDL_RATION_TIME_ID);

                if (appliedServicePaty == null)
                {
                    HIS_PATIENT_TYPE hisPatientType = HisPatientTypeCFG.DATA.Where(o => o.ID == data.PatientTypeId).FirstOrDefault();
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServicePaty_KhongTonTaiDuLieuPhuHop, s.SERVICE_NAME, s.SERVICE_CODE, hisPatientType.PATIENT_TYPE_NAME);
                    return false;
                }

                if (data.PrimaryPatientTypeId.HasValue)
                {
                    //Lay thong tin chinh sach gia
                    //Tam thoi ko can lay chinh sach gia theo luot chi dinh va dieu kien dich vu (do chi ko ap dung voi BN BHYT)
                    V_HIS_SERVICE_PATY appliedPrimaryServicePaty = ServicePatyUtil.GetApplied(HisServicePatyCFG.DATA, executeRoom.BRANCH_ID, sr.EXECUTE_ROOM_ID, sr.REQUEST_ROOM_ID, sr.REQUEST_DEPARTMENT_ID, sr.INTRUCTION_TIME, treatment.IN_TIME, data.AlterServiceId, data.PrimaryPatientTypeId.Value, null, null, ss.PACKAGE_ID, null, treatment.TDL_PATIENT_CLASSIFY_ID, ss.TDL_RATION_TIME_ID);

                    if (appliedServicePaty == null)
                    {
                        HIS_PATIENT_TYPE hisPatientType = HisPatientTypeCFG.DATA.Where(o => o.ID == data.PrimaryPatientTypeId.Value).FirstOrDefault();
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServicePaty_KhongTonTaiDuLieuPhuHop, s.SERVICE_NAME, s.SERVICE_CODE, hisPatientType.PATIENT_TYPE_NAME);
                        return false;
                    }
                }

                serviceReq = sr;
                sereServ = ss;
                service = s;

                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
        }

        internal bool HasNoApprovedChangeReq(HIS_SERE_SERV sereServ, ref List<HIS_SERVICE_CHANGE_REQ> serviceChangeReqs)
        {
            try
            {
                HisServiceChangeReqFilterQuery filter = new HisServiceChangeReqFilterQuery();
                filter.TDL_SERVICE_REQ_ID = sereServ.SERVICE_REQ_ID;
                filter.SERE_SERV_ID = sereServ.ID;

                List<HIS_SERVICE_CHANGE_REQ> changeReqs = new HisServiceChangeReqGet().Get(filter);
                List<HIS_SERVICE_CHANGE_REQ> approveds = changeReqs != null ? changeReqs.Where(o => o.APPROVAL_LOGINNAME != null).ToList() : null;

                if (IsNotNullOrEmpty(approveds))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceChangeReq_TonTaiYeuCauDoiDaDuocDuyet, sereServ.TDL_SERVICE_NAME);
                    return false;
                }
                serviceChangeReqs = changeReqs;

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
