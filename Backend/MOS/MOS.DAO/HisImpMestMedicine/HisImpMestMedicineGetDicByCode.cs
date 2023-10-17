using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisImpMestMedicine
{
    partial class HisImpMestMedicineGet : EntityBase
    {
        public Dictionary<string, HIS_IMP_MEST_MEDICINE> GetDicByCode(HisImpMestMedicineSO search, CommonParam param)
        {
            Dictionary<string, HIS_IMP_MEST_MEDICINE> dic = new Dictionary<string, HIS_IMP_MEST_MEDICINE>();
            try
            {
                List<HIS_IMP_MEST_MEDICINE> listRecord = Get(search, param);
                if (listRecord != null)
                {
                    foreach (var item in listRecord)
                    {
                        if (!dic.ContainsKey(item.IMP_MEST_MEDICINE_CODE))
                        {
                            dic.Add(item.IMP_MEST_MEDICINE_CODE, item);
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
