using ACS.DAO.StagingObject;
using ACS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace ACS.DAO.AcsAppOtpType
{
    public partial class AcsAppOtpTypeDAO : EntityBase
    {
        private AcsAppOtpTypeCreate CreateWorker
        {
            get
            {
                return (AcsAppOtpTypeCreate)Worker.Get<AcsAppOtpTypeCreate>();
            }
        }
        private AcsAppOtpTypeUpdate UpdateWorker
        {
            get
            {
                return (AcsAppOtpTypeUpdate)Worker.Get<AcsAppOtpTypeUpdate>();
            }
        }
        private AcsAppOtpTypeDelete DeleteWorker
        {
            get
            {
                return (AcsAppOtpTypeDelete)Worker.Get<AcsAppOtpTypeDelete>();
            }
        }
        private AcsAppOtpTypeTruncate TruncateWorker
        {
            get
            {
                return (AcsAppOtpTypeTruncate)Worker.Get<AcsAppOtpTypeTruncate>();
            }
        }
        private AcsAppOtpTypeGet GetWorker
        {
            get
            {
                return (AcsAppOtpTypeGet)Worker.Get<AcsAppOtpTypeGet>();
            }
        }
        private AcsAppOtpTypeCheck CheckWorker
        {
            get
            {
                return (AcsAppOtpTypeCheck)Worker.Get<AcsAppOtpTypeCheck>();
            }
        }

        public bool Create(ACS_APP_OTP_TYPE data)
        {
            bool result = false;
            try
            {
                result = CreateWorker.Create(data);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        public bool CreateList(List<ACS_APP_OTP_TYPE> listData)
        {
            bool result = false;
            try
            {
                result = CreateWorker.CreateList(listData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        public bool Update(ACS_APP_OTP_TYPE data)
        {
            bool result = false;
            try
            {
                result = UpdateWorker.Update(data);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        public bool UpdateList(List<ACS_APP_OTP_TYPE> listData)
        {
            bool result = false;
            try
            {
                result = UpdateWorker.UpdateList(listData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        public bool Delete(ACS_APP_OTP_TYPE data)
        {
            bool result = false;
            try
            {
                result = DeleteWorker.Delete(data);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        public bool DeleteList(List<ACS_APP_OTP_TYPE> listData)
        {
            bool result = false;

            try
            {
                result = DeleteWorker.DeleteList(listData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        public bool Truncate(ACS_APP_OTP_TYPE data)
        {
            bool result = false;
            try
            {
                result = TruncateWorker.Truncate(data);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        public bool TruncateList(List<ACS_APP_OTP_TYPE> listData)
        {
            bool result = false;
            try
            {
                result = TruncateWorker.TruncateList(listData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        public List<ACS_APP_OTP_TYPE> Get(AcsAppOtpTypeSO search, CommonParam param)
        {
            List<ACS_APP_OTP_TYPE> result = new List<ACS_APP_OTP_TYPE>();
            try
            {
                result = GetWorker.Get(search, param);
            }
            catch (Exception ex)
            {
                param.HasException = true;
                Inventec.Common.Logging.LogSystem.Error(ex);
                result.Clear();
            }
            return result;
        }

        public ACS_APP_OTP_TYPE GetById(long id, AcsAppOtpTypeSO search)
        {
            ACS_APP_OTP_TYPE result = null;
            try
            {
                result = GetWorker.GetById(id, search);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }

            return result;
        }

        public bool IsUnLock(long id)
        {
            try
            {
                return CheckWorker.IsUnLock(id);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                throw;
            }
        }
    }
}
