using Inventec.Core;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.AnticipateDetail;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System.Windows.Forms;

namespace Inventec.Desktop.Plugins.AnticipateDetail.AnticipateDetail
{
    public sealed class AnticipateDetailBehavior : Tool<IDesktopToolContext>, IAnticipateDetail
    {
        object entity;
        public AnticipateDetailBehavior()
            : base()
        {
        }

        public AnticipateDetailBehavior(CommonParam param, object filter)
            : base()
        {
            this.entity = filter;
        }

        object IAnticipateDetail.Run()
        {
            try
            {
                return new frmAnticipateDetail();
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
