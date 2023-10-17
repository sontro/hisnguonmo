using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisImpMestMaterial
{
    partial class HisImpMestMaterialGet : EntityBase
    {
        public Dictionary<string, HIS_IMP_MEST_MATERIAL> GetDicByCode(HisImpMestMaterialSO search, CommonParam param)
        {
            Dictionary<string, HIS_IMP_MEST_MATERIAL> dic = new Dictionary<string, HIS_IMP_MEST_MATERIAL>();
            try
            {
                List<HIS_IMP_MEST_MATERIAL> listRecord = Get(search, param);
                if (listRecord != null)
                {
                    foreach (var item in listRecord)
                    {
                        if (!dic.ContainsKey(item.IMP_MEST_MATERIAL_CODE))
                        {
                            dic.Add(item.IMP_MEST_MATERIAL_CODE, item);
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
