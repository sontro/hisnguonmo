using HIS.Desktop.Plugins.Library.PrintOtherForm.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.Library.PrintOtherForm.RunPrintTemplate
{
    public class RunUpdateFactory
    {
        internal static IRunTemp RunTemplateByUpdateType(object data, UpdateType.TYPE updateType)
        {
            IRunTemp result = null;
            try
            {
                switch (updateType)
                {
                    case UpdateType.TYPE.TREATMENT:
                        result = new RunUpdateTreatmentBehavior(data);
                        break;
                    case UpdateType.TYPE.SERVICE_REQ:
                        result = new RunUpdateServiceReqBehavior(data);
                        break;
                    case UpdateType.TYPE.SERE_SERV:
                        //result = new RunUpdateSereServBehavior();
                        break;
                    default:
                        break;
                }
                if (result == null) throw new NullReferenceException();
            }
            catch (Exception ex)
            {
                result = null;
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
            return result;
        }
    }
}
