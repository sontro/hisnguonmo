using SDA.MANAGER.Core.SdaLicense;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Manager
{
    public partial class SdaLicenseManager : ManagerBase
    {
        public T GetLast<T>(object data)
        {
            T result = default(T);
            try
            {
                if (!IsNotNull(data)) throw new ArgumentNullException("data");
                SdaLicenseBO bo = new SdaLicenseBO();
                bo.CopyCommonParamInfoGet(param);
                result = bo.GetLast<T>(data);
                CopyCommonParamInfo(bo);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = default(T);
            }
            return result;
        }

        public T Decode<T>(object data)
        {
            T result = default(T);
            try
            {
                if (!IsNotNull(data)) throw new ArgumentNullException("data");
                SdaLicenseBO bo = new SdaLicenseBO();
                bo.CopyCommonParamInfoGet(param);
                result = bo.Decode<T>(data);
                CopyCommonParamInfo(bo);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = default(T);
            }
            return result;
        }

        public T CreateT<T>(object data)
        {
            T result = default(T);
            try
            {
                if (!IsNotNull(data)) throw new ArgumentNullException("data");
                SdaLicenseBO bo = new SdaLicenseBO();
                bo.CopyCommonParamInfoGet(param);
                result = bo.CreateT<T>(data);
                CopyCommonParamInfo(bo);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = default(T);
            }
            return result;
        }

        public T UpdateT<T>(object data)
        {
            T result = default(T);
            try
            {
                if (!IsNotNull(data)) throw new ArgumentNullException("data");
                SdaLicenseBO bo = new SdaLicenseBO();
                bo.CopyCommonParamInfoGet(param);
                result = bo.UpdateT<T>(data);
                CopyCommonParamInfo(bo);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = default(T);
            }
            return result;
        }
    }
}
