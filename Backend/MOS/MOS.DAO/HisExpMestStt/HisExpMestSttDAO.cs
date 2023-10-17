using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisExpMestStt
{
    public partial class HisExpMestSttDAO : EntityBase
    {
        private HisExpMestSttCreate CreateWorker
        {
            get
            {
                return (HisExpMestSttCreate)Worker.Get<HisExpMestSttCreate>();
            }
        }
        private HisExpMestSttUpdate UpdateWorker
        {
            get
            {
                return (HisExpMestSttUpdate)Worker.Get<HisExpMestSttUpdate>();
            }
        }
        private HisExpMestSttDelete DeleteWorker
        {
            get
            {
                return (HisExpMestSttDelete)Worker.Get<HisExpMestSttDelete>();
            }
        }
        private HisExpMestSttTruncate TruncateWorker
        {
            get
            {
                return (HisExpMestSttTruncate)Worker.Get<HisExpMestSttTruncate>();
            }
        }
        private HisExpMestSttGet GetWorker
        {
            get
            {
                return (HisExpMestSttGet)Worker.Get<HisExpMestSttGet>();
            }
        }
        private HisExpMestSttCheck CheckWorker
        {
            get
            {
                return (HisExpMestSttCheck)Worker.Get<HisExpMestSttCheck>();
            }
        }

        public bool Create(HIS_EXP_MEST_STT data)
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

        public bool CreateList(List<HIS_EXP_MEST_STT> listData)
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

        public bool Update(HIS_EXP_MEST_STT data)
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

        public bool UpdateList(List<HIS_EXP_MEST_STT> listData)
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

        public bool Delete(HIS_EXP_MEST_STT data)
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

        public bool DeleteList(List<HIS_EXP_MEST_STT> listData)
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

        public bool Truncate(HIS_EXP_MEST_STT data)
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

        public bool TruncateList(List<HIS_EXP_MEST_STT> listData)
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

        public List<HIS_EXP_MEST_STT> Get(HisExpMestSttSO search, CommonParam param)
        {
            List<HIS_EXP_MEST_STT> result = new List<HIS_EXP_MEST_STT>();
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

        public HIS_EXP_MEST_STT GetById(long id, HisExpMestSttSO search)
        {
            HIS_EXP_MEST_STT result = null;
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
