using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisMedicineTypeRoom
{
    partial class HisMedicineTypeRoomUpdate : BusinessBase
    {
		private List<HIS_MEDICINE_TYPE_ROOM> beforeUpdateHisMedicineTypeRooms = new List<HIS_MEDICINE_TYPE_ROOM>();
		
        internal HisMedicineTypeRoomUpdate()
            : base()
        {

        }

        internal HisMedicineTypeRoomUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_MEDICINE_TYPE_ROOM data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMedicineTypeRoomCheck checker = new HisMedicineTypeRoomCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_MEDICINE_TYPE_ROOM raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {                    
					if (!DAOWorker.HisMedicineTypeRoomDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMedicineTypeRoom_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisMedicineTypeRoom that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateHisMedicineTypeRooms.Add(raw);
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

        internal bool UpdateList(List<HIS_MEDICINE_TYPE_ROOM> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMedicineTypeRoomCheck checker = new HisMedicineTypeRoomCheck(param);
                List<HIS_MEDICINE_TYPE_ROOM> listRaw = new List<HIS_MEDICINE_TYPE_ROOM>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					if (!DAOWorker.HisMedicineTypeRoomDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMedicineTypeRoom_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisMedicineTypeRoom that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisMedicineTypeRooms.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisMedicineTypeRooms))
            {
                if (!DAOWorker.HisMedicineTypeRoomDAO.UpdateList(this.beforeUpdateHisMedicineTypeRooms))
                {
                    LogSystem.Warn("Rollback du lieu HisMedicineTypeRoom that bai, can kiem tra lai." + LogUtil.TraceData("HisMedicineTypeRooms", this.beforeUpdateHisMedicineTypeRooms));
                }
				this.beforeUpdateHisMedicineTypeRooms = null;
            }
        }
    }
}
