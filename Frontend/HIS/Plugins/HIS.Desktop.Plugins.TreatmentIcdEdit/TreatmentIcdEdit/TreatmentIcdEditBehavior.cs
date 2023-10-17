using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TreatmentIcdEdit.TreatmentIcdEdit
{
    class TreatmentIcdEditBehavior : Tool<IDesktopToolContext>, ITreatmentIcdEdit
    {
        object[] entity;
        internal TreatmentIcdEditBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object ITreatmentIcdEdit.Run()
        {
            Inventec.Desktop.Common.Modules.Module moduleData = new Inventec.Desktop.Common.Modules.Module();
            long treatmentId = 0;
            HIS.Desktop.Common.RefeshReference refreshData = null;
            try
            {
                if (entity != null && entity.Count() > 0)
                {
                    foreach (var item in entity)
                    {
                        if (item is Inventec.Desktop.Common.Modules.Module)
                            moduleData = (Inventec.Desktop.Common.Modules.Module)item;
                        if (item is long)
                            treatmentId = (long)item;
                        if (item is HIS.Desktop.Common.RefeshReference)
                        {
                            refreshData = (HIS.Desktop.Common.RefeshReference)item;
                        }
                    }
                }
                if (moduleData != null && treatmentId != 0)
                {
                    return new FormTreatmentIcdEdit(treatmentId, refreshData, moduleData);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return null;
            }
        }
    }
}
