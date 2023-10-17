using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisImpMestTypeUser
{
    partial class HisImpMestTypeUserGet : EntityBase
    {
        public Dictionary<string, HIS_IMP_MEST_TYPE_USER> GetDicByCode(HisImpMestTypeUserSO search, CommonParam param)
        {
            Dictionary<string, HIS_IMP_MEST_TYPE_USER> dic = new Dictionary<string, HIS_IMP_MEST_TYPE_USER>();
            try
            {
                List<HIS_IMP_MEST_TYPE_USER> listRecord = Get(search, param);
                if (listRecord != null)
                {
                    foreach (var item in listRecord)
                    {
                        if (!dic.ContainsKey(item.IMP_MEST_TYPE_USER_CODE))
                        {
                            dic.Add(item.IMP_MEST_TYPE_USER_CODE, item);
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
