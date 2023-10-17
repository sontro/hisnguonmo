using HIS.Desktop.ADO;
using HIS.Desktop.Common;
using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TransactionBillKiosk.TransactionBillKiosk
{
    class TransactionBillKioskBehavior : Tool<IDesktopToolContext>, ITransactionBillKiosk
    {
        HIS_CASHIER_ROOM cashierRoom = null;
        long treatmentId = 0;
        Inventec.Desktop.Common.Modules.Module Module;
        RefeshReference refeshReference = null;
        
        object[] entity;
        internal TransactionBillKioskBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object ITransactionBillKiosk.Run()
        {
            object result = null;
            try
            {
                if (entity != null && entity.Count() > 0)
                {
                    DelegateCloseForm_Uc closingForm = null;
                    for (int i = 0; i < entity.Count(); i++)
                    {
                        if (entity[i] is HIS_CASHIER_ROOM)
                        {
                            cashierRoom = (HIS_CASHIER_ROOM)entity[i];
                        }
                        else if (entity[i] is Inventec.Desktop.Common.Modules.Module)
                        {
                            Module = (Inventec.Desktop.Common.Modules.Module)entity[i];
                        }
                        else if (entity[i] is long)
                        {
                            treatmentId = (long)entity[i];
                        }
                        else if (entity[i] is RefeshReference)
                        {
                            refeshReference = (RefeshReference)entity[i];
                        }
                        else if (entity[i] is DelegateCloseForm_Uc)
                        {
                            closingForm = (DelegateCloseForm_Uc)entity[i];
                        }
                    }

                    result = new frmTransactionBillKiosk(Module, treatmentId, cashierRoom, closingForm);
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
