using Inventec.Core;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.CoTreatmentList;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.CoTreatmentList.CoTreatmentList
{
    public sealed class CoTreatmentListBehavior : Tool<IDesktopToolContext>, ICoTreatmentList
    {
        object[] entity;
        Inventec.Desktop.Common.Modules.Module currentModule;
        public CoTreatmentListBehavior()
            : base()
        {
        }

        public CoTreatmentListBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object ICoTreatmentList.Run()
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
                        
                        if (currentModule != null)
                        {
                            result = new UCCoTreatmentList(currentModule);
                            break;
                        }
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
