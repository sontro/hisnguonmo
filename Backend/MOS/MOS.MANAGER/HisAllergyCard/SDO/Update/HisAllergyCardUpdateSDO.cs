using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisAllergenic;
using MOS.MANAGER.HisTreatment;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisAllergyCard.SDO.Update
{
    class HisAllergyCardUpdateSDO : BusinessBase
    {
        private AllergyCardProcessor allergyCardProcessor;
        private AllergenicProcessor allergenicProcessor;

        internal HisAllergyCardUpdateSDO()
            : base()
        {
            this.Init();
        }

        internal HisAllergyCardUpdateSDO(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.allergyCardProcessor = new AllergyCardProcessor(param);
            this.allergenicProcessor = new AllergenicProcessor(param);
        }

        internal bool Run(HisAllergyCardSDO data, ref HisAllergyCardResultSDO resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_ALLERGY_CARD raw = null;
                HIS_TREATMENT treatment = null;
                List<HIS_ALLERGENIC> olds = null;
                HisAllergyCardCheck checker = new HisAllergyCardCheck(param);
                HisAllergyCardSDOCheck sdoChecker = new HisAllergyCardSDOCheck(param);
                HisTreatmentCheck treatChecker = new HisTreatmentCheck(param);
                valid = valid && sdoChecker.ValidData(data);
                valid = valid && checker.VerifyId(data.AllergyCard.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && treatChecker.VerifyId(raw.TREATMENT_ID, ref treatment);
                valid = valid && checker.AllowUpdateOrDeleteLoginname(raw, ref olds);
                if (valid)
                {
                    List<string> sqls = new List<string>();
                    data.Allergenics.ForEach(o => o.TDL_PATIENT_ID = treatment.PATIENT_ID);

                    if (!this.allergyCardProcessor.Run(data.AllergyCard, raw))
                    {
                        throw new Exception("allergyCardProcessor. Rollback du lieu");
                    }

                    if (!this.allergenicProcessor.Run(data.AllergyCard,data.Allergenics,olds,ref sqls))
                    {
                        throw new Exception("allergenicProcessor. Rollback du lieu");
                    }

                    if (IsNotNullOrEmpty(sqls))
                    {
                        if (!DAOWorker.SqlDAO.Execute(sqls))
                        {
                            throw new Exception("sqls. Rollback du lieu");
                        }
                    }

                    this.PassResult(data, ref resultData);
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

        private void PassResult(HisAllergyCardSDO data, ref HisAllergyCardResultSDO resultData)
        {
            try
            {
                resultData = new HisAllergyCardResultSDO();
                resultData.HisAllergyCard = new HisAllergyCardGet().GetViewById(data.AllergyCard.ID);
                resultData.HisAllergenics = new HisAllergenicGet().GetByAllergyCardId(data.AllergyCard.ID);
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
                this.allergenicProcessor.RollbackData();
                this.allergyCardProcessor.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
