using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMatyMaty
{
    partial class HisMatyMatyGet : EntityBase
    {
        public Dictionary<string, HIS_MATY_MATY> GetDicByCode(HisMatyMatySO search, CommonParam param)
        {
            Dictionary<string, HIS_MATY_MATY> dic = new Dictionary<string, HIS_MATY_MATY>();
            try
            {
                List<HIS_MATY_MATY> listRecord = Get(search, param);
                if (listRecord != null)
                {
                    foreach (var item in listRecord)
                    {
                        if (!dic.ContainsKey(item.MATY_MATY_CODE))
                        {
                            dic.Add(item.MATY_MATY_CODE, item);
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
