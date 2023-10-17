using SDA.MANAGER.Core.SdaGroup;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Manager
{
    public partial class SdaGroupManager : ManagerBase
    {
        public SdaGroupManager(CommonParam param)
            : base(param)
        {

        }

        public T Get<T>(object data)
        {
            T result = default(T);
            try
            {
                if (!IsNotNull(data)) throw new ArgumentNullException("data");
                SdaGroupBO bo = new SdaGroupBO();
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

        //public object Create(object data)
        //{
        //    object result = null;
        //    try
        //    {
        //        if (!IsNotNull(data)) throw new ArgumentNullException("data");
        //        SdaGroupBO bo = new SdaGroupBO();
        //        if (bo.Create(data))
        //        {
        //            result = data;
        //        }
        //        CopyCommonParamInfo(bo);
        //    }
        //    catch (Exception ex)
        //    {
        //        Inventec.Common.Logging.LogSystem.Error(ex);
        //        param.HasException = true;
        //        result = null;
        //    }
        //    return result;
        //}

        //public object Update(object data)
        //{
        //    object result = null;
        //    try
        //    {
        //        if (!IsNotNull(data)) throw new ArgumentNullException("data");
        //        SdaGroupBO bo = new SdaGroupBO();
        //        if (bo.Update(data))
        //        {
        //            result = data;
        //        }
        //        CopyCommonParamInfo(bo);
        //    }
        //    catch (Exception ex)
        //    {
        //        Inventec.Common.Logging.LogSystem.Error(ex);
        //        param.HasException = true;
        //        result = null;
        //    }
        //    return result;
        //}

        public object ChangeLock(object data)
        {
            object result = null;
            try
            {
                if (!IsNotNull(data)) throw new ArgumentNullException("data");
                SdaGroupBO bo = new SdaGroupBO();
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
                SdaGroupBO bo = new SdaGroupBO();
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
