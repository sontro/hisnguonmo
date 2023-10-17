using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisDebateInviteUser
{
    public partial class HisDebateInviteUserDAO : EntityBase
    {
        private HisDebateInviteUserCreate CreateWorker
        {
            get
            {
                return (HisDebateInviteUserCreate)Worker.Get<HisDebateInviteUserCreate>();
            }
        }
        private HisDebateInviteUserUpdate UpdateWorker
        {
            get
            {
                return (HisDebateInviteUserUpdate)Worker.Get<HisDebateInviteUserUpdate>();
            }
        }
        private HisDebateInviteUserDelete DeleteWorker
        {
            get
            {
                return (HisDebateInviteUserDelete)Worker.Get<HisDebateInviteUserDelete>();
            }
        }
        private HisDebateInviteUserTruncate TruncateWorker
        {
            get
            {
                return (HisDebateInviteUserTruncate)Worker.Get<HisDebateInviteUserTruncate>();
            }
        }
        private HisDebateInviteUserGet GetWorker
        {
            get
            {
                return (HisDebateInviteUserGet)Worker.Get<HisDebateInviteUserGet>();
            }
        }
        private HisDebateInviteUserCheck CheckWorker
        {
            get
            {
                return (HisDebateInviteUserCheck)Worker.Get<HisDebateInviteUserCheck>();
            }
        }

        public bool Create(HIS_DEBATE_INVITE_USER data)
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

        public bool CreateList(List<HIS_DEBATE_INVITE_USER> listData)
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

        public bool Update(HIS_DEBATE_INVITE_USER data)
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

        public bool UpdateList(List<HIS_DEBATE_INVITE_USER> listData)
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

        public bool Delete(HIS_DEBATE_INVITE_USER data)
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

        public bool DeleteList(List<HIS_DEBATE_INVITE_USER> listData)
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

        public bool Truncate(HIS_DEBATE_INVITE_USER data)
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

        public bool TruncateList(List<HIS_DEBATE_INVITE_USER> listData)
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

        public List<HIS_DEBATE_INVITE_USER> Get(HisDebateInviteUserSO search, CommonParam param)
        {
            List<HIS_DEBATE_INVITE_USER> result = new List<HIS_DEBATE_INVITE_USER>();
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

        public HIS_DEBATE_INVITE_USER GetById(long id, HisDebateInviteUserSO search)
        {
            HIS_DEBATE_INVITE_USER result = null;
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
