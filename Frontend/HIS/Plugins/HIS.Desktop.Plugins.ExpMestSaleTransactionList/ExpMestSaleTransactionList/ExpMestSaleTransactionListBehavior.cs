using Inventec.Core;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.ExpMestSaleTransactionList;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System.Windows.Forms;
using MOS.EFMODEL.DataModels;
using HIS.Desktop.ADO;

namespace Inventec.Desktop.Plugins.ExpMestSaleTransactionList.ExpMestSaleTransactionList
{
    public sealed class ExpMestSaleTransactionListBehavior : Tool<IDesktopToolContext>, IExpMestSaleTransactionList
    {
        object[] entity;
        Inventec.Desktop.Common.Modules.Module currentModule;
        public ExpMestSaleTransactionListBehavior()
            : base()
        {
        }

        public ExpMestSaleTransactionListBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object IExpMestSaleTransactionList.Run()
        {
            object result = null;
            try
            {
                HIS.Desktop.Common.DelegateRefreshData _dlgRef = null;
                ExpMestSaleTranADO ado = null;
                if (entity != null && entity.Count() > 0)
                {
                    foreach (var item in entity)
                    {
                        if (item is Inventec.Desktop.Common.Modules.Module)
                        {
                            currentModule = (Inventec.Desktop.Common.Modules.Module)item;
                        }
                        else if (item is DelegateRefreshData)
                        {
                            _dlgRef = (DelegateRefreshData)item;
                        }
                        else if (item is ExpMestSaleTranADO)
                        {
                            ado = (ExpMestSaleTranADO)item;
                        }
                    }
                    if (currentModule != null)
                    {
                        if (ado != null)
                            result = new frmExpMestSaleTransactionList(currentModule, ado);
                        else
                            result = new frmExpMestSaleTransactionList(currentModule);
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
