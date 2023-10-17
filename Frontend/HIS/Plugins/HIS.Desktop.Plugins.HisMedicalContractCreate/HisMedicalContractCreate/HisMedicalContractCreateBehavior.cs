using HIS.Desktop.Common;
using HIS.Desktop.Plugins.HisMedicalContractCreate.Run;
using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisMedicalContractCreate.HisMedicalContractCreate
{
    class HisMedicalContractCreateBehavior : Tool<IDesktopToolContext>, IHisMedicalContractCreate
    {
        object[] entity;
        public HisMedicalContractCreateBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object IHisMedicalContractCreate.Run()
        {
            object result = null;
            try
            {
                if (entity != null && entity.Count() > 0)
                {
                    Inventec.Desktop.Common.Modules.Module module = null;
                    DelegateRefreshData refreshData = null;
                    long medicalContractId = 0;
                    foreach (var item in entity)
                    {
                        if (item is Inventec.Desktop.Common.Modules.Module)
                        {
                            module = (Inventec.Desktop.Common.Modules.Module)item;
                        }
                        else if (item is long)
                        {
                            medicalContractId = (long)item;
                        }
                        else if (item is DelegateRefreshData)
                        {
                            refreshData = (DelegateRefreshData)item;
                        }
                    }

                    result = new FormHisMedicalContractCreate(module, medicalContractId, refreshData);
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
