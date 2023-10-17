using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisSereServFile
{
    partial class HisSereServFileGet : EntityBase
    {
        public Dictionary<string, HIS_SERE_SERV_FILE> GetDicByCode(HisSereServFileSO search, CommonParam param)
        {
            Dictionary<string, HIS_SERE_SERV_FILE> dic = new Dictionary<string, HIS_SERE_SERV_FILE>();
            try
            {
                List<HIS_SERE_SERV_FILE> listRecord = Get(search, param);
                if (listRecord != null)
                {
                    foreach (var item in listRecord)
                    {
                        if (!dic.ContainsKey(item.SERE_SERV_FILE_CODE))
                        {
                            dic.Add(item.SERE_SERV_FILE_CODE, item);
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
