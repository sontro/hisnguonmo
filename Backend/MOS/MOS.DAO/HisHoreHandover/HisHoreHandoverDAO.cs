using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisHoreHandover
{
    public partial class HisHoreHandoverDAO : EntityBase
    {
        private HisHoreHandoverCreate CreateWorker
        {
            get
            {
                return (HisHoreHandoverCreate)Worker.Get<HisHoreHandoverCreate>();
            }
        }
        private HisHoreHandoverUpdate UpdateWorker
        {
            get
            {
                return (HisHoreHandoverUpdate)Worker.Get<HisHoreHandoverUpdate>();
            }
        }
        private HisHoreHandoverDelete DeleteWorker
        {
            get
            {
                return (HisHoreHandoverDelete)Worker.Get<HisHoreHandoverDelete>();
            }
        }
        private HisHoreHandoverTruncate TruncateWorker
        {
            get
            {
                return (HisHoreHandoverTruncate)Worker.Get<HisHoreHandoverTruncate>();
            }
        }
        private HisHoreHandoverGet GetWorker
        {
            get
            {
                return (HisHoreHandoverGet)Worker.Get<HisHoreHandoverGet>();
            }
        }
        private HisHoreHandoverCheck CheckWorker
        {
            get
            {
                return (HisHoreHandoverCheck)Worker.Get<HisHoreHandoverCheck>();
            }
        }

        public bool Create(HIS_HORE_HANDOVER data)
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

        public bool CreateList(List<HIS_HORE_HANDOVER> listData)
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

        public bool Update(HIS_HORE_HANDOVER data)
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

        public bool UpdateList(List<HIS_HORE_HANDOVER> listData)
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

        public bool Delete(HIS_HORE_HANDOVER data)
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

        public bool DeleteList(List<HIS_HORE_HANDOVER> listData)
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

        public bool Truncate(HIS_HORE_HANDOVER data)
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

        public bool TruncateList(List<HIS_HORE_HANDOVER> listData)
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

        public List<HIS_HORE_HANDOVER> Get(HisHoreHandoverSO search, CommonParam param)
        {
            List<HIS_HORE_HANDOVER> result = new List<HIS_HORE_HANDOVER>();
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

        public HIS_HORE_HANDOVER GetById(long id, HisHoreHandoverSO search)
        {
            HIS_HORE_HANDOVER result = null;
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
