using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using Inventec.Core;

namespace HIS.Desktop.Plugins.InfomationExecute.InfomationExecute
{
    public class InfomationExecuteBehavior : Tool<IDesktopToolContext>, IInfomationExecute
    {
        object[] entity;
        Inventec.Desktop.Common.Modules.Module currentModule;
        long treatmentId = 0;
        bool returnObject = false;
        List<string> content = null;
        public InfomationExecuteBehavior()
            : base()
        {
        }
        public InfomationExecuteBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }
        object IInfomationExecute.Run()
        {
            object result = null;
            try
            {
                if (entity != null && entity.Count() > 0)
                {
                    HIS.Desktop.Common.DelegateSelectData _DelegateSelectData = null;

                    foreach (var item in entity)
                    {
                        if (item is Inventec.Desktop.Common.Modules.Module)
                        {
                            currentModule = (Inventec.Desktop.Common.Modules.Module)item;
                        }
                        else if (item is long)
                        {
                            treatmentId = (long)item;
                        }
                        else if (item is HIS.Desktop.Common.DelegateSelectData)
                        {
                            _DelegateSelectData = (HIS.Desktop.Common.DelegateSelectData)item;
                        }
                        else if (item is bool)
                        {
                            returnObject = (bool)item;
                        }
                        else if (item is List<string>)
                        {
                            content = (List<string>)item;
                        }

                    }
                    if (currentModule != null && treatmentId > 0 && _DelegateSelectData != null)
                    {
                        result = new frmInfomationExecute(currentModule, treatmentId, _DelegateSelectData, returnObject,content);
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
