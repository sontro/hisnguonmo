using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisEkip;
using MOS.MANAGER.HisEkipUser;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisSereServFile;
using MOS.MANAGER.HisSereServPttt;
using MOS.MANAGER.HisServiceReq;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisSereServExt
{
    partial class HisSereServExtUpdate : BusinessBase
    {
        private List<HIS_SERE_SERV_EXT> beforeUpdateHisSereServExts = new List<HIS_SERE_SERV_EXT>();

        private HisEkipUserCreate hisEkipUserCreate;
        private HisEkipUserTruncate hisEkipUserTruncate;
        private HisEkipUserUpdate hisEkipUserUpdate;
        private HisEkipCreate hisEkipCreate;
        private HisSereServUpdate hisSereServUpdate;

        internal HisSereServExtUpdate()
            : base()
        {

        }

        internal HisSereServExtUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisEkipUserTruncate = new HisEkipUserTruncate(param);
            this.hisEkipUserUpdate = new HisEkipUserUpdate(param);
            this.hisEkipUserCreate = new HisEkipUserCreate(param);
            this.hisEkipCreate = new HisEkipCreate(param);
            this.hisSereServUpdate = new HisSereServUpdate(param);
        }

        internal bool Update(HIS_SERE_SERV_EXT data, bool isValidate)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisSereServExtCheck checker = new HisSereServExtCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_SERE_SERV_EXT raw = null;

                if (isValidate)
                {
                    valid = valid && checker.VerifyId(data.ID, ref raw);
                    valid = valid && checker.IsUnLock(raw);
                    valid = valid && checker.IsNotAprovedSurgeryRemuneration(raw);
                }
                
                if (valid)
                {
                    this.beforeUpdateHisSereServExts.Add(raw);
                    if (!DAOWorker.HisSereServExtDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSereServExt_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisSereServExt that bai." + LogUtil.TraceData("data", data));
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

        internal bool UpdateList(List<HIS_SERE_SERV_EXT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisSereServExtCheck checker = new HisSereServExtCheck(param);
                List<HIS_SERE_SERV_EXT> listRaw = new List<HIS_SERE_SERV_EXT>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    this.beforeUpdateHisSereServExts.AddRange(listRaw);
                    if (!DAOWorker.HisSereServExtDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSereServExt_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisSereServExt that bai." + LogUtil.TraceData("listData", listData));
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

        internal bool UpdateList(List<HIS_SERE_SERV_EXT> listData, List<HIS_SERE_SERV_EXT> listBefore)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisSereServExtCheck checker = new HisSereServExtCheck(param);
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.IsUnLock(listBefore);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    this.beforeUpdateHisSereServExts.AddRange(listBefore);
                    if (!DAOWorker.HisSereServExtDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSereServExt_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisSereServExt that bai." + LogUtil.TraceData("listData", listData));
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
            this.hisEkipUserUpdate.RollbackData();
            this.hisEkipUserCreate.RollbackData();
            this.hisEkipCreate.RollbackData();
            if (IsNotNullOrEmpty(this.beforeUpdateHisSereServExts))
            {
                if (!DAOWorker.HisSereServExtDAO.UpdateList(this.beforeUpdateHisSereServExts))
                {
                    LogSystem.Warn("Rollback du lieu HisSereServExt that bai, can kiem tra lai." + LogUtil.TraceData("HisSereServExts", this.beforeUpdateHisSereServExts));
                }
            }
        }

        
    }
}
