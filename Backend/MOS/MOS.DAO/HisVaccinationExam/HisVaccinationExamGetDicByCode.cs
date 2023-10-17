using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisVaccinationExam
{
    partial class HisVaccinationExamGet : EntityBase
    {
        public Dictionary<string, HIS_VACCINATION_EXAM> GetDicByCode(HisVaccinationExamSO search, CommonParam param)
        {
            Dictionary<string, HIS_VACCINATION_EXAM> dic = new Dictionary<string, HIS_VACCINATION_EXAM>();
            try
            {
                List<HIS_VACCINATION_EXAM> listRecord = Get(search, param);
                if (listRecord != null)
                {
                    foreach (var item in listRecord)
                    {
                        if (!dic.ContainsKey(item.VACCINATION_EXAM_CODE))
                        {
                            dic.Add(item.VACCINATION_EXAM_CODE, item);
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
