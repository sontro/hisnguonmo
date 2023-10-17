using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisLicenseClass
{
    public partial class HisLicenseClassDAO : EntityBase
    {
        private HisLicenseClassCreate CreateWorker
        {
            get
            {
                return (HisLicenseClassCreate)Worker.Get<HisLicenseClassCreate>();
            }
        }
        private HisLicenseClassUpdate UpdateWorker
        {
            get
            {
                return (HisLicenseClassUpdate)Worker.Get<HisLicenseClassUpdate>();
            }
        }
        private HisLicenseClassDelete DeleteWorker
        {
            get
            {
                return (HisLicenseClassDelete)Worker.Get<HisLicenseClassDelete>();
            }
        }
        private HisLicenseClassTruncate TruncateWorker
        {
            get
            {
                return (HisLicenseClassTruncate)Worker.Get<HisLicenseClassTruncate>();
            }
        }
        private HisLicenseClassGet GetWorker
        {
            get
            {
                return (HisLicenseClassGet)Worker.Get<HisLicenseClassGet>();
            }
        }
        private HisLicenseClassCheck CheckWorker
        {
            get
            {
                return (HisLicenseClassCheck)Worker.Get<HisLicenseClassCheck>();
            }
        }

        public bool Create(HIS_LICENSE_CLASS data)
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

        public bool CreateList(List<HIS_LICENSE_CLASS> listData)
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

        public bool Update(HIS_LICENSE_CLASS data)
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

        public bool UpdateList(List<HIS_LICENSE_CLASS> listData)
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

        public bool Delete(HIS_LICENSE_CLASS data)
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

        public bool DeleteList(List<HIS_LICENSE_CLASS> listData)
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

        public bool Truncate(HIS_LICENSE_CLASS data)
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

        public bool TruncateList(List<HIS_LICENSE_CLASS> listData)
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

        public List<HIS_LICENSE_CLASS> Get(HisLicenseClassSO search, CommonParam param)
        {
            List<HIS_LICENSE_CLASS> result = new List<HIS_LICENSE_CLASS>();
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

        public HIS_LICENSE_CLASS GetById(long id, HisLicenseClassSO search)
        {
            HIS_LICENSE_CLASS result = null;
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
