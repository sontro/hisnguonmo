using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisDocHoldType
{
    public partial class HisDocHoldTypeDAO : EntityBase
    {
        private HisDocHoldTypeCreate CreateWorker
        {
            get
            {
                return (HisDocHoldTypeCreate)Worker.Get<HisDocHoldTypeCreate>();
            }
        }
        private HisDocHoldTypeUpdate UpdateWorker
        {
            get
            {
                return (HisDocHoldTypeUpdate)Worker.Get<HisDocHoldTypeUpdate>();
            }
        }
        private HisDocHoldTypeDelete DeleteWorker
        {
            get
            {
                return (HisDocHoldTypeDelete)Worker.Get<HisDocHoldTypeDelete>();
            }
        }
        private HisDocHoldTypeTruncate TruncateWorker
        {
            get
            {
                return (HisDocHoldTypeTruncate)Worker.Get<HisDocHoldTypeTruncate>();
            }
        }
        private HisDocHoldTypeGet GetWorker
        {
            get
            {
                return (HisDocHoldTypeGet)Worker.Get<HisDocHoldTypeGet>();
            }
        }
        private HisDocHoldTypeCheck CheckWorker
        {
            get
            {
                return (HisDocHoldTypeCheck)Worker.Get<HisDocHoldTypeCheck>();
            }
        }

        public bool Create(HIS_DOC_HOLD_TYPE data)
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

        public bool CreateList(List<HIS_DOC_HOLD_TYPE> listData)
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

        public bool Update(HIS_DOC_HOLD_TYPE data)
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

        public bool UpdateList(List<HIS_DOC_HOLD_TYPE> listData)
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

        public bool Delete(HIS_DOC_HOLD_TYPE data)
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

        public bool DeleteList(List<HIS_DOC_HOLD_TYPE> listData)
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

        public bool Truncate(HIS_DOC_HOLD_TYPE data)
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

        public bool TruncateList(List<HIS_DOC_HOLD_TYPE> listData)
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

        public List<HIS_DOC_HOLD_TYPE> Get(HisDocHoldTypeSO search, CommonParam param)
        {
            List<HIS_DOC_HOLD_TYPE> result = new List<HIS_DOC_HOLD_TYPE>();
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

        public HIS_DOC_HOLD_TYPE GetById(long id, HisDocHoldTypeSO search)
        {
            HIS_DOC_HOLD_TYPE result = null;
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
