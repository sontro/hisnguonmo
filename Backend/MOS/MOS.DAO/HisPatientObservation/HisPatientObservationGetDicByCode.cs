using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisPatientObservation
{
    partial class HisPatientObservationGet : EntityBase
    {
        public Dictionary<string, HIS_PATIENT_OBSERVATION> GetDicByCode(HisPatientObservationSO search, CommonParam param)
        {
            Dictionary<string, HIS_PATIENT_OBSERVATION> dic = new Dictionary<string, HIS_PATIENT_OBSERVATION>();
            try
            {
                List<HIS_PATIENT_OBSERVATION> listRecord = Get(search, param);
                if (listRecord != null)
                {
                    foreach (var item in listRecord)
                    {
                        if (!dic.ContainsKey(item.PATIENT_OBSERVATION_CODE))
                        {
                            dic.Add(item.PATIENT_OBSERVATION_CODE, item);
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
