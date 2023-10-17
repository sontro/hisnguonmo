using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisMediRecordType
{
    public partial class HisMediRecordTypeDAO : EntityBase
    {
        private HisMediRecordTypeCreate CreateWorker
        {
            get
            {
                return (HisMediRecordTypeCreate)Worker.Get<HisMediRecordTypeCreate>();
            }
        }
        private HisMediRecordTypeUpdate UpdateWorker
        {
            get
            {
                return (HisMediRecordTypeUpdate)Worker.Get<HisMediRecordTypeUpdate>();
            }
        }
        private HisMediRecordTypeDelete DeleteWorker
        {
            get
            {
                return (HisMediRecordTypeDelete)Worker.Get<HisMediRecordTypeDelete>();
            }
        }
        private HisMediRecordTypeTruncate TruncateWorker
        {
            get
            {
                return (HisMediRecordTypeTruncate)Worker.Get<HisMediRecordTypeTruncate>();
            }
        }
        private HisMediRecordTypeGet GetWorker
        {
            get
            {
                return (HisMediRecordTypeGet)Worker.Get<HisMediRecordTypeGet>();
            }
        }
        private HisMediRecordTypeCheck CheckWorker
        {
            get
            {
                return (HisMediRecordTypeCheck)Worker.Get<HisMediRecordTypeCheck>();
            }
        }

        public bool Create(HIS_MEDI_RECORD_TYPE data)
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

        public bool CreateList(List<HIS_MEDI_RECORD_TYPE> listData)
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

        public bool Update(HIS_MEDI_RECORD_TYPE data)
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

        public bool UpdateList(List<HIS_MEDI_RECORD_TYPE> listData)
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

        public bool Delete(HIS_MEDI_RECORD_TYPE data)
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

        public bool DeleteList(List<HIS_MEDI_RECORD_TYPE> listData)
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

        public bool Truncate(HIS_MEDI_RECORD_TYPE data)
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

        public bool TruncateList(List<HIS_MEDI_RECORD_TYPE> listData)
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

        public List<HIS_MEDI_RECORD_TYPE> Get(HisMediRecordTypeSO search, CommonParam param)
        {
            List<HIS_MEDI_RECORD_TYPE> result = new List<HIS_MEDI_RECORD_TYPE>();
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

        public HIS_MEDI_RECORD_TYPE GetById(long id, HisMediRecordTypeSO search)
        {
            HIS_MEDI_RECORD_TYPE result = null;
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
