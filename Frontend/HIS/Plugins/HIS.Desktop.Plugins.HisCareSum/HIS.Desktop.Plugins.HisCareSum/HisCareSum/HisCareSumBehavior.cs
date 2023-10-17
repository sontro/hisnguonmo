using Inventec.Core;
using Inventec.Desktop.Common;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOS.Filter;

namespace HIS.Desktop.Plugins.HisCareSum
{
    class HisCareSumBehavior : BusinessBase, IHisCareSum
    {
        object[] entity;
        long treatmentId = 0;
        HisTreatmentBedRoomLViewFilter dataTransferTreatmentBedRoomFilter;

        internal HisCareSumBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object IHisCareSum.Run()
        {
            try
            {
                Inventec.Desktop.Common.Modules.Module moduleData = null;
                bool isTreatmentList = false;

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
                            else if (entity[i] is long)
                            {
                                treatmentId = (long)entity[i];
                            }
                            else if (entity[i] is bool)
                            {
                                isTreatmentList = (bool)entity[i];
                            }
                            else if (entity[i] is HisTreatmentBedRoomLViewFilter)
                            {
                                dataTransferTreatmentBedRoomFilter = (HisTreatmentBedRoomLViewFilter)entity[i];
                            }
                        }
                    }
                }

                return new frmHisCareSum(moduleData, treatmentId, isTreatmentList, dataTransferTreatmentBedRoomFilter);
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
