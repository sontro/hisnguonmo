using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisRoomType
{
    class HisRoomTypeCreate : BusinessBase
    {
        internal HisRoomTypeCreate()
            : base()
        {

        }

        internal HisRoomTypeCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_ROOM_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisRoomTypeCheck checker = new HisRoomTypeCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.ROOM_TYPE_CODE, null);
                if (valid)
                {
                    result = DAOWorker.HisRoomTypeDAO.Create(data);
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

        internal bool CreateList(List<HIS_ROOM_TYPE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisRoomTypeCheck checker = new HisRoomTypeCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.ROOM_TYPE_CODE, null);
                }
                if (valid)
                {
                    result = DAOWorker.HisRoomTypeDAO.CreateList(listData);
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
