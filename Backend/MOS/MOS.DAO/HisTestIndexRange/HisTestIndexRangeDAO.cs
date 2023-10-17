using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisTestIndexRange
{
    public partial class HisTestIndexRangeDAO : EntityBase
    {
        private HisTestIndexRangeCreate CreateWorker
        {
            get
            {
                return (HisTestIndexRangeCreate)Worker.Get<HisTestIndexRangeCreate>();
            }
        }
        private HisTestIndexRangeUpdate UpdateWorker
        {
            get
            {
                return (HisTestIndexRangeUpdate)Worker.Get<HisTestIndexRangeUpdate>();
            }
        }
        private HisTestIndexRangeDelete DeleteWorker
        {
            get
            {
                return (HisTestIndexRangeDelete)Worker.Get<HisTestIndexRangeDelete>();
            }
        }
        private HisTestIndexRangeTruncate TruncateWorker
        {
            get
            {
                return (HisTestIndexRangeTruncate)Worker.Get<HisTestIndexRangeTruncate>();
            }
        }
        private HisTestIndexRangeGet GetWorker
        {
            get
            {
                return (HisTestIndexRangeGet)Worker.Get<HisTestIndexRangeGet>();
            }
        }
        private HisTestIndexRangeCheck CheckWorker
        {
            get
            {
                return (HisTestIndexRangeCheck)Worker.Get<HisTestIndexRangeCheck>();
            }
        }

        public bool Create(HIS_TEST_INDEX_RANGE data)
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

        public bool CreateList(List<HIS_TEST_INDEX_RANGE> listData)
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

        public bool Update(HIS_TEST_INDEX_RANGE data)
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

        public bool UpdateList(List<HIS_TEST_INDEX_RANGE> listData)
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

        public bool Delete(HIS_TEST_INDEX_RANGE data)
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

        public bool DeleteList(List<HIS_TEST_INDEX_RANGE> listData)
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

        public bool Truncate(HIS_TEST_INDEX_RANGE data)
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

        public bool TruncateList(List<HIS_TEST_INDEX_RANGE> listData)
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

        public List<HIS_TEST_INDEX_RANGE> Get(HisTestIndexRangeSO search, CommonParam param)
        {
            List<HIS_TEST_INDEX_RANGE> result = new List<HIS_TEST_INDEX_RANGE>();
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

        public HIS_TEST_INDEX_RANGE GetById(long id, HisTestIndexRangeSO search)
        {
            HIS_TEST_INDEX_RANGE result = null;
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
