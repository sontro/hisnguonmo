using Inventec.Core;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.PublicMedicineByDate;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System.Windows.Forms;
using MOS.SDO;
using MOS.EFMODEL.DataModels;

namespace HIS.Desktop.Plugins.PublicMedicineByDate.PublicMedicineByDate
{
    public sealed class PublicMedicineByDateBehavior : Tool<IDesktopToolContext>, IPublicMedicineByDate
    {
        object[] entity;
        Inventec.Desktop.Common.Modules.Module currentModule;
        L_HIS_TREATMENT_BED_ROOM currentTreatment;
        long treatmentId = 0;

        public PublicMedicineByDateBehavior()
            : base()
        {
        }

        public PublicMedicineByDateBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object IPublicMedicineByDate.Run()
        {
            object result = null;
            try
            {
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
                        else if (item is long)
                        {
                            this.treatmentId = (long)item;
                        }
                    }

                    if (currentModule != null && currentTreatment != null)
                    {
                        result = new frmPublicMedicinesByDate(currentModule, currentTreatment);
                    }
                    else if (currentModule != null && this.treatmentId > 0)
                    {
                        result = new frmPublicMedicinesByDate(currentModule, this.treatmentId);
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
