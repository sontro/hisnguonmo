using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisKskDriver
{
    partial class HisKskDriverGet : EntityBase
    {
        public Dictionary<string, HIS_KSK_DRIVER> GetDicByCode(HisKskDriverSO search, CommonParam param)
        {
            Dictionary<string, HIS_KSK_DRIVER> dic = new Dictionary<string, HIS_KSK_DRIVER>();
            try
            {
                List<HIS_KSK_DRIVER> listRecord = Get(search, param);
                if (listRecord != null)
                {
                    foreach (var item in listRecord)
                    {
                        if (!dic.ContainsKey(item.KSK_DRIVER_CODE))
                        {
                            dic.Add(item.KSK_DRIVER_CODE, item);
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
