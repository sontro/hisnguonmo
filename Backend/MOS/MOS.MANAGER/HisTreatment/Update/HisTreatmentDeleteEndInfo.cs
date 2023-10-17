using Inventec.Core;
using Inventec.Common.Logging;
using MOS.MANAGER.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using MOS.MANAGER.HisSevereIllnessInfo;
using MOS.MANAGER.HisEventsCausesDeath;

namespace MOS.MANAGER.HisTreatment.Update
{
    class HisTreatmentDeleteEndInfo : BusinessBase
    {
        private HIS_TREATMENT beforeUpdate;

        private HisEventsCausesDeathTruncate hisEventsCausesDeathTruncate;
        private HisSevereIllnessInfoTruncate hisSevereIllnessInfoTruncate;

        internal HisTreatmentDeleteEndInfo()
            : base()
        {
            this.Init();
        }

        internal HisTreatmentDeleteEndInfo(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisEventsCausesDeathTruncate = new HisEventsCausesDeathTruncate(param);
            this.hisSevereIllnessInfoTruncate = new HisSevereIllnessInfoTruncate(param);
        }
        internal bool Run(long id, ref HIS_TREATMENT resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_TREATMENT raw = null;
                HisTreatmentCheck checker = new HisTreatmentCheck(param);
                valid = valid && IsGreaterThanZero(id);
                valid = valid && checker.HasTreatmentFinished(id, ref raw);
                if (valid)
                {
                    this.beforeUpdate = raw;
                    AutoMapper.Mapper.CreateMap<HIS_TREATMENT, HIS_TREATMENT>();
                    HIS_TREATMENT data = AutoMapper.Mapper.Map<HIS_TREATMENT>(raw);
                    List<HIS_SEVERE_ILLNESS_INFO> illnessInfos = null;
                    List<HIS_EVENTS_CAUSES_DEATH> eventsCausesDeaths = null;
                    this.DeleteEndInfo(data, ref illnessInfos, ref eventsCausesDeaths);
                    if (!DAOWorker.HisTreatmentDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisTreatment_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisTreatment that bai." + LogUtil.TraceData("data", data));
                    }
                    if (IsNotNullOrEmpty(illnessInfos))
                    {
                        if ((IsNotNullOrEmpty(eventsCausesDeaths) && !this.hisEventsCausesDeathTruncate.TruncateList(eventsCausesDeaths))
                            || !this.hisSevereIllnessInfoTruncate.TruncateList(illnessInfos))
                        {
                            MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisTreatment_CapNhatThatBai);
                            throw new Exception(String.Format("Xoa thong tin List<HIS_SEVERE_ILLNESS_INFO> cua ho so {0} that bai.", data.TREATMENT_CODE) + LogUtil.TraceData("illnessInfo", illnessInfos));
                        }
                    }

                    result = true;
                    resultData = data;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
                Rollback();
            }
            return result;
        }

        internal void DeleteEndInfo(HIS_TREATMENT data, ref List<HIS_SEVERE_ILLNESS_INFO> illnessInfos, ref List<HIS_EVENTS_CAUSES_DEATH> eventsCausesDeaths)
        {
            data.OUT_DATE = null;
            data.OUT_TIME = null;
            data.TREATMENT_RESULT_ID = null;
            data.TREATMENT_END_TYPE_ID = null;
            //Thong tin tu vong:
            data.DEATH_CAUSE_ID = null;
            data.DEATH_CERT_BOOK_FIRST_ID = null;
            data.DEATH_CERT_BOOK_ID = null;
            data.DEATH_CERT_ISSUER_LOGINNAME = null;
            data.DEATH_CERT_ISSUER_USERNAME = null;
            data.DEATH_CERT_NUM = null;
            data.DEATH_CERT_NUM_FIRST = null;
            data.DEATH_DOCUMENT_DATE = null;
            data.DEATH_DOCUMENT_NUMBER = null;
            data.DEATH_DOCUMENT_PLACE = null;
            data.DEATH_DOCUMENT_TYPE = null;
            data.DEATH_DOCUMENT_TYPE_CODE = null;
            data.DEATH_ISSUED_DATE = null;
            data.DEATH_PLACE = null;
            data.DEATH_STATUS = null;
            data.DEATH_SYNC_FAILD_REASON = null;
            data.DEATH_SYNC_RESULT_TYPE = null;
            data.DEATH_SYNC_TIME = null;
            data.DEATH_TIME = null;
            data.DEATH_WITHIN_ID = null;
            data.MAIN_CAUSE = null;
            //Thong tin chuyen di:
            data.MEDI_ORG_CODE = null;
            data.MEDI_ORG_NAME = null;
            data.TRAN_PATI_FORM_ID = null;
            data.TRAN_PATI_REASON_ID = null;
            data.TRAN_PATI_TECH_ID = null;
            data.PATIENT_CONDITION = null;
            data.TRANSPORT_VEHICLE = null;
            data.TRANSPORTER = null;
            data.USED_MEDICINE = null;
            data.TRAN_PATI_HOSPITAL_LOGINNAME = null;
            data.TRAN_PATI_HOSPITAL_USERNAME = null;
            //Thong tin hen kham lai:
            data.APPOINTMENT_CODE = null;
            data.APPOINTMENT_DATE = null;
            data.APPOINTMENT_DESC = null;
            data.APPOINTMENT_EXAM_ROOM_IDS = null;
            data.APPOINTMENT_EXAM_SERVICE_ID = null;
            data.APPOINTMENT_PERIOD_ID = null;
            data.APPOINTMENT_SURGERY = null;
            data.APPOINTMENT_TIME = null;
            //Thong tin ket thuc bo sung
            data.TREATMENT_END_TYPE_EXT_ID = null;
            //
            HisSevereIllnessInfoFilterQuery filterIllnessInfo = new HisSevereIllnessInfoFilterQuery();
            filterIllnessInfo.TREATMENT_ID = data.ID;
            illnessInfos = new HisSevereIllnessInfoGet().Get(filterIllnessInfo);
            //
            if (IsNotNullOrEmpty(illnessInfos))
            {
                List<long> illnessInfoIds = illnessInfos.Select(o => o.ID).ToList();
                HisEventsCausesDeathFilterQuery filterEventsCausesDeath = new HisEventsCausesDeathFilterQuery();
                filterEventsCausesDeath.SEVERE_ILLNESS_INFO_IDs = illnessInfoIds;
                eventsCausesDeaths = new HisEventsCausesDeathGet().Get(filterEventsCausesDeath);
            }
        }

        internal void Rollback()
        {
            try
            {
                if (this.beforeUpdate != null && !DAOWorker.HisTreatmentDAO.Update(this.beforeUpdate))
                {
                    LogSystem.Warn("Rollback treatment that bai");
                }
                this.hisSevereIllnessInfoTruncate.RollbackData();
                this.hisEventsCausesDeathTruncate.RollbackData();
                //if (this.beforeDeleteIllnessInfo != null && !DAOWorker.HisSevereIllnessInfoDAO.Update(this.beforeDeleteIllnessInfo))
                //{
                //    LogSystem.Warn("Rollback HisSevereIllnessInfo that bai");
                //}
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

        }
    }
}
