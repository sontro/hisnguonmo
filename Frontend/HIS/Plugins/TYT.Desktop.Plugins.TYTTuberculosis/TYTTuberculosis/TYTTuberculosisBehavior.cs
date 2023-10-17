using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using MOS.EFMODEL.DataModels;
using TYT.EFMODEL.DataModels;

namespace TYT.Desktop.Plugins.TYTTuberculosis.TYTTuberculosis
{
    class TYTTuberculosisBehavior : Tool<IDesktopToolContext>, ITYTTuberculosis
    {
        object[] entity;
        Inventec.Desktop.Common.Modules.Module currentModule;

        internal TYTTuberculosisBehavior()
            : base()
        {

        }

        internal TYTTuberculosisBehavior(CommonParam param, object[] data)
            : base()
        {
            entity = data;
        }

        object ITYTTuberculosis.Run()
        {
            object result = null;
            try
            {
                V_HIS_PATIENT Patient = new V_HIS_PATIENT();
                TYT_TUBERCULOSIS _TYT_KHH = new TYT_TUBERCULOSIS();
                if (entity != null && entity.Count() > 0)
                {
                    foreach (var item in entity)
                    {
                        if (item is TYT_TUBERCULOSIS)
                        {
                            _TYT_KHH = (TYT_TUBERCULOSIS)item;
                        }
                        else if (item is Inventec.Desktop.Common.Modules.Module)
                        {
                            currentModule = (Inventec.Desktop.Common.Modules.Module)item;
                        }
                        else if (item is V_HIS_PATIENT)
                        {
                            Patient = (V_HIS_PATIENT)item;
                        }
                    }

                    if (currentModule != null && _TYT_KHH != null && _TYT_KHH.ID > 0)
                    {
                        result = new frm(currentModule, _TYT_KHH);
                    }
                    else if (currentModule != null && Patient != null)
                    {
                        result = new frm(currentModule, Patient);
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
