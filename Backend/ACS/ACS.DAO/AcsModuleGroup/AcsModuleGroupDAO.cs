using ACS.DAO.StagingObject;
using ACS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace ACS.DAO.AcsModuleGroup
{
    public partial class AcsModuleGroupDAO : EntityBase
    {
        private AcsModuleGroupCreate CreateWorker
        {
            get
            {
                return (AcsModuleGroupCreate)Worker.Get<AcsModuleGroupCreate>();
            }
        }
        private AcsModuleGroupUpdate UpdateWorker
        {
            get
            {
                return (AcsModuleGroupUpdate)Worker.Get<AcsModuleGroupUpdate>();
            }
        }
        private AcsModuleGroupDelete DeleteWorker
        {
            get
            {
                return (AcsModuleGroupDelete)Worker.Get<AcsModuleGroupDelete>();
            }
        }
        private AcsModuleGroupTruncate TruncateWorker
        {
            get
            {
                return (AcsModuleGroupTruncate)Worker.Get<AcsModuleGroupTruncate>();
            }
        }
        private AcsModuleGroupGet GetWorker
        {
            get
            {
                return (AcsModuleGroupGet)Worker.Get<AcsModuleGroupGet>();
            }
        }
        private AcsModuleGroupCheck CheckWorker
        {
            get
            {
                return (AcsModuleGroupCheck)Worker.Get<AcsModuleGroupCheck>();
            }
        }

        public bool Create(ACS_MODULE_GROUP data)
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

        public bool CreateList(List<ACS_MODULE_GROUP> listData)
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

        public bool Update(ACS_MODULE_GROUP data)
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

        public bool UpdateList(List<ACS_MODULE_GROUP> listData)
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

        public bool Delete(ACS_MODULE_GROUP data)
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

        public bool DeleteList(List<ACS_MODULE_GROUP> listData)
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

        public bool Truncate(ACS_MODULE_GROUP data)
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

        public bool TruncateList(List<ACS_MODULE_GROUP> listData)
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

        public List<ACS_MODULE_GROUP> Get(AcsModuleGroupSO search, CommonParam param)
        {
            List<ACS_MODULE_GROUP> result = new List<ACS_MODULE_GROUP>();
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

        public ACS_MODULE_GROUP GetById(long id, AcsModuleGroupSO search)
        {
            ACS_MODULE_GROUP result = null;
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
