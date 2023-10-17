using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisTestSampleType
{
    public partial class HisTestSampleTypeDAO : EntityBase
    {
        private HisTestSampleTypeCreate CreateWorker
        {
            get
            {
                return (HisTestSampleTypeCreate)Worker.Get<HisTestSampleTypeCreate>();
            }
        }
        private HisTestSampleTypeUpdate UpdateWorker
        {
            get
            {
                return (HisTestSampleTypeUpdate)Worker.Get<HisTestSampleTypeUpdate>();
            }
        }
        private HisTestSampleTypeDelete DeleteWorker
        {
            get
            {
                return (HisTestSampleTypeDelete)Worker.Get<HisTestSampleTypeDelete>();
            }
        }
        private HisTestSampleTypeTruncate TruncateWorker
        {
            get
            {
                return (HisTestSampleTypeTruncate)Worker.Get<HisTestSampleTypeTruncate>();
            }
        }
        private HisTestSampleTypeGet GetWorker
        {
            get
            {
                return (HisTestSampleTypeGet)Worker.Get<HisTestSampleTypeGet>();
            }
        }
        private HisTestSampleTypeCheck CheckWorker
        {
            get
            {
                return (HisTestSampleTypeCheck)Worker.Get<HisTestSampleTypeCheck>();
            }
        }

        public bool Create(HIS_TEST_SAMPLE_TYPE data)
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

        public bool CreateList(List<HIS_TEST_SAMPLE_TYPE> listData)
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

        public bool Update(HIS_TEST_SAMPLE_TYPE data)
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

        public bool UpdateList(List<HIS_TEST_SAMPLE_TYPE> listData)
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

        public bool Delete(HIS_TEST_SAMPLE_TYPE data)
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

        public bool DeleteList(List<HIS_TEST_SAMPLE_TYPE> listData)
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

        public bool Truncate(HIS_TEST_SAMPLE_TYPE data)
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

        public bool TruncateList(List<HIS_TEST_SAMPLE_TYPE> listData)
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

        public List<HIS_TEST_SAMPLE_TYPE> Get(HisTestSampleTypeSO search, CommonParam param)
        {
            List<HIS_TEST_SAMPLE_TYPE> result = new List<HIS_TEST_SAMPLE_TYPE>();
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

        public HIS_TEST_SAMPLE_TYPE GetById(long id, HisTestSampleTypeSO search)
        {
            HIS_TEST_SAMPLE_TYPE result = null;
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
