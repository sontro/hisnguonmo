using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisKskAccess
{
    public partial class HisKskAccessDAO : EntityBase
    {
        private HisKskAccessCreate CreateWorker
        {
            get
            {
                return (HisKskAccessCreate)Worker.Get<HisKskAccessCreate>();
            }
        }
        private HisKskAccessUpdate UpdateWorker
        {
            get
            {
                return (HisKskAccessUpdate)Worker.Get<HisKskAccessUpdate>();
            }
        }
        private HisKskAccessDelete DeleteWorker
        {
            get
            {
                return (HisKskAccessDelete)Worker.Get<HisKskAccessDelete>();
            }
        }
        private HisKskAccessTruncate TruncateWorker
        {
            get
            {
                return (HisKskAccessTruncate)Worker.Get<HisKskAccessTruncate>();
            }
        }
        private HisKskAccessGet GetWorker
        {
            get
            {
                return (HisKskAccessGet)Worker.Get<HisKskAccessGet>();
            }
        }
        private HisKskAccessCheck CheckWorker
        {
            get
            {
                return (HisKskAccessCheck)Worker.Get<HisKskAccessCheck>();
            }
        }

        public bool Create(HIS_KSK_ACCESS data)
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

        public bool CreateList(List<HIS_KSK_ACCESS> listData)
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

        public bool Update(HIS_KSK_ACCESS data)
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

        public bool UpdateList(List<HIS_KSK_ACCESS> listData)
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

        public bool Delete(HIS_KSK_ACCESS data)
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

        public bool DeleteList(List<HIS_KSK_ACCESS> listData)
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

        public bool Truncate(HIS_KSK_ACCESS data)
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

        public bool TruncateList(List<HIS_KSK_ACCESS> listData)
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

        public List<HIS_KSK_ACCESS> Get(HisKskAccessSO search, CommonParam param)
        {
            List<HIS_KSK_ACCESS> result = new List<HIS_KSK_ACCESS>();
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

        public HIS_KSK_ACCESS GetById(long id, HisKskAccessSO search)
        {
            HIS_KSK_ACCESS result = null;
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
