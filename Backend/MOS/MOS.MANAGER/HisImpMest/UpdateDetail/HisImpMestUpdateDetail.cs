using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisImpMestBlood;
using MOS.MANAGER.HisImpMestMaterial;
using MOS.MANAGER.HisImpMestMedicine;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisImpMest.UpdateDetail
{
    class HisImpMestUpdateDetail : BusinessBase
    {
        private ImpMestProcessor impMestProcessor;
        private BloodProcessor bloodProcessor;
        private MaterialProcessor materialProcessor;
        private MedicineProcessor medicineProcessor;
        private HisImpMestUpdateDetailLog logProcessor;

        internal HisImpMestUpdateDetail()
            : base()
        {
            this.Init();
        }

        internal HisImpMestUpdateDetail(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.impMestProcessor = new ImpMestProcessor(param);
            this.bloodProcessor = new BloodProcessor(param);
            this.materialProcessor = new MaterialProcessor(param);
            this.medicineProcessor = new MedicineProcessor(param);
            this.logProcessor = new HisImpMestUpdateDetailLog();
        }

        internal bool Run(HisImpMestUpdateDetailSDO data, ref HisImpMestResultSDO resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_IMP_MEST impMest = null;
                HisImpMestUpdateDetailCheck checker = new HisImpMestUpdateDetailCheck(param);
                HisImpMestCheck commonChecker = new HisImpMestCheck(param);
                valid = valid && checker.ValidData(data);
                valid = valid && commonChecker.VerifyId(data.ImpMestId, ref impMest);
                valid = valid && commonChecker.IsUnLock(impMest);
                valid = valid && commonChecker.IsImported(impMest);
                valid = valid && checker.VerifyImpMestType(impMest);
                if (valid)
                {
                    if (!this.impMestProcessor.Run(data, impMest, this.logProcessor))
                    {
                        throw new Exception("impMestProcessor. Ket thuc nghiep vu");
                    }

                    if (!this.bloodProcessor.Run(data, impMest, this.logProcessor))
                    {
                        throw new Exception("bloodProcessor. Ket thuc nghiep vu");
                    }

                    if (!this.materialProcessor.Run(data, impMest,this.logProcessor))
                    {
                        throw new Exception("materialProcessor. Ket thuc nghiep vu");
                    }

                    if (!this.medicineProcessor.Run(data, impMest,this.logProcessor))
                    {
                        throw new Exception("medicineProcessor. Ket thuc nghiep vu");
                    }

                    this.PassResult(impMest, ref resultData);

                    result = true;
                    logProcessor.Run(impMest);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                this.Rollback();
                param.HasException = true;
                resultData = null;
                result = false;
            }
            return result;
        }

        /// <summary>
        /// Truyen ket qua thong qua bien "data"
        /// </summary>
        /// <param name="data"></param>
        private void PassResult(HIS_IMP_MEST impMest, ref HisImpMestResultSDO resultData)
        {
            resultData = new HisImpMestResultSDO();
            resultData.ImpMest = new HisImpMestGet().GetViewById(impMest.ID);
            resultData.ImpMedicines = new HisImpMestMedicineGet().GetViewByImpMestId(impMest.ID);
            resultData.ImpMaterials = new HisImpMestMaterialGet().GetViewByImpMestId(impMest.ID);
            resultData.ImpBloods = new HisImpMestBloodGet().GetViewByImpMestId(impMest.ID);
        }

        private void Rollback()
        {
            try
            {
                this.medicineProcessor.RollbackData();
                this.materialProcessor.RollbackData();
                this.bloodProcessor.RollbackData();
                this.impMestProcessor.Rollback();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

    }
}
