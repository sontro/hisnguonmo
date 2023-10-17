using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisPtttCondition
{
    public partial class HisPtttConditionDAO : EntityBase
    {
        private HisPtttConditionCreate CreateWorker
        {
            get
            {
                return (HisPtttConditionCreate)Worker.Get<HisPtttConditionCreate>();
            }
        }
        private HisPtttConditionUpdate UpdateWorker
        {
            get
            {
                return (HisPtttConditionUpdate)Worker.Get<HisPtttConditionUpdate>();
            }
        }
        private HisPtttConditionDelete DeleteWorker
        {
            get
            {
                return (HisPtttConditionDelete)Worker.Get<HisPtttConditionDelete>();
            }
        }
        private HisPtttConditionTruncate TruncateWorker
        {
            get
            {
                return (HisPtttConditionTruncate)Worker.Get<HisPtttConditionTruncate>();
            }
        }
        private HisPtttConditionGet GetWorker
        {
            get
            {
                return (HisPtttConditionGet)Worker.Get<HisPtttConditionGet>();
            }
        }
        private HisPtttConditionCheck CheckWorker
        {
            get
            {
                return (HisPtttConditionCheck)Worker.Get<HisPtttConditionCheck>();
            }
        }

        public bool Create(HIS_PTTT_CONDITION data)
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

        public bool CreateList(List<HIS_PTTT_CONDITION> listData)
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

        public bool Update(HIS_PTTT_CONDITION data)
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

        public bool UpdateList(List<HIS_PTTT_CONDITION> listData)
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

        public bool Delete(HIS_PTTT_CONDITION data)
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

        public bool DeleteList(List<HIS_PTTT_CONDITION> listData)
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

        public bool Truncate(HIS_PTTT_CONDITION data)
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

        public bool TruncateList(List<HIS_PTTT_CONDITION> listData)
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

        public List<HIS_PTTT_CONDITION> Get(HisPtttConditionSO search, CommonParam param)
        {
            List<HIS_PTTT_CONDITION> result = new List<HIS_PTTT_CONDITION>();
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

        public HIS_PTTT_CONDITION GetById(long id, HisPtttConditionSO search)
        {
            HIS_PTTT_CONDITION result = null;
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
