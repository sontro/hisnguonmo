using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;

namespace HIS.Desktop.Plugins.ExaminationReqEdit.ExaminationReqEdit
{
    class ExaminationReqEditBehavior : Tool<IDesktopToolContext>, IExaminationReqEdit
    {
        object[] entity;

        internal ExaminationReqEditBehavior()
            : base()
        {

        }

        internal ExaminationReqEditBehavior(CommonParam param, object[] data)
            : base()
        {
            entity = data;
        }

        object IExaminationReqEdit.Run()
        {
            try
            {
                Inventec.Desktop.Common.Modules.Module moduleData = null;
                if (entity != null && entity.Count() > 0)
                {
                    foreach (var item in entity)
                    {
                        if (item is Inventec.Desktop.Common.Modules.Module)
                            moduleData = (Inventec.Desktop.Common.Modules.Module)item;
                    }
                }
                if (moduleData != null)
                {
                    return new FormExaminationReqEdit(moduleData);
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
