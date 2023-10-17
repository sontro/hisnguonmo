using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisRoomTime
{
    partial class HisRoomTimeCreate : BusinessBase
    {
		private List<HIS_ROOM_TIME> recentHisRoomTimes = new List<HIS_ROOM_TIME>();
		
        internal HisRoomTimeCreate()
            : base()
        {

        }

        internal HisRoomTimeCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_ROOM_TIME data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisRoomTimeCheck checker = new HisRoomTimeCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisRoomTimeDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisRoomTime_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisRoomTime that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisRoomTimes.Add(data);
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
		
		internal bool CreateList(List<HIS_ROOM_TIME> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisRoomTimeCheck checker = new HisRoomTimeCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisRoomTimeDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisRoomTime_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisRoomTime that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisRoomTimes.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisRoomTimes))
            {
                if (!DAOWorker.HisRoomTimeDAO.TruncateList(this.recentHisRoomTimes))
                {
                    LogSystem.Warn("Rollback du lieu HisRoomTime that bai, can kiem tra lai." + LogUtil.TraceData("recentHisRoomTimes", this.recentHisRoomTimes));
                }
				this.recentHisRoomTimes = null;
            }
        }
    }
}
