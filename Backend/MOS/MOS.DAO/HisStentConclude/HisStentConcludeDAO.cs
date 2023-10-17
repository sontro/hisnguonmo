using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisStentConclude
{
    public partial class HisStentConcludeDAO : EntityBase
    {
        private HisStentConcludeCreate CreateWorker
        {
            get
            {
                return (HisStentConcludeCreate)Worker.Get<HisStentConcludeCreate>();
            }
        }
        private HisStentConcludeUpdate UpdateWorker
        {
            get
            {
                return (HisStentConcludeUpdate)Worker.Get<HisStentConcludeUpdate>();
            }
        }
        private HisStentConcludeDelete DeleteWorker
        {
            get
            {
                return (HisStentConcludeDelete)Worker.Get<HisStentConcludeDelete>();
            }
        }
        private HisStentConcludeTruncate TruncateWorker
        {
            get
            {
                return (HisStentConcludeTruncate)Worker.Get<HisStentConcludeTruncate>();
            }
        }
        private HisStentConcludeGet GetWorker
        {
            get
            {
                return (HisStentConcludeGet)Worker.Get<HisStentConcludeGet>();
            }
        }
        private HisStentConcludeCheck CheckWorker
        {
            get
            {
                return (HisStentConcludeCheck)Worker.Get<HisStentConcludeCheck>();
            }
        }

        public bool Create(HIS_STENT_CONCLUDE data)
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

        public bool CreateList(List<HIS_STENT_CONCLUDE> listData)
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

        public bool Update(HIS_STENT_CONCLUDE data)
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

        public bool UpdateList(List<HIS_STENT_CONCLUDE> listData)
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

        public bool Delete(HIS_STENT_CONCLUDE data)
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

        public bool DeleteList(List<HIS_STENT_CONCLUDE> listData)
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

        public bool Truncate(HIS_STENT_CONCLUDE data)
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

        public bool TruncateList(List<HIS_STENT_CONCLUDE> listData)
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

        public List<HIS_STENT_CONCLUDE> Get(HisStentConcludeSO search, CommonParam param)
        {
            List<HIS_STENT_CONCLUDE> result = new List<HIS_STENT_CONCLUDE>();
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

        public HIS_STENT_CONCLUDE GetById(long id, HisStentConcludeSO search)
        {
            HIS_STENT_CONCLUDE result = null;
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
