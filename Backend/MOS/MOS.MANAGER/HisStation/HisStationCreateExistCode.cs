using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisRoom;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisStation
{
    partial class HisStationCreate : BusinessBase
    {
        private List<HIS_STATION> recentHisStations = new List<HIS_STATION>();

        internal HisStationCreate()
            : base()
        {

        }

        internal HisStationCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HisStationSDO data)
        {
            bool result = false;
            try
            {
                if (data != null)
                {
                    data.HisRoom.ROOM_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_ROOM_TYPE.ID__TR;
                    if (new HisRoomCreate(param).Create(data.HisRoom))
                    {
                        data.HisStation.ROOM_ID = data.HisRoom.ID;
                        result = this.Create(data.HisStation);
                        if (!result)
                        {
                            if (!new HisRoomTruncate(param).Truncate(data.HisRoom))
                            {
                                LogSystem.Warn("Rollback du lieu his_room that bai. Can kiem tra lai. " + LogUtil.TraceData("HisRoom", data.HisRoom));
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

        internal bool Create(HIS_STATION data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisStationCheck checker = new HisStationCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.STATION_CODE, null);
                if (valid)
                {
                    if (!DAOWorker.HisStationDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisStation_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisStation that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisStations.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisStations))
            {
                if (!DAOWorker.HisStationDAO.TruncateList(this.recentHisStations))
                {
                    LogSystem.Warn("Rollback du lieu HisStation that bai, can kiem tra lai." + LogUtil.TraceData("recentHisStations", this.recentHisStations));
                }
                this.recentHisStations = null;
            }
        }
    }
}
