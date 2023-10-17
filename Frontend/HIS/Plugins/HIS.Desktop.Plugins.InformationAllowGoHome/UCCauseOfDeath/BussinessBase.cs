using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.InformationAllowGoHome.UCCauseOfDeath
{
    public abstract class BussinessBase : EntityBase
    {
        protected CommonParam param { get; set; }

        public BussinessBase()
            : base()
        {
            param = new CommonParam();
        }

        public BussinessBase(CommonParam paramBusiness)
            : base()
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }
    }
}
