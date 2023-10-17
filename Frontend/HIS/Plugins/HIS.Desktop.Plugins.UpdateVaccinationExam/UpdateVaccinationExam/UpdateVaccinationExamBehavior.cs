using Inventec.Core;
using Inventec.Desktop.Common;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.UpdateVaccinationExam.Run;

namespace HIS.Desktop.Plugins.UpdateVaccinationExam
{
    class UpdateVaccinationExamBehavior : BusinessBase, IUpdateVaccinationExam
    {
        object[] entity;
        internal UpdateVaccinationExamBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object IUpdateVaccinationExam.Run()
        {
            try
            {
                Inventec.Desktop.Common.Modules.Module moduleData = null;
                long vaccinationExamId = 0;
                DelegateSelectData delegateSelectData = null;

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
                            else if (entity[i] is long)
                            {
                                vaccinationExamId = (long)entity[i];
                            }
                            else if (entity[i] is DelegateSelectData)
                            {
                                delegateSelectData = (DelegateSelectData)entity[i];
                            }
                        }
                    }
                }
                if (moduleData != null && vaccinationExamId > 0 && delegateSelectData != null)
                {
                    return new frmUpdateVaccinationExam(moduleData, vaccinationExamId, delegateSelectData);
                }
                else if (moduleData != null && vaccinationExamId > 0)
                {
                    return new frmUpdateVaccinationExam(moduleData, vaccinationExamId);
                }
                else
                {
                    return null;
                }
                
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
