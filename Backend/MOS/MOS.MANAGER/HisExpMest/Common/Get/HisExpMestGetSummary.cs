using MOS.MANAGER.Base;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.SDO;

using AutoMapper;

using Inventec.Core;
using Inventec.Common.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Common.Get
{
    partial class HisExpMestGet : GetBase
    {
        internal HisExpMestSummarySDO GetSummary(HisExpMestView2FilterQuery filter)
        {
            try
            {
                List<V_HIS_EXP_MEST_2> vHisExpMest2 = this.GetView2(filter);
                if (vHisExpMest2 != null && vHisExpMest2.Count > 0)
                {
                    HisExpMestSummarySDO sdo = new HisExpMestSummarySDO();
                    sdo.TotalOfPatient = vHisExpMest2.Where(o => o.TDL_PATIENT_ID.HasValue && o.TDL_PATIENT_ID.Value > 0).Select(s => s.TDL_PATIENT_ID).Distinct().Count();
                    sdo.TotalOfMedicine = vHisExpMest2.Select(s => s.ID).Distinct().Count();

                    decimal totalPrice = 0;
                    foreach (var medicine in vHisExpMest2)
                    {
                        if (medicine.TDL_TOTAL_PRICE.HasValue)
                        {
                            totalPrice += medicine.TDL_TOTAL_PRICE.Value;
                        }
                    }

                    sdo.TotalOfPrice = totalPrice;
                    return sdo;
                }
                return null;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }
    }
}
