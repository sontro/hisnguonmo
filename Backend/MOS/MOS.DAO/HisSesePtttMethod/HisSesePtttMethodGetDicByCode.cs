using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisSesePtttMethod
{
    partial class HisSesePtttMethodGet : EntityBase
    {
        public Dictionary<string, HIS_SESE_PTTT_METHOD> GetDicByCode(HisSesePtttMethodSO search, CommonParam param)
        {
            Dictionary<string, HIS_SESE_PTTT_METHOD> dic = new Dictionary<string, HIS_SESE_PTTT_METHOD>();
            try
            {
                List<HIS_SESE_PTTT_METHOD> listRecord = Get(search, param);
                if (listRecord != null)
                {
                    foreach (var item in listRecord)
                    {
                        if (!dic.ContainsKey(item.SESE_PTTT_METHOD_CODE))
                        {
                            dic.Add(item.SESE_PTTT_METHOD_CODE, item);
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
