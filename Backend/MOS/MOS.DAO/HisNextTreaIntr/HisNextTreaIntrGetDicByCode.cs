using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisNextTreaIntr
{
    partial class HisNextTreaIntrGet : EntityBase
    {
        public Dictionary<string, HIS_NEXT_TREA_INTR> GetDicByCode(HisNextTreaIntrSO search, CommonParam param)
        {
            Dictionary<string, HIS_NEXT_TREA_INTR> dic = new Dictionary<string, HIS_NEXT_TREA_INTR>();
            try
            {
                List<HIS_NEXT_TREA_INTR> listRecord = Get(search, param);
                if (listRecord != null)
                {
                    foreach (var item in listRecord)
                    {
                        if (!dic.ContainsKey(item.NEXT_TREA_INTR_CODE))
                        {
                            dic.Add(item.NEXT_TREA_INTR_CODE, item);
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
