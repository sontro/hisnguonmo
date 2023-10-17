using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisPtttCatastrophe
{
    public partial class HisPtttCatastropheDAO : EntityBase
    {
        private HisPtttCatastropheCreate CreateWorker
        {
            get
            {
                return (HisPtttCatastropheCreate)Worker.Get<HisPtttCatastropheCreate>();
            }
        }
        private HisPtttCatastropheUpdate UpdateWorker
        {
            get
            {
                return (HisPtttCatastropheUpdate)Worker.Get<HisPtttCatastropheUpdate>();
            }
        }
        private HisPtttCatastropheDelete DeleteWorker
        {
            get
            {
                return (HisPtttCatastropheDelete)Worker.Get<HisPtttCatastropheDelete>();
            }
        }
        private HisPtttCatastropheTruncate TruncateWorker
        {
            get
            {
                return (HisPtttCatastropheTruncate)Worker.Get<HisPtttCatastropheTruncate>();
            }
        }
        private HisPtttCatastropheGet GetWorker
        {
            get
            {
                return (HisPtttCatastropheGet)Worker.Get<HisPtttCatastropheGet>();
            }
        }
        private HisPtttCatastropheCheck CheckWorker
        {
            get
            {
                return (HisPtttCatastropheCheck)Worker.Get<HisPtttCatastropheCheck>();
            }
        }

        public bool Create(HIS_PTTT_CATASTROPHE data)
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

        public bool CreateList(List<HIS_PTTT_CATASTROPHE> listData)
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

        public bool Update(HIS_PTTT_CATASTROPHE data)
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

        public bool UpdateList(List<HIS_PTTT_CATASTROPHE> listData)
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

        public bool Delete(HIS_PTTT_CATASTROPHE data)
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

        public bool DeleteList(List<HIS_PTTT_CATASTROPHE> listData)
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

        public bool Truncate(HIS_PTTT_CATASTROPHE data)
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

        public bool TruncateList(List<HIS_PTTT_CATASTROPHE> listData)
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

        public List<HIS_PTTT_CATASTROPHE> Get(HisPtttCatastropheSO search, CommonParam param)
        {
            List<HIS_PTTT_CATASTROPHE> result = new List<HIS_PTTT_CATASTROPHE>();
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

        public HIS_PTTT_CATASTROPHE GetById(long id, HisPtttCatastropheSO search)
        {
            HIS_PTTT_CATASTROPHE result = null;
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
