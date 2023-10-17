using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisEmteMaterialType
{
    partial class HisEmteMaterialTypeGet : EntityBase
    {
        public Dictionary<string, HIS_EMTE_MATERIAL_TYPE> GetDicByCode(HisEmteMaterialTypeSO search, CommonParam param)
        {
            Dictionary<string, HIS_EMTE_MATERIAL_TYPE> dic = new Dictionary<string, HIS_EMTE_MATERIAL_TYPE>();
            try
            {
                List<HIS_EMTE_MATERIAL_TYPE> listRecord = Get(search, param);
                if (listRecord != null)
                {
                    foreach (var item in listRecord)
                    {
                        if (!dic.ContainsKey(item.EMTE_MATERIAL_TYPE_CODE))
                        {
                            dic.Add(item.EMTE_MATERIAL_TYPE_CODE, item);
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
