using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisRoom;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisStation
{
    partial class HisStationUpdate : BusinessBase
    {
		private List<HIS_STATION> beforeUpdateHisStations = new List<HIS_STATION>();
		
        internal HisStationUpdate()
            : base()
        {

        }

        internal HisStationUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HisStationSDO data)
        {
            bool result = false;
            try
            {
                if (data != null)
                {
                    //backup du lieu de phuc vu rollback
                    Mapper.CreateMap<HIS_ROOM, HIS_ROOM>();
                    HIS_ROOM originalHisRoomDTO = Mapper.Map<HIS_ROOM>(data.HisRoom);

                    if (new HisRoomUpdate(param).Update(data.HisRoom))
                    {
                        data.HisStation.ROOM_ID = data.HisRoom.ID;
                        result = this.Update(data.HisStation);
                        if (!result)
                        {
                            if (!new HisRoomUpdate(param).Update(originalHisRoomDTO))
                            {
                                LogSystem.Warn("Rollback du lieu his_room that bai. Can kiem tra lai. " + LogUtil.TraceData(LogUtil.GetMemberName(() => originalHisRoomDTO), originalHisRoomDTO));
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

        internal bool Update(HIS_STATION data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisStationCheck checker = new HisStationCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_STATION raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.STATION_CODE, data.ID);
                if (valid)
                {
					if (!DAOWorker.HisStationDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisStation_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisStation that bai." + LogUtil.TraceData("data", data));
                    }
					
					this.beforeUpdateHisStations.Add(raw);
                    
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

        internal bool UpdateList(List<HIS_STATION> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisStationCheck checker = new HisStationCheck(param);
                List<HIS_STATION> listRaw = new List<HIS_STATION>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.STATION_CODE, data.ID);
                }
                if (valid)
                {
					if (!DAOWorker.HisStationDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisStation_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisStation that bai." + LogUtil.TraceData("listData", listData));
                    }
					this.beforeUpdateHisStations.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisStations))
            {
                if (!DAOWorker.HisStationDAO.UpdateList(this.beforeUpdateHisStations))
                {
                    LogSystem.Warn("Rollback du lieu HisStation that bai, can kiem tra lai." + LogUtil.TraceData("HisStations", this.beforeUpdateHisStations));
                }
				this.beforeUpdateHisStations = null;
            }
        }
    }
}
