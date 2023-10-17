using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisImpMest.Common.Delete;
using MOS.MANAGER.Token;
using MOS.SDO;
using System;
using System.Collections.Generic;


namespace MOS.MANAGER.HisImpMest
{
    /// <summary>
    /// Tao phieu nhap
    /// </summary>
    class HisImpMestCreate : BusinessBase
    {
        private List<HIS_IMP_MEST> recentHisImpMests = new List<HIS_IMP_MEST>();

        internal HisImpMestCreate()
            : base()
        {
        }

        internal HisImpMestCreate(CommonParam paramCreate)
            : base(paramCreate)
        {
        }

        internal bool Create(HIS_IMP_MEST data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                WorkPlaceSDO workPlace = null;
                HisImpMestCheck checker = new HisImpMestCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.VerifyStatusForInsert(data);
                valid = valid && checker.IsUnLockMediStock(data);
                valid = valid && checker.CheckRequestRoomPermission(data, ref workPlace);
                if (valid)
                {
                    data.REQ_LOGINNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                    data.REQ_USERNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
                    data.REQ_ROOM_ID = workPlace != null ? new Nullable<long>(workPlace.RoomId) : null;
                    data.REQ_DEPARTMENT_ID = workPlace != null ? new Nullable<long>(workPlace.DepartmentId) : null;
                    if (!DAOWorker.HisImpMestDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisImpMest_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisImpMest that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisImpMests.Add(data);
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

        internal bool CreateList(List<HIS_IMP_MEST> datas)
        {
            bool result = false;
            try
            {
                bool valid = true;
                WorkPlaceSDO workPlace = null;
                HisImpMestCheck checker = new HisImpMestCheck(param);
                foreach (HIS_IMP_MEST data in datas)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.VerifyStatusForInsert(data);
                    valid = valid && checker.IsUnLockMediStock(data);
                    valid = valid && checker.CheckRequestRoomPermission(data, ref workPlace);
                    if (valid)
                    {
                        data.REQ_LOGINNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                        data.REQ_USERNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
                        data.REQ_ROOM_ID = workPlace != null ? new Nullable<long>(workPlace.RoomId) : null;
                        data.REQ_DEPARTMENT_ID = workPlace != null ? new Nullable<long>(workPlace.DepartmentId) : null;
                    }
                }
                if (valid)
                {
                    if (!DAOWorker.HisImpMestDAO.CreateList(datas))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisImpMest_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisImpMest that bai." + LogUtil.TraceData("datas", datas));
                    }
                    this.recentHisImpMests = datas;
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
            if (IsNotNullOrEmpty(this.recentHisImpMests))
            {
                if (!DAOWorker.HisImpMestDAO.TruncateList(this.recentHisImpMests))
                {
                    LogSystem.Warn("Rollback du lieu HisImpMest that bai, can kiem tra lai." + LogUtil.TraceData("HisImpMest", this.recentHisImpMests));
                }
            }
        }
    }
}
