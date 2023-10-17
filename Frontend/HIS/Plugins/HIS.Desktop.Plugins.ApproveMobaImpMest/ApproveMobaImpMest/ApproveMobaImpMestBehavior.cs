using Inventec.Core;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System.Windows.Forms;
using MOS.SDO;

namespace HIS.Desktop.Plugins.ApproveMobaImpMest.ApproveMobaImpMest
{
    public sealed class ApproveMobaImpMestBehavior : Tool<IDesktopToolContext>, IApproveMobaImpMest
    {
        object[] entity;
        Inventec.Desktop.Common.Modules.Module currentModule;
        long expMestId = 0;
        public ApproveMobaImpMestBehavior()
            : base()
        {
        }

        public ApproveMobaImpMestBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object IApproveMobaImpMest.Run()
        {
            object result = null;
            MOS.EFMODEL.DataModels.V_HIS_IMP_MEST _HisImpMest = null;
            DelegateSelectData delegateCloseForm = null;
            try
            {
                if (entity != null && entity.Count() > 0)
                {
                    foreach (var item in entity)
                    {
                        if (item is Inventec.Desktop.Common.Modules.Module)
                        {
                            currentModule = (Inventec.Desktop.Common.Modules.Module)item;
                        }
                        else if (item is MOS.EFMODEL.DataModels.V_HIS_IMP_MEST)
                        {
                            _HisImpMest = (MOS.EFMODEL.DataModels.V_HIS_IMP_MEST)item;
                        }
                        else if (item is DelegateSelectData)
                        {
                            delegateCloseForm = (DelegateSelectData)item;
                        }
                    }

                    if (delegateCloseForm != null)
                    {
                        result = new frmApproveMobaImpMest(_HisImpMest, delegateCloseForm, currentModule);
                    }
                    else
                    {
                        result = new frmApproveMobaImpMest(_HisImpMest, currentModule);
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
