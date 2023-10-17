using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TransactionBillTwoInOne.TransactionBillTwoInOne
{
    class TransactionBillTwoInOneBehavior : Tool<IDesktopToolContext>, ITransactionBillTwoInOne
    {
        V_HIS_TREATMENT_FEE treatment = null;
        List<V_HIS_SERE_SERV_5> listSereServ = null;
        V_HIS_PATIENT_TYPE_ALTER patientTypeAlter;
        Inventec.Desktop.Common.Modules.Module Module;
        bool? IsBill = null;
        internal TransactionBillTwoInOneBehavior()
            : base()
        {

        }

        internal TransactionBillTwoInOneBehavior(Inventec.Desktop.Common.Modules.Module module, CommonParam param, V_HIS_TREATMENT_FEE data, List<V_HIS_SERE_SERV_5> servServ5s, V_HIS_PATIENT_TYPE_ALTER patientTypeAlter, bool? isBill)
            : base()
        {
            this.Module = module;
            this.treatment = data;
            this.listSereServ = servServ5s;
            this.patientTypeAlter = patientTypeAlter;
            this.IsBill = isBill;
        }
        internal TransactionBillTwoInOneBehavior(Inventec.Desktop.Common.Modules.Module module, CommonParam param, bool? isBill)
            : base()
        {
            this.Module = module;
            this.IsBill = isBill;
        }

        object ITransactionBillTwoInOne.Run()
        {
            object result = null;
            try
            {
                if (treatment != null)
                {
                    result = new frmTransactionBillTwoInOne(Module, treatment, this.listSereServ, this.IsBill);
                    if (result == null) throw new NullReferenceException(Inventec.Common.Logging.LogUtil.TraceData("treatment", treatment));
                }
                else
                {
                    result = new frmTransactionBillTwoInOne(Module, this.IsBill);
                }
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
