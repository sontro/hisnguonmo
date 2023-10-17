using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisDiseaseRelation
{
    public partial class HisDiseaseRelationDAO : EntityBase
    {
        private HisDiseaseRelationCreate CreateWorker
        {
            get
            {
                return (HisDiseaseRelationCreate)Worker.Get<HisDiseaseRelationCreate>();
            }
        }
        private HisDiseaseRelationUpdate UpdateWorker
        {
            get
            {
                return (HisDiseaseRelationUpdate)Worker.Get<HisDiseaseRelationUpdate>();
            }
        }
        private HisDiseaseRelationDelete DeleteWorker
        {
            get
            {
                return (HisDiseaseRelationDelete)Worker.Get<HisDiseaseRelationDelete>();
            }
        }
        private HisDiseaseRelationTruncate TruncateWorker
        {
            get
            {
                return (HisDiseaseRelationTruncate)Worker.Get<HisDiseaseRelationTruncate>();
            }
        }
        private HisDiseaseRelationGet GetWorker
        {
            get
            {
                return (HisDiseaseRelationGet)Worker.Get<HisDiseaseRelationGet>();
            }
        }
        private HisDiseaseRelationCheck CheckWorker
        {
            get
            {
                return (HisDiseaseRelationCheck)Worker.Get<HisDiseaseRelationCheck>();
            }
        }

        public bool Create(HIS_DISEASE_RELATION data)
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

        public bool CreateList(List<HIS_DISEASE_RELATION> listData)
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

        public bool Update(HIS_DISEASE_RELATION data)
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

        public bool UpdateList(List<HIS_DISEASE_RELATION> listData)
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

        public bool Delete(HIS_DISEASE_RELATION data)
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

        public bool DeleteList(List<HIS_DISEASE_RELATION> listData)
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

        public bool Truncate(HIS_DISEASE_RELATION data)
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

        public bool TruncateList(List<HIS_DISEASE_RELATION> listData)
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

        public List<HIS_DISEASE_RELATION> Get(HisDiseaseRelationSO search, CommonParam param)
        {
            List<HIS_DISEASE_RELATION> result = new List<HIS_DISEASE_RELATION>();
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

        public HIS_DISEASE_RELATION GetById(long id, HisDiseaseRelationSO search)
        {
            HIS_DISEASE_RELATION result = null;
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
