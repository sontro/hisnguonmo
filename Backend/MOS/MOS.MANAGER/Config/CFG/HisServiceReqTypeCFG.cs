using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.HisServiceReqType;
using SDA.EFMODEL.DataModels;
using System;
using System.Linq;
using System.Collections.Generic;

namespace MOS.MANAGER.Config
{
    public class HisServiceReqTypeCFG
    {
        private static List<HIS_SERVICE_REQ_TYPE> data;
        public static List<HIS_SERVICE_REQ_TYPE> DATA
        {
            get
            {
                if (data == null)
                {
                    data = new HisServiceReqTypeGet().Get(new HisServiceReqTypeFilterQuery());
                }
                return data;
            }
        }

        //Chi dinh don thuoc
        public static List<long> PRESCRIPTION_TYPE_IDs = new List<long>(){
                IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__DONK,
                IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__DONM,
                IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__DONDT,
                IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__DONTT,
            };

        //Chi dinh can lam sang
        public static List<long> SUBCLINICAL_TYPE_IDs = new List<long>(){
                IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__CDHA,
                IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__GPBL,
                IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__NS,
                IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__SA,
                IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__TDCN,
                IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__XN,
            };

        //Chi dinh lam sang
        public static List<long> CLINICAL_TYPE_IDs = new List<long>(){
                IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__KH,
                IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__PHCN,
                IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__PT,
                IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__TT,
            };

        public static List<long> PACS_TYPE_IDs = new List<long>(){
                IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__CDHA,
                IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__SA,
                IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__TDCN,
                IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__NS,
            };

        public static void Reload()
        {
            var tmp = new HisServiceReqTypeGet().Get(new HisServiceReqTypeFilterQuery());
            data = tmp;
        }
    }
}
