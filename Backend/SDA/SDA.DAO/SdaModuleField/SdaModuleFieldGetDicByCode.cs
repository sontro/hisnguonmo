using SDA.DAO.Base;
using SDA.DAO.StagingObject;
using SDA.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SDA.DAO.SdaModuleField
{
    partial class SdaModuleFieldGet : EntityBase
    {
        public Dictionary<string, SDA_MODULE_FIELD> GetDicByCode(SdaModuleFieldSO search, CommonParam param)
        {
            Dictionary<string, SDA_MODULE_FIELD> dic = new Dictionary<string, SDA_MODULE_FIELD>();
            try
            {
                List<SDA_MODULE_FIELD> listRecord = Get(search, param);
                if (listRecord != null)
                {
                    foreach (var item in listRecord)
                    {
                        if (!dic.ContainsKey(item.MODULE_FIELD_CODE))
                        {
                            dic.Add(item.MODULE_FIELD_CODE, item);
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
