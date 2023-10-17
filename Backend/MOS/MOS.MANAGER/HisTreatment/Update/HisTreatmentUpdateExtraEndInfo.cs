using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Linq;
using System.Collections.Generic;
using MOS.MANAGER.HisBaby;
using MOS.MANAGER.Config;
using MOS.MANAGER.CodeGenerator.HisTreatment;
using MOS.MANAGER.HisWorkPlace;
using MOS.MANAGER.HisPatient;
using MOS.UTILITY;
using Inventec.Fss.Utility;
using His.Bhyt.ExportXml;
using System.IO;
using Inventec.Fss.Client;
using His.Bhyt.ExportXml.Base;
using MOS.MANAGER.HisTreatment.Update.Finish;
using System.Threading;
using MOS.MANAGER.HisServiceReq.Exam.Register;
using MOS.MANAGER.Token;

namespace MOS.MANAGER.HisTreatment
{
    partial class HisTreatmentUpdateExtraEndInfo : BusinessBase
    {
        private HisBabyCreate hisBabyCreate;
        private HisBabyTruncate hisBabyTruncate;
        private HisTreatmentUpdate hisTreatmentUpdate;
        private HisPatientUpdate hisPatientUpdate;
   
        internal HisTreatmentUpdateExtraEndInfo()
            : base()
        {
            this.Init();
        }

        internal HisTreatmentUpdateExtraEndInfo(CommonParam paramUpdate)
            : base(paramUpdate)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisBabyCreate = new HisBabyCreate(param);
            this.hisBabyTruncate = new HisBabyTruncate(param);
            this.hisTreatmentUpdate = new HisTreatmentUpdate(param);
            this.hisPatientUpdate = new HisPatientUpdate(param);
        }

        internal bool Run(HisTreatmentExtraEndInfoSDO data, ref HIS_TREATMENT resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_TREATMENT treatment = null;
                HisTreatmentCheck checker = new HisTreatmentCheck(param);
                valid = valid && checker.VerifyId(data.TreatmentId, ref treatment);
                valid = valid && this.IsValidAbortionInfo(data);
                if (valid)
                {
                    Mapper.CreateMap<HIS_TREATMENT, HIS_TREATMENT>();
                    HIS_TREATMENT beforeUpdate = Mapper.Map<HIS_TREATMENT>(treatment);

                    HIS_WORK_PLACE workPlace = null;
                    if (data.WorkPlaceId.HasValue)
                    {
                        workPlace = new HisWorkPlaceGet().GetById(data.WorkPlaceId.Value);
                    }
                    treatment.TDL_PATIENT_RELATIVE_NAME = data.PatientRelativeName;
                    treatment.TDL_PATIENT_RELATIVE_TYPE = data.PatientRelativeType;
                    treatment.TDL_PATIENT_WORK_PLACE = data.PatientWorkPlace;
                    treatment.TDL_PATIENT_WORK_PLACE_NAME = workPlace != null ? workPlace.WORK_PLACE_NAME : null;
                    treatment.TDL_SOCIAL_INSURANCE_NUMBER = data.SocialInsuranceNumber;
                    treatment.END_TYPE_EXT_NOTE = data.EndTypeExtNote;

                    if (!data.TreatmentEndTypeExtId.HasValue)
                    {
                        treatment.SICK_LEAVE_DAY = null;
                        treatment.SICK_LEAVE_FROM = null;
                        treatment.SICK_LEAVE_TO = null;
                        treatment.EXTRA_END_CODE = null;
                        treatment.SICK_LOGINNAME = null;
                        treatment.SICK_USERNAME = null;
                        treatment.DOCUMENT_BOOK_ID = null;
                        treatment.IS_PREGNANCY_TERMINATION = null;
                        treatment.GESTATIONAL_AGE = null;
                        treatment.PREGNANCY_TERMINATION_REASON = null;
                        treatment.PREGNANCY_TERMINATION_TIME = null;
                    }
                    else
                    {
                        treatment.TREATMENT_END_TYPE_EXT_ID = data.TreatmentEndTypeExtId;

                        if (treatment.TREATMENT_END_TYPE_EXT_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_END_TYPE_EXT.ID__NGHI_DUONG_THAI
                            || treatment.TREATMENT_END_TYPE_EXT_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_END_TYPE_EXT.ID__NGHI_OM)
                        {
                            //cap nhat thong tin vao treatment 
                            treatment.SICK_LEAVE_DAY = data.SickLeaveDay;
                            treatment.SICK_LEAVE_FROM = data.SickLeaveFrom;
                            treatment.SICK_LEAVE_TO = data.SickLeaveTo;
                            treatment.TREATMENT_END_TYPE_EXT_ID = data.TreatmentEndTypeExtId.Value;
                            treatment.SICK_HEIN_CARD_NUMBER = data.SickHeinCardNumber;
                            treatment.TREATMENT_METHOD = data.TreatmentMethod;
                            treatment.SICK_LOGINNAME = data.SickLoginname;
                            treatment.SICK_USERNAME = data.SickUsername;

                            if (data.TreatmentEndTypeExtId == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_END_TYPE_EXT.ID__NGHI_OM)
                            {
                                treatment.DOCUMENT_BOOK_ID = data.DocumentBookId;
                                if (data.IsPregnancyTermination == true)
                                {
                                    treatment.IS_PREGNANCY_TERMINATION = Constant.IS_TRUE;
                                }
                                else
                                {
                                    treatment.IS_PREGNANCY_TERMINATION = null;
                                }
                                treatment.GESTATIONAL_AGE = data.GestationalAge;
                                treatment.PREGNANCY_TERMINATION_REASON = data.PregnancyTerminationReason;
                                treatment.PREGNANCY_TERMINATION_TIME = data.PregnancyTerminationTime;
                            }

                            if (treatment.TREATMENT_END_TYPE_EXT_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_END_TYPE_EXT.ID__NGHI_DUONG_THAI)
                            {
                                this.SetBabyInfo(data.Babies, data.TreatmentId);
                            }
                        }
                        else if (treatment.TREATMENT_END_TYPE_EXT_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_END_TYPE_EXT.ID__HEN_MO)
                        {
                            treatment.APPOINTMENT_SURGERY = data.AppointmentSurgery;
                            treatment.SURGERY_APPOINTMENT_TIME = data.SurgeryAppointmentTime;
                            treatment.ADVISE = data.Advise;
                        }

                        if (treatment.TREATMENT_END_TYPE_EXT_ID != beforeUpdate.TREATMENT_END_TYPE_EXT_ID)
                        {
                            this.SetExtraEndCode(treatment);
                        }
                    }

                    if (!this.hisTreatmentUpdate.Update(treatment, beforeUpdate, true))
                    {
                        throw new Exception("Update HisTreatment that bai. Ket thuc nghiep vu");
                    }

                    this.ProcessPatient(data, treatment);

                    ExtraEndCodeGenerator.FinishUpdateDB(treatment.EXTRA_END_CODE);
                    resultData = treatment;

                    if (resultData != null)
                    {
                        this.InitThreadExportXml(treatment);
                    }
                }
               
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                this.RollBack();
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private void SetBabyInfo(List<HisBabySDO> babies, long treatmentId)
        {
            //Chi lay cac dong co nhap thong tin
            babies = IsNotNullOrEmpty(babies) ? babies.Where(o => !string.IsNullOrWhiteSpace(o.BabyName)).ToList() : null;
            if (IsNotNullOrEmpty(babies))
            {
                List<HIS_BABY> exists = new HisBabyGet().GetByTreatmentId(treatmentId);
                if (IsNotNullOrEmpty(exists) && !this.hisBabyTruncate.TruncateList(exists))
                {
                    throw new Exception("Xoa du lieu so sinh (his_baby) cu that bai");
                }

                List<HIS_BABY> toInserts = babies.Select(o => new HIS_BABY
                {
                    TREATMENT_ID = treatmentId,
                    BABY_NAME = o.BabyName,
                    BABY_ORDER = o.BabyOrder,
                    BORN_POSITION_ID = o.BornPositionId,
                    BORN_RESULT_ID = o.BornResultId,
                    BORN_TIME = o.BornTime,
                    BORN_TYPE_ID = o.BornTypeId,
                    FATHER_NAME = o.FatherName,
                    GENDER_ID = o.GenderId,
                    HEAD = o.Head,
                    HEIGHT = o.Height,
                    MIDWIFE = o.Midwife,
                    MONTH_COUNT = o.MonthCount,
                    WEEK_COUNT = o.WeekCount,
                    WEIGHT = o.Weight,
                    ETHNIC_CODE = o.EthnicCode,
                    ETHNIC_NAME = o.EthnicName
                }).ToList();

                if (IsNotNullOrEmpty(toInserts) && !this.hisBabyCreate.CreateList(toInserts))
                {
                    throw new Exception("Insert du lieu so sinh (his_baby) that bai");
                }
            }
        }

        private void SetExtraEndCode(HIS_TREATMENT hisTreatment)
        {
            //Neu co thong tin ket thuc bo sung
            if (hisTreatment.TREATMENT_END_TYPE_EXT_ID.HasValue && hisTreatment.OUT_TIME.HasValue)
            {
                //Lay 2 chu so cuoi cua nam
                string year = hisTreatment.OUT_TIME.ToString().Substring(2, 2);

                HIS_TREATMENT_END_TYPE_EXT treatmentEndTypeExt = HisTreatmentEndTypeExtCFG.DATA.Where(o => o.ID == hisTreatment.TREATMENT_END_TYPE_EXT_ID.Value).FirstOrDefault();

                string seedCode = string.Format("{0}/{1}", treatmentEndTypeExt.TREATMENT_END_TYPE_EXT_CODE, year);
                hisTreatment.EXTRA_END_CODE = ExtraEndCodeGenerator.GetNext(seedCode);
                hisTreatment.EXTRA_END_CODE_SEED_CODE = seedCode;
            }
        }

        private void ProcessPatient(HisTreatmentExtraEndInfoSDO data, HIS_TREATMENT treatment)
        {
            HIS_PATIENT hisPatient = new HisPatientGet().GetById(treatment.PATIENT_ID);

            if (hisPatient != null)
            {
                Mapper.CreateMap<HIS_PATIENT, HIS_PATIENT>();
                HIS_PATIENT before = Mapper.Map<HIS_PATIENT>(hisPatient);

                hisPatient.WORK_PLACE_ID = data.WorkPlaceId;
                hisPatient.SOCIAL_INSURANCE_NUMBER = data.SocialInsuranceNumber;
                hisPatient.RELATIVE_NAME = data.PatientRelativeName;
                hisPatient.RELATIVE_TYPE = data.PatientRelativeType;
                hisPatient.WORK_PLACE = data.PatientWorkPlace;

                if (!this.hisPatientUpdate.Update(hisPatient, before))
                {
                    throw new Exception("Cap nhat his_patient that bai.");
                }
            }
        }

        /// <summary>
        /// Tu dong xuat XML 
        /// </summary>
        /// <param name="treatment"></param>
        private void InitThreadExportXml(HIS_TREATMENT treatment)
        {
            try
            {
                if (IsNotNull(treatment))
                {
                     Thread thread = new Thread(new ParameterizedThreadStart(this.ExportXml2076));
                     thread.Priority = ThreadPriority.Lowest;
                     thread.Start(treatment.ID);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
        private void ExportXml2076(object data)
        {
            try
            {
                long treatId = (long)data;
                new AutoExportXml2076Processor().Run(treatId);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        //ktra thông tin pha thai
        private bool IsValidAbortionInfo(HisTreatmentExtraEndInfoSDO data)
        {
            bool valid = true;
            try
            {
                if (data != null
                    && ((data.TreatmentEndTypeExtId == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_END_TYPE_EXT.ID__NGHI_OM))
                    && (data.IsPregnancyTermination == true)
                    && ((data.GestationalAge == null) || (data.PregnancyTerminationReason == null) || (data.PregnancyTerminationTime == null)))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTreatment_ThieuThongTinTuoiThaiHoacLyDoDinhChiThai);
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
            return valid;
        }

        internal void RollBack()
        {
            this.hisTreatmentUpdate.RollbackData();
            this.hisBabyCreate.RollbackData();
            this.hisPatientUpdate.RollbackData();
        }
    }
}
