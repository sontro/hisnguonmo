using ACS.MANAGER.Core.TokenSys;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Manager
{
    public partial class AcsTokenManagerExtra : Inventec.Backend.MANAGER.ManagerBase
    {
        public AcsTokenManagerExtra(CommonParam param)
            : base(param)
        {

        }

        public object Login(object data)
        {
            object result = null;
            try
            {
                if (!IsNotNull(data)) throw new ArgumentNullException("data");
                AcsTokenBO bo = new AcsTokenBO();
                bo.CopyCommonParamInfoGet(param);
                result = bo.Login(data);
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

        public object LoginBySecretKey(object data)
        {
            object result = null;
            try
            {
                if (!IsNotNull(data)) throw new ArgumentNullException("data");
                AcsTokenBO bo = new AcsTokenBO();
                bo.CopyCommonParamInfoGet(param);
                result = bo.LoginBySecretKey(data);
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

        public object LoginByAuthenRequest(object data)
        {
            object result = null;
            try
            {
                if (!IsNotNull(data)) throw new ArgumentNullException("data");
                AcsTokenBO bo = new AcsTokenBO();
                bo.CopyCommonParamInfoGet(param);
                result = bo.LoginByAuthenRequest(data);
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

        public object LoginByEmail(object data)
        {
            object result = null;
            try
            {
                if (!IsNotNull(data)) throw new ArgumentNullException("data");
                AcsTokenBO bo = new AcsTokenBO();
                bo.CopyCommonParamInfoGet(param);
                result = bo.LoginByEmail(data);
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

        public object Authorize(object data)
        {
            object result = null;
            try
            {
                if (!IsNotNull(data)) throw new ArgumentNullException("data");
                AcsTokenBO bo = new AcsTokenBO();
                bo.CopyCommonParamInfoGet(param);
                result = bo.Authorize(data);
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

        public object SyncToken(object data)
        {
            object result = false;
            try
            {
                if (!IsNotNull(data)) throw new ArgumentNullException("data");
                AcsTokenBO bo = new AcsTokenBO();
                bo.CopyCommonParamInfoGet(param);
                result = bo.SyncToken(data);
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
