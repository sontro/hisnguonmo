using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisPatient;
using MOS.MANAGER.HisRoom;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.OldSystemIntegrate;
using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.Token;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisServiceReq
{
    partial class HisServiceReqSendToOldSystem : BusinessBase
    {
        internal HisServiceReqSendToOldSystem()
            : base()
        {

        }

        internal HisServiceReqSendToOldSystem(CommonParam paramCreate)
            : base(paramCreate)
        {
        }

        /// <summary>
        /// Gui thong tin y lenh sang he thong cu
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool Run(long id)
        {
            bool result = false;
            try
            {
                HIS_SERVICE_REQ serviceReq = new HisServiceReqGet().GetById(id);
                List<HIS_SERE_SERV> sereServs = new HisSereServGet().GetByServiceReqId(id);
                result = OldSystemIntegrateProcessor.CreateServiceReq(serviceReq, sereServs);
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
