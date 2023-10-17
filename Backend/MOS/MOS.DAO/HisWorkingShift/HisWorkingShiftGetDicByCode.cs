using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisWorkingShift
{
    partial class HisWorkingShiftGet : EntityBase
    {
        public Dictionary<string, HIS_WORKING_SHIFT> GetDicByCode(HisWorkingShiftSO search, CommonParam param)
        {
            Dictionary<string, HIS_WORKING_SHIFT> dic = new Dictionary<string, HIS_WORKING_SHIFT>();
            try
            {
                List<HIS_WORKING_SHIFT> listRecord = Get(search, param);
                if (listRecord != null)
                {
                    foreach (var item in listRecord)
                    {
                        if (!dic.ContainsKey(item.WORKING_SHIFT_CODE))
                        {
                            dic.Add(item.WORKING_SHIFT_CODE, item);
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
