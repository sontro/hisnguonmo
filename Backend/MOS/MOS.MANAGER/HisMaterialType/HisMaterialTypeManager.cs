using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMaterialType
{
    public partial class HisMaterialTypeManager : BusinessBase
    {
        public HisMaterialTypeManager()
            : base()
        {

        }

        public HisMaterialTypeManager(CommonParam param)
            : base(param)
        {

        }

        [Logger]
        public ApiResultObject<List<HIS_MATERIAL_TYPE>> Get(HisMaterialTypeFilterQuery filter)
        {
            ApiResultObject<List<HIS_MATERIAL_TYPE>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_MATERIAL_TYPE> resultData = null;
                if (valid)
                {
                    resultData = new HisMaterialTypeGet(param).Get(filter);
                    if (IsNotNullOrEmpty(resultData))
                    {
                        //xu ly de ko bi reference khi convert sang json truyen xuong client
                        //==> dan den loi overload
                        resultData.ForEach(o =>
                        {
                            o.HIS_MATERIAL_TYPE1 = null;
                            o.HIS_MATERIAL_TYPE2 = null;
                        });
                    }
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
        public ApiResultObject<List<V_HIS_MATERIAL_TYPE>> GetView(HisMaterialTypeViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_MATERIAL_TYPE>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_MATERIAL_TYPE> resultData = null;
                if (valid)
                {
                    resultData = new HisMaterialTypeGet(param).GetView(filter);
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

        public ApiResultObject<List<HisMaterialTypeInStockSDO>> GetInStockMaterialType(HisMaterialTypeStockViewFilter filter)
        {
            ApiResultObject<List<HisMaterialTypeInStockSDO>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HisMaterialTypeInStockSDO> resultData = null;
                if (valid)
                {
                    resultData = new HisMaterialTypeGet(param).GetInStockMaterialType(filter);
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

        public ApiResultObject<List<GetMaterialTypeForEmrResultSDO>> GetMaterialTypeForEmr(long treatmentId)
        {
            ApiResultObject<List<GetMaterialTypeForEmrResultSDO>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsGreaterThanZero(treatmentId);
                List<GetMaterialTypeForEmrResultSDO> resultData = null;
                if (valid)
                {
                    resultData = new HisMaterialTypeGet(param).GetMaterialTypeForEmr(treatmentId);
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
        public ApiResultObject<HIS_MATERIAL_TYPE> Create(HIS_MATERIAL_TYPE data)
        {
            ApiResultObject<HIS_MATERIAL_TYPE> result = new ApiResultObject<HIS_MATERIAL_TYPE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MATERIAL_TYPE resultData = null;
                if (valid && new HisMaterialTypeCreate(param).Create(data))
                {
                    resultData = data;
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

        [Logger]
        public ApiResultObject<HIS_MATERIAL_TYPE> Update(HIS_MATERIAL_TYPE data)
        {
            ApiResultObject<HIS_MATERIAL_TYPE> result = new ApiResultObject<HIS_MATERIAL_TYPE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MATERIAL_TYPE resultData = null;
                if (valid && new HisMaterialTypeUpdate(param).Update(data))
                {
                    resultData = data;
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

        [Logger]
        public ApiResultObject<HIS_MATERIAL_TYPE> Lock(HIS_MATERIAL_TYPE data)
        {
            ApiResultObject<HIS_MATERIAL_TYPE> result = new ApiResultObject<HIS_MATERIAL_TYPE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MATERIAL_TYPE resultData = null;
                if (valid && new HisMaterialTypeLock(param).Lock(data))
                {
                    resultData = data;
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

        [Logger]
        public ApiResultObject<HIS_MATERIAL_TYPE> Unlock(HIS_MATERIAL_TYPE data)
        {
            ApiResultObject<HIS_MATERIAL_TYPE> result = new ApiResultObject<HIS_MATERIAL_TYPE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MATERIAL_TYPE resultData = null;
                if (valid && new HisMaterialTypeLock(param).Unlock(data))
                {
                    resultData = data;
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

        [Logger]
        public ApiResultObject<bool> Delete(HIS_MATERIAL_TYPE data)
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
                    resultData = new HisMaterialTypeTruncate(param).Truncate(data);
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

        [Logger]
        public ApiResultObject<List<HIS_MATERIAL_TYPE>> CreateList(List<HIS_MATERIAL_TYPE> listData)
        {
            ApiResultObject<List<HIS_MATERIAL_TYPE>> result = new ApiResultObject<List<HIS_MATERIAL_TYPE>>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNullOrEmpty(listData);
                List<HIS_MATERIAL_TYPE> resultData = null;
                if (valid && new HisMaterialTypeCreateSql(param).Run(listData))
                {
                    resultData = listData;
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

        [Logger]
        public ApiResultObject<HIS_MATERIAL_TYPE> UpdateSdo(HisMaterialTypeSDO data)
        {
            ApiResultObject<HIS_MATERIAL_TYPE> result = new ApiResultObject<HIS_MATERIAL_TYPE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MATERIAL_TYPE resultData = null;
                if (valid && new HisMaterialTypeUpdate(param).UpdateSdo(data))
                {
                    resultData = data.HisMaterialType;
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

        [Logger]
        public ApiResultObject<List<HisMaterialTypeView1SDO>> GetPriceLists(HisMaterialTypeView1SDOFilter filter)
        {
            ApiResultObject<List<HisMaterialTypeView1SDO>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HisMaterialTypeView1SDO> resultData = null;
                if (valid)
                {
                    resultData = new HisMaterialTypeGet(param).GetPriceLists(filter);
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
        public ApiResultObject<HIS_MATERIAL_TYPE> CreateParent(HIS_MATERIAL_TYPE data)
        {
            ApiResultObject<HIS_MATERIAL_TYPE> result = new ApiResultObject<HIS_MATERIAL_TYPE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MATERIAL_TYPE resultData = null;
                if (valid && new HisMaterialTypeCreate(param).CreateParent(data))
                {
                    resultData = data;
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

        [Logger]
        public ApiResultObject<List<HisMaterialTypeInStockSDO>> GetInStockMaterialTypeWithImpStock(HisMatyStockWithImpStockViewFilter filter)
        {
            ApiResultObject<List<HisMaterialTypeInStockSDO>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HisMaterialTypeInStockSDO> resultData = null;
                if (valid)
                {
                    resultData = new HisMaterialTypeGet(param).GetInStockMaterialTypeWithImpStock(filter);
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
        public ApiResultObject<List<HisMaterialTypeInStockSDO>> GetInStockMaterialTypeWithBaseInfo(HisMatyStockWithBaseInfoViewFilter filter)
        {
            ApiResultObject<List<HisMaterialTypeInStockSDO>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HisMaterialTypeInStockSDO> resultData = null;
                if (valid)
                {
                    resultData = new HisMaterialTypeGet(param).GetInStockMaterialTypeWithBaseInfo(filter);
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
        public ApiResultObject<MaterialTypeInHospitalSDO> GetInHospitalMaterialType(HisMaterialTypeHospitalViewFilter filter)
        {
            ApiResultObject<MaterialTypeInHospitalSDO> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                MaterialTypeInHospitalSDO resultData = null;
                if (valid)
                {
                    resultData = new HisMaterialTypeGet(param).GetInHospitalMaterialType(filter);
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
        public ApiResultObject<HIS_MATERIAL_TYPE> UpdateMap(HisMaterialTypeUpdateMapSDO data)
        {
            ApiResultObject<HIS_MATERIAL_TYPE> result = new ApiResultObject<HIS_MATERIAL_TYPE>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MATERIAL_TYPE resultData = null;
                if (valid)
                {
                        new HisMaterialTypeUpdateMap(param).Run(data, ref resultData);
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
