using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMediStockExty
{
    partial class HisMediStockExtyGet : EntityBase
    {
        public Dictionary<string, HIS_MEDI_STOCK_EXTY> GetDicByCode(HisMediStockExtySO search, CommonParam param)
        {
            Dictionary<string, HIS_MEDI_STOCK_EXTY> dic = new Dictionary<string, HIS_MEDI_STOCK_EXTY>();
            try
            {
                List<HIS_MEDI_STOCK_EXTY> listRecord = Get(search, param);
                if (listRecord != null)
                {
                    foreach (var item in listRecord)
                    {
                        if (!dic.ContainsKey(item.MEDI_STOCK_EXTY_CODE))
                        {
                            dic.Add(item.MEDI_STOCK_EXTY_CODE, item);
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
