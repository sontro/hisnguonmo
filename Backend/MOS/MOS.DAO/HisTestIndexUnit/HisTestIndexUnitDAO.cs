using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisTestIndexUnit
{
    public partial class HisTestIndexUnitDAO : EntityBase
    {
        private HisTestIndexUnitCreate CreateWorker
        {
            get
            {
                return (HisTestIndexUnitCreate)Worker.Get<HisTestIndexUnitCreate>();
            }
        }
        private HisTestIndexUnitUpdate UpdateWorker
        {
            get
            {
                return (HisTestIndexUnitUpdate)Worker.Get<HisTestIndexUnitUpdate>();
            }
        }
        private HisTestIndexUnitDelete DeleteWorker
        {
            get
            {
                return (HisTestIndexUnitDelete)Worker.Get<HisTestIndexUnitDelete>();
            }
        }
        private HisTestIndexUnitTruncate TruncateWorker
        {
            get
            {
                return (HisTestIndexUnitTruncate)Worker.Get<HisTestIndexUnitTruncate>();
            }
        }
        private HisTestIndexUnitGet GetWorker
        {
            get
            {
                return (HisTestIndexUnitGet)Worker.Get<HisTestIndexUnitGet>();
            }
        }
        private HisTestIndexUnitCheck CheckWorker
        {
            get
            {
                return (HisTestIndexUnitCheck)Worker.Get<HisTestIndexUnitCheck>();
            }
        }

        public bool Create(HIS_TEST_INDEX_UNIT data)
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

        public bool CreateList(List<HIS_TEST_INDEX_UNIT> listData)
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

        public bool Update(HIS_TEST_INDEX_UNIT data)
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

        public bool UpdateList(List<HIS_TEST_INDEX_UNIT> listData)
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

        public bool Delete(HIS_TEST_INDEX_UNIT data)
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

        public bool DeleteList(List<HIS_TEST_INDEX_UNIT> listData)
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

        public bool Truncate(HIS_TEST_INDEX_UNIT data)
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

        public bool TruncateList(List<HIS_TEST_INDEX_UNIT> listData)
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

        public List<HIS_TEST_INDEX_UNIT> Get(HisTestIndexUnitSO search, CommonParam param)
        {
            List<HIS_TEST_INDEX_UNIT> result = new List<HIS_TEST_INDEX_UNIT>();
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

        public HIS_TEST_INDEX_UNIT GetById(long id, HisTestIndexUnitSO search)
        {
            HIS_TEST_INDEX_UNIT result = null;
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
