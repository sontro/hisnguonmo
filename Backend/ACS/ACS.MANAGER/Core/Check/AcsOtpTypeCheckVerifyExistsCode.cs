using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.Check
{
    class AcsOtpTypeCheckVerifyExistsCode
    {
        internal static bool Verify(CommonParam param, string code, long? id)
        {
            bool result = true;
            try
            {
                if (ACS.MANAGER.Base.DAOWorker.AcsOtpTypeDAO.ExistsCode(code, id))
                {
                    ACS.MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.Common__MaDaTonTaiTrenHeThong);
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
