using Inventec.Core;
using Inventec.Desktop.Common;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOS.EFMODEL.DataModels;

namespace HIS.Desktop.Plugins.HisMedicineTypeTut.HisMedicineTypeTut
{
    class HisMedicineTypeTutBehavior : BusinessBase, IHisMedicineTypeTut
    {
        object[] entity;
        internal HisMedicineTypeTutBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object IHisMedicineTypeTut.Run()
        {
            HIS_MEDICINE_TYPE_TUT medicineTypeTut = new HIS_MEDICINE_TYPE_TUT();
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
                            if (entity[i] is MOS.EFMODEL.DataModels.HIS_MEDICINE_TYPE_TUT)
                            {
                                medicineTypeTut = (MOS.EFMODEL.DataModels.HIS_MEDICINE_TYPE_TUT)entity[i];
                            }
                        }
                    }
                }
                if (moduleData != null && medicineTypeTut != null && medicineTypeTut.ID != 0)
                {
                    return new frmHisMedicineTypeTut(medicineTypeTut, moduleData);
                }
                return new frmHisMedicineTypeTut(moduleData);
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
