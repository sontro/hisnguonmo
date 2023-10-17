using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisTestIndexRange
{
    partial class HisTestIndexRangeGet : EntityBase
    {
        public Dictionary<string, HIS_TEST_INDEX_RANGE> GetDicByCode(HisTestIndexRangeSO search, CommonParam param)
        {
            Dictionary<string, HIS_TEST_INDEX_RANGE> dic = new Dictionary<string, HIS_TEST_INDEX_RANGE>();
            try
            {
                List<HIS_TEST_INDEX_RANGE> listRecord = Get(search, param);
                if (listRecord != null)
                {
                    foreach (var item in listRecord)
                    {
                        if (!dic.ContainsKey(item.TEST_INDEX_RANGE_CODE))
                        {
                            dic.Add(item.TEST_INDEX_RANGE_CODE, item);
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
