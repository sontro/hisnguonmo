using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.AntigenMety.AntigenMetyList
{
    class AntigenMetyListBehavior : Tool<IDesktopToolContext>, IAntigenMetyList
    {
        object entity;
        public AntigenMetyListBehavior()
            : base()
        {
        }

        public AntigenMetyListBehavior(CommonParam param, object filter)
            : base()
        {
            this.entity = filter;
        }

        object IAntigenMetyList.Run()
        {
            try
            {
                return new UCAntigenMetyList();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return null;
            }
        }
    }
}
