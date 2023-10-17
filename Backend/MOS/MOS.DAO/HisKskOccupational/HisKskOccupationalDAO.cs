using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisKskOccupational
{
    public partial class HisKskOccupationalDAO : EntityBase
    {
        private HisKskOccupationalCreate CreateWorker
        {
            get
            {
                return (HisKskOccupationalCreate)Worker.Get<HisKskOccupationalCreate>();
            }
        }
        private HisKskOccupationalUpdate UpdateWorker
        {
            get
            {
                return (HisKskOccupationalUpdate)Worker.Get<HisKskOccupationalUpdate>();
            }
        }
        private HisKskOccupationalDelete DeleteWorker
        {
            get
            {
                return (HisKskOccupationalDelete)Worker.Get<HisKskOccupationalDelete>();
            }
        }
        private HisKskOccupationalTruncate TruncateWorker
        {
            get
            {
                return (HisKskOccupationalTruncate)Worker.Get<HisKskOccupationalTruncate>();
            }
        }
        private HisKskOccupationalGet GetWorker
        {
            get
            {
                return (HisKskOccupationalGet)Worker.Get<HisKskOccupationalGet>();
            }
        }
        private HisKskOccupationalCheck CheckWorker
        {
            get
            {
                return (HisKskOccupationalCheck)Worker.Get<HisKskOccupationalCheck>();
            }
        }

        public bool Create(HIS_KSK_OCCUPATIONAL data)
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

        public bool CreateList(List<HIS_KSK_OCCUPATIONAL> listData)
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

        public bool Update(HIS_KSK_OCCUPATIONAL data)
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

        public bool UpdateList(List<HIS_KSK_OCCUPATIONAL> listData)
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

        public bool Delete(HIS_KSK_OCCUPATIONAL data)
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

        public bool DeleteList(List<HIS_KSK_OCCUPATIONAL> listData)
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

        public bool Truncate(HIS_KSK_OCCUPATIONAL data)
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

        public bool TruncateList(List<HIS_KSK_OCCUPATIONAL> listData)
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

        public List<HIS_KSK_OCCUPATIONAL> Get(HisKskOccupationalSO search, CommonParam param)
        {
            List<HIS_KSK_OCCUPATIONAL> result = new List<HIS_KSK_OCCUPATIONAL>();
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

        public HIS_KSK_OCCUPATIONAL GetById(long id, HisKskOccupationalSO search)
        {
            HIS_KSK_OCCUPATIONAL result = null;
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
