using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Core.SdaModuleField;
using SDA.MANAGER.Core.SdaModuleField.Get;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.MANAGER.Core.Check
{
    class SdaModuleFieldCheckVerifyIsUnlock
    {
        internal static bool Verify(CommonParam param, long id)
        {
            bool result = true;
            try
            {
                SDA_MODULE_FIELD raw = new SdaModuleFieldBO().Get<SDA_MODULE_FIELD>(id);
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

        internal static bool Verify(CommonParam param, long id, ref SDA_MODULE_FIELD raw)
        {
            bool result = true;
            try
            {
                raw = new SdaModuleFieldBO().Get<SDA_MODULE_FIELD>(id);
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

        internal static bool Verify(CommonParam param, SDA_MODULE_FIELD data)
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
                    SdaModuleFieldFilterQuery filter = new SdaModuleFieldFilterQuery();
                    filter.IDs = ids;
                    List<SDA_MODULE_FIELD> listData = new SdaModuleFieldBO().Get<List<SDA_MODULE_FIELD>>(filter);
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

        internal static bool Verify(CommonParam param, List<SDA_MODULE_FIELD> datas)
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

        private static bool Check(CommonParam param, SDA_MODULE_FIELD raw)
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
