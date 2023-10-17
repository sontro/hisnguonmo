using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisTransfusionSum
{
    partial class HisTransfusionSumGet : EntityBase
    {
        public Dictionary<string, HIS_TRANSFUSION_SUM> GetDicByCode(HisTransfusionSumSO search, CommonParam param)
        {
            Dictionary<string, HIS_TRANSFUSION_SUM> dic = new Dictionary<string, HIS_TRANSFUSION_SUM>();
            try
            {
                List<HIS_TRANSFUSION_SUM> listRecord = Get(search, param);
                if (listRecord != null)
                {
                    foreach (var item in listRecord)
                    {
                        if (!dic.ContainsKey(item.TRANSFUSION_SUM_CODE))
                        {
                            dic.Add(item.TRANSFUSION_SUM_CODE, item);
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
