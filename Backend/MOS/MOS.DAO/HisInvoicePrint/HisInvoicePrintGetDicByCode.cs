using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisInvoicePrint
{
    partial class HisInvoicePrintGet : EntityBase
    {
        public Dictionary<string, HIS_INVOICE_PRINT> GetDicByCode(HisInvoicePrintSO search, CommonParam param)
        {
            Dictionary<string, HIS_INVOICE_PRINT> dic = new Dictionary<string, HIS_INVOICE_PRINT>();
            try
            {
                List<HIS_INVOICE_PRINT> listRecord = Get(search, param);
                if (listRecord != null)
                {
                    foreach (var item in listRecord)
                    {
                        if (!dic.ContainsKey(item.INVOICE_PRINT_CODE))
                        {
                            dic.Add(item.INVOICE_PRINT_CODE, item);
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
