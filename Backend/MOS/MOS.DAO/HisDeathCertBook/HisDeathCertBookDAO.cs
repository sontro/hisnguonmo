using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisDeathCertBook
{
    public partial class HisDeathCertBookDAO : EntityBase
    {
        private HisDeathCertBookCreate CreateWorker
        {
            get
            {
                return (HisDeathCertBookCreate)Worker.Get<HisDeathCertBookCreate>();
            }
        }
        private HisDeathCertBookUpdate UpdateWorker
        {
            get
            {
                return (HisDeathCertBookUpdate)Worker.Get<HisDeathCertBookUpdate>();
            }
        }
        private HisDeathCertBookDelete DeleteWorker
        {
            get
            {
                return (HisDeathCertBookDelete)Worker.Get<HisDeathCertBookDelete>();
            }
        }
        private HisDeathCertBookTruncate TruncateWorker
        {
            get
            {
                return (HisDeathCertBookTruncate)Worker.Get<HisDeathCertBookTruncate>();
            }
        }
        private HisDeathCertBookGet GetWorker
        {
            get
            {
                return (HisDeathCertBookGet)Worker.Get<HisDeathCertBookGet>();
            }
        }
        private HisDeathCertBookCheck CheckWorker
        {
            get
            {
                return (HisDeathCertBookCheck)Worker.Get<HisDeathCertBookCheck>();
            }
        }

        public bool Create(HIS_DEATH_CERT_BOOK data)
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

        public bool CreateList(List<HIS_DEATH_CERT_BOOK> listData)
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

        public bool Update(HIS_DEATH_CERT_BOOK data)
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

        public bool UpdateList(List<HIS_DEATH_CERT_BOOK> listData)
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

        public bool Delete(HIS_DEATH_CERT_BOOK data)
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

        public bool DeleteList(List<HIS_DEATH_CERT_BOOK> listData)
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

        public bool Truncate(HIS_DEATH_CERT_BOOK data)
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

        public bool TruncateList(List<HIS_DEATH_CERT_BOOK> listData)
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

        public List<HIS_DEATH_CERT_BOOK> Get(HisDeathCertBookSO search, CommonParam param)
        {
            List<HIS_DEATH_CERT_BOOK> result = new List<HIS_DEATH_CERT_BOOK>();
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

        public HIS_DEATH_CERT_BOOK GetById(long id, HisDeathCertBookSO search)
        {
            HIS_DEATH_CERT_BOOK result = null;
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
