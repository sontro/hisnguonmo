using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisCaroDepartment
{
    partial class HisCaroDepartmentGet : EntityBase
    {
        public Dictionary<string, HIS_CARO_DEPARTMENT> GetDicByCode(HisCaroDepartmentSO search, CommonParam param)
        {
            Dictionary<string, HIS_CARO_DEPARTMENT> dic = new Dictionary<string, HIS_CARO_DEPARTMENT>();
            try
            {
                List<HIS_CARO_DEPARTMENT> listRecord = Get(search, param);
                if (listRecord != null)
                {
                    foreach (var item in listRecord)
                    {
                        if (!dic.ContainsKey(item.CARO_DEPARTMENT_CODE))
                        {
                            dic.Add(item.CARO_DEPARTMENT_CODE, item);
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
