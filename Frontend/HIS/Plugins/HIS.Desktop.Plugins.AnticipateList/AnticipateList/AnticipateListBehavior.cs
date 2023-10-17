using Inventec.Core;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.AnticipateList;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System.Windows.Forms;
using MOS.EFMODEL.DataModels;

namespace Inventec.Desktop.Plugins.AnticipateList.AnticipateList
{
    public sealed class AnticipateListBehavior : Tool<IDesktopToolContext>, IAnticipateList
    {
        object[] entity;
           

        public AnticipateListBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object IAnticipateList.Run()
        {
            try
            {
                Inventec.Desktop.Common.Modules.Module moduleData = null;     
                if (entity != null && entity.Count() > 0)
                {
                    for (int i = 0; i < entity.Count(); i++)
                    {
                        if (entity[i] is Inventec.Desktop.Common.Modules.Module)
                        {
                            moduleData = (Inventec.Desktop.Common.Modules.Module)entity[i];
                        }
                    }
                }
                if (moduleData != null)
                {
                    return new UCAnticipateList(moduleData);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                //param.HasException = true;
                return null;
            }
        }
       
    }
}
