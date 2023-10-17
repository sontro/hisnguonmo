using Inventec.Core;
using System;
using System.Linq;

namespace SDA.MANAGER.Core.Check
{
    class SdaNationalCheckVerifyExistsCode
    {
        internal static bool Verify(CommonParam param, string code, long? id)
        {
            bool result = true;
            try
            {
                if (SDA.MANAGER.Base.DAOWorker.SdaNationalDAO.ExistsCode(code, id))
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

        internal static bool Verify(CommonParam param, System.Collections.Generic.List<EFMODEL.DataModels.SDA_NATIONAL> entities)
        {
            bool result = true;
            try
            {
                System.Collections.Generic.List<string> listCodes = new System.Collections.Generic.List<string>();
                foreach (var item in entities)
                {
                    if (SDA.MANAGER.Base.DAOWorker.SdaNationalDAO.ExistsCode(item.NATIONAL_CODE, null))
                    {
                        listCodes.Add(item.NATIONAL_CODE);
                    }
                }

                if (listCodes != null && listCodes.Count > 0)
                {
                    listCodes = listCodes.Distinct().ToList();
                    var codes = string.Join(",", listCodes);
                    SDA.MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.SdaNational_MaDaTonTaiTrenHeThong, codes);
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
