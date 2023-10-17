using Inventec.Core;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.Debate;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System.Windows.Forms;
using HIS.Desktop.ADO;

namespace Inventec.Desktop.Plugins.Debate.Debate
{
    public sealed class DebateBehavior : Tool<IDesktopToolContext>, IDebate
    {
        object[] entity;
        Inventec.Desktop.Common.Modules.Module currentModule = null;
        long treatmentId;
        public DebateBehavior()
            : base()
        {
        }

        public DebateBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object IDebate.Run()
        {
            object result = null;
            try
            {
                if (entity != null && entity.Count() > 0)
                {
                    bool IsTreatmentList = false;
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
                        else if (item is bool)
                        {
                            IsTreatmentList = (bool)item;
                        }
                    }

                    if (currentModule != null && treatmentId > 0)
                    {
                        result = new frmDebate(currentModule, treatmentId, IsTreatmentList);
                    }
                    else if (currentModule != null)
                    {
                        result = new frmDebate(currentModule, 0);
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
