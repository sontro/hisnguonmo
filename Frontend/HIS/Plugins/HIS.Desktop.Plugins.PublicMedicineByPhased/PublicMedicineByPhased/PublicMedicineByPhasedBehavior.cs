using Inventec.Core;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.PublicMedicineByPhased;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System.Windows.Forms;
using MOS.SDO;
using MOS.EFMODEL.DataModels;

namespace HIS.Desktop.Plugins.PublicMedicineByPhased.PublicMedicineByPhased
{
    public sealed class PublicMedicineByPhasedBehavior : Tool<IDesktopToolContext>, IPublicMedicineByPhased
    {
        object[] entity;
        Inventec.Desktop.Common.Modules.Module currentModule;
        L_HIS_TREATMENT_BED_ROOM currentTreatment;
        public PublicMedicineByPhasedBehavior()
            : base()
        {
        }

        public PublicMedicineByPhasedBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object IPublicMedicineByPhased.Run()
        {
            object result = null;
            try
            {
                V_HIS_TREATMENT_4 _treatment4 = new V_HIS_TREATMENT_4();
                if (entity != null && entity.Count() > 0)
                {
                    foreach (var item in entity)
                    {
                        if (item is Inventec.Desktop.Common.Modules.Module)
                        {
                            currentModule = (Inventec.Desktop.Common.Modules.Module)item;
                        }
                        else if (item is L_HIS_TREATMENT_BED_ROOM)
                        {
                            currentTreatment = (L_HIS_TREATMENT_BED_ROOM)item;
                        }
                        else if (item is V_HIS_TREATMENT_4)
                        {
                            _treatment4 = (V_HIS_TREATMENT_4)item;
                        }
                    }
                    if (currentModule != null && currentTreatment != null)
                    {
                        result = new frmPublicMedicineByPhased(currentModule, currentTreatment);
                    }
                    else
                    {
                        result = new frmPublicMedicineByPhased(currentModule, _treatment4);
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
