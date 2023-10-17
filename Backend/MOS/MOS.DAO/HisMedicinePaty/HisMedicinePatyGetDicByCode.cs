using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMedicinePaty
{
    partial class HisMedicinePatyGet : EntityBase
    {
        public Dictionary<string, HIS_MEDICINE_PATY> GetDicByCode(HisMedicinePatySO search, CommonParam param)
        {
            Dictionary<string, HIS_MEDICINE_PATY> dic = new Dictionary<string, HIS_MEDICINE_PATY>();
            try
            {
                List<HIS_MEDICINE_PATY> listRecord = Get(search, param);
                if (listRecord != null)
                {
                    foreach (var item in listRecord)
                    {
                        if (!dic.ContainsKey(item.MEDICINE_PATY_CODE))
                        {
                            dic.Add(item.MEDICINE_PATY_CODE, item);
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
