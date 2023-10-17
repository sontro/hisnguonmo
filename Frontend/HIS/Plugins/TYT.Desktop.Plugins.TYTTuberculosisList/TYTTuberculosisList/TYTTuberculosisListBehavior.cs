using Inventec.Core;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TYT.Desktop.Plugins.TYTTuberculosisList;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System.Windows.Forms;

namespace Inventec.Desktop.Plugins.TYTTuberculosisList.TYTTuberculosisList
{
    public sealed class TYTTuberculosisListBehavior : Tool<IDesktopToolContext>, ITYTTuberculosisList
    {
        object[] entity;
        public TYTTuberculosisListBehavior()
            : base()
        {
        }

        public TYTTuberculosisListBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object ITYTTuberculosisList.Run()
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

                ////Bắt buộc các chỗ sử dụng module này phải truyền vào phòng đang làm việc (roomId)
                //if (moduleData == null) throw new NullReferenceException("moduleData");
                //if (moduleData.RoomId <= 0) throw new NullReferenceException("moduleData.RoomId = " + moduleData.RoomId);

                return new UCListTYTTuberculosisList(moduleData);
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
