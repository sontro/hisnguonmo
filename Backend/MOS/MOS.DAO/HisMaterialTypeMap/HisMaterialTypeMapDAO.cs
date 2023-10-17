using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisMaterialTypeMap
{
    public partial class HisMaterialTypeMapDAO : EntityBase
    {
        private HisMaterialTypeMapCreate CreateWorker
        {
            get
            {
                return (HisMaterialTypeMapCreate)Worker.Get<HisMaterialTypeMapCreate>();
            }
        }
        private HisMaterialTypeMapUpdate UpdateWorker
        {
            get
            {
                return (HisMaterialTypeMapUpdate)Worker.Get<HisMaterialTypeMapUpdate>();
            }
        }
        private HisMaterialTypeMapDelete DeleteWorker
        {
            get
            {
                return (HisMaterialTypeMapDelete)Worker.Get<HisMaterialTypeMapDelete>();
            }
        }
        private HisMaterialTypeMapTruncate TruncateWorker
        {
            get
            {
                return (HisMaterialTypeMapTruncate)Worker.Get<HisMaterialTypeMapTruncate>();
            }
        }
        private HisMaterialTypeMapGet GetWorker
        {
            get
            {
                return (HisMaterialTypeMapGet)Worker.Get<HisMaterialTypeMapGet>();
            }
        }
        private HisMaterialTypeMapCheck CheckWorker
        {
            get
            {
                return (HisMaterialTypeMapCheck)Worker.Get<HisMaterialTypeMapCheck>();
            }
        }

        public bool Create(HIS_MATERIAL_TYPE_MAP data)
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

        public bool CreateList(List<HIS_MATERIAL_TYPE_MAP> listData)
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

        public bool Update(HIS_MATERIAL_TYPE_MAP data)
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

        public bool UpdateList(List<HIS_MATERIAL_TYPE_MAP> listData)
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

        public bool Delete(HIS_MATERIAL_TYPE_MAP data)
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

        public bool DeleteList(List<HIS_MATERIAL_TYPE_MAP> listData)
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

        public bool Truncate(HIS_MATERIAL_TYPE_MAP data)
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

        public bool TruncateList(List<HIS_MATERIAL_TYPE_MAP> listData)
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

        public List<HIS_MATERIAL_TYPE_MAP> Get(HisMaterialTypeMapSO search, CommonParam param)
        {
            List<HIS_MATERIAL_TYPE_MAP> result = new List<HIS_MATERIAL_TYPE_MAP>();
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

        public HIS_MATERIAL_TYPE_MAP GetById(long id, HisMaterialTypeMapSO search)
        {
            HIS_MATERIAL_TYPE_MAP result = null;
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
