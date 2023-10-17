using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisRoom;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisReceptionRoom
{
    class HisReceptionRoomUpdate : BusinessBase
    {
        internal HisReceptionRoomUpdate()
            : base()
        {

        }

        internal HisReceptionRoomUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HisReceptionRoomSDO data)
        {
            bool result = false;
            try
            {
                if (data != null)
                {
                    Mapper.CreateMap<HIS_ROOM, HIS_ROOM>();
                    //backup du lieu de phuc vu rollback
                    HIS_ROOM originalHisRoomDTO = Mapper.Map<HIS_ROOM>(data.HisRoom);

                    if (new HisRoomUpdate(param).Update(data.HisRoom))
                    {
                        data.HisReceptionRoom.ROOM_ID = data.HisRoom.ID;
                        result = this.Update(data.HisReceptionRoom);
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

        private bool Update(HIS_RECEPTION_ROOM data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisReceptionRoomCheck checker = new HisReceptionRoomCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && IsGreaterThanZero(data.ID);
                valid = valid && checker.IsUnLock(data.ID);
                valid = valid && checker.ExistsCode(data.RECEPTION_ROOM_CODE, data.ID);
                if (valid)
                {
                    result = DAOWorker.HisReceptionRoomDAO.Update(data);
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
