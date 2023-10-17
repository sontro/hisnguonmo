using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using HIS.Desktop.ADO;

namespace HIS.Desktop.Plugins.UploadFileAUP.SignAUP
{
    class SignAUPBehavior : Tool<IDesktopToolContext>, ISignAUP
    {
        object[] entity;

        internal SignAUPBehavior()
            : base()
        { }

        internal SignAUPBehavior(Inventec.Core.CommonParam param, object[] data)
            : base()
        {
            entity = data;
        }

        object ISignAUP.Run()
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

                return new HIS.Desktop.Plugins.UploadFileAUP.UpdateAUP(moduleData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return null;
            }
        }
    }
}
