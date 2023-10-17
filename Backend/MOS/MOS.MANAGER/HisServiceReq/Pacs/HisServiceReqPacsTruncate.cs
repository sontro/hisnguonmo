using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisServiceReq;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisServiceReq.Pacs
{
    class HisServiceReqPacsTruncate : BusinessBase
    {
        internal HisServiceReqPacsTruncate()
            : base()
        {

        }

        internal HisServiceReqPacsTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(HIS_TREATMENT treatment, HIS_SERVICE_REQ data, List<HIS_SERE_SERV> sereServs)
        {
            bool result = false;
            try
            {
                if (data.IS_SENT_EXT.HasValue && (HisServiceReqTypeCFG.PACS_TYPE_IDs.Contains(data.SERVICE_REQ_TYPE_ID) || data.ALLOW_SEND_PACS == Constant.IS_TRUE))
                {
                    IPacsProcessor processor = PacsFactory.GetProcessor(new CommonParam());
                    PacsOrderData order = new PacsOrderData();
                    order.ServiceReq = data;
                    order.Treatment = treatment;
                    order.Deletes = sereServs.Where(o => o.IS_SENT_EXT == Constant.IS_TRUE).ToList();
                    List<string> sqls = new List<string>();
                    result = processor != null ? processor.SendOrder(order, ref sqls) : false;
                }
                else
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        internal bool TruncateList(HIS_TREATMENT treatment, List<HIS_SERVICE_REQ> lstData, List<HIS_SERE_SERV> sereServs)
        {
            bool result = true;
            try
            {
                foreach (HIS_SERVICE_REQ data in lstData)
                {
                    if (data.IS_SENT_EXT.HasValue && (HisServiceReqTypeCFG.PACS_TYPE_IDs.Contains(data.SERVICE_REQ_TYPE_ID) || data.ALLOW_SEND_PACS == Constant.IS_TRUE))
                    {
                        IPacsProcessor processor = PacsFactory.GetProcessor(new CommonParam());
                        PacsOrderData order = new PacsOrderData();
                        order.ServiceReq = data;
                        order.Treatment = treatment;
                        order.Deletes = sereServs.Where(o => o.IS_SENT_EXT == Constant.IS_TRUE && o.SERVICE_REQ_ID == data.ID).ToList();
                        List<string> sqls = new List<string>();
                        result = result && (processor != null ? processor.SendOrder(order, ref sqls) : false);
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }
    }
}
