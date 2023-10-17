using Inventec.Core;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.SecondaryIcd;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System.Windows.Forms;
using HIS.Desktop.Plugins.SecondaryIcd.ADO;
using HIS.Desktop.ADO;
using HIS.Desktop.Plugins.SecondaryIcd.SecondaryIcd;

namespace HIS.Desktop.Plugins.SecondaryIcd.SecondaryIcd
{
    public sealed class SecondaryIcdBehavior : Tool<IDesktopToolContext>, ISecondaryIcd
    {
        object[] entity;

        public SecondaryIcdBehavior()
            : base()
        {
        }

        public SecondaryIcdBehavior(CommonParam param, object[] data)
            : base()
        {
            entity = data;
        }

        object ISecondaryIcd.Run()
        {
            object result = null;
            try
            {
                Inventec.Desktop.Common.Modules.Module module = null;
                HIS.Desktop.ADO.SecondaryIcdADO secondaryIcdADO = null;
                bool isIcdCm = false;
                if (entity != null && entity.Count() > 0)
                {
                    for (int i = 0; i < entity.Count(); i++)
                    {
                        if (entity[i] is Inventec.Desktop.Common.Modules.Module)
                        {
                            module = (Inventec.Desktop.Common.Modules.Module)entity[i];
                        }
                        else if (entity[i] is SecondaryIcdADO)
                        {
                            secondaryIcdADO = (SecondaryIcdADO)entity[i];
                        }
                        else if (entity[i] is bool)
                        {
                            isIcdCm = (bool)entity[i];
                        }
                    }
                }

                if (module != null)
                {
                    result = new frmSecondaryIcd(module, secondaryIcdADO, isIcdCm);
                }
                else
                    result = null;
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
