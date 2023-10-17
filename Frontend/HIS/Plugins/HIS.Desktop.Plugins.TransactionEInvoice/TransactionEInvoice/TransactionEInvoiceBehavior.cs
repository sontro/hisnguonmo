using HIS.Desktop.Common;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HIS.Desktop.Plugins.TransactionEInvoice.TransactionEInvoice
{
    class TransactionEInvoiceBehavior : BusinessBase, ITransactionEInvoice
    {
        object[] entity;
        internal TransactionEInvoiceBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object ITransactionEInvoice.Run()
        {
            try
            {
                Inventec.Desktop.Common.Modules.Module moduleData = null;
                V_HIS_TRANSACTION currentTransaction = new V_HIS_TRANSACTION();

                if (entity.GetType() == typeof(object[]))
                {
                    if (entity != null && entity.Count() > 0)
                    {
                        for (int i = 0; i < entity.Count(); i++)
                        {
                            if (entity[i] is Inventec.Desktop.Common.Modules.Module)
                            {
                                moduleData = (Inventec.Desktop.Common.Modules.Module)entity[i];
                            }
                            if (entity[i] is V_HIS_TRANSACTION)
                            {
                                currentTransaction = (V_HIS_TRANSACTION)entity[i];
                            }
                        }
                    }
                }

                return new frmTransactionEInvoice(moduleData, currentTransaction);
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
