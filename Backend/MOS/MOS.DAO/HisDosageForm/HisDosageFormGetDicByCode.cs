using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisDosageForm
{
    partial class HisDosageFormGet : EntityBase
    {
        public Dictionary<string, HIS_DOSAGE_FORM> GetDicByCode(HisDosageFormSO search, CommonParam param)
        {
            Dictionary<string, HIS_DOSAGE_FORM> dic = new Dictionary<string, HIS_DOSAGE_FORM>();
            try
            {
                List<HIS_DOSAGE_FORM> listRecord = Get(search, param);
                if (listRecord != null)
                {
                    foreach (var item in listRecord)
                    {
                        if (!dic.ContainsKey(item.DOSAGE_FORM_CODE))
                        {
                            dic.Add(item.DOSAGE_FORM_CODE, item);
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
