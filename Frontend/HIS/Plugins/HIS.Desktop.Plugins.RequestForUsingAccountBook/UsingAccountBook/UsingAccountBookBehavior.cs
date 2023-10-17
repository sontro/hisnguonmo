using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.RequestForUsingAccountBook.UsingAccountBook
{
    class UsingAccountBookBehavior : Tool<IDesktopToolContext>, IUsingAccountBook
    {
        Inventec.Desktop.Common.Modules.Module Module;
        public UsingAccountBookBehavior()
            : base()
        {
        }

        public UsingAccountBookBehavior(Inventec.Desktop.Common.Modules.Module module, CommonParam param)
            : base()
        {
            this.Module = module;
        }

        object IUsingAccountBook.Run()
        {
            try
            {
                return new frmRequestForUsingAccountBook(this.Module);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                //param.HasException = true;
                return null;
            }
        }
    }
}
