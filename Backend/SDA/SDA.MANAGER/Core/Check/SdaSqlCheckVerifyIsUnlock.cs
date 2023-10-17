using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Core.SdaSql;
using SDA.MANAGER.Core.SdaSql.Get;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.MANAGER.Core.Check
{
    class SdaSqlCheckVerifyIsUnlock
    {
        internal static bool Verify(CommonParam param, long id)
        {
            bool result = true;
            try
            {
                SDA_SQL raw = new SdaSqlBO().Get<SDA_SQL>(id);
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

        internal static bool Verify(CommonParam param, long id, ref SDA_SQL raw)
        {
            bool result = true;
            try
            {
                raw = new SdaSqlBO().Get<SDA_SQL>(id);
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

        internal static bool Verify(CommonParam param, SDA_SQL data)
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
                    SdaSqlFilterQuery filter = new SdaSqlFilterQuery();
                    filter.IDs = ids;
                    List<SDA_SQL> listData = new SdaSqlBO().Get<List<SDA_SQL>>(filter);
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

        internal static bool Verify(CommonParam param, List<SDA_SQL> datas)
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

        private static bool Check(CommonParam param, SDA_SQL raw)
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
