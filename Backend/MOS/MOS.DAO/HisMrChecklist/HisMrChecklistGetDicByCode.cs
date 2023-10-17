using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMrChecklist
{
    partial class HisMrChecklistGet : EntityBase
    {
        public Dictionary<string, HIS_MR_CHECKLIST> GetDicByCode(HisMrChecklistSO search, CommonParam param)
        {
            Dictionary<string, HIS_MR_CHECKLIST> dic = new Dictionary<string, HIS_MR_CHECKLIST>();
            try
            {
                List<HIS_MR_CHECKLIST> listRecord = Get(search, param);
                if (listRecord != null)
                {
                    foreach (var item in listRecord)
                    {
                        if (!dic.ContainsKey(item.MR_CHECKLIST_CODE))
                        {
                            dic.Add(item.MR_CHECKLIST_CODE, item);
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
