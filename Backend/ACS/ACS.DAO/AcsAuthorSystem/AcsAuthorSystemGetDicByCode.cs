using ACS.DAO.Base;
using ACS.DAO.StagingObject;
using ACS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ACS.DAO.AcsAuthorSystem
{
    partial class AcsAuthorSystemGet : EntityBase
    {
        public Dictionary<string, ACS_AUTHOR_SYSTEM> GetDicByCode(AcsAuthorSystemSO search, CommonParam param)
        {
            Dictionary<string, ACS_AUTHOR_SYSTEM> dic = new Dictionary<string, ACS_AUTHOR_SYSTEM>();
            try
            {
                List<ACS_AUTHOR_SYSTEM> listRecord = Get(search, param);
                if (listRecord != null)
                {
                    foreach (var item in listRecord)
                    {
                        if (!dic.ContainsKey(item.AUTHOR_SYSTEM_CODE))
                        {
                            dic.Add(item.AUTHOR_SYSTEM_CODE, item);
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
