using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisQcNormation
{
    public partial class HisQcNormationDAO : EntityBase
    {
        private HisQcNormationCreate CreateWorker
        {
            get
            {
                return (HisQcNormationCreate)Worker.Get<HisQcNormationCreate>();
            }
        }
        private HisQcNormationUpdate UpdateWorker
        {
            get
            {
                return (HisQcNormationUpdate)Worker.Get<HisQcNormationUpdate>();
            }
        }
        private HisQcNormationDelete DeleteWorker
        {
            get
            {
                return (HisQcNormationDelete)Worker.Get<HisQcNormationDelete>();
            }
        }
        private HisQcNormationTruncate TruncateWorker
        {
            get
            {
                return (HisQcNormationTruncate)Worker.Get<HisQcNormationTruncate>();
            }
        }
        private HisQcNormationGet GetWorker
        {
            get
            {
                return (HisQcNormationGet)Worker.Get<HisQcNormationGet>();
            }
        }
        private HisQcNormationCheck CheckWorker
        {
            get
            {
                return (HisQcNormationCheck)Worker.Get<HisQcNormationCheck>();
            }
        }

        public bool Create(HIS_QC_NORMATION data)
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

        public bool CreateList(List<HIS_QC_NORMATION> listData)
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

        public bool Update(HIS_QC_NORMATION data)
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

        public bool UpdateList(List<HIS_QC_NORMATION> listData)
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

        public bool Delete(HIS_QC_NORMATION data)
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

        public bool DeleteList(List<HIS_QC_NORMATION> listData)
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

        public bool Truncate(HIS_QC_NORMATION data)
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

        public bool TruncateList(List<HIS_QC_NORMATION> listData)
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

        public List<HIS_QC_NORMATION> Get(HisQcNormationSO search, CommonParam param)
        {
            List<HIS_QC_NORMATION> result = new List<HIS_QC_NORMATION>();
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

        public HIS_QC_NORMATION GetById(long id, HisQcNormationSO search)
        {
            HIS_QC_NORMATION result = null;
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
