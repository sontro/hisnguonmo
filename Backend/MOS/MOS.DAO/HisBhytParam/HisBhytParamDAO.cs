using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisBhytParam
{
    public partial class HisBhytParamDAO : EntityBase
    {
        private HisBhytParamCreate CreateWorker
        {
            get
            {
                return (HisBhytParamCreate)Worker.Get<HisBhytParamCreate>();
            }
        }
        private HisBhytParamUpdate UpdateWorker
        {
            get
            {
                return (HisBhytParamUpdate)Worker.Get<HisBhytParamUpdate>();
            }
        }
        private HisBhytParamDelete DeleteWorker
        {
            get
            {
                return (HisBhytParamDelete)Worker.Get<HisBhytParamDelete>();
            }
        }
        private HisBhytParamTruncate TruncateWorker
        {
            get
            {
                return (HisBhytParamTruncate)Worker.Get<HisBhytParamTruncate>();
            }
        }
        private HisBhytParamGet GetWorker
        {
            get
            {
                return (HisBhytParamGet)Worker.Get<HisBhytParamGet>();
            }
        }
        private HisBhytParamCheck CheckWorker
        {
            get
            {
                return (HisBhytParamCheck)Worker.Get<HisBhytParamCheck>();
            }
        }

        public bool Create(HIS_BHYT_PARAM data)
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

        public bool CreateList(List<HIS_BHYT_PARAM> listData)
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

        public bool Update(HIS_BHYT_PARAM data)
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

        public bool UpdateList(List<HIS_BHYT_PARAM> listData)
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

        public bool Delete(HIS_BHYT_PARAM data)
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

        public bool DeleteList(List<HIS_BHYT_PARAM> listData)
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

        public bool Truncate(HIS_BHYT_PARAM data)
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

        public bool TruncateList(List<HIS_BHYT_PARAM> listData)
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

        public List<HIS_BHYT_PARAM> Get(HisBhytParamSO search, CommonParam param)
        {
            List<HIS_BHYT_PARAM> result = new List<HIS_BHYT_PARAM>();
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

        public HIS_BHYT_PARAM GetById(long id, HisBhytParamSO search)
        {
            HIS_BHYT_PARAM result = null;
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
