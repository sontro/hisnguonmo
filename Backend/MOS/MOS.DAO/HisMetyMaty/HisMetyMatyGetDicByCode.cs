using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMetyMaty
{
    partial class HisMetyMatyGet : EntityBase
    {
        public Dictionary<string, HIS_METY_MATY> GetDicByCode(HisMetyMatySO search, CommonParam param)
        {
            Dictionary<string, HIS_METY_MATY> dic = new Dictionary<string, HIS_METY_MATY>();
            try
            {
                List<HIS_METY_MATY> listRecord = Get(search, param);
                if (listRecord != null)
                {
                    foreach (var item in listRecord)
                    {
                        if (!dic.ContainsKey(item.METY_MATY_CODE))
                        {
                            dic.Add(item.METY_MATY_CODE, item);
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
