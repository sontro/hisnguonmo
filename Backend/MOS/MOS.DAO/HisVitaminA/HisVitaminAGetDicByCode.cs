using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisVitaminA
{
    partial class HisVitaminAGet : EntityBase
    {
        public Dictionary<string, HIS_VITAMIN_A> GetDicByCode(HisVitaminASO search, CommonParam param)
        {
            Dictionary<string, HIS_VITAMIN_A> dic = new Dictionary<string, HIS_VITAMIN_A>();
            try
            {
                List<HIS_VITAMIN_A> listRecord = Get(search, param);
                if (listRecord != null)
                {
                    foreach (var item in listRecord)
                    {
                        if (!dic.ContainsKey(item.VITAMIN_A_CODE))
                        {
                            dic.Add(item.VITAMIN_A_CODE, item);
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
