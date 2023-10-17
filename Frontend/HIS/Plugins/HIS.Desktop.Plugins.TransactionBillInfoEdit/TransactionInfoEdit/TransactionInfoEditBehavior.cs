using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.TransactionBillInfoEdit;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System.Windows.Forms;
using MOS.EFMODEL.DataModels;

namespace HIS.Desktop.Plugins.TransactionBillInfoEdit.TransactionBillInfoEdit
{
    public sealed class TransactionBillInfoEditBehavior : Tool<IDesktopToolContext>, ITransactionBillInfoEdit
    {
        object[] entity;
        Inventec.Desktop.Common.Modules.Module currentModule;
        public TransactionBillInfoEditBehavior()
            : base()
        {
        }

        public TransactionBillInfoEditBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object ITransactionBillInfoEdit.Run()
        {
            object result = null;
            try
            {
                if (entity != null && entity.Count() > 0)
                {
                    V_HIS_INVOICE _transaction = null;
                    HIS.Desktop.Common.DelegateRefreshData _dlg = null;
                    foreach (var item in entity)
                    {
                        if (item is Inventec.Desktop.Common.Modules.Module)
                        {
                            currentModule = (Inventec.Desktop.Common.Modules.Module)item;
                        }
                        else if (item is V_HIS_INVOICE)
                        {
                            _transaction = (V_HIS_INVOICE)item;
                        }
                        else if (item is HIS.Desktop.Common.DelegateRefreshData)
                        {
                            _dlg = (HIS.Desktop.Common.DelegateRefreshData)item;
                        }
                    }
                    if (currentModule != null && _transaction != null)
                    {
                        if (_dlg != null)
                            result = new frmTransactionBillInfoEdit(currentModule, _transaction, _dlg);
                        else
                            result = new frmTransactionBillInfoEdit(currentModule, _transaction);
                    }
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
