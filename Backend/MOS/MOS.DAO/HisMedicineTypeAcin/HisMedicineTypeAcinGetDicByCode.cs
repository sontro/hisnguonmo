using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMedicineTypeAcin
{
    partial class HisMedicineTypeAcinGet : EntityBase
    {
        public Dictionary<string, HIS_MEDICINE_TYPE_ACIN> GetDicByCode(HisMedicineTypeAcinSO search, CommonParam param)
        {
            Dictionary<string, HIS_MEDICINE_TYPE_ACIN> dic = new Dictionary<string, HIS_MEDICINE_TYPE_ACIN>();
            try
            {
                List<HIS_MEDICINE_TYPE_ACIN> listRecord = Get(search, param);
                if (listRecord != null)
                {
                    foreach (var item in listRecord)
                    {
                        if (!dic.ContainsKey(item.MEDICINE_TYPE_ACIN_CODE))
                        {
                            dic.Add(item.MEDICINE_TYPE_ACIN_CODE, item);
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
