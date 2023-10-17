using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisSkinSurgeryDesc
{
    partial class HisSkinSurgeryDescGet : EntityBase
    {
        public Dictionary<string, HIS_SKIN_SURGERY_DESC> GetDicByCode(HisSkinSurgeryDescSO search, CommonParam param)
        {
            Dictionary<string, HIS_SKIN_SURGERY_DESC> dic = new Dictionary<string, HIS_SKIN_SURGERY_DESC>();
            try
            {
                List<HIS_SKIN_SURGERY_DESC> listRecord = Get(search, param);
                if (listRecord != null)
                {
                    foreach (var item in listRecord)
                    {
                        if (!dic.ContainsKey(item.SKIN_SURGERY_DESC_CODE))
                        {
                            dic.Add(item.SKIN_SURGERY_DESC_CODE, item);
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
