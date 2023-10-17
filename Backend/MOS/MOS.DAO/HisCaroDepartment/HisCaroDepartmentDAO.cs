using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisCaroDepartment
{
    public partial class HisCaroDepartmentDAO : EntityBase
    {
        private HisCaroDepartmentCreate CreateWorker
        {
            get
            {
                return (HisCaroDepartmentCreate)Worker.Get<HisCaroDepartmentCreate>();
            }
        }
        private HisCaroDepartmentUpdate UpdateWorker
        {
            get
            {
                return (HisCaroDepartmentUpdate)Worker.Get<HisCaroDepartmentUpdate>();
            }
        }
        private HisCaroDepartmentDelete DeleteWorker
        {
            get
            {
                return (HisCaroDepartmentDelete)Worker.Get<HisCaroDepartmentDelete>();
            }
        }
        private HisCaroDepartmentTruncate TruncateWorker
        {
            get
            {
                return (HisCaroDepartmentTruncate)Worker.Get<HisCaroDepartmentTruncate>();
            }
        }
        private HisCaroDepartmentGet GetWorker
        {
            get
            {
                return (HisCaroDepartmentGet)Worker.Get<HisCaroDepartmentGet>();
            }
        }
        private HisCaroDepartmentCheck CheckWorker
        {
            get
            {
                return (HisCaroDepartmentCheck)Worker.Get<HisCaroDepartmentCheck>();
            }
        }

        public bool Create(HIS_CARO_DEPARTMENT data)
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

        public bool CreateList(List<HIS_CARO_DEPARTMENT> listData)
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

        public bool Update(HIS_CARO_DEPARTMENT data)
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

        public bool UpdateList(List<HIS_CARO_DEPARTMENT> listData)
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

        public bool Delete(HIS_CARO_DEPARTMENT data)
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

        public bool DeleteList(List<HIS_CARO_DEPARTMENT> listData)
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

        public bool Truncate(HIS_CARO_DEPARTMENT data)
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

        public bool TruncateList(List<HIS_CARO_DEPARTMENT> listData)
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

        public List<HIS_CARO_DEPARTMENT> Get(HisCaroDepartmentSO search, CommonParam param)
        {
            List<HIS_CARO_DEPARTMENT> result = new List<HIS_CARO_DEPARTMENT>();
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

        public HIS_CARO_DEPARTMENT GetById(long id, HisCaroDepartmentSO search)
        {
            HIS_CARO_DEPARTMENT result = null;
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
