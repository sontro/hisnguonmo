using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisRoom;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisCashierRoom
{
    class HisCashierRoomUpdate : BusinessBase
    {
        internal HisCashierRoomUpdate()
            : base()
        {

        }

        internal HisCashierRoomUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HisCashierRoomSDO data)
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
                        data.HisCashierRoom.ROOM_ID = data.HisRoom.ID;
                        result = this.Update(data.HisCashierRoom);
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

        private bool Update(HIS_CASHIER_ROOM data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisCashierRoomCheck checker = new HisCashierRoomCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && IsGreaterThanZero(data.ID);
                valid = valid && checker.IsUnLock(data.ID);
                valid = valid && checker.ExistsCode(data.CASHIER_ROOM_CODE, data.ID);
                if (valid)
                {
                    result = DAOWorker.HisCashierRoomDAO.Update(data);
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
