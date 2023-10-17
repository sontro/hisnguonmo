using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Core.SdaHideControl;
using SDA.MANAGER.Core.SdaHideControl.Get;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.MANAGER.Core.Check
{
    class SdaHideControlCheckVerifyIsUnlock
    {
        internal static bool Verify(CommonParam param, long id)
        {
            bool result = true;
            try
            {
                SDA_HIDE_CONTROL raw = new SdaHideControlBO().Get<SDA_HIDE_CONTROL>(id);
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

        internal static bool Verify(CommonParam param, long id, ref SDA_HIDE_CONTROL raw)
        {
            bool result = true;
            try
            {
                raw = new SdaHideControlBO().Get<SDA_HIDE_CONTROL>(id);
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

        internal static bool Verify(CommonParam param, SDA_HIDE_CONTROL data)
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
                    SdaHideControlFilterQuery filter = new SdaHideControlFilterQuery();
                    filter.IDs = ids;
                    List<SDA_HIDE_CONTROL> listData = new SdaHideControlBO().Get<List<SDA_HIDE_CONTROL>>(filter);
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

        internal static bool Verify(CommonParam param, List<SDA_HIDE_CONTROL> datas)
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

        private static bool Check(CommonParam param, SDA_HIDE_CONTROL raw)
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

        internal static bool Verify(CommonParam param, List<SDA_HIDE_CONTROL> entity, ref List<SDA_HIDE_CONTROL> current)
        {
            bool result = true;
            try
            {
                foreach (var item in entity)
                {
                    var raw = new SdaHideControlBO().Get<SDA_HIDE_CONTROL>(item.ID);
                    result = Check(param, raw);

                    if (current == null) current = new List<SDA_HIDE_CONTROL>();
                    current.Add(raw);
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
    }
}
