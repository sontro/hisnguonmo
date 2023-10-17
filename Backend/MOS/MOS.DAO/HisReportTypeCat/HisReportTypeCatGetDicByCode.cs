using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisReportTypeCat
{
    partial class HisReportTypeCatGet : EntityBase
    {
        public Dictionary<string, HIS_REPORT_TYPE_CAT> GetDicByCode(HisReportTypeCatSO search, CommonParam param)
        {
            Dictionary<string, HIS_REPORT_TYPE_CAT> dic = new Dictionary<string, HIS_REPORT_TYPE_CAT>();
            try
            {
                List<HIS_REPORT_TYPE_CAT> listRecord = Get(search, param);
                if (listRecord != null)
                {
                    foreach (var item in listRecord)
                    {
                        if (!dic.ContainsKey(item.REPORT_TYPE_CAT_CODE))
                        {
                            dic.Add(item.REPORT_TYPE_CAT_CODE, item);
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
