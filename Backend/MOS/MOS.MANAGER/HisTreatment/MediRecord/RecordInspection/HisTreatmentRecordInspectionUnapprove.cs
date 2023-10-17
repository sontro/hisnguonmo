using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisMediRecord;
using MOS.MANAGER.HisMediRecordBorrow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTreatment.MediRecord.RecordInspection
{
    internal class HisTreatmentRecordInspectionUnapprove : BusinessBase
    {
        private HisTreatmentUpdate hisTreatmentUpdate;

        internal HisTreatmentRecordInspectionUnapprove()
            : base()
        {
            this.hisTreatmentUpdate = new HisTreatmentUpdate(param);
        }

        internal HisTreatmentRecordInspectionUnapprove(CommonParam param)
            : base(param)
        {
            this.hisTreatmentUpdate = new HisTreatmentUpdate(param);
        }

        internal bool Run(long treatmentId, ref HIS_TREATMENT resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_TREATMENT treatment = null;
                HisTreatmentRecordInspectionCheck checker = new HisTreatmentRecordInspectionCheck(param);
                
                HisTreatmentCheck treatmentChecker = new HisTreatmentCheck(param);
                valid = valid && treatmentChecker.VerifyId(treatmentId, ref treatment);

                valid = valid && checker.IsRecordInspectionApproved(treatment);
                
                if (valid)
                {
                    this.ProcessTreatment(treatment);
                    result = true;
                    resultData = treatment;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                result = false;
                param.HasException = true;
                this.Rollback();
            }
            return result;
        }

        private void ProcessTreatment(HIS_TREATMENT treatment)
        {
            Mapper.CreateMap<HIS_TREATMENT, HIS_TREATMENT>();
            HIS_TREATMENT before = Mapper.Map<HIS_TREATMENT>(treatment);
            treatment.RECORD_INSPECTION_STT_ID = null;

            if (!this.hisTreatmentUpdate.Update(treatment, before, true))
            {
                throw new Exception("hisTreatmentUpdate. Ket thuc nghiep vu");
            }
        }

        private void Rollback()
        {
            try
            {
                this.hisTreatmentUpdate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
