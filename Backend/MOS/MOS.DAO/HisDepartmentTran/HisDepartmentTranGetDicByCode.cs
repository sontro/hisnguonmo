using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisDepartmentTran
{
    partial class HisDepartmentTranGet : EntityBase
    {
        public Dictionary<string, HIS_DEPARTMENT_TRAN> GetDicByCode(HisDepartmentTranSO search, CommonParam param)
        {
            Dictionary<string, HIS_DEPARTMENT_TRAN> dic = new Dictionary<string, HIS_DEPARTMENT_TRAN>();
            try
            {
                List<HIS_DEPARTMENT_TRAN> listRecord = Get(search, param);
                if (listRecord != null)
                {
                    foreach (var item in listRecord)
                    {
                        if (!dic.ContainsKey(item.DEPARTMENT_TRAN_CODE))
                        {
                            dic.Add(item.DEPARTMENT_TRAN_CODE, item);
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
