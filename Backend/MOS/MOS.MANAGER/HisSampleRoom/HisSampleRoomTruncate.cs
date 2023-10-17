using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisRoom;
using MOS.MANAGER.HisSaroExro;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSampleRoom
{
    class HisSampleRoomTruncate : BusinessBase
    {
        internal HisSampleRoomTruncate()
            : base()
        {

        }

        internal HisSampleRoomTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(HIS_SAMPLE_ROOM data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisSampleRoomCheck checker = new HisSampleRoomCheck(param);
                HIS_SAMPLE_ROOM raw = null;
                HisRoomCheck roomChecker = new HisRoomCheck(param);
                valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && roomChecker.CheckConstraint(raw.ROOM_ID);
                valid = valid && checker.CheckContraint(raw.ID);
                if (valid)
                {
                    if (!new HisSaroExroTruncate(param).TruncateBySampleRoomId(raw.ID))
                    {
                        throw new Exception("Xoa du lieu HIS_SARO_EXRO that bai. Ket thuc nghiep vu");
                    }

                    if (DAOWorker.HisSampleRoomDAO.Truncate(data))
                    {
                        result = new HisRoomTruncate(param).Truncate(data.ROOM_ID);
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

        internal bool TruncateList(List<HIS_SAMPLE_ROOM> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisSampleRoomCheck checker = new HisSampleRoomCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                    valid = valid && checker.IsUnLock(data.ID);
                }
                if (valid)
                {
                    result = DAOWorker.HisSampleRoomDAO.TruncateList(listData);
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
