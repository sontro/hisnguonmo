using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisImpMestMaterial
{
    public partial class HisImpMestMaterialDAO : EntityBase
    {
        private HisImpMestMaterialCreate CreateWorker
        {
            get
            {
                return (HisImpMestMaterialCreate)Worker.Get<HisImpMestMaterialCreate>();
            }
        }
        private HisImpMestMaterialUpdate UpdateWorker
        {
            get
            {
                return (HisImpMestMaterialUpdate)Worker.Get<HisImpMestMaterialUpdate>();
            }
        }
        private HisImpMestMaterialDelete DeleteWorker
        {
            get
            {
                return (HisImpMestMaterialDelete)Worker.Get<HisImpMestMaterialDelete>();
            }
        }
        private HisImpMestMaterialTruncate TruncateWorker
        {
            get
            {
                return (HisImpMestMaterialTruncate)Worker.Get<HisImpMestMaterialTruncate>();
            }
        }
        private HisImpMestMaterialGet GetWorker
        {
            get
            {
                return (HisImpMestMaterialGet)Worker.Get<HisImpMestMaterialGet>();
            }
        }
        private HisImpMestMaterialCheck CheckWorker
        {
            get
            {
                return (HisImpMestMaterialCheck)Worker.Get<HisImpMestMaterialCheck>();
            }
        }

        public bool Create(HIS_IMP_MEST_MATERIAL data)
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

        public bool CreateList(List<HIS_IMP_MEST_MATERIAL> listData)
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

        public bool Update(HIS_IMP_MEST_MATERIAL data)
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

        public bool UpdateList(List<HIS_IMP_MEST_MATERIAL> listData)
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

        public bool Delete(HIS_IMP_MEST_MATERIAL data)
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

        public bool DeleteList(List<HIS_IMP_MEST_MATERIAL> listData)
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

        public bool Truncate(HIS_IMP_MEST_MATERIAL data)
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

        public bool TruncateList(List<HIS_IMP_MEST_MATERIAL> listData)
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

        public List<HIS_IMP_MEST_MATERIAL> Get(HisImpMestMaterialSO search, CommonParam param)
        {
            List<HIS_IMP_MEST_MATERIAL> result = new List<HIS_IMP_MEST_MATERIAL>();
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

        public HIS_IMP_MEST_MATERIAL GetById(long id, HisImpMestMaterialSO search)
        {
            HIS_IMP_MEST_MATERIAL result = null;
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
