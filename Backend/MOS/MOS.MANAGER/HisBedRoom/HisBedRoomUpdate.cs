using AutoMapper;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisRoom;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBedRoom
{
    class HisBedRoomUpdate : BusinessBase
    {
        internal HisBedRoomUpdate()
            : base()
        {

        }

        internal HisBedRoomUpdate(Inventec.Core.CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HisBedRoomSDO data)
        {
            bool result = false;
            try
            {
                if (data != null)
                {
                    //backup du lieu de phuc vu rollback
                    Mapper.CreateMap<HIS_ROOM, HIS_ROOM>();
                    HIS_ROOM originalHisRoomDTO = Mapper.Map<HIS_ROOM>(data.HisRoom);

                    data.HisRoom.ROOM_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_ROOM_TYPE.ID__BUONG;
                    if (new HisRoomUpdate(param).Update(data.HisRoom))
                    {
                        data.HisBedRoom.ROOM_ID = data.HisRoom.ID;
                        result = this.Update(data.HisBedRoom);
                        if (!result)
                        {
                            if (!new HisRoomUpdate(param).Update(originalHisRoomDTO))
                            {
                                LogSystem.Warn("Rollback du lieu his_room that bai. Can kiem tra lai. " + LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => originalHisRoomDTO), originalHisRoomDTO));
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

        private bool Update(HIS_BED_ROOM data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisBedRoomCheck checker = new HisBedRoomCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && IsGreaterThanZero(data.ID);
                valid = valid && checker.IsUnLock(data.ID);
                valid = valid && checker.ExistsCode(data.BED_ROOM_CODE, data.ID);
                if (valid)
                {
                    result = DAOWorker.HisBedRoomDAO.Update(data);
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
    }
}
