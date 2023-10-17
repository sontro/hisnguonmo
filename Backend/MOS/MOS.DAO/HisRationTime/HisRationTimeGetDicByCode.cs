using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisRationTime
{
    partial class HisRationTimeGet : EntityBase
    {
        public Dictionary<string, HIS_RATION_TIME> GetDicByCode(HisRationTimeSO search, CommonParam param)
        {
            Dictionary<string, HIS_RATION_TIME> dic = new Dictionary<string, HIS_RATION_TIME>();
            try
            {
                List<HIS_RATION_TIME> listRecord = Get(search, param);
                if (listRecord != null)
                {
                    foreach (var item in listRecord)
                    {
                        if (!dic.ContainsKey(item.RATION_TIME_CODE))
                        {
                            dic.Add(item.RATION_TIME_CODE, item);
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
