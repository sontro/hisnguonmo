using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ServSegrList.ServSegrList
{
    class ServSegrListBehavior : Tool<IDesktopToolContext>, IServSegrList
    {
        object entity;
        public ServSegrListBehavior()
            : base()
        {
        }

        public ServSegrListBehavior(CommonParam param, object filter)
            : base()
        {
            this.entity = filter;
        }

        object IServSegrList.Run()
        {
            try
            {
                return new UCServSegrList();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return null;
            }
        }
    }
}
