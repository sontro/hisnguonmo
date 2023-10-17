using Inventec.Core;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Utility;
using SDA.EFMODEL.DataModels;

namespace HIS.Desktop.Plugins.ChanelManager.Run
{
    class ChanelManagerBehavior : BusinessBase, IChanelManager
    {
        object[] entity;
        internal ChanelManagerBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object IChanelManager.Run()
        {
            try
            {
                Inventec.Desktop.Common.Modules.Module module = null;            
                foreach (var item in entity)
                {
                    if (item is Inventec.Desktop.Common.Modules.Module)
                    {
                        module = (Inventec.Desktop.Common.Modules.Module)item;
                    }                   
                }
                return new frmChanelManager(module);
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
