using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisRationSum
{
    public partial class HisRationSumDAO : EntityBase
    {
        private HisRationSumCreate CreateWorker
        {
            get
            {
                return (HisRationSumCreate)Worker.Get<HisRationSumCreate>();
            }
        }
        private HisRationSumUpdate UpdateWorker
        {
            get
            {
                return (HisRationSumUpdate)Worker.Get<HisRationSumUpdate>();
            }
        }
        private HisRationSumDelete DeleteWorker
        {
            get
            {
                return (HisRationSumDelete)Worker.Get<HisRationSumDelete>();
            }
        }
        private HisRationSumTruncate TruncateWorker
        {
            get
            {
                return (HisRationSumTruncate)Worker.Get<HisRationSumTruncate>();
            }
        }
        private HisRationSumGet GetWorker
        {
            get
            {
                return (HisRationSumGet)Worker.Get<HisRationSumGet>();
            }
        }
        private HisRationSumCheck CheckWorker
        {
            get
            {
                return (HisRationSumCheck)Worker.Get<HisRationSumCheck>();
            }
        }

        public bool Create(HIS_RATION_SUM data)
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

        public bool CreateList(List<HIS_RATION_SUM> listData)
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

        public bool Update(HIS_RATION_SUM data)
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

        public bool UpdateList(List<HIS_RATION_SUM> listData)
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

        public bool Delete(HIS_RATION_SUM data)
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

        public bool DeleteList(List<HIS_RATION_SUM> listData)
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

        public bool Truncate(HIS_RATION_SUM data)
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

        public bool TruncateList(List<HIS_RATION_SUM> listData)
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

        public List<HIS_RATION_SUM> Get(HisRationSumSO search, CommonParam param)
        {
            List<HIS_RATION_SUM> result = new List<HIS_RATION_SUM>();
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

        public HIS_RATION_SUM GetById(long id, HisRationSumSO search)
        {
            HIS_RATION_SUM result = null;
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
