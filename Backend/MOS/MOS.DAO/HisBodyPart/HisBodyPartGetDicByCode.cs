using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisBodyPart
{
    partial class HisBodyPartGet : EntityBase
    {
        public Dictionary<string, HIS_BODY_PART> GetDicByCode(HisBodyPartSO search, CommonParam param)
        {
            Dictionary<string, HIS_BODY_PART> dic = new Dictionary<string, HIS_BODY_PART>();
            try
            {
                List<HIS_BODY_PART> listRecord = Get(search, param);
                if (listRecord != null)
                {
                    foreach (var item in listRecord)
                    {
                        if (!dic.ContainsKey(item.BODY_PART_CODE))
                        {
                            dic.Add(item.BODY_PART_CODE, item);
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
