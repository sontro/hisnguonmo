using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisDebateEkipUser
{
    public partial class HisDebateEkipUserDAO : EntityBase
    {
        private HisDebateEkipUserCreate CreateWorker
        {
            get
            {
                return (HisDebateEkipUserCreate)Worker.Get<HisDebateEkipUserCreate>();
            }
        }
        private HisDebateEkipUserUpdate UpdateWorker
        {
            get
            {
                return (HisDebateEkipUserUpdate)Worker.Get<HisDebateEkipUserUpdate>();
            }
        }
        private HisDebateEkipUserDelete DeleteWorker
        {
            get
            {
                return (HisDebateEkipUserDelete)Worker.Get<HisDebateEkipUserDelete>();
            }
        }
        private HisDebateEkipUserTruncate TruncateWorker
        {
            get
            {
                return (HisDebateEkipUserTruncate)Worker.Get<HisDebateEkipUserTruncate>();
            }
        }
        private HisDebateEkipUserGet GetWorker
        {
            get
            {
                return (HisDebateEkipUserGet)Worker.Get<HisDebateEkipUserGet>();
            }
        }
        private HisDebateEkipUserCheck CheckWorker
        {
            get
            {
                return (HisDebateEkipUserCheck)Worker.Get<HisDebateEkipUserCheck>();
            }
        }

        public bool Create(HIS_DEBATE_EKIP_USER data)
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

        public bool CreateList(List<HIS_DEBATE_EKIP_USER> listData)
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

        public bool Update(HIS_DEBATE_EKIP_USER data)
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

        public bool UpdateList(List<HIS_DEBATE_EKIP_USER> listData)
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

        public bool Delete(HIS_DEBATE_EKIP_USER data)
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

        public bool DeleteList(List<HIS_DEBATE_EKIP_USER> listData)
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

        public bool Truncate(HIS_DEBATE_EKIP_USER data)
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

        public bool TruncateList(List<HIS_DEBATE_EKIP_USER> listData)
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

        public List<HIS_DEBATE_EKIP_USER> Get(HisDebateEkipUserSO search, CommonParam param)
        {
            List<HIS_DEBATE_EKIP_USER> result = new List<HIS_DEBATE_EKIP_USER>();
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

        public HIS_DEBATE_EKIP_USER GetById(long id, HisDebateEkipUserSO search)
        {
            HIS_DEBATE_EKIP_USER result = null;
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
