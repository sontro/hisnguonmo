using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisSereServPtttTemp
{
    partial class HisSereServPtttTempGet : EntityBase
    {
        public Dictionary<string, HIS_SERE_SERV_PTTT_TEMP> GetDicByCode(HisSereServPtttTempSO search, CommonParam param)
        {
            Dictionary<string, HIS_SERE_SERV_PTTT_TEMP> dic = new Dictionary<string, HIS_SERE_SERV_PTTT_TEMP>();
            try
            {
                List<HIS_SERE_SERV_PTTT_TEMP> listRecord = Get(search, param);
                if (listRecord != null)
                {
                    foreach (var item in listRecord)
                    {
                        if (!dic.ContainsKey(item.SERE_SERV_PTTT_TEMP_CODE))
                        {
                            dic.Add(item.SERE_SERV_PTTT_TEMP_CODE, item);
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
