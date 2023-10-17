using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisKskOther
{
    partial class HisKskOtherGet : EntityBase
    {
        public Dictionary<string, HIS_KSK_OTHER> GetDicByCode(HisKskOtherSO search, CommonParam param)
        {
            Dictionary<string, HIS_KSK_OTHER> dic = new Dictionary<string, HIS_KSK_OTHER>();
            try
            {
                List<HIS_KSK_OTHER> listRecord = Get(search, param);
                if (listRecord != null)
                {
                    foreach (var item in listRecord)
                    {
                        if (!dic.ContainsKey(item.KSK_OTHER_CODE))
                        {
                            dic.Add(item.KSK_OTHER_CODE, item);
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
