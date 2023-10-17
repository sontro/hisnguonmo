using Inventec.Core;
using Inventec.Common.Logging;
using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace SDA.MANAGER.SdaCustomizeButton
{
    public partial class SdaCustomizeButtonManager : BusinessBase
    {
        public SdaCustomizeButtonManager()
            : base()
        {

        }
        
        public SdaCustomizeButtonManager(CommonParam param)
            : base(param)
        {

        }
		
        //[Logger]
        public ApiResultObject<List<SDA_CUSTOMIZE_BUTTON>> Get(SdaCustomizeButtonFilterQuery filter)
        {
            ApiResultObject<List<SDA_CUSTOMIZE_BUTTON>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<SDA_CUSTOMIZE_BUTTON> resultData = null;
                if (valid)
                {
                    resultData = new SdaCustomizeButtonGet(param).Get(filter);
                }
                result = this.PackSuccess(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }
            return result;
        }

        //[Logger]
        public ApiResultObject<SDA_CUSTOMIZE_BUTTON> Create(SDA_CUSTOMIZE_BUTTON data)
        {
            ApiResultObject<SDA_CUSTOMIZE_BUTTON> result = new ApiResultObject<SDA_CUSTOMIZE_BUTTON>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                SDA_CUSTOMIZE_BUTTON resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new SdaCustomizeButtonCreate(param).Create(data);
                    resultData = isSuccess ? data : null;
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }
            return result;
        }

        //[Logger]
        public ApiResultObject<SDA_CUSTOMIZE_BUTTON> Update(SDA_CUSTOMIZE_BUTTON data)
        {
            ApiResultObject<SDA_CUSTOMIZE_BUTTON> result = new ApiResultObject<SDA_CUSTOMIZE_BUTTON>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                SDA_CUSTOMIZE_BUTTON resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new SdaCustomizeButtonUpdate(param).Update(data);
                    resultData = isSuccess ? data : null;
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }
            
            return result;
        }

        //[Logger]
        public ApiResultObject<SDA_CUSTOMIZE_BUTTON> ChangeLock(long id)
        {
            ApiResultObject<SDA_CUSTOMIZE_BUTTON> result = new ApiResultObject<SDA_CUSTOMIZE_BUTTON>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                SDA_CUSTOMIZE_BUTTON resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new SdaCustomizeButtonLock(param).ChangeLock(id, ref resultData);
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }
            
            return result;
        }
		
        //[Logger]
        public ApiResultObject<SDA_CUSTOMIZE_BUTTON> Lock(long id)
        {
            ApiResultObject<SDA_CUSTOMIZE_BUTTON> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                SDA_CUSTOMIZE_BUTTON resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new SdaCustomizeButtonLock(param).Lock(id, ref resultData);
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }
		
        //[Logger]
        public ApiResultObject<SDA_CUSTOMIZE_BUTTON> Unlock(long id)
        {
            ApiResultObject<SDA_CUSTOMIZE_BUTTON> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                SDA_CUSTOMIZE_BUTTON resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new SdaCustomizeButtonLock(param).Unlock(id, ref resultData);
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        //[Logger]
        public ApiResultObject<bool> Delete(long id)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                bool resultData = false;
                if (valid)
                {
                    resultData = new SdaCustomizeButtonTruncate(param).Truncate(id);
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }
            
            return result;
        }

        public ApiResultObject<List<SDA_CUSTOMIZE_BUTTON>> CreateList(List<SDA_CUSTOMIZE_BUTTON> data)
        {
            ApiResultObject<List<SDA_CUSTOMIZE_BUTTON>> result = new ApiResultObject<List<SDA_CUSTOMIZE_BUTTON>>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<SDA_CUSTOMIZE_BUTTON> resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new SdaCustomizeButtonCreate(param).CreateList(data);
                    resultData = isSuccess ? data : null;
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }
            return result;
        }

        public ApiResultObject<List<SDA_CUSTOMIZE_BUTTON>> UpdateList(List<SDA_CUSTOMIZE_BUTTON> data)
        {
            ApiResultObject<List<SDA_CUSTOMIZE_BUTTON>> result = new ApiResultObject<List<SDA_CUSTOMIZE_BUTTON>>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<SDA_CUSTOMIZE_BUTTON> resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new SdaCustomizeButtonUpdate(param).UpdateList(data);
                    resultData = isSuccess ? data : null;
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        public ApiResultObject<bool> DeleteList(List<long> ids)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                bool resultData = false;
                if (valid)
                {
                    List<SDA_CUSTOMIZE_BUTTON> listDel = new List<SDA_CUSTOMIZE_BUTTON>();
                    foreach (var id in ids)
                    {
                        SDA_CUSTOMIZE_BUTTON del = new SDA_CUSTOMIZE_BUTTON();
                        del.ID = id;
                        listDel.Add(del);
                    }

                    resultData = new SdaCustomizeButtonTruncate(param).TruncateList(listDel);
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }
    }
}
