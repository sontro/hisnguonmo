using Inventec.Core;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.RegisterV2;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System.Windows.Forms;
using HIS.Desktop.Plugins.RegisterV2.Run2;

namespace HIS.Desktop.Plugins.RegisterV2.Run2
{
    public sealed class RunBehavior : Tool<IDesktopToolContext>, IRun
    {
        object[] entity;
        public RunBehavior()
            : base()
        {
        }

        public RunBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object IRun.Run()
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

                HIS.Desktop.Plugins.RegisterV2.GlobalStore.CurrentModule = moduleData;

                //Bắt buộc các chỗ sử dụng module này phải truyền vào phòng đang làm việc (roomId)
                if (moduleData == null) throw new NullReferenceException("moduleData");
                if (moduleData.RoomId <= 0) throw new NullReferenceException("moduleData.RoomId = " + moduleData.RoomId);

                return new UCRegister(moduleData);                
            }
            catch (NullReferenceException ex)
            {
                Inventec.Common.Logging.LogSystem.Error("Factory khong khoi tao duoc doi tuong." + entity.GetType().ToString() + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => entity), entity), ex);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return null;
        }
    }
}
