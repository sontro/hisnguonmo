using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Desktop.Core;
using Inventec.Core;
using HIS.Desktop.Common;

namespace HIS.Desktop.Plugins.HisRationSumList.HisRationSumList
{
    class HisRationSumListBehavior : BusinessBase, IHisRationSumList
    {
        object[] entity;
        internal HisRationSumListBehavior(CommonParam param, object[] filter)
            : base()
        {
            try
            {
                this.entity = filter;
            }
            catch (Exception ex)
            {

                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
        object IHisRationSumList.Run()
        {
            Inventec.Desktop.Common.Modules.Module module = null;
            try
            {
                if (this.entity != null && this.entity.Count() > 0 && this.entity.GetType() == typeof(object[]))
                {
                    foreach (var item in this.entity)
                    {
                        if (item is Inventec.Desktop.Common.Modules.Module)
                            module = (Inventec.Desktop.Common.Modules.Module)item;
                    }
                    
                }
                if (module != null)
                    return new HIS.Desktop.Plugins.HisRationSumList.HisRationSumList.UcHisRationSumList(module);
                else
                    return new HIS.Desktop.Plugins.HisRationSumList.HisRationSumList.UcHisRationSumList();
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
