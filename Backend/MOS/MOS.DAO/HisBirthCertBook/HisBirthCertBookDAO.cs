using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisBirthCertBook
{
    public partial class HisBirthCertBookDAO : EntityBase
    {
        private HisBirthCertBookCreate CreateWorker
        {
            get
            {
                return (HisBirthCertBookCreate)Worker.Get<HisBirthCertBookCreate>();
            }
        }
        private HisBirthCertBookUpdate UpdateWorker
        {
            get
            {
                return (HisBirthCertBookUpdate)Worker.Get<HisBirthCertBookUpdate>();
            }
        }
        private HisBirthCertBookDelete DeleteWorker
        {
            get
            {
                return (HisBirthCertBookDelete)Worker.Get<HisBirthCertBookDelete>();
            }
        }
        private HisBirthCertBookTruncate TruncateWorker
        {
            get
            {
                return (HisBirthCertBookTruncate)Worker.Get<HisBirthCertBookTruncate>();
            }
        }
        private HisBirthCertBookGet GetWorker
        {
            get
            {
                return (HisBirthCertBookGet)Worker.Get<HisBirthCertBookGet>();
            }
        }
        private HisBirthCertBookCheck CheckWorker
        {
            get
            {
                return (HisBirthCertBookCheck)Worker.Get<HisBirthCertBookCheck>();
            }
        }

        public bool Create(HIS_BIRTH_CERT_BOOK data)
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

        public bool CreateList(List<HIS_BIRTH_CERT_BOOK> listData)
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

        public bool Update(HIS_BIRTH_CERT_BOOK data)
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

        public bool UpdateList(List<HIS_BIRTH_CERT_BOOK> listData)
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

        public bool Delete(HIS_BIRTH_CERT_BOOK data)
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

        public bool DeleteList(List<HIS_BIRTH_CERT_BOOK> listData)
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

        public bool Truncate(HIS_BIRTH_CERT_BOOK data)
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

        public bool TruncateList(List<HIS_BIRTH_CERT_BOOK> listData)
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

        public List<HIS_BIRTH_CERT_BOOK> Get(HisBirthCertBookSO search, CommonParam param)
        {
            List<HIS_BIRTH_CERT_BOOK> result = new List<HIS_BIRTH_CERT_BOOK>();
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

        public HIS_BIRTH_CERT_BOOK GetById(long id, HisBirthCertBookSO search)
        {
            HIS_BIRTH_CERT_BOOK result = null;
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
