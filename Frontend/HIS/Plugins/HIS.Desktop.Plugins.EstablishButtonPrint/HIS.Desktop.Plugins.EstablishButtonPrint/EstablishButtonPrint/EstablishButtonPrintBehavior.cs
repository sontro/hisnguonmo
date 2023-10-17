using HIS.Desktop.Common;
using Inventec.Core;
using System;
using System.Linq;

namespace HIS.Desktop.Plugins.EstablishButtonPrint
{
    class HisBedTypeBehavior : BusinessBase, IEstablishButtonPrint
    {
        object[] entity;
        internal HisBedTypeBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object IEstablishButtonPrint.Run()
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

                return new frmEstablishButtonPrint(moduleData);
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
