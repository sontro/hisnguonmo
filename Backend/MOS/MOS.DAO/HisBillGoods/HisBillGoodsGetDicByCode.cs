using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisBillGoods
{
    partial class HisBillGoodsGet : EntityBase
    {
        public Dictionary<string, HIS_BILL_GOODS> GetDicByCode(HisBillGoodsSO search, CommonParam param)
        {
            Dictionary<string, HIS_BILL_GOODS> dic = new Dictionary<string, HIS_BILL_GOODS>();
            try
            {
                List<HIS_BILL_GOODS> listRecord = Get(search, param);
                if (listRecord != null)
                {
                    foreach (var item in listRecord)
                    {
                        if (!dic.ContainsKey(item.BILL_GOODS_CODE))
                        {
                            dic.Add(item.BILL_GOODS_CODE, item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logging(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => search), search) + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => param), param), LogType.Error);
                LogSystem.Error(ex);
                dic.Clear();
            }
            return dic;
        }
    }
}
