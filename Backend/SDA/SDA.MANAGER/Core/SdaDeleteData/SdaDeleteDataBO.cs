using System;

namespace SDA.MANAGER.Core.SdaDeleteData
{
    partial class SdaDeleteDataBO : BusinessObjectBase
    {
        internal SdaDeleteDataBO()
            : base()
        {

        }

        internal T Get<T>(object data)
        {
            T result = default(T);
            try
            {
                IDelegacyT delegacy = new SdaDeleteDataGet(param, data);
                result = delegacy.Execute<T>();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = default(T);
            }
            return result;
        }

        internal bool Create(object data)
        {
            bool result = false;
            try
            {
                IDelegacy delegacy = new SdaDeleteDataCreate(param, data);
                result = delegacy.Execute();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        internal bool Update(object data)
        {
            bool result = false;
            try
            {
                IDelegacy delegacy = new SdaDeleteDataUpdate(param, data);
                result = delegacy.Execute();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        internal bool ChangeLock(object data)
        {
            bool result = false;
            try
            {
                IDelegacy delegacy = new SdaDeleteDataChangeLock(param, data);
                result = delegacy.Execute();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        internal bool Delete(object data)
        {
            bool result = false;
            try
            {
                IDelegacy delegacy = new SdaDeleteDataDelete(param, data);
                result = delegacy.Execute();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }
    }
}
