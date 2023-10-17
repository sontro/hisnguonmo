using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTreatmentRoom
{
    partial class HisTreatmentRoomCreate : BusinessBase
    {
		private List<HIS_TREATMENT_ROOM> recentHisTreatmentRooms = new List<HIS_TREATMENT_ROOM>();
		
        internal HisTreatmentRoomCreate()
            : base()
        {

        }

        internal HisTreatmentRoomCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_TREATMENT_ROOM data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisTreatmentRoomCheck checker = new HisTreatmentRoomCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.TREATMENT_ROOM_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisTreatmentRoomDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisTreatmentRoom_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisTreatmentRoom that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisTreatmentRooms.Add(data);
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

        internal bool CreateList(List<HIS_TREATMENT_ROOM> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisTreatmentRoomCheck checker = new HisTreatmentRoomCheck(param);
                valid = valid && IsNotNullOrEmpty(listData);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.TREATMENT_ROOM_CODE, null);
                }
                if (valid)
                {
                    if (!DAOWorker.HisTreatmentRoomDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisTreatmentRoom_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisTreatmentRoom that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisTreatmentRooms.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisTreatmentRooms))
            {
                if (!DAOWorker.HisTreatmentRoomDAO.TruncateList(this.recentHisTreatmentRooms))
                {
                    LogSystem.Warn("Rollback du lieu HisTreatmentRoom that bai, can kiem tra lai." + LogUtil.TraceData("recentHisTreatmentRooms", this.recentHisTreatmentRooms));
                }
				this.recentHisTreatmentRooms = null;
            }
        }
    }
}
