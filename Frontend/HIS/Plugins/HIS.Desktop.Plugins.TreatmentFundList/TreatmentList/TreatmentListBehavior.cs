using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TreatmentFundList.TreatmentList
{
    class TreatmentListBehavior : Tool<IDesktopToolContext>, ITreatmentList
    {
        Inventec.Desktop.Common.Modules.Module Module;
        //V_HIS_TREATMENT_FEE_2D treatment = null;
        //V_HIS_ACCOUNT_BOOK accountBook = null;
        long FundId = 0;

        internal TreatmentListBehavior()
            : base()
        {

        }

        internal TreatmentListBehavior(Inventec.Desktop.Common.Modules.Module module, CommonParam param)
            : base()
        {
            Module = module;
        }

        internal TreatmentListBehavior(Inventec.Desktop.Common.Modules.Module module, CommonParam param, long data)
            : base()
        {
            Module = module;
            FundId = data;
        }

        object ITreatmentList.Run()
        {
            object result = null;
            try
            {

                if (FundId >0)
                {
                    result = new frmTreatmentList(Module, FundId);
                }
                else
                {
                    result = new frmTreatmentList(Module);
                }
                if (result == null) throw new NullReferenceException(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => Module), Module));
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
