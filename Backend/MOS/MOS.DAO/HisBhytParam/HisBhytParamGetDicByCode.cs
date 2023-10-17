using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisBhytParam
{
    partial class HisBhytParamGet : EntityBase
    {
        public Dictionary<string, HIS_BHYT_PARAM> GetDicByCode(HisBhytParamSO search, CommonParam param)
        {
            Dictionary<string, HIS_BHYT_PARAM> dic = new Dictionary<string, HIS_BHYT_PARAM>();
            try
            {
                List<HIS_BHYT_PARAM> listRecord = Get(search, param);
                if (listRecord != null)
                {
                    foreach (var item in listRecord)
                    {
                        if (!dic.ContainsKey(item.BHYT_PARAM_CODE))
                        {
                            dic.Add(item.BHYT_PARAM_CODE, item);
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
