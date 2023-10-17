using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisContraindication
{
    public partial class HisContraindicationDAO : EntityBase
    {
        private HisContraindicationCreate CreateWorker
        {
            get
            {
                return (HisContraindicationCreate)Worker.Get<HisContraindicationCreate>();
            }
        }
        private HisContraindicationUpdate UpdateWorker
        {
            get
            {
                return (HisContraindicationUpdate)Worker.Get<HisContraindicationUpdate>();
            }
        }
        private HisContraindicationDelete DeleteWorker
        {
            get
            {
                return (HisContraindicationDelete)Worker.Get<HisContraindicationDelete>();
            }
        }
        private HisContraindicationTruncate TruncateWorker
        {
            get
            {
                return (HisContraindicationTruncate)Worker.Get<HisContraindicationTruncate>();
            }
        }
        private HisContraindicationGet GetWorker
        {
            get
            {
                return (HisContraindicationGet)Worker.Get<HisContraindicationGet>();
            }
        }
        private HisContraindicationCheck CheckWorker
        {
            get
            {
                return (HisContraindicationCheck)Worker.Get<HisContraindicationCheck>();
            }
        }

        public bool Create(HIS_CONTRAINDICATION data)
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

        public bool CreateList(List<HIS_CONTRAINDICATION> listData)
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

        public bool Update(HIS_CONTRAINDICATION data)
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

        public bool UpdateList(List<HIS_CONTRAINDICATION> listData)
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

        public bool Delete(HIS_CONTRAINDICATION data)
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

        public bool DeleteList(List<HIS_CONTRAINDICATION> listData)
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

        public bool Truncate(HIS_CONTRAINDICATION data)
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

        public bool TruncateList(List<HIS_CONTRAINDICATION> listData)
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

        public List<HIS_CONTRAINDICATION> Get(HisContraindicationSO search, CommonParam param)
        {
            List<HIS_CONTRAINDICATION> result = new List<HIS_CONTRAINDICATION>();
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

        public HIS_CONTRAINDICATION GetById(long id, HisContraindicationSO search)
        {
            HIS_CONTRAINDICATION result = null;
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
