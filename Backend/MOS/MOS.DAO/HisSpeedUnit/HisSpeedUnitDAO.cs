using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisSpeedUnit
{
    public partial class HisSpeedUnitDAO : EntityBase
    {
        private HisSpeedUnitCreate CreateWorker
        {
            get
            {
                return (HisSpeedUnitCreate)Worker.Get<HisSpeedUnitCreate>();
            }
        }
        private HisSpeedUnitUpdate UpdateWorker
        {
            get
            {
                return (HisSpeedUnitUpdate)Worker.Get<HisSpeedUnitUpdate>();
            }
        }
        private HisSpeedUnitDelete DeleteWorker
        {
            get
            {
                return (HisSpeedUnitDelete)Worker.Get<HisSpeedUnitDelete>();
            }
        }
        private HisSpeedUnitTruncate TruncateWorker
        {
            get
            {
                return (HisSpeedUnitTruncate)Worker.Get<HisSpeedUnitTruncate>();
            }
        }
        private HisSpeedUnitGet GetWorker
        {
            get
            {
                return (HisSpeedUnitGet)Worker.Get<HisSpeedUnitGet>();
            }
        }
        private HisSpeedUnitCheck CheckWorker
        {
            get
            {
                return (HisSpeedUnitCheck)Worker.Get<HisSpeedUnitCheck>();
            }
        }

        public bool Create(HIS_SPEED_UNIT data)
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

        public bool CreateList(List<HIS_SPEED_UNIT> listData)
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

        public bool Update(HIS_SPEED_UNIT data)
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

        public bool UpdateList(List<HIS_SPEED_UNIT> listData)
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

        public bool Delete(HIS_SPEED_UNIT data)
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

        public bool DeleteList(List<HIS_SPEED_UNIT> listData)
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

        public bool Truncate(HIS_SPEED_UNIT data)
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

        public bool TruncateList(List<HIS_SPEED_UNIT> listData)
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

        public List<HIS_SPEED_UNIT> Get(HisSpeedUnitSO search, CommonParam param)
        {
            List<HIS_SPEED_UNIT> result = new List<HIS_SPEED_UNIT>();
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

        public HIS_SPEED_UNIT GetById(long id, HisSpeedUnitSO search)
        {
            HIS_SPEED_UNIT result = null;
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
