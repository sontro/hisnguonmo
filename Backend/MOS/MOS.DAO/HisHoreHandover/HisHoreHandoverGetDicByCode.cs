using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisHoreHandover
{
    partial class HisHoreHandoverGet : EntityBase
    {
        public Dictionary<string, HIS_HORE_HANDOVER> GetDicByCode(HisHoreHandoverSO search, CommonParam param)
        {
            Dictionary<string, HIS_HORE_HANDOVER> dic = new Dictionary<string, HIS_HORE_HANDOVER>();
            try
            {
                List<HIS_HORE_HANDOVER> listRecord = Get(search, param);
                if (listRecord != null)
                {
                    foreach (var item in listRecord)
                    {
                        if (!dic.ContainsKey(item.HORE_HANDOVER_CODE))
                        {
                            dic.Add(item.HORE_HANDOVER_CODE, item);
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
