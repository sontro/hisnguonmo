using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisKskOther
{
    public partial class HisKskOtherDAO : EntityBase
    {
        private HisKskOtherCreate CreateWorker
        {
            get
            {
                return (HisKskOtherCreate)Worker.Get<HisKskOtherCreate>();
            }
        }
        private HisKskOtherUpdate UpdateWorker
        {
            get
            {
                return (HisKskOtherUpdate)Worker.Get<HisKskOtherUpdate>();
            }
        }
        private HisKskOtherDelete DeleteWorker
        {
            get
            {
                return (HisKskOtherDelete)Worker.Get<HisKskOtherDelete>();
            }
        }
        private HisKskOtherTruncate TruncateWorker
        {
            get
            {
                return (HisKskOtherTruncate)Worker.Get<HisKskOtherTruncate>();
            }
        }
        private HisKskOtherGet GetWorker
        {
            get
            {
                return (HisKskOtherGet)Worker.Get<HisKskOtherGet>();
            }
        }
        private HisKskOtherCheck CheckWorker
        {
            get
            {
                return (HisKskOtherCheck)Worker.Get<HisKskOtherCheck>();
            }
        }

        public bool Create(HIS_KSK_OTHER data)
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

        public bool CreateList(List<HIS_KSK_OTHER> listData)
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

        public bool Update(HIS_KSK_OTHER data)
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

        public bool UpdateList(List<HIS_KSK_OTHER> listData)
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

        public bool Delete(HIS_KSK_OTHER data)
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

        public bool DeleteList(List<HIS_KSK_OTHER> listData)
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

        public bool Truncate(HIS_KSK_OTHER data)
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

        public bool TruncateList(List<HIS_KSK_OTHER> listData)
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

        public List<HIS_KSK_OTHER> Get(HisKskOtherSO search, CommonParam param)
        {
            List<HIS_KSK_OTHER> result = new List<HIS_KSK_OTHER>();
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

        public HIS_KSK_OTHER GetById(long id, HisKskOtherSO search)
        {
            HIS_KSK_OTHER result = null;
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
