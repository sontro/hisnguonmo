using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisHoreDhty
{
    partial class HisHoreDhtyGet : EntityBase
    {
        public Dictionary<string, HIS_HORE_DHTY> GetDicByCode(HisHoreDhtySO search, CommonParam param)
        {
            Dictionary<string, HIS_HORE_DHTY> dic = new Dictionary<string, HIS_HORE_DHTY>();
            try
            {
                List<HIS_HORE_DHTY> listRecord = Get(search, param);
                if (listRecord != null)
                {
                    foreach (var item in listRecord)
                    {
                        if (!dic.ContainsKey(item.HORE_DHTY_CODE))
                        {
                            dic.Add(item.HORE_DHTY_CODE, item);
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
