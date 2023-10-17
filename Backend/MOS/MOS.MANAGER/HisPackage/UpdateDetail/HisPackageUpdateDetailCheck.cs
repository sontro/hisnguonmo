using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisSereServ;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Linq;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPackage
{
    partial class HisPackageUpdateDetailCheck : BusinessBase
    {
        internal HisPackageUpdateDetailCheck()
            : base()
        {

        }

        internal HisPackageUpdateDetailCheck(CommonParam paramCheck)
            : base(paramCheck) 
        {

        }

        internal bool IsFixedService(HIS_PACKAGE data)
        {
            bool valid = true;
            try
            {
                if (data.IS_NOT_FIXED_SERVICE == Constant.IS_TRUE)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisPackage_GoiKhongCoDinhDichVu, data.PACKAGE_NAME);
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool IsNotDuplicated(List<HisPackageDetailSDO> details)
        {
            bool valid = true;
            try
            {
                bool isDuplicated = details != null && details.Count > details.Select(o => o.ServiceId).Distinct().ToList().Count;
                if (isDuplicated)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.DuLieuDauVaoKhongHopLe);
                    LogSystem.Warn("Ton tai service_id trung nhau");
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }
    }
}
