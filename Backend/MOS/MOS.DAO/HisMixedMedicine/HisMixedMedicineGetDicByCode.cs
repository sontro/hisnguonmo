using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMixedMedicine
{
    partial class HisMixedMedicineGet : EntityBase
    {
        public Dictionary<string, HIS_MIXED_MEDICINE> GetDicByCode(HisMixedMedicineSO search, CommonParam param)
        {
            Dictionary<string, HIS_MIXED_MEDICINE> dic = new Dictionary<string, HIS_MIXED_MEDICINE>();
            try
            {
                List<HIS_MIXED_MEDICINE> listRecord = Get(search, param);
                if (listRecord != null)
                {
                    foreach (var item in listRecord)
                    {
                        if (!dic.ContainsKey(item.MIXED_MEDICINE_CODE))
                        {
                            dic.Add(item.MIXED_MEDICINE_CODE, item);
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
