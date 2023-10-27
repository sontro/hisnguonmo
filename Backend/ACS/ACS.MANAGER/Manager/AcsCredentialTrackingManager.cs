using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Core.AcsUser;
using ACS.SDO;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.Manager
{
    public partial class AcsCredentialTrackingManager : Inventec.Backend.MANAGER.ManagerBase
    {
        public AcsCredentialTrackingManager()
            : base(new CommonParam())
        {
        }

        /// <summary>
        /// Phần mềm cần chức năng để kiểm soát số lượng máy/ tài khoản/ phiên bản phần mềm đang kết nối với hệ thống HIS
        /// để đánh giá hiệu năng, kiểm soát lỗi.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public object Get(object data)
        {
            object result = null;
            try
            {
                if (!IsNotNull(data)) throw new ArgumentNullException("data");

                List<AcsCredentialTrackingSDO> credentialTrackingSDOs = new List<AcsCredentialTrackingSDO>();

                AcsCredentialTrackingSDO itemTracking = new AcsCredentialTrackingSDO();

                credentialTrackingSDOs.Add(itemTracking);

                result = credentialTrackingSDOs;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }
            return result;
        }

    }
}
