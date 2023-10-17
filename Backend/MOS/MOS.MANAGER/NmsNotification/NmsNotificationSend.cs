using Inventec.Core;
using Inventec.Token.ResourceSystem;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.NmsNotification
{
    class NmsNotificationSend : BusinessBase
    {
        internal enum Category
        {
            KQ_CLS,
            VAO_KHAM,
            TAI_KHAM
        }
        class NmsNotificationSendRequest
        {
            public string ApplicationCode { get; set; }
            public string CardCode  { get; set; }
            public string PhoneNumber { get; set; }
            public string Content { get; set; }
            public string CategoryCode { get; set; }
        }

        public NmsNotificationSend()
            :base()
        {

        }

        public NmsNotificationSend(CommonParam param)
            :base(param)
        {

        }

        internal bool SendByIdentifierInfo(string content, string cardCode, string phoneNumber, Category category)
        {
            bool result = false;
            try
            {
                CommonParam apiParam = new CommonParam();
                NmsNotificationSendRequest requestData = new NmsNotificationSendRequest();
                requestData.ApplicationCode = UTILITY.Constant.THE_VIET_APPLICATION_CODE;
                requestData.CardCode = cardCode;
                requestData.PhoneNumber = phoneNumber;
                requestData.Content = content;
                switch (category)
                {
                    case Category.KQ_CLS:
                        requestData.CategoryCode = "029";
                        break;
                    case Category.VAO_KHAM:
                        requestData.CategoryCode = "030";
                        break;
                    case Category.TAI_KHAM:
                        requestData.CategoryCode = "031";
                        break;
                    default:
                        break;
                }
                Inventec.Common.Logging.LogSystem.Info(Inventec.Common.Logging.LogUtil.TraceData("NmsNotification/SendByIndentifierInfo() - requestData:", requestData));
                bool resultData = MOS.ApiConsumerManager.ApiConsumerStore.NmsConsumer.Post<bool>(true, "api/NmsNotification/SendByIdentifierInfo", apiParam, requestData);
                Inventec.Common.Logging.LogSystem.Info(Inventec.Common.Logging.LogUtil.TraceData("NmsNotification/SendByIndentifierInfo() - resultData:", resultData));
                if (resultData)
                {
                    result = true;
                }
                else if (apiParam != null)
                {
                    Inventec.Common.Logging.LogSystem.Warn(apiParam.GetMessage());
                }
            }
            catch (Exception ex)
            {
                result = false;
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return result;
        }
    }
}
