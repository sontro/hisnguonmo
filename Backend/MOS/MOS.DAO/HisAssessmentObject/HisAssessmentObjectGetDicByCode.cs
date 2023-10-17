using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisAssessmentObject
{
    partial class HisAssessmentObjectGet : EntityBase
    {
        public Dictionary<string, HIS_ASSESSMENT_OBJECT> GetDicByCode(HisAssessmentObjectSO search, CommonParam param)
        {
            Dictionary<string, HIS_ASSESSMENT_OBJECT> dic = new Dictionary<string, HIS_ASSESSMENT_OBJECT>();
            try
            {
                List<HIS_ASSESSMENT_OBJECT> listRecord = Get(search, param);
                if (listRecord != null)
                {
                    foreach (var item in listRecord)
                    {
                        if (!dic.ContainsKey(item.ASSESSMENT_OBJECT_CODE))
                        {
                            dic.Add(item.ASSESSMENT_OBJECT_CODE, item);
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
