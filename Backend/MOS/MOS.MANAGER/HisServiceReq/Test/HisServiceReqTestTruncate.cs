using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisServiceReq;
using MOS.MANAGER.HisServiceReq.Test.LisIntegreateProcessor;
using MOS.MANAGER.HisServiceReq.Test.LisSenderV1;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisServiceReq.Test
{
    class HisServiceReqTestTruncate : BusinessBase
    {
        internal HisServiceReqTestTruncate()
            : base()
        {

        }

        internal HisServiceReqTestTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(HIS_SERVICE_REQ data, List<HIS_SERE_SERV> sereServs)
        {
            bool result = true;
            try
            {
                bool valid = true;
                HisServiceReqCheck checker = new HisServiceReqCheck(param);
                valid = valid && checker.IsUnLock(data);
                if (valid)
                {
                    if (Lis2CFG.LIS_INTEGRATION_VERSION == Lis2CFG.LisIntegrationVersion.V2)
                    {
                        if (data.IS_SENT_EXT == Constant.IS_TRUE && data.SERVICE_REQ_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__XN && !Lis2CFG.IS_SEND_REQUEST_LABCONN)
                        {
                            OrderData d = new OrderData();
                            d.ServiceReq = data;
                            d.Availables = sereServs;
                            ILisProcessor sender = LisFactory.GetProcessor(param);
                            List<string> messages = null;
                            result = sender != null && sender.DeleteOrder(d, ref messages);
                        }
                        else
                        {
                            result = true;
                        }
                    }
                    else
                    {
                        if (data.LIS_STT_ID.HasValue)
                        {
                            if (LisCFG.LIS_INTEGRATE_OPTION == (int)LisCFG.LisIntegrateOption.LIS)
                            {
                                result = new LisUtil(param).SendDeleteOrderToLis(data.ID);
                            }
                            else
                            {
                                result = true;
                            }
                        }
                        else
                        {
                            result = true;
                        }
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

        internal bool TruncateList(List<HIS_SERVICE_REQ> lstData, List<HIS_SERE_SERV> sereServs)
        {
            bool result = true;
            try
            {
                bool valid = true;
                HisServiceReqCheck checker = new HisServiceReqCheck(param);
                valid = valid && checker.IsUnLock(lstData);
                if (valid)
                {
                    if (Lis2CFG.LIS_INTEGRATION_VERSION == Lis2CFG.LisIntegrationVersion.V2)
                    {
                        foreach (HIS_SERVICE_REQ data in lstData)
                        {
                            if (data.IS_SENT_EXT == Constant.IS_TRUE && data.SERVICE_REQ_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__XN)
                            {
                                OrderData d = new OrderData();
                                d.ServiceReq = data;
                                d.Availables = sereServs != null ? sereServs.Where(o => o.SERVICE_REQ_ID == data.ID).ToList() : null;
                                ILisProcessor sender = LisFactory.GetProcessor(param);
                                List<string> messages = null;
                                result = result && (sender != null && sender.DeleteOrder(d, ref messages));
                            }
                        }
                    }
                    else
                    {
                        foreach (HIS_SERVICE_REQ data in lstData)
                        {
                            if (data.LIS_STT_ID.HasValue)
                            {
                                if (LisCFG.LIS_INTEGRATE_OPTION == (int)LisCFG.LisIntegrateOption.LIS)
                                {
                                    result = result && new LisUtil(param).SendDeleteOrderToLis(data.ID);
                                }
                            }
                        }
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
