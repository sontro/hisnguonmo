using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Inventec.Core;
using ACS.Filter;
using ACS.EFMODEL.DataModels;
using System.Collections.Generic;
using ACS.SDO;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Consumer.Init();
            CommonParam param = new CommonParam();
            AcsFilterLoginSDO filter = new AcsFilterLoginSDO();
            filter.LOGIN_NAME = "";//phuongtest__1
            filter.PASSWORD = "";
            var ro = GlobalStore.ApiConsumerObj.Get<Inventec.Core.ApiResultObject<ACS_EV_ACS_USER>>("/api/AcsUser/LoginForAcs", param, filter);
            if (ro != null)
            {
                param = ro.Param != null ? ro.Param : param;
            }

            //CommonParam param = new CommonParam();
            //Inventec.Common.WebApiClient.ApiConsumer acsConsumer = new Inventec.Common.WebApiClient.ApiConsumer("http://192.168.1.108:3005", false);
            //////Login 
            //AcsFilterLoginSDO filter = new AcsFilterLoginSDO();
            //filter.LOGIN_NAME = "phuongtest__1";
            //filter.PASSWORD = "Acs123456";
            //var roLogin = GlobalStore.ApiConsumerObj.Get<Inventec.Core.ApiResultObject<AWA_V_USER>>("/api/AcsUser/Login", param, filter);

            ////Change Password
            //AcsChangePasswordSDO data = new AcsChangePasswordSDO();
            //data.LOGIN_NAME = "phuongtest__1";
            //data.PASSWORD__OLD = "2";
            //data.PASSWORD__NEW = "1";
            //var roChange = GlobalStore.ApiConsumerObj.Post<Inventec.Core.ApiResultObject<bool>>("/api/AcsUser/ChangePassword", param, data);
            //if (roChange != null)
            //{
            //}

            //AWA_EV_USER data = new AWA_EV_USER();
            //data.LOGIN_NAME = "phuongtest__1";
            //data.PASSWORD_TEMP = "Acs123456@";
            //data.LANGUAGE = "vi";
            //data.GROUP_ID = 3;
            //data.G_CODE = "0000000002";
            //data.EMAIL = "p1@gmail.com";
            //var ro1 = GlobalStore.ApiConsumerObj.Post<Inventec.Core.ApiResultObject<AWA_EV_USER>>("/api/AcsUser/Create", param, data);
            //if (ro1 != null)
            //{
            //    param = ro1.Param != null ? ro1.Param : param;
            //}

            //Assert.AreEqual((ro != null && ro.Data != null), true);

        }
    }

    class Consumer
    {
        private static string loginName = "phuongdt";
        private static string password = "1";
        private static string uri = "http://192.168.1.108:3005";
        //private static string uri = "http://localhost:23233/";

        internal static void Init()
        {
            if (GlobalStore.ApiConsumerObj == null)
            {
                GlobalStore.ApiConsumerObj = new Inventec.Common.WebApiClient.ApiConsumer(uri, false);

            }
        }
    }

    public class GlobalStore
    {
        public static Inventec.Common.WebApiClient.ApiConsumer ApiConsumerObj { get; set; }
    }
}



