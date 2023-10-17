using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisServiceRoom
{
    partial class HisServiceRoomGet : EntityBase
    {
        public Dictionary<string, HIS_SERVICE_ROOM> GetDicByCode(HisServiceRoomSO search, CommonParam param)
        {
            Dictionary<string, HIS_SERVICE_ROOM> dic = new Dictionary<string, HIS_SERVICE_ROOM>();
            try
            {
                List<HIS_SERVICE_ROOM> listRecord = Get(search, param);
                if (listRecord != null)
                {
                    foreach (var item in listRecord)
                    {
                        if (!dic.ContainsKey(item.SERVICE_ROOM_CODE))
                        {
                            dic.Add(item.SERVICE_ROOM_CODE, item);
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
