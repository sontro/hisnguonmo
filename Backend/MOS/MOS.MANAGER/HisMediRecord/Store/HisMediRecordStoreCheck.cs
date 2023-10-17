using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.UTILITY;
using System;
using System.Linq;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMediRecord.Store
{
    partial class HisMediRecordStoreCheck : BusinessBase
    {
        internal HisMediRecordStoreCheck()
            : base()
        {

        }

        internal HisMediRecordStoreCheck(CommonParam paramCheck)
            : base(paramCheck) 
        {

        }

        internal bool IsNotStored(List<HIS_MEDI_RECORD> data)
        {
            bool valid = true;
            try
            {
                List<string> storeds = data != null ? data.Where(t => t.IS_NOT_STORED != Constant.IS_TRUE).Select(o => o.STORE_CODE).ToList() : null;
                if (IsNotNullOrEmpty(storeds))
                {
                    string storeCodeStr = string.Join(",", storeds);
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisMediRecord_BenhAnDaDuocNhapKho, storeCodeStr);
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

        internal bool IsStored(HIS_MEDI_RECORD data)
        {
            bool valid = true;
            try
            {
                if (data.IS_NOT_STORED == Constant.IS_TRUE)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisMediRecord_BenhAnChuaDuocNhapKho, data.STORE_CODE);
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
