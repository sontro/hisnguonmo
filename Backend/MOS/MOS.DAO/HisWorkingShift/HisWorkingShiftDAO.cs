using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisWorkingShift
{
    public partial class HisWorkingShiftDAO : EntityBase
    {
        private HisWorkingShiftCreate CreateWorker
        {
            get
            {
                return (HisWorkingShiftCreate)Worker.Get<HisWorkingShiftCreate>();
            }
        }
        private HisWorkingShiftUpdate UpdateWorker
        {
            get
            {
                return (HisWorkingShiftUpdate)Worker.Get<HisWorkingShiftUpdate>();
            }
        }
        private HisWorkingShiftDelete DeleteWorker
        {
            get
            {
                return (HisWorkingShiftDelete)Worker.Get<HisWorkingShiftDelete>();
            }
        }
        private HisWorkingShiftTruncate TruncateWorker
        {
            get
            {
                return (HisWorkingShiftTruncate)Worker.Get<HisWorkingShiftTruncate>();
            }
        }
        private HisWorkingShiftGet GetWorker
        {
            get
            {
                return (HisWorkingShiftGet)Worker.Get<HisWorkingShiftGet>();
            }
        }
        private HisWorkingShiftCheck CheckWorker
        {
            get
            {
                return (HisWorkingShiftCheck)Worker.Get<HisWorkingShiftCheck>();
            }
        }

        public bool Create(HIS_WORKING_SHIFT data)
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

        public bool CreateList(List<HIS_WORKING_SHIFT> listData)
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

        public bool Update(HIS_WORKING_SHIFT data)
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

        public bool UpdateList(List<HIS_WORKING_SHIFT> listData)
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

        public bool Delete(HIS_WORKING_SHIFT data)
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

        public bool DeleteList(List<HIS_WORKING_SHIFT> listData)
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

        public bool Truncate(HIS_WORKING_SHIFT data)
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

        public bool TruncateList(List<HIS_WORKING_SHIFT> listData)
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

        public List<HIS_WORKING_SHIFT> Get(HisWorkingShiftSO search, CommonParam param)
        {
            List<HIS_WORKING_SHIFT> result = new List<HIS_WORKING_SHIFT>();
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

        public HIS_WORKING_SHIFT GetById(long id, HisWorkingShiftSO search)
        {
            HIS_WORKING_SHIFT result = null;
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
