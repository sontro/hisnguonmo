using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisCoTreatment
{
    partial class HisCoTreatmentGet : EntityBase
    {
        public Dictionary<string, HIS_CO_TREATMENT> GetDicByCode(HisCoTreatmentSO search, CommonParam param)
        {
            Dictionary<string, HIS_CO_TREATMENT> dic = new Dictionary<string, HIS_CO_TREATMENT>();
            try
            {
                List<HIS_CO_TREATMENT> listRecord = Get(search, param);
                if (listRecord != null)
                {
                    foreach (var item in listRecord)
                    {
                        if (!dic.ContainsKey(item.CO_TREATMENT_CODE))
                        {
                            dic.Add(item.CO_TREATMENT_CODE, item);
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
