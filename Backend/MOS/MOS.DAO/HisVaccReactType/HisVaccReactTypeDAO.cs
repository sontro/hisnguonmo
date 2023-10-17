using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisVaccReactType
{
    public partial class HisVaccReactTypeDAO : EntityBase
    {
        private HisVaccReactTypeCreate CreateWorker
        {
            get
            {
                return (HisVaccReactTypeCreate)Worker.Get<HisVaccReactTypeCreate>();
            }
        }
        private HisVaccReactTypeUpdate UpdateWorker
        {
            get
            {
                return (HisVaccReactTypeUpdate)Worker.Get<HisVaccReactTypeUpdate>();
            }
        }
        private HisVaccReactTypeDelete DeleteWorker
        {
            get
            {
                return (HisVaccReactTypeDelete)Worker.Get<HisVaccReactTypeDelete>();
            }
        }
        private HisVaccReactTypeTruncate TruncateWorker
        {
            get
            {
                return (HisVaccReactTypeTruncate)Worker.Get<HisVaccReactTypeTruncate>();
            }
        }
        private HisVaccReactTypeGet GetWorker
        {
            get
            {
                return (HisVaccReactTypeGet)Worker.Get<HisVaccReactTypeGet>();
            }
        }
        private HisVaccReactTypeCheck CheckWorker
        {
            get
            {
                return (HisVaccReactTypeCheck)Worker.Get<HisVaccReactTypeCheck>();
            }
        }

        public bool Create(HIS_VACC_REACT_TYPE data)
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

        public bool CreateList(List<HIS_VACC_REACT_TYPE> listData)
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

        public bool Update(HIS_VACC_REACT_TYPE data)
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

        public bool UpdateList(List<HIS_VACC_REACT_TYPE> listData)
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

        public bool Delete(HIS_VACC_REACT_TYPE data)
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

        public bool DeleteList(List<HIS_VACC_REACT_TYPE> listData)
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

        public bool Truncate(HIS_VACC_REACT_TYPE data)
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

        public bool TruncateList(List<HIS_VACC_REACT_TYPE> listData)
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

        public List<HIS_VACC_REACT_TYPE> Get(HisVaccReactTypeSO search, CommonParam param)
        {
            List<HIS_VACC_REACT_TYPE> result = new List<HIS_VACC_REACT_TYPE>();
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

        public HIS_VACC_REACT_TYPE GetById(long id, HisVaccReactTypeSO search)
        {
            HIS_VACC_REACT_TYPE result = null;
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
