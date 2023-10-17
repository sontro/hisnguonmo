using SDA.DAO.Base;
using SDA.DAO.StagingObject;
using SDA.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SDA.DAO.SdaSqlParam
{
    partial class SdaSqlParamGet : EntityBase
    {
        public Dictionary<string, SDA_SQL_PARAM> GetDicByCode(SdaSqlParamSO search, CommonParam param)
        {
            Dictionary<string, SDA_SQL_PARAM> dic = new Dictionary<string, SDA_SQL_PARAM>();
            try
            {
                List<SDA_SQL_PARAM> listRecord = Get(search, param);
                if (listRecord != null)
                {
                    foreach (var item in listRecord)
                    {
                        if (!dic.ContainsKey(item.SQL_PARAM_CODE))
                        {
                            dic.Add(item.SQL_PARAM_CODE, item);
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
