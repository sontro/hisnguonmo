using SAR.DAO.Base;
using SAR.DAO.StagingObject;
using SAR.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SAR.DAO.SarFormData
{
    partial class SarFormDataGet : EntityBase
    {
        public Dictionary<string, SAR_FORM_DATA> GetDicByCode(SarFormDataSO search, CommonParam param)
        {
            Dictionary<string, SAR_FORM_DATA> dic = new Dictionary<string, SAR_FORM_DATA>();
            try
            {
                List<SAR_FORM_DATA> listRecord = Get(search, param);
                if (listRecord != null)
                {
                    foreach (var item in listRecord)
                    {
                        if (!dic.ContainsKey(item.FORM_DATA_CODE))
                        {
                            dic.Add(item.FORM_DATA_CODE, item);
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
