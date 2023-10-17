using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisQcNormation
{
    partial class HisQcNormationGet : EntityBase
    {
        public Dictionary<string, HIS_QC_NORMATION> GetDicByCode(HisQcNormationSO search, CommonParam param)
        {
            Dictionary<string, HIS_QC_NORMATION> dic = new Dictionary<string, HIS_QC_NORMATION>();
            try
            {
                List<HIS_QC_NORMATION> listRecord = Get(search, param);
                if (listRecord != null)
                {
                    foreach (var item in listRecord)
                    {
                        if (!dic.ContainsKey(item.QC_NORMATION_CODE))
                        {
                            dic.Add(item.QC_NORMATION_CODE, item);
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
