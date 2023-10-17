using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisInfusionSum
{
    partial class HisInfusionSumGet : EntityBase
    {
        public Dictionary<string, HIS_INFUSION_SUM> GetDicByCode(HisInfusionSumSO search, CommonParam param)
        {
            Dictionary<string, HIS_INFUSION_SUM> dic = new Dictionary<string, HIS_INFUSION_SUM>();
            try
            {
                List<HIS_INFUSION_SUM> listRecord = Get(search, param);
                if (listRecord != null)
                {
                    foreach (var item in listRecord)
                    {
                        if (!dic.ContainsKey(item.INFUSION_SUM_CODE))
                        {
                            dic.Add(item.INFUSION_SUM_CODE, item);
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
