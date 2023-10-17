using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisAntibioticNewReg
{
    partial class HisAntibioticNewRegGet : EntityBase
    {
        public Dictionary<string, HIS_ANTIBIOTIC_NEW_REG> GetDicByCode(HisAntibioticNewRegSO search, CommonParam param)
        {
            Dictionary<string, HIS_ANTIBIOTIC_NEW_REG> dic = new Dictionary<string, HIS_ANTIBIOTIC_NEW_REG>();
            try
            {
                List<HIS_ANTIBIOTIC_NEW_REG> listRecord = Get(search, param);
                if (listRecord != null)
                {
                    foreach (var item in listRecord)
                    {
                        if (!dic.ContainsKey(item.ANTIBIOTIC_NEW_REG_CODE))
                        {
                            dic.Add(item.ANTIBIOTIC_NEW_REG_CODE, item);
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
