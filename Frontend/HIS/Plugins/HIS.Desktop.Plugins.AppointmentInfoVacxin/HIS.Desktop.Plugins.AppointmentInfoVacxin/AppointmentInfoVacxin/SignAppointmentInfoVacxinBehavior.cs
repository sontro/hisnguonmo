
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;

namespace HIS.Desktop.Plugins.AppointmentInfoVacxin.AppointmentInfoVacxin
{
    class SignAppointmentInfoVacxinBehavior : Tool<IDesktopToolContext>, ISignAppointmentInfoVacxin
    {
        object[] entity;

    internal SignAppointmentInfoVacxinBehavior()
            : base()
        { }

    internal SignAppointmentInfoVacxinBehavior(Inventec.Core.CommonParam param, object[] data)
            : base()
        {
        entity = data;
    }

    object ISignAppointmentInfoVacxin.Run()
    {
        try
        {
            Inventec.Desktop.Common.Modules.Module moduleData = null;

            if (entity.GetType() == typeof(object[]))
            {
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
            }

            return new HIS.Desktop.Plugins.AppointmentInfoVacxin.AppointmentInfoVacxinTiem(moduleData);
        }
        catch (Exception ex)
        {
            Inventec.Common.Logging.LogSystem.Error(ex);
            return null;
        }
    }
}

