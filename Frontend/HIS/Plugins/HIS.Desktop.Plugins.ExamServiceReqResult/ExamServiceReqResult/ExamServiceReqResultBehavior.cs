using Inventec.Core;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.ExamServiceReqResult;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System.Windows.Forms;
using MOS.SDO;
using HIS.Desktop.Plugins.ExamServiceReqResult.Run;
using MOS.EFMODEL.DataModels;

namespace HIS.Desktop.Plugins.ExamServiceReqResult.ExamServiceReqResult
{
    public sealed class ExamServiceReqResultBehavior : Tool<IDesktopToolContext>, IExamServiceReqResult
    {
        object[] entity;
        Inventec.Desktop.Common.Modules.Module currentModule;
        long sereServId;
        public ExamServiceReqResultBehavior()
            : base()
        {
        }

        public ExamServiceReqResultBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object IExamServiceReqResult.Run()
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
                        else if (item is long)
                        {
                            sereServId = (long)item;
                        }
                        if (currentModule != null && sereServId > 0)
                        {
                            result = new frmExamServiceReqResult(currentModule, sereServId);
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
