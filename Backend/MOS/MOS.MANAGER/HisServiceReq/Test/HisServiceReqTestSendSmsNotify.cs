using Inventec.Common.Logging;
using Inventec.Core;
using MOS.ApiConsumerManager;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisPatient;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisSereServTein;
using MOS.UTILITY;
using SMS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Test
{
    class HisServiceReqTestSendSmsNotify : BusinessBase
    {
        private static List<string> SYMBOL = new List<string>() { "\"", "#", "$", "%", "&", "'", "*", "<", "=", ">", "?", "¡", "¢", "£", "¤", "¥", "₹", "@", "^", "|" };

        internal HisServiceReqTestSendSmsNotify()
            : base()
        {

        }

        internal HisServiceReqTestSendSmsNotify(CommonParam param)
            : base(param)
        {

        }

        internal bool Run(HIS_PATIENT patient, HIS_SERVICE_REQ data, List<HIS_SERE_SERV> sereServs, List<V_HIS_SERE_SERV_TEIN> ssTeins)
        {
            bool result = false;
            try
            {
                if (String.IsNullOrWhiteSpace(SmsCFG.MERCHANT_CODE) || String.IsNullOrWhiteSpace(SmsCFG.SECURITY_CODE))
                {
                    return false;
                }

                if (data == null
                    || data.SERVICE_REQ_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__XN
                    || data.IS_INFORM_RESULT_BY_SMS != Constant.IS_TRUE
                    || data.SERVICE_REQ_STT_ID != IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__HT
                    || patient == null)
                {
                    return false;
                }

                string phoneNumber = !string.IsNullOrWhiteSpace(patient.MOBILE) ? patient.MOBILE : patient.PHONE;
                if (String.IsNullOrWhiteSpace(phoneNumber))
                {
                    LogSystem.Warn("Benh nhan khong co SDT. Khong gui duoc SMS ServiceReqCode: " + data.SERVICE_REQ_CODE);
                    return false;
                }

                sereServs = sereServs != null ? sereServs.Where(o => o.IS_NO_EXECUTE != Constant.IS_TRUE && o.AMOUNT > 0).ToList() : null;
                if (!IsNotNullOrEmpty(sereServs))
                {
                    LogSystem.Warn("Yeu cau khong co SereServ. Khong gui duoc SMS ServiceReqCode: " + data.SERVICE_REQ_CODE);
                    return false;
                }

                if (!IsNotNullOrEmpty(ssTeins))
                {
                    LogSystem.Warn("Yeu cau khong co SereServTein. Khong gui duoc SMS ServiceReqCode: " + data.SERVICE_REQ_CODE);
                    return false;
                }

                if (!ssTeins.Exists(e => !String.IsNullOrWhiteSpace(e.VALUE)))
                {
                    LogSystem.Warn("SereServTein chua co ket qua. Khong gui duoc SMS ServiceReqCode: " + data.SERVICE_REQ_CODE);
                    return false;
                }

                StringBuilder sb = new StringBuilder().Append("Tra ket qua XN: ")
                    .Append(data.SERVICE_REQ_CODE);
                ssTeins = ssTeins.OrderBy(o => o.SERE_SERV_ID).ThenByDescending(t => t.NUM_ORDER).ToList();
                foreach (var item in ssTeins)
                {
                    if (String.IsNullOrWhiteSpace(item.VALUE))
                    {
                        continue;
                    }
                    sb.Append("\n")
                        .Append(Inventec.Common.String.Convert.UnSignVNese(item.TEST_INDEX_NAME))
                        .Append(": ")
                        .Append(Inventec.Common.String.Convert.UnSignVNese(item.VALUE));
                    if (!String.IsNullOrWhiteSpace(item.TEST_INDEX_UNIT_NAME))
                    {
                        sb.Append(" - ")
                        .Append(Inventec.Common.String.Convert.UnSignVNese(item.TEST_INDEX_UNIT_NAME));
                    }
                }
                string ms = sb.ToString();
                foreach (string item in SYMBOL)
                {
                    ms = ms.Replace(item, "");
                }
                SmsMtSDO mtSDO = new SmsMtSDO();
                mtSDO.Content = sb.ToString();
                mtSDO.MerchantCode = SmsCFG.MERCHANT_CODE;
                mtSDO.SecurityCode = SmsCFG.SECURITY_CODE;
                mtSDO.Mobile = phoneNumber;

                var rs = ApiConsumerStore.SmsConsumer.Post<SmsMtSDO>(true, "api/SmsMt/Create", param, mtSDO);
                if (rs != null && rs.ResponseCode == SmsCFG.RESPONSE_CODE__SUCCESS)
                {
                    result = true;
                }
                else
                {
                    LogSystem.Warn("Gui yeu cau nhan tin sang SMS that bai ServiceReqCode: " + data.SERVICE_REQ_CODE + "." + LogUtil.TraceData("ResponseData", rs));
                    result = false;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }
    }
}
