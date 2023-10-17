using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisHoldReturn
{
    public partial class HisHoldReturnDAO : EntityBase
    {
        private HisHoldReturnCreate CreateWorker
        {
            get
            {
                return (HisHoldReturnCreate)Worker.Get<HisHoldReturnCreate>();
            }
        }
        private HisHoldReturnUpdate UpdateWorker
        {
            get
            {
                return (HisHoldReturnUpdate)Worker.Get<HisHoldReturnUpdate>();
            }
        }
        private HisHoldReturnDelete DeleteWorker
        {
            get
            {
                return (HisHoldReturnDelete)Worker.Get<HisHoldReturnDelete>();
            }
        }
        private HisHoldReturnTruncate TruncateWorker
        {
            get
            {
                return (HisHoldReturnTruncate)Worker.Get<HisHoldReturnTruncate>();
            }
        }
        private HisHoldReturnGet GetWorker
        {
            get
            {
                return (HisHoldReturnGet)Worker.Get<HisHoldReturnGet>();
            }
        }
        private HisHoldReturnCheck CheckWorker
        {
            get
            {
                return (HisHoldReturnCheck)Worker.Get<HisHoldReturnCheck>();
            }
        }

        public bool Create(HIS_HOLD_RETURN data)
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

        public bool CreateList(List<HIS_HOLD_RETURN> listData)
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

        public bool Update(HIS_HOLD_RETURN data)
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

        public bool UpdateList(List<HIS_HOLD_RETURN> listData)
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

        public bool Delete(HIS_HOLD_RETURN data)
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

        public bool DeleteList(List<HIS_HOLD_RETURN> listData)
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

        public bool Truncate(HIS_HOLD_RETURN data)
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

        public bool TruncateList(List<HIS_HOLD_RETURN> listData)
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

        public List<HIS_HOLD_RETURN> Get(HisHoldReturnSO search, CommonParam param)
        {
            List<HIS_HOLD_RETURN> result = new List<HIS_HOLD_RETURN>();
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

        public HIS_HOLD_RETURN GetById(long id, HisHoldReturnSO search)
        {
            HIS_HOLD_RETURN result = null;
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
