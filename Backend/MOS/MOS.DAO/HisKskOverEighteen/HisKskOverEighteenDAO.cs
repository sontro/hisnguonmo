using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisKskOverEighteen
{
    public partial class HisKskOverEighteenDAO : EntityBase
    {
        private HisKskOverEighteenCreate CreateWorker
        {
            get
            {
                return (HisKskOverEighteenCreate)Worker.Get<HisKskOverEighteenCreate>();
            }
        }
        private HisKskOverEighteenUpdate UpdateWorker
        {
            get
            {
                return (HisKskOverEighteenUpdate)Worker.Get<HisKskOverEighteenUpdate>();
            }
        }
        private HisKskOverEighteenDelete DeleteWorker
        {
            get
            {
                return (HisKskOverEighteenDelete)Worker.Get<HisKskOverEighteenDelete>();
            }
        }
        private HisKskOverEighteenTruncate TruncateWorker
        {
            get
            {
                return (HisKskOverEighteenTruncate)Worker.Get<HisKskOverEighteenTruncate>();
            }
        }
        private HisKskOverEighteenGet GetWorker
        {
            get
            {
                return (HisKskOverEighteenGet)Worker.Get<HisKskOverEighteenGet>();
            }
        }
        private HisKskOverEighteenCheck CheckWorker
        {
            get
            {
                return (HisKskOverEighteenCheck)Worker.Get<HisKskOverEighteenCheck>();
            }
        }

        public bool Create(HIS_KSK_OVER_EIGHTEEN data)
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

        public bool CreateList(List<HIS_KSK_OVER_EIGHTEEN> listData)
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

        public bool Update(HIS_KSK_OVER_EIGHTEEN data)
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

        public bool UpdateList(List<HIS_KSK_OVER_EIGHTEEN> listData)
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

        public bool Delete(HIS_KSK_OVER_EIGHTEEN data)
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

        public bool DeleteList(List<HIS_KSK_OVER_EIGHTEEN> listData)
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

        public bool Truncate(HIS_KSK_OVER_EIGHTEEN data)
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

        public bool TruncateList(List<HIS_KSK_OVER_EIGHTEEN> listData)
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

        public List<HIS_KSK_OVER_EIGHTEEN> Get(HisKskOverEighteenSO search, CommonParam param)
        {
            List<HIS_KSK_OVER_EIGHTEEN> result = new List<HIS_KSK_OVER_EIGHTEEN>();
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

        public HIS_KSK_OVER_EIGHTEEN GetById(long id, HisKskOverEighteenSO search)
        {
            HIS_KSK_OVER_EIGHTEEN result = null;
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
