using AutoMapper;
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
    internal class HisTreatmentRecordInspectionApprove : BusinessBase
    {
        private HisTreatmentUpdate hisTreatmentUpdate;

        internal HisTreatmentRecordInspectionApprove()
            : base()
        {
            this.hisTreatmentUpdate = new HisTreatmentUpdate(param);
        }

        internal HisTreatmentRecordInspectionApprove(CommonParam param)
            : base(param)
        {
            this.hisTreatmentUpdate = new HisTreatmentUpdate(param);
        }

        internal bool Run(List<long> treatmentIds, ref List<HIS_TREATMENT> resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                List<HIS_TREATMENT> treatments = new List<HIS_TREATMENT>();
                HisTreatmentRecordInspectionCheck checker = new HisTreatmentRecordInspectionCheck(param);
                
                HisTreatmentCheck treatmentChecker = new HisTreatmentCheck(param);
                valid = valid && treatmentChecker.VerifyIds(treatmentIds, treatments);

                valid = valid && checker.HasMediRecordId(treatments);
                valid = valid && checker.IsNotRecordInspectioned(treatments);
                
                if (valid)
                {
                    this.ProcessTreatment(treatments);
                    result = true;
                    resultData = treatments;
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

        private void ProcessTreatment(List<HIS_TREATMENT> treatments)
        {
            Mapper.CreateMap<HIS_TREATMENT, HIS_TREATMENT>();
            List<HIS_TREATMENT> befores = Mapper.Map<List<HIS_TREATMENT>>(treatments);
            treatments.ForEach(o =>
            {
                o.RECORD_INSPECTION_STT_ID = 1;
            });

            if (!this.hisTreatmentUpdate.UpdateList(treatments, befores))
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
