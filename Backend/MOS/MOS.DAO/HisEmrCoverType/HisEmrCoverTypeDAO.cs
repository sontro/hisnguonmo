using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisEmrCoverType
{
    public partial class HisEmrCoverTypeDAO : EntityBase
    {
        private HisEmrCoverTypeCreate CreateWorker
        {
            get
            {
                return (HisEmrCoverTypeCreate)Worker.Get<HisEmrCoverTypeCreate>();
            }
        }
        private HisEmrCoverTypeUpdate UpdateWorker
        {
            get
            {
                return (HisEmrCoverTypeUpdate)Worker.Get<HisEmrCoverTypeUpdate>();
            }
        }
        private HisEmrCoverTypeDelete DeleteWorker
        {
            get
            {
                return (HisEmrCoverTypeDelete)Worker.Get<HisEmrCoverTypeDelete>();
            }
        }
        private HisEmrCoverTypeTruncate TruncateWorker
        {
            get
            {
                return (HisEmrCoverTypeTruncate)Worker.Get<HisEmrCoverTypeTruncate>();
            }
        }
        private HisEmrCoverTypeGet GetWorker
        {
            get
            {
                return (HisEmrCoverTypeGet)Worker.Get<HisEmrCoverTypeGet>();
            }
        }
        private HisEmrCoverTypeCheck CheckWorker
        {
            get
            {
                return (HisEmrCoverTypeCheck)Worker.Get<HisEmrCoverTypeCheck>();
            }
        }

        public bool Create(HIS_EMR_COVER_TYPE data)
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

        public bool CreateList(List<HIS_EMR_COVER_TYPE> listData)
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

        public bool Update(HIS_EMR_COVER_TYPE data)
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

        public bool UpdateList(List<HIS_EMR_COVER_TYPE> listData)
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

        public bool Delete(HIS_EMR_COVER_TYPE data)
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

        public bool DeleteList(List<HIS_EMR_COVER_TYPE> listData)
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

        public bool Truncate(HIS_EMR_COVER_TYPE data)
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

        public bool TruncateList(List<HIS_EMR_COVER_TYPE> listData)
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

        public List<HIS_EMR_COVER_TYPE> Get(HisEmrCoverTypeSO search, CommonParam param)
        {
            List<HIS_EMR_COVER_TYPE> result = new List<HIS_EMR_COVER_TYPE>();
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

        public HIS_EMR_COVER_TYPE GetById(long id, HisEmrCoverTypeSO search)
        {
            HIS_EMR_COVER_TYPE result = null;
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
