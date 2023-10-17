using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisInvoiceDetail
{
    partial class HisInvoiceDetailGet : EntityBase
    {
        public Dictionary<string, HIS_INVOICE_DETAIL> GetDicByCode(HisInvoiceDetailSO search, CommonParam param)
        {
            Dictionary<string, HIS_INVOICE_DETAIL> dic = new Dictionary<string, HIS_INVOICE_DETAIL>();
            try
            {
                List<HIS_INVOICE_DETAIL> listRecord = Get(search, param);
                if (listRecord != null)
                {
                    foreach (var item in listRecord)
                    {
                        if (!dic.ContainsKey(item.INVOICE_DETAIL_CODE))
                        {
                            dic.Add(item.INVOICE_DETAIL_CODE, item);
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
