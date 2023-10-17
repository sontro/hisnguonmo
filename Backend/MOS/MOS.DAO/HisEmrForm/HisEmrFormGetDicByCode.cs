using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisEmrForm
{
    partial class HisEmrFormGet : EntityBase
    {
        public Dictionary<string, HIS_EMR_FORM> GetDicByCode(HisEmrFormSO search, CommonParam param)
        {
            Dictionary<string, HIS_EMR_FORM> dic = new Dictionary<string, HIS_EMR_FORM>();
            try
            {
                List<HIS_EMR_FORM> listRecord = Get(search, param);
                if (listRecord != null)
                {
                    foreach (var item in listRecord)
                    {
                        if (!dic.ContainsKey(item.EMR_FORM_CODE))
                        {
                            dic.Add(item.EMR_FORM_CODE, item);
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
