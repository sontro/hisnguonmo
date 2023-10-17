using HIS.Desktop.Common;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisCareTemp.CareTemp
{
    class CareTempBehavior : BusinessBase, ICareTemp
    {
        object[] entity;
        internal CareTempBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object ICareTemp.Run()
        {
            try
            {
                Inventec.Desktop.Common.Modules.Module moduleData = null;
                HIS_CARE_TEMP data = null;

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
                            if (entity[i] is HIS_CARE_TEMP)
                            {
                                data = (HIS_CARE_TEMP)entity[i];
                            }
                        }
                    }
                }

                return new FormCareTemp(moduleData, data);
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
