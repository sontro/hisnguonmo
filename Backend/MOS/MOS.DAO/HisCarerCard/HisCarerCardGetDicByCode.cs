using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisCarerCard
{
    partial class HisCarerCardGet : EntityBase
    {
        public Dictionary<string, HIS_CARER_CARD> GetDicByCode(HisCarerCardSO search, CommonParam param)
        {
            Dictionary<string, HIS_CARER_CARD> dic = new Dictionary<string, HIS_CARER_CARD>();
            try
            {
                List<HIS_CARER_CARD> listRecord = Get(search, param);
                if (listRecord != null)
                {
                    foreach (var item in listRecord)
                    {
                        if (!dic.ContainsKey(item.CARER_CARD_CODE))
                        {
                            dic.Add(item.CARER_CARD_CODE, item);
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
