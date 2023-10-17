using Inventec.Core;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.FeeHospitalWarning;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System.Windows.Forms;

namespace Inventec.Desktop.Plugins.FeeHospitalWarning.FeeWarning
{
    public sealed class FeeWarningBehavior : Tool<IDesktopToolContext>, IFeeWarning
    {object[] entity;
        public FeeWarningBehavior()
            : base()
        {
        }

        public FeeWarningBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object IFeeWarning.Run()
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
                if (moduleData != null)
                {
                    return new UCFeeWarning(moduleData);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }
            return result;
        }
        //long roomId;
        //long roomtypeId;
        //object entity;
        //public FeeWarningBehavior()
        //    : base()
        //{
        //}

        //public FeeWarningBehavior(CommonParam param, object filter)
        //    : base()
        //{
        //    this.entity = filter;
        //}

        //object IFeeWarning.Run()
        //{
        //    try
        //    {
        //    //    return new UCFeeWarning(roomId,roomtypeId);
        //    //}
        //        Inventec.Desktop.Common.Modules.Module moduleData = null;
        //        if (entity != null && entity.Count() > 0)
        //        {
        //            for (int i = 0; i < entity.Count(); i++)
        //            {
        //                if (entity[i] is Inventec.Desktop.Common.Modules.Module)
        //                {
        //                    moduleData = (Inventec.Desktop.Common.Modules.Module)entity[i];
        //                }
        //            }
        //        }
        //        if (moduleData != null)
        //        {
        //            return new UCExecuteRoom(moduleData.RoomId, moduleData.RoomTypeId);
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    catch (Exception ex)
        //    {
        //        Inventec.Common.Logging.LogSystem.Error(ex);
        //        //param.HasException = true;
        //        return null;
        //    }
        //}
       
    }
}
