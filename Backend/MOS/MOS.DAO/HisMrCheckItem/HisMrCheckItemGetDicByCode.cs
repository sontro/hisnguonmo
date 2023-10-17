using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMrCheckItem
{
    partial class HisMrCheckItemGet : EntityBase
    {
        public Dictionary<string, HIS_MR_CHECK_ITEM> GetDicByCode(HisMrCheckItemSO search, CommonParam param)
        {
            Dictionary<string, HIS_MR_CHECK_ITEM> dic = new Dictionary<string, HIS_MR_CHECK_ITEM>();
            try
            {
                List<HIS_MR_CHECK_ITEM> listRecord = Get(search, param);
                if (listRecord != null)
                {
                    foreach (var item in listRecord)
                    {
                        if (!dic.ContainsKey(item.MR_CHECK_ITEM_CODE))
                        {
                            dic.Add(item.MR_CHECK_ITEM_CODE, item);
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
