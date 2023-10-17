using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisRoomSaro
{
    partial class HisRoomSaroCreate : BusinessBase
    {
		private List<HIS_ROOM_SARO> recentHisRoomSaros = new List<HIS_ROOM_SARO>();
		
        internal HisRoomSaroCreate()
            : base()
        {

        }

        internal HisRoomSaroCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_ROOM_SARO data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisRoomSaroCheck checker = new HisRoomSaroCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisRoomSaroDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisRoomSaro_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisRoomSaro that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisRoomSaros.Add(data);
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
		
		internal bool CreateList(List<HIS_ROOM_SARO> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisRoomSaroCheck checker = new HisRoomSaroCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisRoomSaroDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisRoomSaro_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisRoomSaro that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisRoomSaros.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisRoomSaros))
            {
                if (!DAOWorker.HisRoomSaroDAO.TruncateList(this.recentHisRoomSaros))
                {
                    LogSystem.Warn("Rollback du lieu HisRoomSaro that bai, can kiem tra lai." + LogUtil.TraceData("recentHisRoomSaros", this.recentHisRoomSaros));
                }
				this.recentHisRoomSaros = null;
            }
        }
    }
}
