using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisEmteMaterialType
{
    public partial class HisEmteMaterialTypeDAO : EntityBase
    {
        private HisEmteMaterialTypeCreate CreateWorker
        {
            get
            {
                return (HisEmteMaterialTypeCreate)Worker.Get<HisEmteMaterialTypeCreate>();
            }
        }
        private HisEmteMaterialTypeUpdate UpdateWorker
        {
            get
            {
                return (HisEmteMaterialTypeUpdate)Worker.Get<HisEmteMaterialTypeUpdate>();
            }
        }
        private HisEmteMaterialTypeDelete DeleteWorker
        {
            get
            {
                return (HisEmteMaterialTypeDelete)Worker.Get<HisEmteMaterialTypeDelete>();
            }
        }
        private HisEmteMaterialTypeTruncate TruncateWorker
        {
            get
            {
                return (HisEmteMaterialTypeTruncate)Worker.Get<HisEmteMaterialTypeTruncate>();
            }
        }
        private HisEmteMaterialTypeGet GetWorker
        {
            get
            {
                return (HisEmteMaterialTypeGet)Worker.Get<HisEmteMaterialTypeGet>();
            }
        }
        private HisEmteMaterialTypeCheck CheckWorker
        {
            get
            {
                return (HisEmteMaterialTypeCheck)Worker.Get<HisEmteMaterialTypeCheck>();
            }
        }

        public bool Create(HIS_EMTE_MATERIAL_TYPE data)
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

        public bool CreateList(List<HIS_EMTE_MATERIAL_TYPE> listData)
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

        public bool Update(HIS_EMTE_MATERIAL_TYPE data)
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

        public bool UpdateList(List<HIS_EMTE_MATERIAL_TYPE> listData)
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

        public bool Delete(HIS_EMTE_MATERIAL_TYPE data)
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

        public bool DeleteList(List<HIS_EMTE_MATERIAL_TYPE> listData)
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

        public bool Truncate(HIS_EMTE_MATERIAL_TYPE data)
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

        public bool TruncateList(List<HIS_EMTE_MATERIAL_TYPE> listData)
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

        public List<HIS_EMTE_MATERIAL_TYPE> Get(HisEmteMaterialTypeSO search, CommonParam param)
        {
            List<HIS_EMTE_MATERIAL_TYPE> result = new List<HIS_EMTE_MATERIAL_TYPE>();
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

        public HIS_EMTE_MATERIAL_TYPE GetById(long id, HisEmteMaterialTypeSO search)
        {
            HIS_EMTE_MATERIAL_TYPE result = null;
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
