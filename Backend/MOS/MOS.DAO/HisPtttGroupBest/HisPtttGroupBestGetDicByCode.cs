using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisPtttGroupBest
{
    partial class HisPtttGroupBestGet : EntityBase
    {
        public Dictionary<string, HIS_PTTT_GROUP_BEST> GetDicByCode(HisPtttGroupBestSO search, CommonParam param)
        {
            Dictionary<string, HIS_PTTT_GROUP_BEST> dic = new Dictionary<string, HIS_PTTT_GROUP_BEST>();
            try
            {
                List<HIS_PTTT_GROUP_BEST> listRecord = Get(search, param);
                if (listRecord != null)
                {
                    foreach (var item in listRecord)
                    {
                        if (!dic.ContainsKey(item.PTTT_GROUP_BEST_CODE))
                        {
                            dic.Add(item.PTTT_GROUP_BEST_CODE, item);
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
