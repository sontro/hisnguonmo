using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisRoom;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisDataStore
{
    class HisDataStoreUpdate : BusinessBase
    {
        internal HisDataStoreUpdate()
            : base()
        {

        }

        internal HisDataStoreUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HisDataStoreSDO data)
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
                        data.HisDataStore.ROOM_ID = data.HisRoom.ID;
                        result = this.Update(data.HisDataStore);
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

        private bool Update(HIS_DATA_STORE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisDataStoreCheck checker = new HisDataStoreCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && IsGreaterThanZero(data.ID);
                valid = valid && checker.IsUnLock(data.ID);
                valid = valid && checker.ExistsCode(data.DATA_STORE_CODE, data.ID);
                if (valid)
                {
                    result = DAOWorker.HisDataStoreDAO.Update(data);
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
