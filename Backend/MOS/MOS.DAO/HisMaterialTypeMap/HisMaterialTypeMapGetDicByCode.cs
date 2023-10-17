using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMaterialTypeMap
{
    partial class HisMaterialTypeMapGet : EntityBase
    {
        public Dictionary<string, HIS_MATERIAL_TYPE_MAP> GetDicByCode(HisMaterialTypeMapSO search, CommonParam param)
        {
            Dictionary<string, HIS_MATERIAL_TYPE_MAP> dic = new Dictionary<string, HIS_MATERIAL_TYPE_MAP>();
            try
            {
                List<HIS_MATERIAL_TYPE_MAP> listRecord = Get(search, param);
                if (listRecord != null)
                {
                    foreach (var item in listRecord)
                    {
                        if (!dic.ContainsKey(item.MATERIAL_TYPE_MAP_CODE))
                        {
                            dic.Add(item.MATERIAL_TYPE_MAP_CODE, item);
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
