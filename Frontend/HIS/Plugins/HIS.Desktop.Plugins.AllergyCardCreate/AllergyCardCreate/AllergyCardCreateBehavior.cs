using Inventec.Core;
using Inventec.Desktop.Common;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.AllergyCardCreate
{
    class AllergyCardCreateBehavior : BusinessBase, IAllergyCardCreate
    {
        object[] entity;
        internal AllergyCardCreateBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object IAllergyCardCreate.Run()
        {
            try
            {
                object result = null;
                Inventec.Desktop.Common.Modules.Module moduleData = null;
                MOS.EFMODEL.DataModels.V_HIS_ALLERGY_CARD allergyCard = null;
                long? treatmentId = null;

                if (entity.GetType() == typeof(object[]))
                {
                    if (entity != null && entity.Count() > 0)
                    {
                        for (int i = 0; i < entity.Count(); i++)
                        {
                            if (entity[i] is Inventec.Desktop.Common.Modules.Module)
                            {
                                moduleData = (Inventec.Desktop.Common.Modules.Module)entity[i];
                            }
                            if (entity[i] is long?)
                            {
                                treatmentId = (long?)entity[i];
                            }
                            if (entity[i] is MOS.EFMODEL.DataModels.V_HIS_ALLERGY_CARD)
                            {
                                allergyCard = (MOS.EFMODEL.DataModels.V_HIS_ALLERGY_CARD)entity[i];
                            }
                        }
                    }
                }

                if (treatmentId != null && moduleData != null)
                {
                    result= new frmAllergyCardCreate(moduleData, (long)treatmentId);
                }
                else if (allergyCard != null && moduleData != null)
                {
                    result = new frmAllergyCardCreate(moduleData, allergyCard);
                }
                return result;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }
    }
}
