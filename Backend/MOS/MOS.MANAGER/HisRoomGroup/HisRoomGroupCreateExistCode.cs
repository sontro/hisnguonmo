using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisRoomGroup
{
    partial class HisRoomGroupCreate : BusinessBase
    {
		private List<HIS_ROOM_GROUP> recentHisRoomGroups = new List<HIS_ROOM_GROUP>();
		
        internal HisRoomGroupCreate()
            : base()
        {

        }

        internal HisRoomGroupCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_ROOM_GROUP data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisRoomGroupCheck checker = new HisRoomGroupCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.ROOM_GROUP_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisRoomGroupDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisRoomGroup_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisRoomGroup that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisRoomGroups.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisRoomGroups))
            {
                if (!DAOWorker.HisRoomGroupDAO.TruncateList(this.recentHisRoomGroups))
                {
                    LogSystem.Warn("Rollback du lieu HisRoomGroup that bai, can kiem tra lai." + LogUtil.TraceData("recentHisRoomGroups", this.recentHisRoomGroups));
                }
				this.recentHisRoomGroups = null;
            }
        }
    }
}
