using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisRationSumStt
{
    public partial class HisRationSumSttDAO : EntityBase
    {
        private HisRationSumSttCreate CreateWorker
        {
            get
            {
                return (HisRationSumSttCreate)Worker.Get<HisRationSumSttCreate>();
            }
        }
        private HisRationSumSttUpdate UpdateWorker
        {
            get
            {
                return (HisRationSumSttUpdate)Worker.Get<HisRationSumSttUpdate>();
            }
        }
        private HisRationSumSttDelete DeleteWorker
        {
            get
            {
                return (HisRationSumSttDelete)Worker.Get<HisRationSumSttDelete>();
            }
        }
        private HisRationSumSttTruncate TruncateWorker
        {
            get
            {
                return (HisRationSumSttTruncate)Worker.Get<HisRationSumSttTruncate>();
            }
        }
        private HisRationSumSttGet GetWorker
        {
            get
            {
                return (HisRationSumSttGet)Worker.Get<HisRationSumSttGet>();
            }
        }
        private HisRationSumSttCheck CheckWorker
        {
            get
            {
                return (HisRationSumSttCheck)Worker.Get<HisRationSumSttCheck>();
            }
        }

        public bool Create(HIS_RATION_SUM_STT data)
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

        public bool CreateList(List<HIS_RATION_SUM_STT> listData)
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

        public bool Update(HIS_RATION_SUM_STT data)
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

        public bool UpdateList(List<HIS_RATION_SUM_STT> listData)
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

        public bool Delete(HIS_RATION_SUM_STT data)
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

        public bool DeleteList(List<HIS_RATION_SUM_STT> listData)
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

        public bool Truncate(HIS_RATION_SUM_STT data)
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

        public bool TruncateList(List<HIS_RATION_SUM_STT> listData)
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

        public List<HIS_RATION_SUM_STT> Get(HisRationSumSttSO search, CommonParam param)
        {
            List<HIS_RATION_SUM_STT> result = new List<HIS_RATION_SUM_STT>();
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

        public HIS_RATION_SUM_STT GetById(long id, HisRationSumSttSO search)
        {
            HIS_RATION_SUM_STT result = null;
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
