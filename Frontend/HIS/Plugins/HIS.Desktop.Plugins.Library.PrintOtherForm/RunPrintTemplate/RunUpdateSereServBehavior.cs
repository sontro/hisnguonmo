using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.Library.PrintOtherForm.RunPrintTemplate
{
    public class RunUpdateSereServBehavior : IRunTemp
    {
        public RunUpdateSereServBehavior()
            : base()
        {

        }

        bool IRunTemp.Run(string printTypeCode)
        {
            bool result = false;
            try
            {

            }
            catch (Exception ex)
            {
                result = false;
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
            return result;
        }
    }
}
