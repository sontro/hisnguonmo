using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisContactPoint
{
    partial class HisContactPointGet : EntityBase
    {
        public Dictionary<string, HIS_CONTACT_POINT> GetDicByCode(HisContactPointSO search, CommonParam param)
        {
            Dictionary<string, HIS_CONTACT_POINT> dic = new Dictionary<string, HIS_CONTACT_POINT>();
            try
            {
                List<HIS_CONTACT_POINT> listRecord = Get(search, param);
                if (listRecord != null)
                {
                    foreach (var item in listRecord)
                    {
                        if (!dic.ContainsKey(item.CONTACT_POINT_CODE))
                        {
                            dic.Add(item.CONTACT_POINT_CODE, item);
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
