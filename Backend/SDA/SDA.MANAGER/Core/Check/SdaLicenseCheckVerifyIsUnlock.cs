using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Core.SdaLicense;
using SDA.MANAGER.Core.SdaLicense.Get;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.MANAGER.Core.Check
{
    class SdaLicenseCheckVerifyIsUnlock
    {
        internal static bool Verify(CommonParam param, long id)
        {
            bool result = true;
            try
            {
                SDA_LICENSE raw = new SdaLicenseBO().Get<SDA_LICENSE>(id);
                result = Check(param, raw);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
                param.HasException = true;
            }
            return result;
        }

        internal static bool Verify(CommonParam param, long id, ref SDA_LICENSE raw)
        {
            bool result = true;
            try
            {
                raw = new SdaLicenseBO().Get<SDA_LICENSE>(id);
                result = Check(param, raw);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
                param.HasException = true;
            }
            return result;
        }

        internal static bool Verify(CommonParam param, SDA_LICENSE data)
        {
            bool result = true;
            try
            {
                result = Check(param, data);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
                param.HasException = true;
            }
            return result;
        }

        internal static bool Verify(CommonParam param, List<long> ids)
        {
            bool result = true;
            try
            {
                if (ids != null && ids.Count > 0)
                {
                    SdaLicenseFilterQuery filter = new SdaLicenseFilterQuery();
                    filter.IDs = ids;
                    List<SDA_LICENSE> listData = new SdaLicenseBO().Get<List<SDA_LICENSE>>(filter);
                    if (listData != null && listData.Count > 0)
                    {
                        foreach (var data in listData)
                        {
                            result = result && Check(param, data);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
                param.HasException = true;
            }
            return result;
        }

        internal static bool Verify(CommonParam param, List<SDA_LICENSE> datas)
        {
            bool result = true;
            try
            {
                if (datas != null && datas.Count > 0)
                {
                    foreach (var data in datas)
                    {
                        result = result && Check(param, data);
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
                param.HasException = true;
            }
            return result;
        }

        private static bool Check(CommonParam param, SDA_LICENSE raw)
        {
            bool result = true;
            if (raw == null)
            {
                result = false;
                SDA.MANAGER.Base.BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.Common__KXDDDuLieuCanXuLy);
            }
            else if (IMSys.DbConfig.SDA_RS.COMMON.IS_ACTIVE__TRUE != raw.IS_ACTIVE)
            {
                result = false;
                SDA.MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.Common__DuLieuDangBiKhoa);
            }
            return result;
        }
    }
}
