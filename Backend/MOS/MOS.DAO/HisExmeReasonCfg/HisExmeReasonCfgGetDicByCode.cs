using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisExmeReasonCfg
{
    partial class HisExmeReasonCfgGet : EntityBase
    {
        public Dictionary<string, HIS_EXME_REASON_CFG> GetDicByCode(HisExmeReasonCfgSO search, CommonParam param)
        {
            Dictionary<string, HIS_EXME_REASON_CFG> dic = new Dictionary<string, HIS_EXME_REASON_CFG>();
            try
            {
                List<HIS_EXME_REASON_CFG> listRecord = Get(search, param);
                if (listRecord != null)
                {
                    foreach (var item in listRecord)
                    {
                        if (!dic.ContainsKey(item.EXME_REASON_CFG_CODE))
                        {
                            dic.Add(item.EXME_REASON_CFG_CODE, item);
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
