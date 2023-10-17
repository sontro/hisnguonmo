using Inventec.Common.Logging;
using SDA.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.Config
{
    /// <summary>
    /// Luu cac cau hinh de tich hop voi Lis labconn
    /// </summary>
    class LisLabconnCFG
    {
        private const string IS_USING_SID_INSTEAD_OF_SERVICE_REQ_CODE_CFG = "MOS.LIS.LABCONN.IS_USING_SID_INSTEAD_OF_SERVICE_REQ_CODE";
        private const string REQUEST_ROOM_CODE_PREFIX_CFG = "MOS.LIS.LABCONN.REQUEST_ROOM_CODE_PREFIX";
        private const string REQUEST_DEPRTMENT_CODE_PREFIX_CFG = "MOS.LIS.LABCONN.REQUEST_DEPRTMENT_CODE_PREFIX";

        private static bool? isUsingSidInsteadOfServiceReqCode;
        public static bool IS_USING_SID_INSTEAD_OF_SERVICE_REQ_CODE
        {
            get
            {
                if (!isUsingSidInsteadOfServiceReqCode.HasValue)
                {
                    isUsingSidInsteadOfServiceReqCode = ConfigUtil.GetIntConfig(IS_USING_SID_INSTEAD_OF_SERVICE_REQ_CODE_CFG) == 1;
                }
                return isUsingSidInsteadOfServiceReqCode.Value;
            }
        }

        private static string requestRoomCodePrefix;
        public static string REQUEST_ROOM_CODE_PREFIX
        {
            get
            {
                if (requestRoomCodePrefix == null)
                {
                    requestRoomCodePrefix = ConfigUtil.GetStrConfig(REQUEST_ROOM_CODE_PREFIX_CFG);
                }
                return requestRoomCodePrefix;
            }
            set
            {
                requestRoomCodePrefix = value;
            }
        }

        private static string requestDepartmentCodePrefix;
        public static string REQUEST_DEPRTMENT_CODE_PREFIX
        {
            get
            {
                if (requestDepartmentCodePrefix == null)
                {
                    requestDepartmentCodePrefix = ConfigUtil.GetStrConfig(REQUEST_DEPRTMENT_CODE_PREFIX_CFG);
                }
                return requestDepartmentCodePrefix;
            }
            set
            {
                requestDepartmentCodePrefix = value;
            }
        }

        public static void Reload()
        {
            isUsingSidInsteadOfServiceReqCode = ConfigUtil.GetIntConfig(IS_USING_SID_INSTEAD_OF_SERVICE_REQ_CODE_CFG) == 1;
            requestDepartmentCodePrefix = ConfigUtil.GetStrConfig(REQUEST_DEPRTMENT_CODE_PREFIX_CFG);
            requestRoomCodePrefix = ConfigUtil.GetStrConfig(REQUEST_ROOM_CODE_PREFIX_CFG);
        }
    }
}
