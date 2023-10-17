using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisSevereIllnessInfo
{
    partial class HisSevereIllnessInfoGet : EntityBase
    {
        public Dictionary<string, HIS_SEVERE_ILLNESS_INFO> GetDicByCode(HisSevereIllnessInfoSO search, CommonParam param)
        {
            Dictionary<string, HIS_SEVERE_ILLNESS_INFO> dic = new Dictionary<string, HIS_SEVERE_ILLNESS_INFO>();
            try
            {
                List<HIS_SEVERE_ILLNESS_INFO> listRecord = Get(search, param);
                if (listRecord != null)
                {
                    foreach (var item in listRecord)
                    {
                        if (!dic.ContainsKey(item.SEVERE_ILLNESS_INFO_CODE))
                        {
                            dic.Add(item.SEVERE_ILLNESS_INFO_CODE, item);
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
