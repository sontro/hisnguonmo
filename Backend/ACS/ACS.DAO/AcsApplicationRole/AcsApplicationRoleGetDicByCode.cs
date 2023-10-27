using ACS.DAO.Base;
using ACS.DAO.StagingObject;
using ACS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ACS.DAO.AcsApplicationRole
{
    partial class AcsApplicationRoleGet : EntityBase
    {
        public Dictionary<string, ACS_APPLICATION_ROLE> GetDicByCode(AcsApplicationRoleSO search, CommonParam param)
        {
            Dictionary<string, ACS_APPLICATION_ROLE> dic = new Dictionary<string, ACS_APPLICATION_ROLE>();
            try
            {
                List<ACS_APPLICATION_ROLE> listRecord = Get(search, param);
                if (listRecord != null)
                {
                    foreach (var item in listRecord)
                    {
                        if (!dic.ContainsKey(item.APPLICATION_ROLE_CODE))
                        {
                            dic.Add(item.APPLICATION_ROLE_CODE, item);
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
