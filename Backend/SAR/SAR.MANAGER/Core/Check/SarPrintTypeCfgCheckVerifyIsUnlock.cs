using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Core.SarPrintTypeCfg;
using SAR.MANAGER.Core.SarPrintTypeCfg.Get;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SAR.MANAGER.Core.Check
{
    class SarPrintTypeCfgCheckVerifyIsUnlock
    {
        internal static bool Verify(CommonParam param, long id)
        {
            bool result = true;
            try
            {
                SAR_PRINT_TYPE_CFG raw = new SarPrintTypeCfgBO().Get<SAR_PRINT_TYPE_CFG>(id);
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

        internal static bool Verify(CommonParam param, long id, ref SAR_PRINT_TYPE_CFG raw)
        {
            bool result = true;
            try
            {
                raw = new SarPrintTypeCfgBO().Get<SAR_PRINT_TYPE_CFG>(id);
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

        internal static bool Verify(CommonParam param, SAR_PRINT_TYPE_CFG data)
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
                    SarPrintTypeCfgFilterQuery filter = new SarPrintTypeCfgFilterQuery();
                    filter.IDs = ids;
                    List<SAR_PRINT_TYPE_CFG> listData = new SarPrintTypeCfgBO().Get<List<SAR_PRINT_TYPE_CFG>>(filter);
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

        internal static bool Verify(CommonParam param, List<SAR_PRINT_TYPE_CFG> datas)
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

        private static bool Check(CommonParam param, SAR_PRINT_TYPE_CFG raw)
        {
            bool result = true;
            if (raw == null)
            {
                result = false;
                SAR.MANAGER.Base.BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.Common__KXDDDuLieuCanXuLy);
            }
            else if (IMSys.DbConfig.SAR_RS.COMMON.IS_ACTIVE__TRUE != raw.IS_ACTIVE)
            {
                result = false;
                SAR.MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.Common__DuLieuDangBiKhoa);
            }
            return result;
        }

        internal static bool Verify(CommonParam param, List<SAR_PRINT_TYPE_CFG> datas, ref List<SAR_PRINT_TYPE_CFG> raws)
        {
            bool result = true;
            try
            {
                if (datas == null) throw new Exception("datas null");
                if (raws == null)
                {
                    raws = new List<SAR_PRINT_TYPE_CFG>();
                }

                foreach (var item in datas)
                {
                    SAR_PRINT_TYPE_CFG raw = new SAR_PRINT_TYPE_CFG();
                    result = result && Verify(param, item.ID, ref raw);
                    if (result)
                    {
                        raws.Add(raw);
                    }
                    else
                    {
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                result = false;
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return result;
        }
    }
}
