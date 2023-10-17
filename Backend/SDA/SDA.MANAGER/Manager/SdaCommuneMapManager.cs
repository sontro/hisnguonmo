using SDA.MANAGER.Core.SdaCommuneMap;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Manager
{
    public partial class SdaCommuneMapManager : ManagerBase
    {
        public SdaCommuneMapManager(CommonParam param)
            : base(param)
        {

        }

        public T Get<T>(object data)
        {
            T result = default(T);
            try
            {
                if (!IsNotNull(data)) throw new ArgumentNullException("data");
                SdaCommuneMapBO bo = new SdaCommuneMapBO();
                bo.CopyCommonParamInfoGet(param);
                result = bo.Get<T>(data);
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

        public object Create(object data)
        {
            object result = null;
            try
            {
                if (!IsNotNull(data)) throw new ArgumentNullException("data");
                SdaCommuneMapBO bo = new SdaCommuneMapBO();
                if (bo.Create(data))
                {
                    result = data;
                }
                CopyCommonParamInfo(bo);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }
            return result;
        }

        public object Update(object data)
        {
            object result = null;
            try
            {
                if (!IsNotNull(data)) throw new ArgumentNullException("data");
                SdaCommuneMapBO bo = new SdaCommuneMapBO();
                if (bo.Update(data))
                {
                    result = data;
                }
                CopyCommonParamInfo(bo);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }
            return result;
        }

        public object ChangeLock(object data)
        {
            object result = null;
            try
            {
                if (!IsNotNull(data)) throw new ArgumentNullException("data");
                SdaCommuneMapBO bo = new SdaCommuneMapBO();
                if (bo.ChangeLock(data))
                {
                    result = data;
                }
                CopyCommonParamInfo(bo);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }
            return result;
        }

        public bool Delete(object data)
        {
            bool result = false;
            try
            {
                if (!IsNotNull(data)) throw new ArgumentNullException("data");
                SdaCommuneMapBO bo = new SdaCommuneMapBO();
                if (bo.Delete(data))
                {
                    result = true;
                }
                CopyCommonParamInfo(bo);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }
    }
}
