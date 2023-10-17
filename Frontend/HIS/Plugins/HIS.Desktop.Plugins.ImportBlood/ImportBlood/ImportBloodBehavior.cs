using HIS.Desktop.Common;
using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ImportBlood.ImportBlood
{
    public sealed class ImportBloodBehavior : Tool<IDesktopToolContext>, IImportBlood
    {
        object[] entity;
        Inventec.Desktop.Common.Modules.Module moduleData = null;
        long impMestId = 0;
        public ImportBloodBehavior()
            : base()
        {

        }

        public ImportBloodBehavior(CommonParam param, object[] data)
            : base()
        {
            entity = data;
        }

        object IImportBlood.Run()
        {
            object result = null;
            DelegateSelectData delegateSelectData = null;
            try
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
                            impMestId = (long)entity[i];
                        }
                        else if (entity[i] is DelegateSelectData)
                        {
                            delegateSelectData = (DelegateSelectData)entity[i];
                        }
                    }
                }
                if (moduleData != null && impMestId > 0)
                {
                    return new FrmImportBlood(moduleData, impMestId, delegateSelectData);
                }
                else if (moduleData != null)
                {
                    return new UCImportBloodPlus(moduleData);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }
            return result;
        }
    }
}
