using Inventec.Core;
using Inventec.Desktop.Common;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOS.EFMODEL.DataModels;

namespace HIS.Desktop.Plugins.HisServiceUnitEdit.HisServiceUnitEdit
{
    class HisServiceUnitEditBehavior : BusinessBase, IHisServiceUnitEdit
    {
        object[] entity;
        internal HisServiceUnitEditBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object IHisServiceUnitEdit.Run()
        {
            HIS_SERVICE_UNIT serviceUnit = new HIS_SERVICE_UNIT();
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
                            if (entity[i] is MOS.EFMODEL.DataModels.HIS_SERVICE_UNIT)
                            {
                                serviceUnit = (MOS.EFMODEL.DataModels.HIS_SERVICE_UNIT)entity[i];
                            }
                        }
                    }
                }
                if (moduleData != null && serviceUnit != null && serviceUnit.ID != 0)
                {
                    return new frmHisServiceUnitEdit(serviceUnit, moduleData);
                }
                return new frmHisServiceUnitEdit(moduleData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }
    }
}
