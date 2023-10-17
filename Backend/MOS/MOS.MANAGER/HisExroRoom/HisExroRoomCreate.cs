using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisExroRoom
{
    partial class HisExroRoomCreate : BusinessBase
    {
		private List<HIS_EXRO_ROOM> recentHisExroRooms = new List<HIS_EXRO_ROOM>();
		
        internal HisExroRoomCreate()
            : base()
        {

        }

        internal HisExroRoomCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_EXRO_ROOM data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisExroRoomCheck checker = new HisExroRoomCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisExroRoomDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisExroRoom_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisExroRoom that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisExroRooms.Add(data);
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
		
		internal bool CreateList(List<HIS_EXRO_ROOM> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisExroRoomCheck checker = new HisExroRoomCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisExroRoomDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisExroRoom_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisExroRoom that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisExroRooms.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisExroRooms))
            {
                if (!DAOWorker.HisExroRoomDAO.TruncateList(this.recentHisExroRooms))
                {
                    LogSystem.Warn("Rollback du lieu HisExroRoom that bai, can kiem tra lai." + LogUtil.TraceData("recentHisExroRooms", this.recentHisExroRooms));
                }
				this.recentHisExroRooms = null;
            }
        }
    }
}
