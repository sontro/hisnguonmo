using Inventec.Common.Logging;

using MOS.EFMODEL.DataModels;
using SDA.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.Config
{
    class HisServiceReqTypeMappingCFG
    {
        public static Dictionary<long, long> SERVICE_TYPE_SERVICE_REQ_TYPE_MAPPING = new Dictionary<long, long>() {
            {IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__G, IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__G},
            {IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__CDHA, IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__CDHA},
            {IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__NS, IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__NS},
            {IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__KH, IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__KH},
            {IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__TDCN, IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__TDCN},
            {IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__TT, IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__TT},
            {IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__KHAC, IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__KHAC},
            {IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__SA, IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__SA},
            {IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__PT, IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__PT},
            {IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__PHCN, IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__PHCN},
            {IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__XN, IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__XN},
            {IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__GPBL, IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__GPBL},
            {IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__AN, IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__AN},
            {IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__THUOC, IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__KHAC},
        };

        public static Dictionary<long, long> SERVICE_REQ_TYPE_SERVICE_TYPE_MAPPING = new Dictionary<long, long>() {
            {IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__G, IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__G},
            {IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__CDHA, IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__CDHA},
            {IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__NS, IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__NS},
            {IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__KH, IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__KH},
            {IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__TDCN, IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__TDCN},
            {IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__TT, IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__TT},
            {IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__KHAC, IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__KHAC},
            {IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__SA, IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__SA},
            {IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__PT, IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__PT},
            {IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__PHCN, IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__PHCN},
            {IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__XN, IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__XN},
            {IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__GPBL, IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__GPBL},
            {IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__AN, IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__AN}
            ,
        };
    }
}
