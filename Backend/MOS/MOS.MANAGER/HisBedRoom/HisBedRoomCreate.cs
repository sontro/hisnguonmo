using AutoMapper;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisRoom;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisBedRoom
{
    class HisBedRoomCreate : BusinessBase
    {
        internal HisBedRoomCreate()
            : base()
        {

        }

        internal HisBedRoomCreate(Inventec.Core.CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HisBedRoomSDO data)
        {
            bool result = false;
            try
            {
                if (data != null)
                {
                    data.HisRoom.ROOM_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_ROOM_TYPE.ID__BUONG;
                    if (new HisRoomCreate(param).Create(data.HisRoom))
                    {
                        data.HisBedRoom.ROOM_ID = data.HisRoom.ID;
                        result = this.Create(data.HisBedRoom);
                        if (!result)
                        {
                            if (!new HisRoomTruncate(param).Truncate(data.HisRoom))
                            {
                                LogSystem.Warn("Rollback du lieu his_room that bai. Can kiem tra lai. " + LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => data.HisRoom), data.HisRoom));
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

        internal bool CreateList(List<HisBedRoomSDO> listData)
        {
            bool result = false;
            try
            {
                if (IsNotNullOrEmpty(listData))
                {
                    List<HIS_ROOM> listRoom = listData.Select(s => s.HisRoom).ToList();
                    listRoom.ForEach(o => o.ROOM_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_ROOM_TYPE.ID__BUONG);
                    HisRoomCreate hisRoomCreate = new HisRoomCreate(param);
                    if (hisRoomCreate.CreateList(listRoom))
                    {
                        listData.ForEach(o => o.HisBedRoom.ROOM_ID = o.HisRoom.ID);
                        List<HIS_BED_ROOM> listBedRoom = listData.Select(s => s.HisBedRoom).ToList();
                        result = this.CreateList(listBedRoom);
                        if (!result)
                        {
                            hisRoomCreate.Rollback();
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

        private bool Create(HIS_BED_ROOM data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisBedRoomCheck checker = new HisBedRoomCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.BED_ROOM_CODE, null);
                if (valid)
                {
                    result = DAOWorker.HisBedRoomDAO.Create(data);
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

        private bool CreateList(List<HIS_BED_ROOM> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(listData);
                HisBedRoomCheck checker = new HisBedRoomCheck(param);
                foreach (HIS_BED_ROOM data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.BED_ROOM_CODE, null);
                }
                if (valid)
                {
                    result = DAOWorker.HisBedRoomDAO.CreateList(listData);
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
