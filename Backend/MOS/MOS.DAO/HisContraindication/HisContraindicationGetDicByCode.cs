using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisContraindication
{
    partial class HisContraindicationGet : EntityBase
    {
        public Dictionary<string, HIS_CONTRAINDICATION> GetDicByCode(HisContraindicationSO search, CommonParam param)
        {
            Dictionary<string, HIS_CONTRAINDICATION> dic = new Dictionary<string, HIS_CONTRAINDICATION>();
            try
            {
                List<HIS_CONTRAINDICATION> listRecord = Get(search, param);
                if (listRecord != null)
                {
                    foreach (var item in listRecord)
                    {
                        if (!dic.ContainsKey(item.CONTRAINDICATION_CODE))
                        {
                            dic.Add(item.CONTRAINDICATION_CODE, item);
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
