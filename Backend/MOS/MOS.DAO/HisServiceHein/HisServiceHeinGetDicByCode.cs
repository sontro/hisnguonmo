using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisServiceHein
{
    partial class HisServiceHeinGet : EntityBase
    {
        public Dictionary<string, HIS_SERVICE_HEIN> GetDicByCode(HisServiceHeinSO search, CommonParam param)
        {
            Dictionary<string, HIS_SERVICE_HEIN> dic = new Dictionary<string, HIS_SERVICE_HEIN>();
            try
            {
                List<HIS_SERVICE_HEIN> listRecord = Get(search, param);
                if (listRecord != null)
                {
                    foreach (var item in listRecord)
                    {
                        if (!dic.ContainsKey(item.SERVICE_HEIN_CODE))
                        {
                            dic.Add(item.SERVICE_HEIN_CODE, item);
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
