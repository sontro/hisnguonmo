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

namespace MOS.MANAGER.HisAllergyCard.SDO.Create
{
    class HisAllergyCardCreateSDO: BusinessBase
    {
        private AllergyCardProcessor allergyCardProcessor;
        private AllergenicProcessor allergenicProcessor;

        internal HisAllergyCardCreateSDO()
            : base()
        {
            this.Init();
        }

        internal HisAllergyCardCreateSDO(CommonParam param)
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
                HIS_TREATMENT treatment = null;
                HisAllergyCardSDOCheck sdoChecker = new HisAllergyCardSDOCheck(param);
                HisTreatmentCheck treatChecker = new HisTreatmentCheck(param);
                valid = valid && sdoChecker.ValidData(data);
                valid = valid && treatChecker.VerifyId(data.AllergyCard.TREATMENT_ID, ref treatment);
                if (valid)
                {
                    data.Allergenics.ForEach(o => o.TDL_PATIENT_ID = treatment.PATIENT_ID);
                    if (!this.allergyCardProcessor.Run(data.AllergyCard))
                    {
                        throw new Exception("allergyCardProcessor. Rollback du lieu");
                    }

                    if (!this.allergenicProcessor.Run(data.AllergyCard,data.Allergenics))
                    {
                        throw new Exception("allergenicProcessor. Rollback du lieu");
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
