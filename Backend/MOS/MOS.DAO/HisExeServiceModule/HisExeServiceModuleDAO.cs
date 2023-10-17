using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisExeServiceModule
{
    public partial class HisExeServiceModuleDAO : EntityBase
    {
        private HisExeServiceModuleCreate CreateWorker
        {
            get
            {
                return (HisExeServiceModuleCreate)Worker.Get<HisExeServiceModuleCreate>();
            }
        }
        private HisExeServiceModuleUpdate UpdateWorker
        {
            get
            {
                return (HisExeServiceModuleUpdate)Worker.Get<HisExeServiceModuleUpdate>();
            }
        }
        private HisExeServiceModuleDelete DeleteWorker
        {
            get
            {
                return (HisExeServiceModuleDelete)Worker.Get<HisExeServiceModuleDelete>();
            }
        }
        private HisExeServiceModuleTruncate TruncateWorker
        {
            get
            {
                return (HisExeServiceModuleTruncate)Worker.Get<HisExeServiceModuleTruncate>();
            }
        }
        private HisExeServiceModuleGet GetWorker
        {
            get
            {
                return (HisExeServiceModuleGet)Worker.Get<HisExeServiceModuleGet>();
            }
        }
        private HisExeServiceModuleCheck CheckWorker
        {
            get
            {
                return (HisExeServiceModuleCheck)Worker.Get<HisExeServiceModuleCheck>();
            }
        }

        public bool Create(HIS_EXE_SERVICE_MODULE data)
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

        public bool CreateList(List<HIS_EXE_SERVICE_MODULE> listData)
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

        public bool Update(HIS_EXE_SERVICE_MODULE data)
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

        public bool UpdateList(List<HIS_EXE_SERVICE_MODULE> listData)
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

        public bool Delete(HIS_EXE_SERVICE_MODULE data)
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

        public bool DeleteList(List<HIS_EXE_SERVICE_MODULE> listData)
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

        public bool Truncate(HIS_EXE_SERVICE_MODULE data)
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

        public bool TruncateList(List<HIS_EXE_SERVICE_MODULE> listData)
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

        public List<HIS_EXE_SERVICE_MODULE> Get(HisExeServiceModuleSO search, CommonParam param)
        {
            List<HIS_EXE_SERVICE_MODULE> result = new List<HIS_EXE_SERVICE_MODULE>();
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

        public HIS_EXE_SERVICE_MODULE GetById(long id, HisExeServiceModuleSO search)
        {
            HIS_EXE_SERVICE_MODULE result = null;
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
