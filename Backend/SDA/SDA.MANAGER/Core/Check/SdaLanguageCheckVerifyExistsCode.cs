using Inventec.Core;
using SDA.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace SDA.MANAGER.Core.Check
{
    class SdaLanguageCheckVerifyExistsCode
    {
        internal static bool Verify(CommonParam param, string code, long? id)
        {
            bool result = true;
            try
            {
                if (SDA.MANAGER.Base.DAOWorker.SdaLanguageDAO.ExistsCode(code, id))
                {
                    SDA.MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.Common__MaDaTonTaiTrenHeThong);
                    result = false;
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

        internal static bool Verify(CommonParam param, List<SDA_LANGUAGE> list)
        {
            bool result = true;
            try
            {
                foreach (var item in list)
                {
                    if (Verify(param, item.LANGUAGE_CODE, item.ID))
                    {
                        result = false;
                        break;
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
    }
}
