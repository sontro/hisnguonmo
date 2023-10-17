using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisContactPointList
{
    class HisContactPointListBehavior : Tool<DesktopToolContext>, IHisContactPointList
    {
        object[] entity;
        Inventec.Desktop.Common.Modules.Module currentModule;
        string _TreatmentCode = "";

        internal HisContactPointListBehavior()
            : base()
        {

        }

        public HisContactPointListBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object IHisContactPointList.Run()
        {
            object result = null;
            try
            {
                if (entity != null && entity.Count() > 0)
                {
                    foreach (var item in entity)
                    {
                        if (item is Inventec.Desktop.Common.Modules.Module)
                        {
                            currentModule = (Inventec.Desktop.Common.Modules.Module)item;
                        }
                        else if (item is string)
                        {
                            _TreatmentCode = (string)item;
                        }
                    }
                    if (currentModule != null && !string.IsNullOrEmpty(_TreatmentCode))
                    {
                        result = new UCHisContactPointList(currentModule, _TreatmentCode);
                    }
                    else if (currentModule != null)
                    {
                        result = new UCHisContactPointList(currentModule);
                    }
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
