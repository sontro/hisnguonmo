using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisServSegr
{
    partial class HisServSegrGet : EntityBase
    {
        public Dictionary<string, HIS_SERV_SEGR> GetDicByCode(HisServSegrSO search, CommonParam param)
        {
            Dictionary<string, HIS_SERV_SEGR> dic = new Dictionary<string, HIS_SERV_SEGR>();
            try
            {
                List<HIS_SERV_SEGR> listRecord = Get(search, param);
                if (listRecord != null)
                {
                    foreach (var item in listRecord)
                    {
                        if (!dic.ContainsKey(item.SERV_SEGR_CODE))
                        {
                            dic.Add(item.SERV_SEGR_CODE, item);
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
