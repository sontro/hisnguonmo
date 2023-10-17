using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisStorageCondition
{
    partial class HisStorageConditionGet : EntityBase
    {
        public Dictionary<string, HIS_STORAGE_CONDITION> GetDicByCode(HisStorageConditionSO search, CommonParam param)
        {
            Dictionary<string, HIS_STORAGE_CONDITION> dic = new Dictionary<string, HIS_STORAGE_CONDITION>();
            try
            {
                List<HIS_STORAGE_CONDITION> listRecord = Get(search, param);
                if (listRecord != null)
                {
                    foreach (var item in listRecord)
                    {
                        if (!dic.ContainsKey(item.STORAGE_CONDITION_CODE))
                        {
                            dic.Add(item.STORAGE_CONDITION_CODE, item);
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
