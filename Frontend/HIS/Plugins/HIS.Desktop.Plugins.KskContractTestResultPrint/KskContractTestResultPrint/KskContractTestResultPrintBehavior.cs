using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.KskContractTestResultPrint.KskContractTestResultPrint
{
    class KskContractTestResultPrintBehavior : Tool<IDesktopToolContext>, IKskContractTestResultPrint
    {
        Inventec.Desktop.Common.Modules.Module Module;
        internal KskContractTestResultPrintBehavior()
            : base()
        {

        }

        internal KskContractTestResultPrintBehavior(Inventec.Desktop.Common.Modules.Module moduleData, CommonParam param)
            : base()
        {
            Module = moduleData;
        }

        object IKskContractTestResultPrint.Run()
        {
            object result = null;
            try
            {
                result = new frmKskContractTestResultPrint(Module);
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
