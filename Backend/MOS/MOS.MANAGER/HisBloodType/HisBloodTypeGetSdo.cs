using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisBlood;
using MOS.SDO;
using System;
using System.Linq;
using System.Collections.Generic;
using MOS.UTILITY;

namespace MOS.MANAGER.HisBloodType
{
    partial class HisBloodTypeGet : BusinessBase
    {
        internal List<HisBloodTypeInStockSDO> GetInStockBloodType(HisBloodTypeStockViewFilter filter)
        {
            try
            {
                List<HisBloodTypeInStockSDO> result = null;
                HisBloodViewFilterQuery bloodFilter = new HisBloodViewFilterQuery();
                bloodFilter.BLOOD_TYPE_ID = filter.ID;
                bloodFilter.MEDI_STOCK_ID = filter.MEDI_STOCK_ID;
                bloodFilter.MEDI_STOCK_IDs = filter.MEDI_STOCK_IDs;
                bloodFilter.BLOOD_TYPE_IDs = filter.IDs;
                bloodFilter.ORDER_FIELD = "NUM_ORDER";
                bloodFilter.ORDER_DIRECTION = "DESC";
                bloodFilter.BLOOD_TYPE_IS_ACTIVE = filter.IS_ACTIVE;
                if (filter.IS_AVAILABLE.HasValue && filter.IS_AVAILABLE.Value)
                {
                    bloodFilter.IS_ACTIVE = Constant.IS_TRUE;
                }
                else if (filter.IS_AVAILABLE.HasValue && !filter.IS_AVAILABLE.Value)
                {
                    bloodFilter.IS_ACTIVE = Constant.IS_FALSE;
                }

                List<V_HIS_BLOOD> vBloods = new HisBloodGet().GetView(bloodFilter);
                if (IsNotNullOrEmpty(vBloods))
                {
                    result = new List<HisBloodTypeInStockSDO>();
                    Dictionary<long, HisBloodTypeInStockSDO> dic = new Dictionary<long, HisBloodTypeInStockSDO>();

                    foreach (V_HIS_BLOOD blood in vBloods)
                    {
                        if (dic.ContainsKey(blood.BLOOD_TYPE_ID))
                        {
                            HisBloodTypeInStockSDO sdo = dic[blood.BLOOD_TYPE_ID];
                            sdo.Amount += 1;
                        }
                        else
                        {
                            HisBloodTypeInStockSDO sdo = new HisBloodTypeInStockSDO();
                            sdo.Id = blood.BLOOD_TYPE_ID;
                            //blood thi is_leaf luon la true
                            sdo.IsLeaf = MOS.UTILITY.Constant.IS_TRUE;
                            sdo.BloodTypeCode = blood.BLOOD_TYPE_CODE;
                            sdo.BloodTypeName = blood.BLOOD_TYPE_NAME;
                            sdo.MediStockId = blood.MEDI_STOCK_ID;
                            sdo.ParentId = blood.PARENT_ID;
                            sdo.ServiceId = blood.SERVICE_ID;
                            sdo.Volume = blood.VOLUME;
                            sdo.Amount = 1;
                            dic.Add(sdo.Id, sdo);
                        }
                    }

                    ////sort lai du lieu
                    if (dic.Values != null)
                    {
                        result = dic.Values.ToList();
                    }
                    //Thuc hien phan trang lai theo du lieu param tu client (do du lieu ko duoc phan trang duoi tang DAO)
                    int start = param.Start.HasValue ? param.Start.Value : 0;
                    int limit = param.Limit.HasValue ? param.Limit.Value : Int32.MaxValue;
                    param.Count = result.Count;
                    return result.Skip(start).Take(limit).ToList();
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
