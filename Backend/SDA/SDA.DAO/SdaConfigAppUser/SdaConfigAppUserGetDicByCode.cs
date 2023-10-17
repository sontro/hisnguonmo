using SDA.DAO.Base;
using SDA.DAO.StagingObject;
using SDA.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SDA.DAO.SdaConfigAppUser
{
    partial class SdaConfigAppUserGet : EntityBase
    {
        public Dictionary<string, SDA_CONFIG_APP_USER> GetDicByCode(SdaConfigAppUserSO search, CommonParam param)
        {
            Dictionary<string, SDA_CONFIG_APP_USER> dic = new Dictionary<string, SDA_CONFIG_APP_USER>();
            try
            {
                List<SDA_CONFIG_APP_USER> listRecord = Get(search, param);
                if (listRecord != null)
                {
                    foreach (var item in listRecord)
                    {
                        if (!dic.ContainsKey(item.CONFIG_APP_USER_CODE))
                        {
                            dic.Add(item.CONFIG_APP_USER_CODE, item);
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
