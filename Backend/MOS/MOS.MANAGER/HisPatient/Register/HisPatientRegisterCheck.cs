using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using Inventec.Fss.Client;
using Inventec.Fss.Utility;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.LibraryHein.Bhyt;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisCard;
using MOS.MANAGER.HisDepartmentTran;
using MOS.MANAGER.HisPatient;
using MOS.MANAGER.HisPatientTypeAlter;
using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.HisTreatment.Update;
using MOS.MANAGER.Token;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MOS.MANAGER.HisPatient.Register
{
    /// <summary>
    /// Xu ly nghiep vu dang ky dich vu -> tao cac thong tin ho so benh nhan
    /// </summary>
    class HisPatientRegisterCheck : BusinessBase
    {
        internal HisPatientRegisterCheck()
            : base()
        {
        }

        internal HisPatientRegisterCheck(CommonParam paramCreate)
            : base(paramCreate)
        {
        }

        internal bool IsValidInCode(HisPatientProfileSDO data)
        {
            bool valid = true;
            try
            {
                //Neu cau hinh "nhap so vao vien" va dien dieu tri la "dieu tri noi tru" hoac "dieu tri ngoai tru" thi kiem tra thong tin "so vao vien"
                if (HisTreatmentCFG.IS_MANUAL_IN_CODE && data != null && data.HisPatientTypeAlter != null
                    && (data.HisPatientTypeAlter.TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNGOAITRU
                    || data.HisPatientTypeAlter.TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNOITRU))
                {
                    if (string.IsNullOrWhiteSpace(data.HisTreatment.IN_CODE))
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTreatment_ChuaNhapSoVaoVien);
                        return false;
                    }
                    if (!new HisTreatmentCheck(param).IsNotExistsInCode(data.HisTreatment.IN_CODE))
                    {
                        return false;
                    }
                }
                else if (!string.IsNullOrWhiteSpace(data.HisTreatment.IN_CODE))
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    LogSystem.Warn("Khong cho phep nhap so vao vien");
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

        internal bool IsValidCardInfo(HisPatientProfileSDO data, ref HIS_CARD hisCard)
        {
            if (IsNotNullOrEmpty(data.CardCode))
            {
                hisCard = new HisCardGet().GetByCardCode(data.CardCode);
                if (hisCard == null)
                {
                    //Tam thoi comment doan nay de phuc vu dang ky qua tong dai (do khi nguoi dung su dung the tren DKK se ko co nghiep vu tao HIS_CARD truoc khi dang ky tiep don)
                    //Ban sau se bo dung nghiep vu truy van sang he thong COS de lay thong tin the --> check the co hop le ko
                    //MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisPatient_SoTheKhongHopLe);
                    //return false;
                }
                else if (hisCard.PATIENT_ID.HasValue && hisCard.PATIENT_ID.Value != data.HisPatient.ID)
                {
                    HIS_PATIENT patient = new HisPatientGet().GetById(hisCard.PATIENT_ID.Value);
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisPatient_TheDaDuocSuDungBoiBenhNhanKhac, patient.VIR_PATIENT_NAME, patient.PATIENT_CODE);
                    return false;
                }
            }
            return true;
        }
    }
}
