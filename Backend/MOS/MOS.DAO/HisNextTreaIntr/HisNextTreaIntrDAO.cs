using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisNextTreaIntr
{
    public partial class HisNextTreaIntrDAO : EntityBase
    {
        private HisNextTreaIntrCreate CreateWorker
        {
            get
            {
                return (HisNextTreaIntrCreate)Worker.Get<HisNextTreaIntrCreate>();
            }
        }
        private HisNextTreaIntrUpdate UpdateWorker
        {
            get
            {
                return (HisNextTreaIntrUpdate)Worker.Get<HisNextTreaIntrUpdate>();
            }
        }
        private HisNextTreaIntrDelete DeleteWorker
        {
            get
            {
                return (HisNextTreaIntrDelete)Worker.Get<HisNextTreaIntrDelete>();
            }
        }
        private HisNextTreaIntrTruncate TruncateWorker
        {
            get
            {
                return (HisNextTreaIntrTruncate)Worker.Get<HisNextTreaIntrTruncate>();
            }
        }
        private HisNextTreaIntrGet GetWorker
        {
            get
            {
                return (HisNextTreaIntrGet)Worker.Get<HisNextTreaIntrGet>();
            }
        }
        private HisNextTreaIntrCheck CheckWorker
        {
            get
            {
                return (HisNextTreaIntrCheck)Worker.Get<HisNextTreaIntrCheck>();
            }
        }

        public bool Create(HIS_NEXT_TREA_INTR data)
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

        public bool CreateList(List<HIS_NEXT_TREA_INTR> listData)
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

        public bool Update(HIS_NEXT_TREA_INTR data)
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

        public bool UpdateList(List<HIS_NEXT_TREA_INTR> listData)
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

        public bool Delete(HIS_NEXT_TREA_INTR data)
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

        public bool DeleteList(List<HIS_NEXT_TREA_INTR> listData)
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

        public bool Truncate(HIS_NEXT_TREA_INTR data)
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

        public bool TruncateList(List<HIS_NEXT_TREA_INTR> listData)
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

        public List<HIS_NEXT_TREA_INTR> Get(HisNextTreaIntrSO search, CommonParam param)
        {
            List<HIS_NEXT_TREA_INTR> result = new List<HIS_NEXT_TREA_INTR>();
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

        public HIS_NEXT_TREA_INTR GetById(long id, HisNextTreaIntrSO search)
        {
            HIS_NEXT_TREA_INTR result = null;
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
