using SDA.MANAGER.Core.SdaGroup;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Manager
{
    public partial class SdaGroupManager : ManagerBase
    {
        public bool Create(object data)
        {
            bool result = false;
            try
            {
                if (!IsNotNull(data)) throw new ArgumentNullException("data");
                SdaGroupBO bo = new SdaGroupBO();
                if (bo.CreateWithUpdatePath(data))
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

        public bool Update(object data)
        {
            bool result = false;
            try
            {
                if (!IsNotNull(data)) throw new ArgumentNullException("data");
                SdaGroupBO bo = new SdaGroupBO();
                if (bo.UpdateWithUpdatePath(data))
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

        public bool UpdateAllPath(object data)
        {
            bool result = false;
            try
            {
                if (!IsNotNull(data)) throw new ArgumentNullException("data");
                SdaGroupBO bo = new SdaGroupBO();
                if (bo.UpdateAllPath(data))
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
