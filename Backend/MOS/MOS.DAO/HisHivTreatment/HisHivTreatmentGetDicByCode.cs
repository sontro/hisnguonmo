using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisHivTreatment
{
    partial class HisHivTreatmentGet : EntityBase
    {
        public Dictionary<string, HIS_HIV_TREATMENT> GetDicByCode(HisHivTreatmentSO search, CommonParam param)
        {
            Dictionary<string, HIS_HIV_TREATMENT> dic = new Dictionary<string, HIS_HIV_TREATMENT>();
            try
            {
                List<HIS_HIV_TREATMENT> listRecord = Get(search, param);
                if (listRecord != null)
                {
                    foreach (var item in listRecord)
                    {
                        if (!dic.ContainsKey(item.HIV_TREATMENT_CODE.ToString()))
                        {
                            dic.Add(item.HIV_TREATMENT_CODE.ToString(), item);
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
