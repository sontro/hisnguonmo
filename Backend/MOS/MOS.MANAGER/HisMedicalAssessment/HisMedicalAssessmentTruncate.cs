using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisAssessmentMember;
using MOS.MANAGER.HisRestRetrType;
using MOS.MANAGER.HisTreatment;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisMedicalAssessment
{
    partial class HisMedicalAssessmentTruncate : BusinessBase
    {
        HisAssessmentMemberTruncate assessmentMemberTruncate { get; set; } 
        internal HisMedicalAssessmentTruncate()
            : base()
        {

        }

        internal HisMedicalAssessmentTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {
            this.Init();
        }
        private void Init()
        {
            this.assessmentMemberTruncate = new HisAssessmentMemberTruncate(param);
        }
        internal bool Truncate(long TreatmentId)
        {
            bool result = false;
            try
            {
                HisMedicalAssessmentCheck checker = new HisMedicalAssessmentCheck(param);

                HisMedicalAssessmentFilterQuery filter = new HisMedicalAssessmentFilterQuery();
                filter.TREATMENT_ID = TreatmentId;
                List<HIS_MEDICAL_ASSESSMENT> MedicalAssessements = new HisMedicalAssessmentGet().Get(filter);

                if (IsNotNullOrEmpty(MedicalAssessements))
                {
                    List<long> medicalAssessementIds = MedicalAssessements.Select(o => o.ID).ToList();
                    HisAssessmentMemberFilterQuery filterMember = new HisAssessmentMemberFilterQuery();
                    filterMember.MEDICAL_ASSESSMENT_IDs = medicalAssessementIds;

                    List<HIS_ASSESSMENT_MEMBER> members = new HisAssessmentMemberGet().Get(filterMember);
                    if (IsNotNullOrEmpty(members))
                    {
                        if (!this.assessmentMemberTruncate.TruncateList(members))
                        {
                            Inventec.Common.Logging.LogSystem.Error("Xoa List<HIS_ASSESSMENT_MEMBER> that bai. Ket thuc nghiep vu");
                            return false;
                        }
                    }

                    if (!this.TruncateList(MedicalAssessements))
                    {
                        Inventec.Common.Logging.LogSystem.Error("Xoa List<HIS_MEDICAL_ASSESSMENT> that bai. Ket thuc nghiep vu");
                        return false;
                    }

                    result = true;
                    HIS_TREATMENT treatment = new HisTreatmentGet().GetById(TreatmentId);
                    new EventLogGenerator(LibraryEventLog.EventLog.Enum.HisMedicalAssessment_XoaThongTinGiamDinhKhoa)
                            .TreatmentCode(treatment.TREATMENT_CODE)
                            .Run();
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

        internal bool TruncateList(List<HIS_MEDICAL_ASSESSMENT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMedicalAssessmentCheck checker = new HisMedicalAssessmentCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                    valid = valid && checker.IsUnLock(data.ID);
					valid = valid && checker.CheckConstraint(data.ID);
                }
                if (valid)
                {
                    result = DAOWorker.HisMedicalAssessmentDAO.TruncateList(listData);
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
    }
}
