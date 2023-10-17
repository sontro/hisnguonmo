using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisPatientTypeAlter;
using MOS.MANAGER.HisServiceReq;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisDepartmentTran.Hospitalize
{
    public class HisDepartmentTranHospitalizeCheck : BusinessBase
    {
        internal HisDepartmentTranHospitalizeCheck()
            : base()
        {
        }

        internal HisDepartmentTranHospitalizeCheck(CommonParam param)
            : base(param)
        {
        }

        internal bool IsAllow(HisDepartmentTranHospitalizeSDO sdo, ref List<HIS_DEPARTMENT_TRAN> allDepartmentTrans, ref HIS_DEPARTMENT_TRAN lastDepartmentTran, ref HIS_PATIENT_TYPE_ALTER lastPta, WorkPlaceSDO workPlace)
        {
            try
            {
                //Kiem tra xem khoa tiep nhan co duoc phep dieu tri voi dien dieu tri nay ko
                HIS_DEPARTMENT receiveDepartment = HisDepartmentCFG.DATA.Where(o => o.ID == sdo.DepartmentId).FirstOrDefault();

                if (receiveDepartment.ALLOW_TREATMENT_TYPE_IDS == null || !("," + receiveDepartment.ALLOW_TREATMENT_TYPE_IDS + ",").Contains("," + sdo.TreatmentTypeId + ","))
                {
                    HIS_TREATMENT_TYPE treatmentType = HisTreatmentTypeCFG.DATA.Where(o => o.ID == sdo.TreatmentTypeId).FirstOrDefault();
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisDepartmentTran_KhoaKhongChoPhepDienDieuTri, receiveDepartment.DEPARTMENT_NAME, treatmentType.TREATMENT_TYPE_NAME);
                    return false;
                }

                //Kiem tra khoa hien tai cua BN
                List<HIS_DEPARTMENT_TRAN> lsDt = new HisDepartmentTranGet().GetByTreatmentId(sdo.TreatmentId);
                HIS_DEPARTMENT_TRAN departmentTran = null;
                if (lsDt != null)
                {
                    if (lsDt.Exists(e => !e.DEPARTMENT_IN_TIME.HasValue))
                    {
                        departmentTran = lsDt.Where(o => !o.DEPARTMENT_IN_TIME.HasValue).FirstOrDefault();
                    }
                    else
                    {
                        departmentTran = lsDt
                            .OrderByDescending(o => o.DEPARTMENT_IN_TIME)
                            .ThenByDescending(t => t.ID)
                            .FirstOrDefault();
                    }
                }
                var pta = new HisPatientTypeAlterGet().GetLastByTreatmentId(sdo.TreatmentId);

                if (departmentTran == null || pta == null)
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.LoiDuLieu);
                    throw new Exception("Khong tim thay thong tin department_tran hoac patient_type_alter cua ho so dieu tri");
                }

                if (pta.TREATMENT_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__KHAM)
                {
                    HIS_TREATMENT_TYPE t = HisTreatmentTypeCFG.DATA.Where(o => o.ID == pta.TREATMENT_TYPE_ID).FirstOrDefault();
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisDepartmentTran_HoSoDangODienDieuTri, t.TREATMENT_TYPE_NAME);
                    return false;
                }

                HIS_DEPARTMENT department = HisDepartmentCFG.DATA.Where(o => o.ID == departmentTran.DEPARTMENT_ID).FirstOrDefault();
                if (!departmentTran.DEPARTMENT_IN_TIME.HasValue)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisDepartmentTran_BenhNhanDangChoTiepNhanVaoKhoa, department.DEPARTMENT_NAME);
                    return false;
                }

                if (sdo.BedRoomId.HasValue)
                {
                    V_HIS_BED_ROOM bedRoom = HisBedRoomCFG.DATA.Where(o => o.ID == sdo.BedRoomId).FirstOrDefault();
                    if (bedRoom == null || bedRoom.DEPARTMENT_ID != workPlace.DepartmentId)
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisDepartmentTran_BuongBenhKhongThuocKhoaDangLamViec);
                        return false;
                    }
                }

                if (workPlace == null || workPlace.DepartmentId != departmentTran.DEPARTMENT_ID)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisDepartmentTran_BenhNhanDangThuocKhoa, department.DEPARTMENT_NAME);
                    return false;
                }

                if (sdo.Time < departmentTran.DEPARTMENT_IN_TIME.Value)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisDepartmentTran_ThoiGianKhongDuocNhoThoiGianVaoKhoaTruoc, department.DEPARTMENT_NAME);
                    return false;
                }

                lastDepartmentTran = departmentTran;
                lastPta = pta;
                allDepartmentTrans = lsDt;

                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
        }

        internal bool IsFinishAllExamForHospitalize(long treatmentId, long? serviceReqId)
        {
            bool valid = true;
            try
            {
                if (HisTreatmentCFG.MUST_FINISH_ALL_EXAM_FOR_HOSPITALIZE)
                {
                    HisServiceReqFilterQuery filter = new HisServiceReqFilterQuery();
                    filter.TREATMENT_ID = treatmentId;
                    filter.SERVICE_REQ_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__KH;
                    filter.IS_NO_EXECUTE = false;
                    filter.ID__NOT_EQUAL = serviceReqId;
                    filter.SERVICE_REQ_STT_IDs = new List<long>()
                    {
                        IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__CXL,
                        IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__DXL
                    };

                    List<HIS_SERVICE_REQ> needFinishs = new HisServiceReqGet().Get(filter);

                    if (IsNotNullOrEmpty(needFinishs))
                    {
                        string codes = String.Join(",", needFinishs.Select(s => s.SERVICE_REQ_CODE).ToList());
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisServiceReq_CacYeuCauKhamChuaKetThucKhongChoPhepNhapVien, codes);
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

        internal bool IsAllowIcdTreatment(HisDepartmentTranHospitalizeSDO data)
        {
            bool valid = true;
            try
            {
                var icdCodes = new List<string>()
                        {
                            data.IcdCode,
                            data.TraditionalIcdCode,
                            data.TraditionalIcdSubCode,
                            data.IcdSubCode
                        };
                var icds = IsNotNullOrEmpty(HisIcdCFG.DATA_BY_UNABLE_FOR_TREATMENT) ? HisIcdCFG.DATA_BY_UNABLE_FOR_TREATMENT.Where(o => icdCodes.Contains(o.ICD_CODE)).ToList() : null;
                if (IsNotNullOrEmpty(icds))
                {
                    var codes = icds.Select(o => o.ICD_CODE).ToList();
                    string strCode = IsNotNullOrEmpty(codes) ? string.Join(",", codes) : "";
                    if (!string.IsNullOrWhiteSpace(strCode))
                    {
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisDepartmentTran_MaBenhKhongChoPhepDieuTriTaiVien, strCode);
                        valid = false;
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }
    }
}
