using Inventec.Core;
using Inventec.Desktop.Common;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAR.Desktop.Plugins.SarRetyFofi
{
    class SarRetyFofiBehavior : BusinessBase, ISarRetyFofi
    {
        object[] entity;
        internal SarRetyFofiBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object ISarRetyFofi.Run()
        {
            try
            {
                Inventec.Desktop.Common.Modules.Module moduleData = null;

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
                        }
                    }
                }

                string vlCustomFormType = HIS.Desktop.LocalStorage.HisConfig.HisConfigs.Get<string>("HIS.Desktop.Plugins.ReportCreate.CustomFormType");
                if (vlCustomFormType == "1")
                {
                    return new frmSarRetyFofi1(moduleData);
                }
                else
                    return new frmSarRetyFofi(moduleData);
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
