using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMestInveUser
{
    partial class HisMestInveUserGet : EntityBase
    {
        public Dictionary<string, HIS_MEST_INVE_USER> GetDicByCode(HisMestInveUserSO search, CommonParam param)
        {
            Dictionary<string, HIS_MEST_INVE_USER> dic = new Dictionary<string, HIS_MEST_INVE_USER>();
            try
            {
                List<HIS_MEST_INVE_USER> listRecord = Get(search, param);
                if (listRecord != null)
                {
                    foreach (var item in listRecord)
                    {
                        if (!dic.ContainsKey(item.MEST_INVE_USER_CODE))
                        {
                            dic.Add(item.MEST_INVE_USER_CODE, item);
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
