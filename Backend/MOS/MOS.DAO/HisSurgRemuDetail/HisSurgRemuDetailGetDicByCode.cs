using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisSurgRemuDetail
{
    partial class HisSurgRemuDetailGet : EntityBase
    {
        public Dictionary<string, HIS_SURG_REMU_DETAIL> GetDicByCode(HisSurgRemuDetailSO search, CommonParam param)
        {
            Dictionary<string, HIS_SURG_REMU_DETAIL> dic = new Dictionary<string, HIS_SURG_REMU_DETAIL>();
            try
            {
                List<HIS_SURG_REMU_DETAIL> listRecord = Get(search, param);
                if (listRecord != null)
                {
                    foreach (var item in listRecord)
                    {
                        if (!dic.ContainsKey(item.SURG_REMU_DETAIL_CODE))
                        {
                            dic.Add(item.SURG_REMU_DETAIL_CODE, item);
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
