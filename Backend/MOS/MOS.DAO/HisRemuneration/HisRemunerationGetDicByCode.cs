using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisRemuneration
{
    partial class HisRemunerationGet : EntityBase
    {
        public Dictionary<string, HIS_REMUNERATION> GetDicByCode(HisRemunerationSO search, CommonParam param)
        {
            Dictionary<string, HIS_REMUNERATION> dic = new Dictionary<string, HIS_REMUNERATION>();
            try
            {
                List<HIS_REMUNERATION> listRecord = Get(search, param);
                if (listRecord != null)
                {
                    foreach (var item in listRecord)
                    {
                        if (!dic.ContainsKey(item.REMUNERATION_CODE))
                        {
                            dic.Add(item.REMUNERATION_CODE, item);
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
