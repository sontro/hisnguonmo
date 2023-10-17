using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisStentConclude
{
    partial class HisStentConcludeGet : EntityBase
    {
        public Dictionary<string, HIS_STENT_CONCLUDE> GetDicByCode(HisStentConcludeSO search, CommonParam param)
        {
            Dictionary<string, HIS_STENT_CONCLUDE> dic = new Dictionary<string, HIS_STENT_CONCLUDE>();
            try
            {
                List<HIS_STENT_CONCLUDE> listRecord = Get(search, param);
                if (listRecord != null)
                {
                    foreach (var item in listRecord)
                    {
                        if (!dic.ContainsKey(item.STENT_CONCLUDE_CODE))
                        {
                            dic.Add(item.STENT_CONCLUDE_CODE, item);
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
