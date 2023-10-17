using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisExamSereDire
{
    partial class HisExamSereDireGet : EntityBase
    {
        public Dictionary<string, HIS_EXAM_SERE_DIRE> GetDicByCode(HisExamSereDireSO search, CommonParam param)
        {
            Dictionary<string, HIS_EXAM_SERE_DIRE> dic = new Dictionary<string, HIS_EXAM_SERE_DIRE>();
            try
            {
                List<HIS_EXAM_SERE_DIRE> listRecord = Get(search, param);
                if (listRecord != null)
                {
                    foreach (var item in listRecord)
                    {
                        if (!dic.ContainsKey(item.EXAM_SERE_DIRE_CODE))
                        {
                            dic.Add(item.EXAM_SERE_DIRE_CODE, item);
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
