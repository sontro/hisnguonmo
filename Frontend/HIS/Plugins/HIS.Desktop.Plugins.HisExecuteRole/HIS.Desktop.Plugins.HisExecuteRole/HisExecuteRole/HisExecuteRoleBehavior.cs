using Inventec.Core;
using Inventec.Desktop.Common;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisExecuteRole.HisExecuteRole
{
    class HisExecuteRoleBehavior : BusinessBase, IHisExecuteRole
    {
        object[] entity;
        internal HisExecuteRoleBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object IHisExecuteRole.Run()
        {
            try
            {
                Inventec.Desktop.Common.Modules.Module moduleData = null;
                bool isDebate = false;
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
                            else if (entity[i] is bool)
                            {
                                isDebate = (bool)entity[i];
                            }
                        }
                    }
                }

                return new frmHisExecuteRole(moduleData, isDebate);
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
