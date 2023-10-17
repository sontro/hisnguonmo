using Inventec.Core;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.HisTreatmentDetailGOV;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System.Windows.Forms;
using MOS.SDO;
using HIS.Desktop.Plugins.HisTreatmentDetailGOV.Run;
using MOS.EFMODEL.DataModels;

namespace HIS.Desktop.Plugins.HisTreatmentDetailGOV.Run
{
    public sealed class HisTreatmentDetailGOVBehavior : Tool<IDesktopToolContext>, IHisTreatmentDetailGOV
    {
        object[] entity;
        Inventec.Desktop.Common.Modules.Module currentModule;

        public HisTreatmentDetailGOVBehavior()
            : base()
        {
        }

        public HisTreatmentDetailGOVBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object IHisTreatmentDetailGOV.Run()
        {
            object result = null;
            try
            {
                if (entity != null && entity.Count() > 0)
                {
                    string maHoSo = "";
                    foreach (var item in entity)
                    {
                        if (item is Inventec.Desktop.Common.Modules.Module)
                        {
                            currentModule = (Inventec.Desktop.Common.Modules.Module)item;
                        }
                        else if (item is string)
                        {
                            maHoSo = (string)item;
                        }
                    }
                    if (currentModule != null && !string.IsNullOrEmpty(maHoSo))
                    {
                        result = new frmHisTreatmentDetailGOV(currentModule, maHoSo);
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
