using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisCoTreatment;
using MOS.MANAGER.HisTreatment;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisDepartmentTran.Receive
{
    class HisDepartmentTranReceiveCheck : BusinessBase
    {
        internal HisDepartmentTranReceiveCheck()
            : base()
        {
        }

        internal HisDepartmentTranReceiveCheck(CommonParam param)
            : base(param)
        {
        }

        internal bool IsAllow(HisDepartmentTranReceiveSDO data, ref HIS_DEPARTMENT_TRAN departmentTran)
        {
            try
            {
                HIS_DEPARTMENT_TRAN dt = new HisDepartmentTranGet().GetById(data.DepartmentTranId);
                HIS_DEPARTMENT department = HisDepartmentCFG.DATA != null ? HisDepartmentCFG.DATA.Where(o => o.ID == data.DepartmentId).FirstOrDefault() : null;
                if (dt.DEPARTMENT_IN_TIME.HasValue)
                {
                    string departmentName = department != null ? department.DEPARTMENT_NAME : "";
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisDepartmentTran_BenhNhanDangThuocKhoa, departmentName);
                    return false;
                }
                if (department.ALLOW_TREATMENT_TYPE_IDS == null || !("," + department.ALLOW_TREATMENT_TYPE_IDS + ",").Contains("," + data.TreatmentTypeId + ","))
                {
                    HIS_TREATMENT_TYPE treatmentType = HisTreatmentTypeCFG.DATA.Where(o => o.ID == data.TreatmentTypeId).FirstOrDefault();
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisDepartmentTran_KhoaKhongChoPhepDienDieuTri, department.DEPARTMENT_NAME, treatmentType.TREATMENT_TYPE_NAME);
                    return false;
                }

                if (dt.PREVIOUS_ID.HasValue)
                {
                    HIS_DEPARTMENT_TRAN previous = new HisDepartmentTranGet().GetById(dt.PREVIOUS_ID.Value);
                    if (previous != null && previous.DEPARTMENT_IN_TIME.HasValue && data.InTime < previous.DEPARTMENT_IN_TIME)
                    {
                        string previousInTime = Inventec.Common.DateTime.Convert.TimeNumberToTimeString(previous.DEPARTMENT_IN_TIME.Value);
                        HIS_DEPARTMENT previousDepartment = HisDepartmentCFG.DATA.Where(o => o.ID == previous.DEPARTMENT_ID).FirstOrDefault();
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisDepartmentTran_ThoiGianKhongDuocNhoThoiGianVaoKhoaTruoc, previousDepartment.DEPARTMENT_NAME, previousInTime);
                        return false;
                    }

                    List<HIS_CO_TREATMENT> hisCoTreatments = new HisCoTreatmentGet().GetByDepartmentTranId(previous.ID);
                    HIS_CO_TREATMENT coTreat = hisCoTreatments != null ? hisCoTreatments.OrderByDescending(o => o.FINISH_TIME).FirstOrDefault() : null;
                    if (coTreat != null && coTreat.FINISH_TIME.HasValue && coTreat.FINISH_TIME.Value > data.InTime)
                    {
                        string finishTime = Inventec.Common.DateTime.Convert.TimeNumberToTimeString(coTreat.FINISH_TIME.Value);
                        HIS_DEPARTMENT previousDepartment = HisDepartmentCFG.DATA.Where(o => o.ID == coTreat.DEPARTMENT_ID).FirstOrDefault();
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisDepartmentTran_ThoiGianVaoKhoaNhoHonThoiGianKetThucDieuTriKeHopCuaKhoaTruoc, previousDepartment.DEPARTMENT_NAME, finishTime);
                        return false;
                    }
                }

                departmentTran = dt;
                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
        }

        internal bool IsValidData(HisDepartmentTranReceiveSDO data, HIS_DEPARTMENT_TRAN departmentTran, HIS_TREATMENT treatment)
        {
            try
            {
                if (HisSereServCFG.IS_USING_BED_TEMP && data.BedId.HasValue && (!data.BedServiceId.HasValue || !data.PatientTypeId.HasValue))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisDepartmentTran_BatBuocNhapDichVuGiuongVaDoiTuongThanhToan);
                    return false;
                }

                if (departmentTran.IS_HOSPITALIZED == Constant.IS_TRUE && HisTreatmentCFG.IS_MANUAL_IN_CODE)
                {
                    if (string.IsNullOrWhiteSpace(data.InCode))
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTreatment_ChuaNhapSoVaoVien);
                        return false;
                    }

                    if (!new HisTreatmentCheck(param).IsNotExistsInCode(data.InCode, departmentTran.TREATMENT_ID))
                    {
                        return false;
                    }
                }

                if ((treatment.TDL_TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNOITRU || data.TreatmentTypeId == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNOITRU)
                    && HisDepartmentTranCFG.REQUIRED_BED_WHEN_RECEIVE_PATIENT_OPTION == HisDepartmentTranCFG.RequiredBedWhenReceivePatientOption.IN_PATIENT
                    && !data.BedId.HasValue)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisDepartmentTran_ChuaChonGiuongBenh);
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

        internal bool IsAllowIcdTreatment(HisDepartmentTranReceiveSDO data, HIS_TREATMENT treatment)
        {
            bool valid = true;
            try
            {
                List<long> treatmentTypes = new List<long>()
                {
                    IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNOITRU,
                    IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNGOAITRU
                };
                if (IsNotNullOrEmpty(treatmentTypes) && IsNotNull(data))
                {
                    if ((treatment.TDL_TREATMENT_TYPE_ID.HasValue && treatmentTypes.Contains(treatment.TDL_TREATMENT_TYPE_ID.Value))
                        || (data.TreatmentTypeId.HasValue && treatmentTypes.Contains(data.TreatmentTypeId.Value)))
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
                                valid =  false;
                            }
                        }
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

        internal bool IsValidInTime(long inTime, bool isModify)
        {
            
            bool valid = true;
            try
            {
                if (HisDepartmentTranCFG.IS_DEPARTMENT_IN_TIME_NOT_GREATER_THAN_CURRENT_TIME_CFG && inTime > Inventec.Common.DateTime.Get.Now())
                {
                    if (isModify)
                    {
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisDepartmentTran_KhongChoPhepSuaThoiGianVaoKhoaLonHonThoiGianHienTai);
                    }
                    else
                    {
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisDepartmentTran_KhongChoPhepTiepNhanBenhNhanCoThoiGianLonHonThoiGianHienTai);
                    }
                    valid = false;
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
