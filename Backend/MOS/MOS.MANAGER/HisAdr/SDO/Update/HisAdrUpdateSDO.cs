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

namespace MOS.MANAGER.HisAdr.SDO.Update
{
    class HisAdrUpdateSDO : BusinessBase
    {
        private AdrProcessor adrProcessor;
        private AdrMedicineTypeProcessor adrMedicineTypeProcessor;

        internal HisAdrUpdateSDO()
            : base()
        {
            this.Init();
        }

        internal HisAdrUpdateSDO(CommonParam param)
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
                HIS_ADR raw = null;
                List<HIS_ADR_MEDICINE_TYPE> adrMedicineTypes = null;
                List<HIS_ADR_MEDICINE_TYPE> olds = null;
                HisAdrSDOCheck checker = new HisAdrSDOCheck(param);
                HisAdrCheck adrChecker = new HisAdrCheck(param);
                HisTreatmentCheck treatmentChecker = new HisTreatmentCheck(param);

                valid = valid && checker.ValidData(data);
                valid = valid && adrChecker.VerifyId(data.Adr.ID, ref raw);
                valid = valid && adrChecker.IsUnLock(raw);
                valid = valid && adrChecker.AllowUpdateOrDeleteLoginname(raw, ref olds);
                valid = valid && treatmentChecker.VerifyId(data.Adr.TREATMENT_ID, ref treatment);
                if (valid)
                {
                    List<string> sqls = new List<string>();
                    if (!this.adrProcessor.Run(data, raw, ref adr))
                    {
                        throw new Exception("adrProcessor. Rollback du lieu");
                    }

                    if (!this.adrMedicineTypeProcessor.Run(data, adr, olds, ref adrMedicineTypes, ref sqls))
                    {
                        throw new Exception("adrMedicineTypeProcessor. Rollback du lieu");
                    }

                    if (IsNotNullOrEmpty(sqls))
                    {
                        if (!DAOWorker.SqlDAO.Execute(sqls))
                        {
                            throw new Exception("sqls. Rollback du lieu");
                        }
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
