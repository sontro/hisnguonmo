using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisNoneMediService
{
    partial class HisNoneMediServiceGet : EntityBase
    {
        public Dictionary<string, HIS_NONE_MEDI_SERVICE> GetDicByCode(HisNoneMediServiceSO search, CommonParam param)
        {
            Dictionary<string, HIS_NONE_MEDI_SERVICE> dic = new Dictionary<string, HIS_NONE_MEDI_SERVICE>();
            try
            {
                List<HIS_NONE_MEDI_SERVICE> listRecord = Get(search, param);
                if (listRecord != null)
                {
                    foreach (var item in listRecord)
                    {
                        if (!dic.ContainsKey(item.NONE_MEDI_SERVICE_CODE))
                        {
                            dic.Add(item.NONE_MEDI_SERVICE_CODE, item);
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
