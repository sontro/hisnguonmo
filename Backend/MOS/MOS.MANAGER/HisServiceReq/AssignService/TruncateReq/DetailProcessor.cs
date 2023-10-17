using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisServiceReq.Pacs;
using MOS.MANAGER.HisServiceReq.Test;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.AssignService.TruncateReq
{
    class DetailProcessor : BusinessBase
    {
        internal DetailProcessor()
            : base()
        {

        }

        internal DetailProcessor(CommonParam param)
            : base(param)
        {
        }

        internal bool Run(List<HIS_SERVICE_REQ> lstServiceReqTruncate, List<HIS_SERE_SERV> lstSereSevTruncate, HIS_TREATMENT hisTreatment)
        {
            bool result = false;
            try
            {
                List<HIS_SERVICE_REQ> reqTests = lstServiceReqTruncate != null ? lstServiceReqTruncate.Where(o => o.SERVICE_REQ_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__XN).ToList() : null;
                List<HIS_SERE_SERV> sereServTests = lstSereSevTruncate != null ? lstSereSevTruncate.Where(o => o.TDL_SERVICE_REQ_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__XN).ToList() : null;
                if (IsNotNullOrEmpty(reqTests))
                {
                    if (!new HisServiceReqTestTruncate(param).TruncateList(reqTests, sereServTests))
                    {
                        throw new Exception("Xoa du lieu bang chi tiet that bai. HisServiceReqTestTruncate");
                    }
                }


                List<HIS_SERVICE_REQ> reqPacks = lstServiceReqTruncate != null ? lstServiceReqTruncate.Where(o => HisServiceReqTypeCFG.PACS_TYPE_IDs.Contains(o.SERVICE_REQ_TYPE_ID) || o.ALLOW_SEND_PACS == Constant.IS_TRUE).ToList() : null;
                List<HIS_SERE_SERV> sereServPacks = lstSereSevTruncate != null ? lstSereSevTruncate.Where(o => reqPacks.Exists(e => e.ID == o.SERVICE_REQ_ID)).ToList() : null;
                if (IsNotNullOrEmpty(reqPacks))
                {
                    if (!new HisServiceReqPacsTruncate(param).TruncateList(hisTreatment, reqPacks, sereServPacks))
                    {
                        throw new Exception("Xoa du lieu bang chi tiet that bai. HisServiceReqPacsTruncate");
                    }
                }
                result = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
                param.HasException = true;
            }
            return result;
        }
    }
}
