using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisMediRecord
{
    public partial class HisMediRecordDAO : EntityBase
    {
        private HisMediRecordCreate CreateWorker
        {
            get
            {
                return (HisMediRecordCreate)Worker.Get<HisMediRecordCreate>();
            }
        }
        private HisMediRecordUpdate UpdateWorker
        {
            get
            {
                return (HisMediRecordUpdate)Worker.Get<HisMediRecordUpdate>();
            }
        }
        private HisMediRecordDelete DeleteWorker
        {
            get
            {
                return (HisMediRecordDelete)Worker.Get<HisMediRecordDelete>();
            }
        }
        private HisMediRecordTruncate TruncateWorker
        {
            get
            {
                return (HisMediRecordTruncate)Worker.Get<HisMediRecordTruncate>();
            }
        }
        private HisMediRecordGet GetWorker
        {
            get
            {
                return (HisMediRecordGet)Worker.Get<HisMediRecordGet>();
            }
        }
        private HisMediRecordCheck CheckWorker
        {
            get
            {
                return (HisMediRecordCheck)Worker.Get<HisMediRecordCheck>();
            }
        }

        public bool Create(HIS_MEDI_RECORD data)
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

        public bool CreateList(List<HIS_MEDI_RECORD> listData)
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

        public bool Update(HIS_MEDI_RECORD data)
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

        public bool UpdateList(List<HIS_MEDI_RECORD> listData)
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

        public bool Delete(HIS_MEDI_RECORD data)
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

        public bool DeleteList(List<HIS_MEDI_RECORD> listData)
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

        public bool Truncate(HIS_MEDI_RECORD data)
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

        public bool TruncateList(List<HIS_MEDI_RECORD> listData)
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

        public List<HIS_MEDI_RECORD> Get(HisMediRecordSO search, CommonParam param)
        {
            List<HIS_MEDI_RECORD> result = new List<HIS_MEDI_RECORD>();
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

        public HIS_MEDI_RECORD GetById(long id, HisMediRecordSO search)
        {
            HIS_MEDI_RECORD result = null;
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
