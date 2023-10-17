using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisSourceMedicine
{
    partial class HisSourceMedicineGet : EntityBase
    {
        public Dictionary<string, HIS_SOURCE_MEDICINE> GetDicByCode(HisSourceMedicineSO search, CommonParam param)
        {
            Dictionary<string, HIS_SOURCE_MEDICINE> dic = new Dictionary<string, HIS_SOURCE_MEDICINE>();
            try
            {
                List<HIS_SOURCE_MEDICINE> listRecord = Get(search, param);
                if (listRecord != null)
                {
                    foreach (var item in listRecord)
                    {
                        if (!dic.ContainsKey(item.SOURCE_MEDICINE_CODE))
                        {
                            dic.Add(item.SOURCE_MEDICINE_CODE, item);
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
