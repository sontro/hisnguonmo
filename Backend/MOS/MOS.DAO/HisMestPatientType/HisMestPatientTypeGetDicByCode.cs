using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMestPatientType
{
    partial class HisMestPatientTypeGet : EntityBase
    {
        public Dictionary<string, HIS_MEST_PATIENT_TYPE> GetDicByCode(HisMestPatientTypeSO search, CommonParam param)
        {
            Dictionary<string, HIS_MEST_PATIENT_TYPE> dic = new Dictionary<string, HIS_MEST_PATIENT_TYPE>();
            try
            {
                List<HIS_MEST_PATIENT_TYPE> listRecord = Get(search, param);
                if (listRecord != null)
                {
                    foreach (var item in listRecord)
                    {
                        if (!dic.ContainsKey(item.MEST_PATIENT_TYPE_CODE))
                        {
                            dic.Add(item.MEST_PATIENT_TYPE_CODE, item);
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
