using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisUserGroupTempDt
{
    partial class HisUserGroupTempDtGet : EntityBase
    {
        public Dictionary<string, HIS_USER_GROUP_TEMP_DT> GetDicByCode(HisUserGroupTempDtSO search, CommonParam param)
        {
            Dictionary<string, HIS_USER_GROUP_TEMP_DT> dic = new Dictionary<string, HIS_USER_GROUP_TEMP_DT>();
            try
            {
                List<HIS_USER_GROUP_TEMP_DT> listRecord = Get(search, param);
                if (listRecord != null)
                {
                    foreach (var item in listRecord)
                    {
                        if (!dic.ContainsKey(item.USER_GROUP_TEMP_DT_CODE))
                        {
                            dic.Add(item.USER_GROUP_TEMP_DT_CODE, item);
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
