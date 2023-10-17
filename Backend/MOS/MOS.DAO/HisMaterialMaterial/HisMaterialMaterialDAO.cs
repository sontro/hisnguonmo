using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisMaterialMaterial
{
    public partial class HisMaterialMaterialDAO : EntityBase
    {
        private HisMaterialMaterialCreate CreateWorker
        {
            get
            {
                return (HisMaterialMaterialCreate)Worker.Get<HisMaterialMaterialCreate>();
            }
        }
        private HisMaterialMaterialUpdate UpdateWorker
        {
            get
            {
                return (HisMaterialMaterialUpdate)Worker.Get<HisMaterialMaterialUpdate>();
            }
        }
        private HisMaterialMaterialDelete DeleteWorker
        {
            get
            {
                return (HisMaterialMaterialDelete)Worker.Get<HisMaterialMaterialDelete>();
            }
        }
        private HisMaterialMaterialTruncate TruncateWorker
        {
            get
            {
                return (HisMaterialMaterialTruncate)Worker.Get<HisMaterialMaterialTruncate>();
            }
        }
        private HisMaterialMaterialGet GetWorker
        {
            get
            {
                return (HisMaterialMaterialGet)Worker.Get<HisMaterialMaterialGet>();
            }
        }
        private HisMaterialMaterialCheck CheckWorker
        {
            get
            {
                return (HisMaterialMaterialCheck)Worker.Get<HisMaterialMaterialCheck>();
            }
        }

        public bool Create(HIS_MATERIAL_MATERIAL data)
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

        public bool CreateList(List<HIS_MATERIAL_MATERIAL> listData)
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

        public bool Update(HIS_MATERIAL_MATERIAL data)
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

        public bool UpdateList(List<HIS_MATERIAL_MATERIAL> listData)
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

        public bool Delete(HIS_MATERIAL_MATERIAL data)
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

        public bool DeleteList(List<HIS_MATERIAL_MATERIAL> listData)
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

        public bool Truncate(HIS_MATERIAL_MATERIAL data)
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

        public bool TruncateList(List<HIS_MATERIAL_MATERIAL> listData)
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

        public List<HIS_MATERIAL_MATERIAL> Get(HisMaterialMaterialSO search, CommonParam param)
        {
            List<HIS_MATERIAL_MATERIAL> result = new List<HIS_MATERIAL_MATERIAL>();
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

        public HIS_MATERIAL_MATERIAL GetById(long id, HisMaterialMaterialSO search)
        {
            HIS_MATERIAL_MATERIAL result = null;
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
