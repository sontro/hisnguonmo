using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisSereServExt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Pacs.Start
{
    class HisServiceReqPacsStartCheck : BusinessBase
    {
        internal HisServiceReqPacsStartCheck()
            : base()
        {

        }

        internal HisServiceReqPacsStartCheck(CommonParam paramCheck)
            : base(paramCheck)
        {

        }

        internal bool IsValidData(string accessionNumber, ref HIS_SERE_SERV sereServ, ref HIS_SERVICE_REQ serviceReq, ref List<HIS_SERE_SERV_EXT> sereServExts)
        {
            try
            {
                long sereServId = long.Parse(accessionNumber);
                sereServ = new HisSereServGet().GetById(sereServId);

                if (sereServ == null || !sereServ.SERVICE_REQ_ID.HasValue)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.DuLieuDauVaoKhongHopLe);
                    return false;
                }

                serviceReq = new HisServiceReqGet().GetById(sereServ.SERVICE_REQ_ID.Value);

                if (serviceReq == null)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.DuLieuDauVaoKhongHopLe);
                    return false;
                }

                sereServExts = new HisSereServExtGet().GetByServiceReqId(sereServ.SERVICE_REQ_ID.Value);
                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
        }
    }
}
