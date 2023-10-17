using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisFuexType
{
    public partial class HisFuexTypeDAO : EntityBase
    {
        private HisFuexTypeCreate CreateWorker
        {
            get
            {
                return (HisFuexTypeCreate)Worker.Get<HisFuexTypeCreate>();
            }
        }
        private HisFuexTypeUpdate UpdateWorker
        {
            get
            {
                return (HisFuexTypeUpdate)Worker.Get<HisFuexTypeUpdate>();
            }
        }
        private HisFuexTypeDelete DeleteWorker
        {
            get
            {
                return (HisFuexTypeDelete)Worker.Get<HisFuexTypeDelete>();
            }
        }
        private HisFuexTypeTruncate TruncateWorker
        {
            get
            {
                return (HisFuexTypeTruncate)Worker.Get<HisFuexTypeTruncate>();
            }
        }
        private HisFuexTypeGet GetWorker
        {
            get
            {
                return (HisFuexTypeGet)Worker.Get<HisFuexTypeGet>();
            }
        }
        private HisFuexTypeCheck CheckWorker
        {
            get
            {
                return (HisFuexTypeCheck)Worker.Get<HisFuexTypeCheck>();
            }
        }

        public bool Create(HIS_FUEX_TYPE data)
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

        public bool CreateList(List<HIS_FUEX_TYPE> listData)
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

        public bool Update(HIS_FUEX_TYPE data)
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

        public bool UpdateList(List<HIS_FUEX_TYPE> listData)
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

        public bool Delete(HIS_FUEX_TYPE data)
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

        public bool DeleteList(List<HIS_FUEX_TYPE> listData)
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

        public bool Truncate(HIS_FUEX_TYPE data)
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

        public bool TruncateList(List<HIS_FUEX_TYPE> listData)
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

        public List<HIS_FUEX_TYPE> Get(HisFuexTypeSO search, CommonParam param)
        {
            List<HIS_FUEX_TYPE> result = new List<HIS_FUEX_TYPE>();
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

        public HIS_FUEX_TYPE GetById(long id, HisFuexTypeSO search)
        {
            HIS_FUEX_TYPE result = null;
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
