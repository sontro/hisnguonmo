using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisMaterialBean.Handle;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMaterialBean
{
    public partial class HisMaterialBeanManager : BusinessBase
    {
        public HisMaterialBeanManager()
            : base()
        {

        }
        
        public HisMaterialBeanManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_MATERIAL_BEAN>> Get(HisMaterialBeanFilterQuery filter)
        {
            ApiResultObject<List<HIS_MATERIAL_BEAN>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_MATERIAL_BEAN> resultData = null;
                if (valid)
                {
                    resultData = new HisMaterialBeanGet(param).Get(filter);
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

		[Logger]
        public ApiResultObject<List<V_HIS_MATERIAL_BEAN>> GetView(HisMaterialBeanViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_MATERIAL_BEAN>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_MATERIAL_BEAN> resultData = null;
                if (valid)
                {
                    resultData = new HisMaterialBeanGet(param).GetView(filter);
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

        [Logger]
        public ApiResultObject<List<HIS_MATERIAL_BEAN>> Take(TakeBeanSDO data)
        {
            ApiResultObject<List<HIS_MATERIAL_BEAN>> result = new ApiResultObject<List<HIS_MATERIAL_BEAN>>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<HIS_MATERIAL_BEAN> resultData = null;
                if (valid)
                {
                    resultData = new HisMaterialBeanTake(param).Take(data);
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<HIS_MATERIAL_BEAN> TakeBySerialAndPatientType(TakeBeanBySerialSDO data)
        {
            ApiResultObject<HIS_MATERIAL_BEAN> result = new ApiResultObject<HIS_MATERIAL_BEAN>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MATERIAL_BEAN resultData = null;
                bool rs = false;
                if (valid)
                {
                    rs = new HisMaterialBeanTakeBySerialAndPatientType(param).Run(data, ref resultData);
                }
                result = this.PackResult(resultData, rs);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<List<TakeMaterialBeanListResultSDO>> Take(List<TakeBeanSDO> data)
        {
            ApiResultObject<List<TakeMaterialBeanListResultSDO>> result = new ApiResultObject<List<TakeMaterialBeanListResultSDO>>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<TakeMaterialBeanListResultSDO> resultData = null;
                if (valid)
                {
                    resultData = new HisMaterialBeanTake(param).Take(data);
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<List<HIS_MATERIAL_BEAN>> TakeByMaterial(TakeBeanByMameSDO data)
        {
            ApiResultObject<List<HIS_MATERIAL_BEAN>> result = new ApiResultObject<List<HIS_MATERIAL_BEAN>>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<HIS_MATERIAL_BEAN> resultData = null;
                if (valid)
                {
                    resultData = new HisMaterialBeanTakeByMaterial(param).Take(data);
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<List<TakeMaterialBeanByMaterialListResultSDO>> TakeByMaterial(List<TakeBeanByMameSDO> data)
        {
            ApiResultObject<List<TakeMaterialBeanByMaterialListResultSDO>> result = new ApiResultObject<List<TakeMaterialBeanByMaterialListResultSDO>>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<TakeMaterialBeanByMaterialListResultSDO> resultData = null;
                if (valid)
                {
                    resultData = new HisMaterialBeanTakeByMaterial(param).Take(data);
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<bool> Release(ReleaseBeanSDO data)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                bool resultData = false;
                if (valid)
                {
                    resultData = new HisMaterialBeanRelease(param).Release(data);
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<bool> Release(string data)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                bool resultData = false;
                if (valid)
                {
                    resultData = new HisMaterialBeanRelease(param).Release(data);
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }
    }
}
