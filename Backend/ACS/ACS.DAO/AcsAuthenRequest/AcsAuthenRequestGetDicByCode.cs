using ACS.DAO.Base;
using ACS.DAO.StagingObject;
using ACS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ACS.DAO.AcsAuthenRequest
{
    partial class AcsAuthenRequestGet : EntityBase
    {
        public Dictionary<string, ACS_AUTHEN_REQUEST> GetDicByCode(AcsAuthenRequestSO search, CommonParam param)
        {
            Dictionary<string, ACS_AUTHEN_REQUEST> dic = new Dictionary<string, ACS_AUTHEN_REQUEST>();
            try
            {
                List<ACS_AUTHEN_REQUEST> listRecord = Get(search, param);
                if (listRecord != null)
                {
                    foreach (var item in listRecord)
                    {
                        if (!dic.ContainsKey(item.AUTHEN_REQUEST_CODE))
                        {
                            dic.Add(item.AUTHEN_REQUEST_CODE, item);
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
