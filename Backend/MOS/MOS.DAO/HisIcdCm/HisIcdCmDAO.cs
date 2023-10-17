using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisIcdCm
{
    public partial class HisIcdCmDAO : EntityBase
    {
        private HisIcdCmCreate CreateWorker
        {
            get
            {
                return (HisIcdCmCreate)Worker.Get<HisIcdCmCreate>();
            }
        }
        private HisIcdCmUpdate UpdateWorker
        {
            get
            {
                return (HisIcdCmUpdate)Worker.Get<HisIcdCmUpdate>();
            }
        }
        private HisIcdCmDelete DeleteWorker
        {
            get
            {
                return (HisIcdCmDelete)Worker.Get<HisIcdCmDelete>();
            }
        }
        private HisIcdCmTruncate TruncateWorker
        {
            get
            {
                return (HisIcdCmTruncate)Worker.Get<HisIcdCmTruncate>();
            }
        }
        private HisIcdCmGet GetWorker
        {
            get
            {
                return (HisIcdCmGet)Worker.Get<HisIcdCmGet>();
            }
        }
        private HisIcdCmCheck CheckWorker
        {
            get
            {
                return (HisIcdCmCheck)Worker.Get<HisIcdCmCheck>();
            }
        }

        public bool Create(HIS_ICD_CM data)
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

        public bool CreateList(List<HIS_ICD_CM> listData)
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

        public bool Update(HIS_ICD_CM data)
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

        public bool UpdateList(List<HIS_ICD_CM> listData)
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

        public bool Delete(HIS_ICD_CM data)
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

        public bool DeleteList(List<HIS_ICD_CM> listData)
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

        public bool Truncate(HIS_ICD_CM data)
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

        public bool TruncateList(List<HIS_ICD_CM> listData)
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

        public List<HIS_ICD_CM> Get(HisIcdCmSO search, CommonParam param)
        {
            List<HIS_ICD_CM> result = new List<HIS_ICD_CM>();
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

        public HIS_ICD_CM GetById(long id, HisIcdCmSO search)
        {
            HIS_ICD_CM result = null;
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
