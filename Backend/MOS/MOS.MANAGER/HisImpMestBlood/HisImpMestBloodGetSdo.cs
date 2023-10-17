using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisBlood;
using MOS.SDO;
using System;
using System.Linq;
using System.Collections.Generic;

namespace MOS.MANAGER.HisImpMestBlood
{
    partial class HisImpMestBloodGet : BusinessBase
    {
        internal List<HisImpMestBloodWithInStockInfoSDO> GetViewWithInStockInfo(long impMestId)
        {
            try
            {
                List<HisImpMestBloodWithInStockInfoSDO> result = null;
                List<V_HIS_IMP_MEST_BLOOD> impMestBloods = this.GetViewByImpMestId(impMestId);
                if (IsNotNullOrEmpty(impMestBloods))
                {
                    Mapper.CreateMap<V_HIS_IMP_MEST_BLOOD, HisImpMestBloodWithInStockInfoSDO>();
                    result = Mapper.Map<List<HisImpMestBloodWithInStockInfoSDO>>(impMestBloods);
                    List<long> bloodIds = impMestBloods.Select(o => o.BLOOD_ID).ToList();

                    HisBloodFilterQuery filter = new HisBloodFilterQuery();
                    filter.MEDI_STOCK_ID = impMestBloods[0].MEDI_STOCK_ID;
                    filter.IDs = bloodIds;
                    List<HIS_BLOOD> inStockBloods = new HisBloodGet(param).Get(filter);

                    if (IsNotNullOrEmpty(inStockBloods))
                    {
                        foreach (HisImpMestBloodWithInStockInfoSDO sdo in result)
                        {
                            HIS_BLOOD blood = inStockBloods.Where(o => o.ID == sdo.BLOOD_ID).FirstOrDefault();
                            sdo.IsInStock = blood != null;
                            sdo.IsAvailable = blood != null && blood.IS_ACTIVE == MOS.UTILITY.Constant.IS_TRUE;
                        }
                    }
                }
                return result;
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
