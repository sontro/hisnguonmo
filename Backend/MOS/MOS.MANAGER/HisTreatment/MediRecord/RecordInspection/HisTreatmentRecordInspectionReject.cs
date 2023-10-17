using AutoMapper;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisMediRecord;
using MOS.MANAGER.HisMediRecordBorrow;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTreatment.MediRecord.RecordInspection
{
    internal class HisTreatmentRecordInspectionReject : BusinessBase
    {
        private HisTreatmentUpdate hisTreatmentUpdate;

        internal HisTreatmentRecordInspectionReject()
            : base()
        {
            this.hisTreatmentUpdate = new HisTreatmentUpdate(param);
        }

        internal HisTreatmentRecordInspectionReject(CommonParam param)
            : base(param)
        {
            this.hisTreatmentUpdate = new HisTreatmentUpdate(param);
        }

        internal bool Run(RecordInspectionRejectSdo sdo, ref HIS_TREATMENT resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_TREATMENT treatment = null;
                HisTreatmentRecordInspectionCheck checker = new HisTreatmentRecordInspectionCheck(param);
                
                HisTreatmentCheck treatmentChecker = new HisTreatmentCheck(param);
                valid = valid && treatmentChecker.VerifyId(sdo.TreatmentId, ref treatment);

                valid = valid && checker.HasMediRecordId(treatment);
                valid = valid && checker.IsNotRecordInspectioned(treatment);
                
                if (valid)
                {
                    this.ProcessTreatment(treatment, sdo);
                    result = true;
                    resultData = treatment;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
                param.HasException = true;
                this.Rollback();
            }
            return result;
        }

        private void ProcessTreatment(HIS_TREATMENT treatment, RecordInspectionRejectSdo sdo)
        {
            Mapper.CreateMap<HIS_TREATMENT, HIS_TREATMENT>();
            HIS_TREATMENT before = Mapper.Map<HIS_TREATMENT>(treatment);
            treatment.RECORD_INSPECTION_STT_ID = 2;
            treatment.RECORD_INSPECTION_REJECT_NOTE = sdo.RecordInspectionRejectNote;

            if (!this.hisTreatmentUpdate.Update(treatment, before))
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
