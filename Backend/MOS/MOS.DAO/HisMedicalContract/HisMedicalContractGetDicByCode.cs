using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMedicalContract
{
    partial class HisMedicalContractGet : EntityBase
    {
        public Dictionary<string, HIS_MEDICAL_CONTRACT> GetDicByCode(HisMedicalContractSO search, CommonParam param)
        {
            Dictionary<string, HIS_MEDICAL_CONTRACT> dic = new Dictionary<string, HIS_MEDICAL_CONTRACT>();
            try
            {
                List<HIS_MEDICAL_CONTRACT> listRecord = Get(search, param);
                if (listRecord != null)
                {
                    foreach (var item in listRecord)
                    {
                        if (!dic.ContainsKey(item.MEDICAL_CONTRACT_CODE))
                        {
                            dic.Add(item.MEDICAL_CONTRACT_CODE, item);
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
