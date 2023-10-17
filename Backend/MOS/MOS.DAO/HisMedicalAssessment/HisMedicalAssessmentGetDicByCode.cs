using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMedicalAssessment
{
    partial class HisMedicalAssessmentGet : EntityBase
    {
        public Dictionary<string, HIS_MEDICAL_ASSESSMENT> GetDicByCode(HisMedicalAssessmentSO search, CommonParam param)
        {
            Dictionary<string, HIS_MEDICAL_ASSESSMENT> dic = new Dictionary<string, HIS_MEDICAL_ASSESSMENT>();
            try
            {
                List<HIS_MEDICAL_ASSESSMENT> listRecord = Get(search, param);
                if (listRecord != null)
                {
                    foreach (var item in listRecord)
                    {
                        if (!dic.ContainsKey(item.MEDICAL_ASSESSMENT_CODE))
                        {
                            dic.Add(item.MEDICAL_ASSESSMENT_CODE, item);
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
