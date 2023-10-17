using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisMestMatyDepa
{
    public partial class HisMestMatyDepaDAO : EntityBase
    {
        private HisMestMatyDepaCreate CreateWorker
        {
            get
            {
                return (HisMestMatyDepaCreate)Worker.Get<HisMestMatyDepaCreate>();
            }
        }
        private HisMestMatyDepaUpdate UpdateWorker
        {
            get
            {
                return (HisMestMatyDepaUpdate)Worker.Get<HisMestMatyDepaUpdate>();
            }
        }
        private HisMestMatyDepaDelete DeleteWorker
        {
            get
            {
                return (HisMestMatyDepaDelete)Worker.Get<HisMestMatyDepaDelete>();
            }
        }
        private HisMestMatyDepaTruncate TruncateWorker
        {
            get
            {
                return (HisMestMatyDepaTruncate)Worker.Get<HisMestMatyDepaTruncate>();
            }
        }
        private HisMestMatyDepaGet GetWorker
        {
            get
            {
                return (HisMestMatyDepaGet)Worker.Get<HisMestMatyDepaGet>();
            }
        }
        private HisMestMatyDepaCheck CheckWorker
        {
            get
            {
                return (HisMestMatyDepaCheck)Worker.Get<HisMestMatyDepaCheck>();
            }
        }

        public bool Create(HIS_MEST_MATY_DEPA data)
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

        public bool CreateList(List<HIS_MEST_MATY_DEPA> listData)
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

        public bool Update(HIS_MEST_MATY_DEPA data)
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

        public bool UpdateList(List<HIS_MEST_MATY_DEPA> listData)
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

        public bool Delete(HIS_MEST_MATY_DEPA data)
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

        public bool DeleteList(List<HIS_MEST_MATY_DEPA> listData)
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

        public bool Truncate(HIS_MEST_MATY_DEPA data)
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

        public bool TruncateList(List<HIS_MEST_MATY_DEPA> listData)
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

        public List<HIS_MEST_MATY_DEPA> Get(HisMestMatyDepaSO search, CommonParam param)
        {
            List<HIS_MEST_MATY_DEPA> result = new List<HIS_MEST_MATY_DEPA>();
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

        public HIS_MEST_MATY_DEPA GetById(long id, HisMestMatyDepaSO search)
        {
            HIS_MEST_MATY_DEPA result = null;
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
