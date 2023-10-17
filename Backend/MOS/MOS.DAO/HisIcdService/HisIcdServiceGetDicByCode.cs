using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisIcdService
{
    partial class HisIcdServiceGet : EntityBase
    {
        public Dictionary<string, HIS_ICD_SERVICE> GetDicByCode(HisIcdServiceSO search, CommonParam param)
        {
            Dictionary<string, HIS_ICD_SERVICE> dic = new Dictionary<string, HIS_ICD_SERVICE>();
            try
            {
                List<HIS_ICD_SERVICE> listRecord = Get(search, param);
                if (listRecord != null)
                {
                    foreach (var item in listRecord)
                    {
                        if (!dic.ContainsKey(item.ICD_SERVICE_CODE))
                        {
                            dic.Add(item.ICD_SERVICE_CODE, item);
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
