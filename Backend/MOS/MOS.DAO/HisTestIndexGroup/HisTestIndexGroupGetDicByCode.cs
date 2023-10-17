using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisTestIndexGroup
{
    partial class HisTestIndexGroupGet : EntityBase
    {
        public Dictionary<string, HIS_TEST_INDEX_GROUP> GetDicByCode(HisTestIndexGroupSO search, CommonParam param)
        {
            Dictionary<string, HIS_TEST_INDEX_GROUP> dic = new Dictionary<string, HIS_TEST_INDEX_GROUP>();
            try
            {
                List<HIS_TEST_INDEX_GROUP> listRecord = Get(search, param);
                if (listRecord != null)
                {
                    foreach (var item in listRecord)
                    {
                        if (!dic.ContainsKey(item.TEST_INDEX_GROUP_CODE))
                        {
                            dic.Add(item.TEST_INDEX_GROUP_CODE, item);
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
