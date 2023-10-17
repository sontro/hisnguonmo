using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisDebateEkipUser;
using MOS.MANAGER.HisDebateUser;
using MOS.MANAGER.HisDebateInviteUser;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisDebate
{
    partial class HisDebateUpdate : BusinessBase
    {
        private List<HIS_DEBATE> beforeUpdateHisDebates = new List<HIS_DEBATE>();
        private List<HIS_DEBATE_INVITE_USER> beforDeleteHisDebateInviteUsers = new List<HIS_DEBATE_INVITE_USER>();

        private HisDebateUserCreate hisDebateUserCreate;
        private HisDebateUserTruncate hisDebateUserTruncate;
        private HisDebateInviteUserCreate hisDebateInviteUserCreate;
        private HisDebateInviteUserUpdate hisDebateInviteUserUpdate;
        private HisDebateInviteUserTruncate hisDebateInviteUserTruncate;
        private HisDebateEkipUserCreate hisDebateEkipUserCreate;
        private HisDebateEkipUserTruncate hisDebateEkipUserTruncate;

        internal HisDebateUpdate()
            : base()
        {
            this.Init();
        }

        internal HisDebateUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisDebateUserCreate = new HisDebateUserCreate(param);
            this.hisDebateUserTruncate = new HisDebateUserTruncate(param);
            this.hisDebateInviteUserCreate = new HisDebateInviteUserCreate(param);
            this.hisDebateInviteUserUpdate = new HisDebateInviteUserUpdate(param);
            this.hisDebateInviteUserTruncate = new HisDebateInviteUserTruncate(param);
            this.hisDebateEkipUserCreate = new HisDebateEkipUserCreate(param);
            this.hisDebateEkipUserTruncate = new HisDebateEkipUserTruncate(param);
        }

        internal bool Update(HIS_DEBATE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisDebateCheck checker = new HisDebateCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_DEBATE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.IsValidDebateUser(data.HIS_DEBATE_USER);
                valid = valid && checker.IsValidDebateInviteUser(data.HIS_DEBATE_INVITE_USER);
                valid = valid && checker.IsValidDebateEkipUser(data.HIS_DEBATE_EKIP_USER);
                valid = valid && checker.IsValidContentType(data);
                if (valid)
                {
                    this.beforeUpdateHisDebates.Add(raw);

                    this.hisDebateUserTruncate.TruncateByDebateId(raw.ID);
                    this.hisDebateEkipUserTruncate.TruncateByDebateId(raw.ID);

                    if (data.HIS_DEBATE_USER != null && data.HIS_DEBATE_USER.Count > 0)
                    {
                        List<HIS_DEBATE_USER> debateUsers = data.HIS_DEBATE_USER.ToList();
                        debateUsers.ForEach(o => o.DEBATE_ID = raw.ID);
                        if (!this.hisDebateUserCreate.CreateList(debateUsers))
                        {
                            throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
                        }
                    }

                    if (data.HIS_DEBATE_EKIP_USER != null && data.HIS_DEBATE_EKIP_USER.Count > 0)
                    {
                        List<HIS_DEBATE_EKIP_USER> debateEkipUsers = data.HIS_DEBATE_EKIP_USER.ToList();
                        debateEkipUsers.ForEach(o => o.DEBATE_ID = raw.ID);
                        if (!this.hisDebateEkipUserCreate.CreateList(debateEkipUsers))
                        {
                            throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
                        }
                    }

                    this.ProcessHisDebateInviteUser(raw.ID,  data.HIS_DEBATE_INVITE_USER);

                    if (!DAOWorker.HisDebateDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisDebate_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisDebate that bai." + LogUtil.TraceData("data", data));
                    }

                    result = true;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private void ProcessHisDebateInviteUser(long debateId, ICollection<HIS_DEBATE_INVITE_USER> debateInviteUsers)
        {
            List<HIS_DEBATE_INVITE_USER> toCreate = null;
            List<HIS_DEBATE_INVITE_USER> toUpdate = null;
            List<HIS_DEBATE_INVITE_USER> toDelete = new HisDebateInviteUserGet().GetByDebateId(debateId);

            if (debateInviteUsers != null && debateInviteUsers.Count > 0)
            {
                toCreate = debateInviteUsers.Where(o => o.ID == 0).ToList();
                toUpdate = debateInviteUsers.Where(o => o.ID > 0).ToList();

                // Tao moi danh sach
                if (toCreate != null && toCreate.Count > 0)
                {
                    toCreate.ForEach(o => o.DEBATE_ID = debateId);
                    if (!this.hisDebateInviteUserCreate.CreateList(toCreate))
                    {
                        throw new Exception("Ket thuc nghiep vu tao danh sach HIS_DEBATE_INVITE_USER. Rollback du lieu");
                    }
                }

                // Cap nhat danh sach
                List<long> updateIDs = null;
                if (toUpdate != null && toUpdate.Count > 0)
                {
                    updateIDs = toUpdate.Select(s => s.ID).ToList();
                    if (!this.hisDebateInviteUserUpdate.UpdateList(toUpdate))
                    {
                        throw new Exception("Ket thuc nghiep vu cap nhat danh sach HIS_DEBATE_INVITE_USER. Rollback du lieu");
                    }
                }

                // Lay cac du lieu cu ma ko co trong du lieu update gui len de xoa
                if (updateIDs != null && updateIDs.Count > 0)
                {
                    toDelete = toDelete.Where(o => !updateIDs.Contains(o.ID)).ToList();
                }

                if (toDelete != null && toDelete.Count > 0)
                {
                    this.beforDeleteHisDebateInviteUsers.AddRange(toDelete);
                    if (!this.hisDebateInviteUserTruncate.TruncateList(toDelete))
                    {
                        throw new Exception("Ket thuc nghiep vu xoa danh sach HIS_DEBATE_INVITE_USER. Rollback du lieu");
                    }
                }
            }
            else
            {
                if (toDelete != null && toDelete.Count > 0)
                {
                    this.beforDeleteHisDebateInviteUsers.AddRange(toDelete);
                    if (!this.hisDebateInviteUserTruncate.TruncateList(toDelete))
                    {
                        throw new Exception("Ket thuc nghiep vu xoa danh sach HIS_DEBATE_INVITE_USER. Rollback du lieu");
                    }
                }
            }
        }

        internal bool UpdateList(List<HIS_DEBATE> listData, List<HIS_DEBATE> listBefore)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisDebateCheck checker = new HisDebateCheck(param);
                valid = valid && checker.IsUnLock(listBefore);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    this.beforeUpdateHisDebates.AddRange(listBefore);
                    if (!DAOWorker.HisDebateDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisDebate_CapNhatThatBai);
                        return false;
                    }
                    result = true;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        internal void RollbackData()
        {
            this.hisDebateUserCreate.RollbackData();
            this.hisDebateEkipUserCreate.RollbackData();
            this.hisDebateInviteUserCreate.RollbackData();
            if (IsNotNullOrEmpty(this.beforDeleteHisDebateInviteUsers))
            {
                if (!DAOWorker.HisDebateInviteUserDAO.CreateList(this.beforDeleteHisDebateInviteUsers))
                {
                    LogSystem.Warn("Rollback du lieu HisDebateInviteUsers that bai, can kiem tra lai." + LogUtil.TraceData("HisDebateInviteUsers", this.beforDeleteHisDebateInviteUsers));
                }
            }
            if (IsNotNullOrEmpty(this.beforeUpdateHisDebates))
            {
                if (!DAOWorker.HisDebateDAO.UpdateList(this.beforeUpdateHisDebates))
                {
                    LogSystem.Warn("Rollback du lieu HisDebate that bai, can kiem tra lai." + LogUtil.TraceData("HisDebates", this.beforeUpdateHisDebates));
                }
            }
        }
    }
}
