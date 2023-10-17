using Inventec.Core;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.ConfirmPresBlood;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System.Windows.Forms;
using MOS.SDO;

namespace HIS.Desktop.Plugins.ConfirmPresBlood.ConfirmPresBlood
{
    public sealed class ConfirmPresBloodBehavior : Tool<IDesktopToolContext>, IConfirmPresBlood
    {
        object[] entity;
        Inventec.Desktop.Common.Modules.Module currentModule;
        long expMestId = 0;
        public ConfirmPresBloodBehavior()
            : base()
        {
        }

        public ConfirmPresBloodBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object IConfirmPresBlood.Run()
        {
            object result = null;
            DelegateSelectData delegateSelectData = null;
            MOS.EFMODEL.DataModels.V_HIS_EXP_MEST_2 expMest = null;
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
                        else if (item is long)
                        {
                            expMestId = (long)item;
                        }
                        else if (item is DelegateSelectData)
                        {
                            delegateSelectData = (DelegateSelectData)item;
                        }
                        else if (item is MOS.EFMODEL.DataModels.V_HIS_EXP_MEST_2)
                        {
                            expMest = (MOS.EFMODEL.DataModels.V_HIS_EXP_MEST_2)item;
                        }
                    }
                    if (currentModule != null && expMestId > 0)
                    {
                        result = new frmConfirmPresBlood(currentModule, expMestId, delegateSelectData);
                    }
                    else if (currentModule != null && expMest != null)
                    {
                        result = new frmConfirmPresBlood(currentModule, expMest, delegateSelectData);
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
