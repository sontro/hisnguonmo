using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisServicePackage
{
    partial class HisServicePackageGet : EntityBase
    {
        public Dictionary<string, HIS_SERVICE_PACKAGE> GetDicByCode(HisServicePackageSO search, CommonParam param)
        {
            Dictionary<string, HIS_SERVICE_PACKAGE> dic = new Dictionary<string, HIS_SERVICE_PACKAGE>();
            try
            {
                List<HIS_SERVICE_PACKAGE> listRecord = Get(search, param);
                if (listRecord != null)
                {
                    foreach (var item in listRecord)
                    {
                        if (!dic.ContainsKey(item.SERVICE_PACKAGE_CODE))
                        {
                            dic.Add(item.SERVICE_PACKAGE_CODE, item);
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
