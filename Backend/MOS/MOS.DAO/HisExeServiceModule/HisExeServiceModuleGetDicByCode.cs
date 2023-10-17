using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisExeServiceModule
{
    partial class HisExeServiceModuleGet : EntityBase
    {
        public Dictionary<string, HIS_EXE_SERVICE_MODULE> GetDicByCode(HisExeServiceModuleSO search, CommonParam param)
        {
            Dictionary<string, HIS_EXE_SERVICE_MODULE> dic = new Dictionary<string, HIS_EXE_SERVICE_MODULE>();
            try
            {
                List<HIS_EXE_SERVICE_MODULE> listRecord = Get(search, param);
                if (listRecord != null)
                {
                    foreach (var item in listRecord)
                    {
                        if (!dic.ContainsKey(item.EXE_SERVICE_MODULE_CODE))
                        {
                            dic.Add(item.EXE_SERVICE_MODULE_CODE, item);
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
