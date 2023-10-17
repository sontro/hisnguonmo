using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisDepartmentTran
{
    public partial class HisDepartmentTranDAO : EntityBase
    {
        private HisDepartmentTranCreate CreateWorker
        {
            get
            {
                return (HisDepartmentTranCreate)Worker.Get<HisDepartmentTranCreate>();
            }
        }
        private HisDepartmentTranUpdate UpdateWorker
        {
            get
            {
                return (HisDepartmentTranUpdate)Worker.Get<HisDepartmentTranUpdate>();
            }
        }
        private HisDepartmentTranDelete DeleteWorker
        {
            get
            {
                return (HisDepartmentTranDelete)Worker.Get<HisDepartmentTranDelete>();
            }
        }
        private HisDepartmentTranTruncate TruncateWorker
        {
            get
            {
                return (HisDepartmentTranTruncate)Worker.Get<HisDepartmentTranTruncate>();
            }
        }
        private HisDepartmentTranGet GetWorker
        {
            get
            {
                return (HisDepartmentTranGet)Worker.Get<HisDepartmentTranGet>();
            }
        }
        private HisDepartmentTranCheck CheckWorker
        {
            get
            {
                return (HisDepartmentTranCheck)Worker.Get<HisDepartmentTranCheck>();
            }
        }

        public bool Create(HIS_DEPARTMENT_TRAN data)
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

        public bool CreateList(List<HIS_DEPARTMENT_TRAN> listData)
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

        public bool Update(HIS_DEPARTMENT_TRAN data)
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

        public bool UpdateList(List<HIS_DEPARTMENT_TRAN> listData)
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

        public bool Delete(HIS_DEPARTMENT_TRAN data)
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

        public bool DeleteList(List<HIS_DEPARTMENT_TRAN> listData)
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

        public bool Truncate(HIS_DEPARTMENT_TRAN data)
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

        public bool TruncateList(List<HIS_DEPARTMENT_TRAN> listData)
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

        public List<HIS_DEPARTMENT_TRAN> Get(HisDepartmentTranSO search, CommonParam param)
        {
            List<HIS_DEPARTMENT_TRAN> result = new List<HIS_DEPARTMENT_TRAN>();
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

        public HIS_DEPARTMENT_TRAN GetById(long id, HisDepartmentTranSO search)
        {
            HIS_DEPARTMENT_TRAN result = null;
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
