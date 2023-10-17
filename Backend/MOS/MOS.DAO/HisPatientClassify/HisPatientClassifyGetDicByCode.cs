using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisPatientClassify
{
    partial class HisPatientClassifyGet : EntityBase
    {
        public Dictionary<string, HIS_PATIENT_CLASSIFY> GetDicByCode(HisPatientClassifySO search, CommonParam param)
        {
            Dictionary<string, HIS_PATIENT_CLASSIFY> dic = new Dictionary<string, HIS_PATIENT_CLASSIFY>();
            try
            {
                List<HIS_PATIENT_CLASSIFY> listRecord = Get(search, param);
                if (listRecord != null)
                {
                    foreach (var item in listRecord)
                    {
                        if (!dic.ContainsKey(item.PATIENT_CLASSIFY_CODE))
                        {
                            dic.Add(item.PATIENT_CLASSIFY_CODE, item);
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
