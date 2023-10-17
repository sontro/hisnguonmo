using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.Check
{
    class SdaConfigCheckVerifyExistsCode
    {
        internal static bool Verify(CommonParam param, string code, long? id)
        {
            bool result = true;
            try
            {
                if (SDA.MANAGER.Base.DAOWorker.SdaConfigDAO.ExistsCode(code, id))
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
    }
}
