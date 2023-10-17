using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.SDO;
using System;
using System.Linq;
using System.Collections.Generic;
using MOS.MANAGER.Token;

namespace MOS.MANAGER.HisExpMest.Other
{
    partial class HisExpMestOtherCheck : BusinessBase
    {
        internal HisExpMestOtherCheck()
            : base()
        {

        }

        internal HisExpMestOtherCheck(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool VerifyRequireField(HisExpMestOtherSDO data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (!IsNotNullOrEmpty(data.Materials) && !IsNotNullOrEmpty(data.Medicines) && !IsNotNullOrEmpty(data.Bloods) && !IsNotNullOrEmpty(data.SerialNumbers)) throw new ArgumentNullException("data.Materials, data.Medicines, data.Bloods, data.SerialNumbers null");
                if (data.MediStockId <= 0) throw new ArgumentNullException("data.MediStockId null");
                if (data.ReqRoomId <= 0) throw new ArgumentNullException("data.ReqRoomId null");
                if (data.ExpMestReasonId <= 0) throw new ArgumentNullException("data.ExpMestReasonId null");
            }
            catch (ArgumentNullException ex)
            {
                BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
                LogSystem.Warn(ex);
                valid = false;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool IsAllowed(HisExpMestOtherSDO data)
        {
            try
            {
                WorkPlaceSDO workPlace = TokenManager.GetWorkPlace(data.ReqRoomId);
                if (workPlace == null || workPlace.MediStockId != data.MediStockId)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_BanDangKhongTruyCapVaoKhoXuatKhongChoPhepThucHien);
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return false;
            }
            return true;
        }
    }
}
