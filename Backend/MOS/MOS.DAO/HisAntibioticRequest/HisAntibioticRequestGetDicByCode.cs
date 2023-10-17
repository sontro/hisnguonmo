using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisAntibioticRequest
{
    partial class HisAntibioticRequestGet : EntityBase
    {
        public Dictionary<string, HIS_ANTIBIOTIC_REQUEST> GetDicByCode(HisAntibioticRequestSO search, CommonParam param)
        {
            Dictionary<string, HIS_ANTIBIOTIC_REQUEST> dic = new Dictionary<string, HIS_ANTIBIOTIC_REQUEST>();
            try
            {
                List<HIS_ANTIBIOTIC_REQUEST> listRecord = Get(search, param);
                if (listRecord != null)
                {
                    foreach (var item in listRecord)
                    {
                        if (!dic.ContainsKey(item.ANTIBIOTIC_REQUEST_CODE))
                        {
                            dic.Add(item.ANTIBIOTIC_REQUEST_CODE, item);
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
