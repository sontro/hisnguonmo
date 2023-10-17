using HIS.Desktop.ADO;
using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HIS.Desktop.Plugins.HisNumOrderBlockChooser.HisNumOrderBlockChooser
{
    class HisNumOrderBlockChooserBehavior : Tool<IDesktopToolContext>, IHisNumOrderBlockChooser
    {
        object[] entity;
        public HisNumOrderBlockChooserBehavior()
            : base()
        {
        }

        public HisNumOrderBlockChooserBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object IHisNumOrderBlockChooser.Run()
        {
            object result = null;
            try
            {
                if (entity != null && entity.Count() > 0)
                {
                    Inventec.Desktop.Common.Modules.Module currentModule = null;
                    NumOrderBlockChooserADO numOrderBlockChooser = null;
                    bool? isNeedTime = null;
                    foreach (var item in entity)
                    {
                        if (item is Inventec.Desktop.Common.Modules.Module)
                        {
                            currentModule = (Inventec.Desktop.Common.Modules.Module)item;
                        }
                        else if (item is NumOrderBlockChooserADO)
                        {
                            numOrderBlockChooser = (NumOrderBlockChooserADO)item;
                        }
                        else if (item is bool?)
                        {
                            isNeedTime = (bool?)item;
                        }
                    }
                    if (isNeedTime == true)
                    {
                        result = new Run.FormHisNumOrderBlockChooser(currentModule, numOrderBlockChooser,isNeedTime);
                    }
                    else
                    {
                        result = new Run.FormHisNumOrderBlockChooser(currentModule, numOrderBlockChooser);
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
