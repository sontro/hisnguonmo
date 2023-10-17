using Inventec.Core;
using Inventec.Desktop.Common;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TreatmentFundList.FundPay
{
    class FundPayBehavior : BusinessBase, IFundPay
    {
        object entity;
        internal FundPayBehavior(CommonParam param, object filter)
            : base()
        {
            this.entity = filter;
        }

        object IFundPay.Run()
        {
            try
            {
                return null;
                //if (this.entity is long)
                //{
                //    return new frmFundPay((long)this.entity);
                //}
                //else if (this.entity is List<long>)
                //{
                //    return new frmFundPay((List<long>)this.entity);
                //}
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }
    }
}
