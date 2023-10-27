using ACS.DAO.StagingObject;
using ACS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace ACS.DAO.AcsOtp
{
    public partial class AcsOtpDAO : EntityBase
    {
        public List<V_ACS_OTP> GetView(AcsOtpSO search, CommonParam param)
        {
            List<V_ACS_OTP> result = new List<V_ACS_OTP>();
            try
            {
                result = GetWorker.GetView(search, param);
            }
            catch (Exception ex)
            {
                param.HasException = true;
                Inventec.Common.Logging.LogSystem.Error(ex);
                result.Clear();
            }
            return result;
        }

        public V_ACS_OTP GetViewById(long id, AcsOtpSO search)
        {
            V_ACS_OTP result = null;

            try
            {
                result = GetWorker.GetViewById(id, search);
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
