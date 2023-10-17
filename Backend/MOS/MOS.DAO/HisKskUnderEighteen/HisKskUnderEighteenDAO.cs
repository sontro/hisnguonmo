using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisKskUnderEighteen
{
    public partial class HisKskUnderEighteenDAO : EntityBase
    {
        private HisKskUnderEighteenCreate CreateWorker
        {
            get
            {
                return (HisKskUnderEighteenCreate)Worker.Get<HisKskUnderEighteenCreate>();
            }
        }
        private HisKskUnderEighteenUpdate UpdateWorker
        {
            get
            {
                return (HisKskUnderEighteenUpdate)Worker.Get<HisKskUnderEighteenUpdate>();
            }
        }
        private HisKskUnderEighteenDelete DeleteWorker
        {
            get
            {
                return (HisKskUnderEighteenDelete)Worker.Get<HisKskUnderEighteenDelete>();
            }
        }
        private HisKskUnderEighteenTruncate TruncateWorker
        {
            get
            {
                return (HisKskUnderEighteenTruncate)Worker.Get<HisKskUnderEighteenTruncate>();
            }
        }
        private HisKskUnderEighteenGet GetWorker
        {
            get
            {
                return (HisKskUnderEighteenGet)Worker.Get<HisKskUnderEighteenGet>();
            }
        }
        private HisKskUnderEighteenCheck CheckWorker
        {
            get
            {
                return (HisKskUnderEighteenCheck)Worker.Get<HisKskUnderEighteenCheck>();
            }
        }

        public bool Create(HIS_KSK_UNDER_EIGHTEEN data)
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

        public bool CreateList(List<HIS_KSK_UNDER_EIGHTEEN> listData)
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

        public bool Update(HIS_KSK_UNDER_EIGHTEEN data)
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

        public bool UpdateList(List<HIS_KSK_UNDER_EIGHTEEN> listData)
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

        public bool Delete(HIS_KSK_UNDER_EIGHTEEN data)
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

        public bool DeleteList(List<HIS_KSK_UNDER_EIGHTEEN> listData)
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

        public bool Truncate(HIS_KSK_UNDER_EIGHTEEN data)
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

        public bool TruncateList(List<HIS_KSK_UNDER_EIGHTEEN> listData)
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

        public List<HIS_KSK_UNDER_EIGHTEEN> Get(HisKskUnderEighteenSO search, CommonParam param)
        {
            List<HIS_KSK_UNDER_EIGHTEEN> result = new List<HIS_KSK_UNDER_EIGHTEEN>();
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

        public HIS_KSK_UNDER_EIGHTEEN GetById(long id, HisKskUnderEighteenSO search)
        {
            HIS_KSK_UNDER_EIGHTEEN result = null;
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
