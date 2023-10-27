using ACS.DAO.Base;
using ACS.DAO.StagingObject;
using ACS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ACS.DAO.AcsRoleBase
{
    partial class AcsRoleBaseGet : EntityBase
    {
        public Dictionary<string, ACS_ROLE_BASE> GetDicByCode(AcsRoleBaseSO search, CommonParam param)
        {
            Dictionary<string, ACS_ROLE_BASE> dic = new Dictionary<string, ACS_ROLE_BASE>();
            try
            {
                List<ACS_ROLE_BASE> listRecord = Get(search, param);
                if (listRecord != null)
                {
                    foreach (var item in listRecord)
                    {
                        if (!dic.ContainsKey(item.ROLE_BASE_CODE))
                        {
                            dic.Add(item.ROLE_BASE_CODE, item);
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
