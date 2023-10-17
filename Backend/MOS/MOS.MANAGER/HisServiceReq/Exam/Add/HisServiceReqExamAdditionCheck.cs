using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisEmployee;
using MOS.MANAGER.HisServiceReq.AssignService;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Exam.Add
{
    class HisServiceReqExamAdditionCheck : BusinessBase
    {
        internal HisServiceReqExamAdditionCheck()
            : base()
        {

        }

        internal HisServiceReqExamAdditionCheck(CommonParam paramCheck)
            : base(paramCheck)
        {

        }

        internal bool IsFinishAllExamForAddExam(HIS_SERVICE_REQ currentServiceReq, long treatmentId)
        {
            bool valid = true;
            try
            {
                if (HisTreatmentCFG.MUST_FINISH_ALL_EXAM_FOR_ADD_EXAM)
                {
                    HisServiceReqFilterQuery filter = new HisServiceReqFilterQuery();
                    filter.TREATMENT_ID = treatmentId;
                    filter.SERVICE_REQ_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__KH;
                    filter.IS_NO_EXECUTE = false;
                    filter.ID__NOT_EQUAL = currentServiceReq.ID;
                    filter.SERVICE_REQ_STT_IDs = new List<long>()
                    {
                        IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__CXL,
                        IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__DXL
                    };
                    List<HIS_SERVICE_REQ> needFinishs = new HisServiceReqGet().Get(filter);
                    if (IsNotNullOrEmpty(needFinishs))
                    {
                        string codes = String.Join(",", needFinishs.Select(s => s.SERVICE_REQ_CODE).ToList());
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisServiceReq_CacYeuCauKhamChuaKetThucKhongChoPhepKhamThem, codes);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool IsValidData(HisServiceReqExamAdditionSDO data, WorkPlaceSDO workPlaceSDO, HIS_SERVICE_REQ currentServiceReq)
        {
            try
            {
                //Kiem tra du lieu hien tai co hop le khong
                if (currentServiceReq == null || currentServiceReq.SERVICE_REQ_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__KH)
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    LogSystem.Warn("serviceReq null hoac ko phai la 'Kham'" + LogUtil.TraceData("serviceReq", currentServiceReq));
                    return false;
                }

                //Chi cho phep nguoi dung tai phong xu ly cua dv hien tai duoc phep chi dinh dich vu kham them
                if (workPlaceSDO.RoomId != currentServiceReq.EXECUTE_ROOM_ID)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_KhongDuocChiDinhKhamThemDoiVoiDichVuDoPhongKhamKhacXuLy);
                    return false;
                }

                //Neu y lenh hien tai ko phai la "kham chinh" thi ko duoc chuyen kham chinh cho dich vu kham them
                if (data.IsPrimary && currentServiceReq.IS_MAIN_EXAM != Constant.IS_TRUE)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_DichVuDangXuLyKhongPhaiLaKhamChinh);
                    return false;
                }

                if (data.IsNotUseBhyt && (data.PatientTypeId == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT || data.PrimaryPatientTypeId == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_CheckKhongHuongBhytThiKhongDuocSuDungDoiTuongThanhToanLaBhyt);
                    return false;
                }

                if (data.AdditionServiceId.HasValue&&!data.PatientTypeId.HasValue)
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("AdditionServiceId is not null and PatientTypeId is null");
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
