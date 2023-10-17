using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryHein.Bhyt.HeinRightRouteType;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisIcd;
using MOS.MANAGER.HisPatient;
using MOS.MANAGER.HisPatientTypeAlter;
using MOS.MANAGER.HisServiceReq;
using MOS.MANAGER.HisTransaction;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisTreatment
{
    partial class HisTreatmentUpdate : BusinessBase
    {
        internal bool UpdateTranPatiInfo(HisTreatmentTranPatiSDO data, ref HIS_TREATMENT resultData)
        {
            bool result = false;
            try
            {
                HIS_TREATMENT raw = null;

                bool valid = true;
                HisTreatmentCheck checker = new HisTreatmentCheck(param);
                valid = valid && IsNotNull(data);
                valid = valid && IsNotNull(data.HisTreatment);
                valid = valid && checker.VerifyId(data.HisTreatment.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.IsUnTemporaryLock(raw);
                if (valid)
                {
                    Mapper.CreateMap<HIS_TREATMENT, HIS_TREATMENT>();
                    this.beforeUpdateHisTreatments.Add(Mapper.Map<HIS_TREATMENT>(raw));
                    this.CheckTranInfo(data, raw, checker);
                    if (!DAOWorker.HisTreatmentDAO.Update(raw))
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.HisTreatment_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin chuyen vien that bai. ket thuc nghiep vu");
                    }
                    result = true;
                    resultData = raw;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        internal void CheckTranInfo(HisTreatmentTranPatiSDO data, HIS_TREATMENT raw, HisTreatmentCheck checker)
        {
            if (data.IsTranIn.HasValue && data.IsTranIn.Value)
            {
                if (!checker.IsUnpause(raw))
                {
                    throw new Exception("Ket thuc nghiep vu");
                }

                if (string.IsNullOrWhiteSpace(data.HisTreatment.TRANSFER_IN_MEDI_ORG_CODE) || string.IsNullOrWhiteSpace(data.HisTreatment.TRANSFER_IN_MEDI_ORG_NAME))
                {
                    HisPatientTypeAlterFilterQuery filter = new HisPatientTypeAlterFilterQuery();
                    filter.PATIENT_TYPE_ID = HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT;
                    filter.TREATMENT_ID = raw.ID;
                    filter.RIGHT_ROUTE_TYPE_CODE__EXACT = HeinRightRouteTypeCode.PRESENT;
                    
                    List<HIS_PATIENT_TYPE_ALTER> ptas = new HisPatientTypeAlterGet().Get(filter);
                    if (IsNotNullOrEmpty(ptas))
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTreatment_BenhNhanChuyenVienDenBatBuocNhapMaVaTenNoiChuyenDen);
                        throw new Exception("Benh nhan chuyen vien den khong co thong tin noi chuyen den");
                    }
                }
                raw.TRANSFER_IN_FORM_ID = data.HisTreatment.TRANSFER_IN_FORM_ID;
                raw.TRANSFER_IN_REASON_ID = data.HisTreatment.TRANSFER_IN_REASON_ID;
                raw.TRANSFER_IN_CODE = data.HisTreatment.TRANSFER_IN_CODE;
                raw.TRANSFER_IN_ICD_NAME = data.HisTreatment.TRANSFER_IN_ICD_NAME;
                raw.TRANSFER_IN_CMKT = data.HisTreatment.TRANSFER_IN_CMKT;
                raw.TRANSFER_IN_ICD_CODE = data.HisTreatment.TRANSFER_IN_ICD_CODE;
                raw.TRANSFER_IN_MEDI_ORG_CODE = data.HisTreatment.TRANSFER_IN_MEDI_ORG_CODE;
                raw.TRANSFER_IN_MEDI_ORG_NAME = data.HisTreatment.TRANSFER_IN_MEDI_ORG_NAME;
                raw.TRANSFER_IN_TIME_FROM = data.HisTreatment.TRANSFER_IN_TIME_FROM;
                raw.TRANSFER_IN_TIME_TO = data.HisTreatment.TRANSFER_IN_TIME_TO;
                raw.TRANSFER_IN_REVIEWS = data.TransferInReviews;
            }
            else
            {
                if (!checker.IsPause(raw))
                {
                    throw new Exception("Ket thuc nghiep vu");
                }
                if (raw.TREATMENT_END_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_TREATMENT_END_TYPE.ID__CHUYEN)
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("Loai ket thuc dieu tri khong phai la chuyen vien");
                }
                //neu ko co cac truong bat buoc nhap thi canh bao
                if (!data.HisTreatment.TRAN_PATI_FORM_ID.HasValue || !data.HisTreatment.TRAN_PATI_REASON_ID.HasValue ||
                    string.IsNullOrWhiteSpace(data.HisTreatment.MEDI_ORG_CODE) || string.IsNullOrWhiteSpace(data.HisTreatment.MEDI_ORG_NAME))
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
                    throw new Exception("Bat buoc nhap noi chuyen den, hinh thuc chuye, ly do chuyen");
                }
                //cap nhat thong tin chuyen di vao treatment
                raw.MEDI_ORG_CODE = data.HisTreatment.MEDI_ORG_CODE;
                raw.MEDI_ORG_NAME = data.HisTreatment.MEDI_ORG_NAME;
                raw.TRAN_PATI_FORM_ID = data.HisTreatment.TRAN_PATI_FORM_ID;
                raw.TRAN_PATI_REASON_ID = data.HisTreatment.TRAN_PATI_REASON_ID;
                raw.CLINICAL_NOTE = data.HisTreatment.CLINICAL_NOTE;
                raw.SUBCLINICAL_RESULT = data.HisTreatment.SUBCLINICAL_RESULT;
                raw.PATIENT_CONDITION = data.HisTreatment.PATIENT_CONDITION;
                raw.TRANSPORT_VEHICLE = data.HisTreatment.TRANSPORT_VEHICLE;
                raw.TRANSPORTER = data.HisTreatment.TRANSPORTER;
                raw.TREATMENT_DIRECTION = data.HisTreatment.TREATMENT_DIRECTION;
                raw.TREATMENT_METHOD = data.HisTreatment.TREATMENT_METHOD;
                raw.ICD_TEXT = HisIcdUtil.RemoveDuplicateIcd(data.IcdText);
                raw.ICD_NAME = data.IcdName;
                raw.ICD_CODE = data.IcdCode;
                raw.ICD_SUB_CODE = HisIcdUtil.RemoveDuplicateIcd(data.IcdSubCode);
            }
        }
    }
}
