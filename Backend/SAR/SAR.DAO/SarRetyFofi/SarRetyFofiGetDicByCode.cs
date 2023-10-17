using SAR.DAO.Base;
using SAR.DAO.StagingObject;
using SAR.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SAR.DAO.SarRetyFofi
{
    partial class SarRetyFofiGet : EntityBase
    {
        public Dictionary<string, SAR_RETY_FOFI> GetDicByCode(SarRetyFofiSO search, CommonParam param)
        {
            Dictionary<string, SAR_RETY_FOFI> dic = new Dictionary<string, SAR_RETY_FOFI>();
            try
            {
                List<SAR_RETY_FOFI> listRecord = Get(search, param);
                if (listRecord != null)
                {
                    foreach (var item in listRecord)
                    {
                        if (!dic.ContainsKey(item.RETY_FOFI_CODE))
                        {
                            dic.Add(item.RETY_FOFI_CODE, item);
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
