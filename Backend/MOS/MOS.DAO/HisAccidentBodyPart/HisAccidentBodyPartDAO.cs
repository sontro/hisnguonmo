using Inventec.Common.Repository;
using Inventec.Core;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisAccidentBodyPart
{
    public partial class HisAccidentBodyPartDAO : EntityBase
    {
        private HisAccidentBodyPartCreate CreateWorker
        {
            get
            {
                return (HisAccidentBodyPartCreate)Worker.Get<HisAccidentBodyPartCreate>();
            }
        }
        private HisAccidentBodyPartUpdate UpdateWorker
        {
            get
            {
                return (HisAccidentBodyPartUpdate)Worker.Get<HisAccidentBodyPartUpdate>();
            }
        }
        private HisAccidentBodyPartDelete DeleteWorker
        {
            get
            {
                return (HisAccidentBodyPartDelete)Worker.Get<HisAccidentBodyPartDelete>();
            }
        }
        private HisAccidentBodyPartTruncate TruncateWorker
        {
            get
            {
                return (HisAccidentBodyPartTruncate)Worker.Get<HisAccidentBodyPartTruncate>();
            }
        }
        private HisAccidentBodyPartGet GetWorker
        {
            get
            {
                return (HisAccidentBodyPartGet)Worker.Get<HisAccidentBodyPartGet>();
            }
        }
        private HisAccidentBodyPartCheck CheckWorker
        {
            get
            {
                return (HisAccidentBodyPartCheck)Worker.Get<HisAccidentBodyPartCheck>();
            }
        }

        public bool Create(HIS_ACCIDENT_BODY_PART data)
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

        public bool CreateList(List<HIS_ACCIDENT_BODY_PART> listData)
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

        public bool Update(HIS_ACCIDENT_BODY_PART data)
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

        public bool UpdateList(List<HIS_ACCIDENT_BODY_PART> listData)
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

        public bool Delete(HIS_ACCIDENT_BODY_PART data)
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

        public bool DeleteList(List<HIS_ACCIDENT_BODY_PART> listData)
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

        public bool Truncate(HIS_ACCIDENT_BODY_PART data)
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

        public bool TruncateList(List<HIS_ACCIDENT_BODY_PART> listData)
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

        public List<HIS_ACCIDENT_BODY_PART> Get(HisAccidentBodyPartSO search, CommonParam param)
        {
            List<HIS_ACCIDENT_BODY_PART> result = new List<HIS_ACCIDENT_BODY_PART>();
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

        public HIS_ACCIDENT_BODY_PART GetById(long id, HisAccidentBodyPartSO search)
        {
            HIS_ACCIDENT_BODY_PART result = null;
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
