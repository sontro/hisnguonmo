using SDA.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.MANAGER.Core.Check
{
    class SdaTranslateCheckVerifyValidData
    {
        internal static bool Verify(CommonParam param, SDA_TRANSLATE data)
        {
            bool result = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
            }
            catch (ArgumentNullException ex)
            {
                SDA.MANAGER.Base.BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.Common__ThieuThongTinBatBuoc);
                Inventec.Common.Logging.LogSystem.Warn(ex);
                result = false;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
                param.HasException = true;
            }
            return result;
        }

        internal static bool Verify(CommonParam param, List<SDA_TRANSLATE> datas)
        {
            bool result = true;
            try
            {
                if (datas == null) throw new ArgumentNullException("datas");
                foreach (var data in datas)
                {
                    if (SDA.MANAGER.Base.DAOWorker.SdaTranslateDAO.IsExistsCreate(data))
                    {
                        SDA.MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.SdaTranslate_DuLieuDaTonTai);
                        result = false;
                        break;
                    }
                }
            }
            catch (ArgumentNullException ex)
            {
                SDA.MANAGER.Base.BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.Common__ThieuThongTinBatBuoc);
                Inventec.Common.Logging.LogSystem.Warn(ex);
                result = false;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
                param.HasException = true;
            }
            return result;
        }

        internal static bool VerifyIsExistsCreate(CommonParam param, SDA_TRANSLATE data)
        {
            bool result = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (SDA.MANAGER.Base.DAOWorker.SdaTranslateDAO.IsExistsCreate(data))
                {
                    SDA.MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.SdaTranslate_DuLieuDaTonTai);
                    result = false;
                }
            }
            catch (ArgumentNullException ex)
            {
                SDA.MANAGER.Base.BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.Common__ThieuThongTinBatBuoc);
                Inventec.Common.Logging.LogSystem.Warn(ex);
                result = false;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
                param.HasException = true;
            }
            return result;
        }

        internal static bool VerifyIsExistsUpdate(CommonParam param, SDA_TRANSLATE data)
        {
            bool result = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (SDA.MANAGER.Base.DAOWorker.SdaTranslateDAO.IsExistsUpdate(data))
                {
                    SDA.MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.SdaTranslate_DuLieuDaTonTai);
                    result = false;
                }
            }
            catch (ArgumentNullException ex)
            {
                SDA.MANAGER.Base.BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.Common__ThieuThongTinBatBuoc);
                Inventec.Common.Logging.LogSystem.Warn(ex);
                result = false;
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
