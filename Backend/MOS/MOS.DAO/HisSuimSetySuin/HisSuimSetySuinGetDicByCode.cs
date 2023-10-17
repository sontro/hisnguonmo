using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisSuimSetySuin
{
    partial class HisSuimSetySuinGet : EntityBase
    {
        public Dictionary<string, HIS_SUIM_SETY_SUIN> GetDicByCode(HisSuimSetySuinSO search, CommonParam param)
        {
            Dictionary<string, HIS_SUIM_SETY_SUIN> dic = new Dictionary<string, HIS_SUIM_SETY_SUIN>();
            try
            {
                List<HIS_SUIM_SETY_SUIN> listRecord = Get(search, param);
                if (listRecord != null)
                {
                    foreach (var item in listRecord)
                    {
                        if (!dic.ContainsKey(item.SUIM_SETY_SUIN_CODE))
                        {
                            dic.Add(item.SUIM_SETY_SUIN_CODE, item);
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
