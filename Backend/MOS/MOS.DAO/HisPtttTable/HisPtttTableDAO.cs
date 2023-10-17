using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisPtttTable
{
    public partial class HisPtttTableDAO : EntityBase
    {
        private HisPtttTableCreate CreateWorker
        {
            get
            {
                return (HisPtttTableCreate)Worker.Get<HisPtttTableCreate>();
            }
        }
        private HisPtttTableUpdate UpdateWorker
        {
            get
            {
                return (HisPtttTableUpdate)Worker.Get<HisPtttTableUpdate>();
            }
        }
        private HisPtttTableDelete DeleteWorker
        {
            get
            {
                return (HisPtttTableDelete)Worker.Get<HisPtttTableDelete>();
            }
        }
        private HisPtttTableTruncate TruncateWorker
        {
            get
            {
                return (HisPtttTableTruncate)Worker.Get<HisPtttTableTruncate>();
            }
        }
        private HisPtttTableGet GetWorker
        {
            get
            {
                return (HisPtttTableGet)Worker.Get<HisPtttTableGet>();
            }
        }
        private HisPtttTableCheck CheckWorker
        {
            get
            {
                return (HisPtttTableCheck)Worker.Get<HisPtttTableCheck>();
            }
        }

        public bool Create(HIS_PTTT_TABLE data)
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

        public bool CreateList(List<HIS_PTTT_TABLE> listData)
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

        public bool Update(HIS_PTTT_TABLE data)
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

        public bool UpdateList(List<HIS_PTTT_TABLE> listData)
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

        public bool Delete(HIS_PTTT_TABLE data)
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

        public bool DeleteList(List<HIS_PTTT_TABLE> listData)
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

        public bool Truncate(HIS_PTTT_TABLE data)
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

        public bool TruncateList(List<HIS_PTTT_TABLE> listData)
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

        public List<HIS_PTTT_TABLE> Get(HisPtttTableSO search, CommonParam param)
        {
            List<HIS_PTTT_TABLE> result = new List<HIS_PTTT_TABLE>();
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

        public HIS_PTTT_TABLE GetById(long id, HisPtttTableSO search)
        {
            HIS_PTTT_TABLE result = null;
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
