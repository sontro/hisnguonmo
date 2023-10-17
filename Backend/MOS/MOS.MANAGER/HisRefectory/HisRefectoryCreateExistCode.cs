using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisRoom;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisRefectory
{
    partial class HisRefectoryCreate : BusinessBase
    {
		private List<HIS_REFECTORY> recentHisRefectorys = new List<HIS_REFECTORY>();
		
        internal HisRefectoryCreate()
            : base()
        {

        }

        internal HisRefectoryCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HisRefectorySDO data)
        {
            bool result = false;
            try
            {
                if (data != null)
                {
                    data.HisRoom.ROOM_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_ROOM_TYPE.ID__NA;
                    if (new HisRoomCreate(param).Create(data.HisRoom))
                    {
                        data.HisRefectory.ROOM_ID = data.HisRoom.ID;
                        result = this.Create(data.HisRefectory);
                        if (!result)
                        {
                            if (!new HisRoomTruncate(param).Truncate(data.HisRoom))
                            {
                                LogSystem.Warn("Rollback du lieu his_room that bai. Can kiem tra lai. " + LogUtil.TraceData(LogUtil.GetMemberName(() => data.HisRoom), data.HisRoom));
                            }
                        }
                    }
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

        internal bool Create(HIS_REFECTORY data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisRefectoryCheck checker = new HisRefectoryCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.REFECTORY_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisRefectoryDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisRefectory_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisRefectory that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisRefectorys.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisRefectorys))
            {
                if (!DAOWorker.HisRefectoryDAO.TruncateList(this.recentHisRefectorys))
                {
                    LogSystem.Warn("Rollback du lieu HisRefectory that bai, can kiem tra lai." + LogUtil.TraceData("recentHisRefectorys", this.recentHisRefectorys));
                }
				this.recentHisRefectorys = null;
            }
        }
    }
}
