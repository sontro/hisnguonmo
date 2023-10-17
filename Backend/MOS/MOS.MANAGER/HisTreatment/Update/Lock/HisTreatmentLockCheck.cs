using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisAccidentHurt;
using MOS.MANAGER.HisCare;
using MOS.MANAGER.HisDhst;
using MOS.MANAGER.HisInfusion;
using MOS.MANAGER.HisMediReact;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisServiceReq;
using MOS.MANAGER.HisTransaction;
using MOS.MANAGER.HisTreatmentBedRoom;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using MOS.MANAGER.HisTracking;
using MOS.MANAGER.HisDebate;
using MOS.SDO;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisPatientProgram;
using MOS.MANAGER.HisDepartmentTran;
using MOS.MANAGER.HisPatientTypeAlter;
using MOS.UTILITY;

namespace MOS.MANAGER.HisTreatment.Lock
{
    partial class HisTreatmentLockCheck : BusinessBase
    {
        internal HisTreatmentLockCheck()
            : base()
        {

        }

        internal HisTreatmentLockCheck(CommonParam paramCheck)
            : base(paramCheck)
        {

        }

        internal bool IsValidFeeLockTime(HIS_TREATMENT treatment, long feeLockTime)
        {
            bool valid = true;
            try
            {
                if (feeLockTime < treatment.OUT_TIME)
                {
                    string time = Inventec.Common.DateTime.Convert.TimeNumberToTimeString(treatment.OUT_TIME.Value);
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTreatment_ThoiGianDuyetKhoaVienPhiKhongDuocBeHonThoiGianKetThucDieuTri, time);
                    return false;
                }

                if (!HisTreatmentCFG.ALLOW_FEE_LOCK_TIME_GREATER_THAN_SYSTEM_TIME)
                {
                    long now = Inventec.Common.DateTime.Get.Now().Value;
                    if (feeLockTime > now)
                    {
                        string time = Inventec.Common.DateTime.Convert.TimeNumberToTimeString(now);
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTreatment_ThoiGianDuyetKhoaVienPhiKhongDuocLonHonThoiGianHienTai, time);
                        return false;
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

        internal bool CheckAllowLockBeforeApproveMediRecord(HIS_TREATMENT data)
        {
            bool valid = true;
            try
            {
                if (data.MEDI_RECORD_ID.HasValue)
                {
                    return true;
                }
                if (HisTreatmentCFG.APPROVE_MEDI_RECORD_BEFORE_LOCK_FEE_OPTION == HisTreatmentCFG.ApproveMediRecordBeforeLockFeeOption.ALLOW)
                {
                    return true;
                }
                if (HisTreatmentCFG.APPROVE_MEDI_RECORD_BEFORE_LOCK_FEE_OPTION == HisTreatmentCFG.ApproveMediRecordBeforeLockFeeOption.ALL)
                {
                    valid = false;
                }
                else if (data.TDL_TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__KHAM
                    && HisTreatmentCFG.APPROVE_MEDI_RECORD_BEFORE_LOCK_FEE_OPTION == HisTreatmentCFG.ApproveMediRecordBeforeLockFeeOption.EXAM)
                {
                    valid = false;
                }
                else if (data.TDL_TREATMENT_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__KHAM
                   && HisTreatmentCFG.APPROVE_MEDI_RECORD_BEFORE_LOCK_FEE_OPTION == HisTreatmentCFG.ApproveMediRecordBeforeLockFeeOption.NOT_EXAM)
                {
                    valid = false;
                }
                else if (data.TDL_TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNGOAITRU
                   && HisTreatmentCFG.APPROVE_MEDI_RECORD_BEFORE_LOCK_FEE_OPTION == HisTreatmentCFG.ApproveMediRecordBeforeLockFeeOption.OUT_TREAT)
                {
                    valid = false;
                }
                else if (data.TDL_TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTBANNGAY
                   && HisTreatmentCFG.APPROVE_MEDI_RECORD_BEFORE_LOCK_FEE_OPTION == HisTreatmentCFG.ApproveMediRecordBeforeLockFeeOption.DAY_TREAT)
                {
                    valid = false;
                }
                else if (data.TDL_TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNOITRU
                   && HisTreatmentCFG.APPROVE_MEDI_RECORD_BEFORE_LOCK_FEE_OPTION == HisTreatmentCFG.ApproveMediRecordBeforeLockFeeOption.IN_TREAT)
                {
                    valid = false;
                }
                if (!valid)
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTreatment_HoSoChuaDuocDuyetBenhAn);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        internal bool HasNoMissingInvoiceInfoMaterial(long treatmentId)
        {
            bool valid = true;
            try
            {
                if (HisTreatmentCFG.VERIFY_INVOICE_INFO_MATERIAL_BEFORE_LOCK_FEE)
                {
                    List<MissingInvoiceInfoMaterialSDO> sdos = new HisTreatmentGet().GetMissingInvoiceInfoMaterialByTreatmentId(treatmentId);
                    if (IsNotNullOrEmpty(sdos))
                    {
                        string maThuCap = MANAGER.Base.MessageUtil.GetMessage(LibraryMessage.Message.Enum.MaThuCap, param.LanguageCode);

                        List<string> materialTypeNames = sdos.Select(o => o.MATERIAL_TYPE_NAME).Distinct().ToList();
                        List<string> impMestCodes = sdos.Select(o => string.Format("{0} ({1}: {2})", o.IMP_MEST_CODE, maThuCap, o.IMP_MEST_SUB_CODE)).Distinct().ToList();
                        

                        string materialTypeNameStr = String.Join(", ", materialTypeNames);
                        string impMestCodeStr = String.Join(", ", impMestCodes);
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTreatment_VatTuThieuThongTinHoaDon, materialTypeNameStr, impMestCodeStr);
                        return false;
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

        internal bool HasNoUnpaid(HIS_TREATMENT treatment)
        {
            bool valid = true;
            try
            {
                if (HisTreatmentCFG.IS_DO_NOT_ALLOW_TO_LOCK_FEE_IF_MUST_PAY && treatment != null)
                {
                    var totalMustPay = new HisTreatmentGet().GetUnpaid(treatment.ID);
                    if (totalMustPay > 0)
                    {
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTreatment_TonTaiDichVuChuaDuocThanhToan);
                        return false;
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

        internal bool IsValidSereServAmountTemp(long treatmentId)
        {
            bool valid = true;
            try
            {
                HisSereServFilterQuery filter = new HisSereServFilterQuery();
                filter.TREATMENT_ID = treatmentId;
                filter.HAS_SERVICE_REQ_ID = true;
                filter.HAS_AMOUNT_TEMP__AND__IS_GREATER_ZERO = true;

                List<HIS_SERE_SERV> ss = new HisSereServGet().Get(filter);
                if (IsNotNullOrEmpty(ss))
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTreatment_DichVuChuaDuocChotSoLuong, string.Join(", ", ss.Select(o => o.TDL_SERVICE_NAME)));
                    return false;
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
