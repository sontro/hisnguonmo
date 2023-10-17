using Inventec.Core;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.TestDeviceSample;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System.Windows.Forms;
using MOS.EFMODEL.DataModels;
using HIS.Desktop.Plugins.TestDeviceSample.Base;

namespace Inventec.Desktop.Plugins.TestDeviceSample.TestDeviceSample
{
    public sealed class TestDeviceSampleBehavior : Tool<IDesktopToolContext>, ITestDeviceSample
    {
        object[] entity;
        bool isExecuteRoom = false;

        public TestDeviceSampleBehavior()
            : base()
        {
        }

        public TestDeviceSampleBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object ITestDeviceSample.Run()
        {
            object result = null;
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
                return new frmTestDeviceSample(moduleData);
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
