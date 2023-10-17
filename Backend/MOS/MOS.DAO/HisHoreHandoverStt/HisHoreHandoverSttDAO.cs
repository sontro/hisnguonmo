using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisHoreHandoverStt
{
    public partial class HisHoreHandoverSttDAO : EntityBase
    {
        private HisHoreHandoverSttCreate CreateWorker
        {
            get
            {
                return (HisHoreHandoverSttCreate)Worker.Get<HisHoreHandoverSttCreate>();
            }
        }
        private HisHoreHandoverSttUpdate UpdateWorker
        {
            get
            {
                return (HisHoreHandoverSttUpdate)Worker.Get<HisHoreHandoverSttUpdate>();
            }
        }
        private HisHoreHandoverSttDelete DeleteWorker
        {
            get
            {
                return (HisHoreHandoverSttDelete)Worker.Get<HisHoreHandoverSttDelete>();
            }
        }
        private HisHoreHandoverSttTruncate TruncateWorker
        {
            get
            {
                return (HisHoreHandoverSttTruncate)Worker.Get<HisHoreHandoverSttTruncate>();
            }
        }
        private HisHoreHandoverSttGet GetWorker
        {
            get
            {
                return (HisHoreHandoverSttGet)Worker.Get<HisHoreHandoverSttGet>();
            }
        }
        private HisHoreHandoverSttCheck CheckWorker
        {
            get
            {
                return (HisHoreHandoverSttCheck)Worker.Get<HisHoreHandoverSttCheck>();
            }
        }

        public bool Create(HIS_HORE_HANDOVER_STT data)
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

        public bool CreateList(List<HIS_HORE_HANDOVER_STT> listData)
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

        public bool Update(HIS_HORE_HANDOVER_STT data)
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

        public bool UpdateList(List<HIS_HORE_HANDOVER_STT> listData)
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

        public bool Delete(HIS_HORE_HANDOVER_STT data)
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

        public bool DeleteList(List<HIS_HORE_HANDOVER_STT> listData)
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

        public bool Truncate(HIS_HORE_HANDOVER_STT data)
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

        public bool TruncateList(List<HIS_HORE_HANDOVER_STT> listData)
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

        public List<HIS_HORE_HANDOVER_STT> Get(HisHoreHandoverSttSO search, CommonParam param)
        {
            List<HIS_HORE_HANDOVER_STT> result = new List<HIS_HORE_HANDOVER_STT>();
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

        public HIS_HORE_HANDOVER_STT GetById(long id, HisHoreHandoverSttSO search)
        {
            HIS_HORE_HANDOVER_STT result = null;
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
