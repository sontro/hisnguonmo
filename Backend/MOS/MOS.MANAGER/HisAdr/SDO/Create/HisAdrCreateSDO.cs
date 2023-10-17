using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisAdrMedicineType;
using MOS.MANAGER.HisTreatment;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisAdr.SDO.Create
{
    class HisAdrCreateSDO : BusinessBase
    {
        private AdrProcessor adrProcessor;
        private AdrMedicineTypeProcessor adrMedicineTypeProcessor;

        internal HisAdrCreateSDO()
            : base()
        {
            this.Init();
        }

        internal HisAdrCreateSDO(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.adrProcessor = new AdrProcessor(param);
            this.adrMedicineTypeProcessor = new AdrMedicineTypeProcessor(param);
        }

        internal bool Run(HisAdrSDO data, ref HisAdrResultSDO resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_TREATMENT treatment = null;
                HIS_ADR adr = null;
                List<HIS_ADR_MEDICINE_TYPE> adrMedicineTypes = null;
                HisAdrSDOCheck checker = new HisAdrSDOCheck(param);
                HisTreatmentCheck treatmentChecker = new HisTreatmentCheck(param);

                valid = valid && checker.ValidData(data);
                valid = valid && treatmentChecker.VerifyId(data.Adr.TREATMENT_ID, ref treatment);
                //valid = valid && treatmentChecker.IsUnLock(treatment);
                if (valid)
                {
                    if (!this.adrProcessor.Run(data, ref adr))
                    {
                        throw new Exception("adrProcessor. Rollback du lieu");
                    }

                    if (!this.adrMedicineTypeProcessor.Run(data, adr, ref adrMedicineTypes))
                    {
                        throw new Exception("adrMedicineTypeProcessor. Rollback du lieu");
                    }

                    this.PassResult(adr, ref resultData);
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);             
                param.HasException = true;
                this.Rollback();
                result = false;
            }
            return result;
        }

        private void PassResult(HIS_ADR adr, ref HisAdrResultSDO resultData)
        {
            try
            {
                resultData = new HisAdrResultSDO();
                resultData.HisAdr = new HisAdrGet().GetViewById(adr.ID);
                resultData.HisAdrMedicineTypes = new HisAdrMedicineTypeGet().GetViewByAdrId(adr.ID);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void Rollback()
        {
            try
            {
                this.adrMedicineTypeProcessor.RollbackData();
                this.adrProcessor.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
