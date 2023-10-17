using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisExpMestMaterial
{
    public partial class HisExpMestMaterialDAO : EntityBase
    {
        private HisExpMestMaterialCreate CreateWorker
        {
            get
            {
                return (HisExpMestMaterialCreate)Worker.Get<HisExpMestMaterialCreate>();
            }
        }
        private HisExpMestMaterialUpdate UpdateWorker
        {
            get
            {
                return (HisExpMestMaterialUpdate)Worker.Get<HisExpMestMaterialUpdate>();
            }
        }
        private HisExpMestMaterialDelete DeleteWorker
        {
            get
            {
                return (HisExpMestMaterialDelete)Worker.Get<HisExpMestMaterialDelete>();
            }
        }
        private HisExpMestMaterialTruncate TruncateWorker
        {
            get
            {
                return (HisExpMestMaterialTruncate)Worker.Get<HisExpMestMaterialTruncate>();
            }
        }
        private HisExpMestMaterialGet GetWorker
        {
            get
            {
                return (HisExpMestMaterialGet)Worker.Get<HisExpMestMaterialGet>();
            }
        }
        private HisExpMestMaterialCheck CheckWorker
        {
            get
            {
                return (HisExpMestMaterialCheck)Worker.Get<HisExpMestMaterialCheck>();
            }
        }

        public bool Create(HIS_EXP_MEST_MATERIAL data)
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

        public bool CreateList(List<HIS_EXP_MEST_MATERIAL> listData)
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

        public bool Update(HIS_EXP_MEST_MATERIAL data)
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

        public bool UpdateList(List<HIS_EXP_MEST_MATERIAL> listData)
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

        public bool Delete(HIS_EXP_MEST_MATERIAL data)
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

        public bool DeleteList(List<HIS_EXP_MEST_MATERIAL> listData)
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

        public bool Truncate(HIS_EXP_MEST_MATERIAL data)
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

        public bool TruncateList(List<HIS_EXP_MEST_MATERIAL> listData)
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

        public List<HIS_EXP_MEST_MATERIAL> Get(HisExpMestMaterialSO search, CommonParam param)
        {
            List<HIS_EXP_MEST_MATERIAL> result = new List<HIS_EXP_MEST_MATERIAL>();
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

        public HIS_EXP_MEST_MATERIAL GetById(long id, HisExpMestMaterialSO search)
        {
            HIS_EXP_MEST_MATERIAL result = null;
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
