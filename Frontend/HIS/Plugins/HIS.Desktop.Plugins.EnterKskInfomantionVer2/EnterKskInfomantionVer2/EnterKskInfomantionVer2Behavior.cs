using Inventec.Core;
using Inventec.Desktop.Common;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOS.EFMODEL.DataModels;
using HIS.Desktop.Plugins.EnterKskInfomantionVer2.Run;
namespace HIS.Desktop.Plugins.EnterKskInfomantionVer2.EnterKskInfomantionVer2
{
    public class EnterKskInfomantionVer2Behavior : BusinessBase, IEnterKskInfomantionVer2
    {
        object[] entity;
        internal EnterKskInfomantionVer2Behavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object IEnterKskInfomantionVer2.Run()
        {
            try
            {
                Inventec.Desktop.Common.Modules.Module moduleData = null;
                V_HIS_SERVICE_REQ data = null;
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
                            if (entity[i] is V_HIS_SERVICE_REQ)
                            {
                                data = (V_HIS_SERVICE_REQ)entity[i];
                            }
                        }
                    }
                }

                return new frmEnterKskInfomantionVer2(moduleData, data);
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
