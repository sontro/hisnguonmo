using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMedicineType
{
    public partial class HisMedicineTypeManager : BusinessBase
    {
        public HisMedicineTypeManager()
            : base()
        {

        }

        public HisMedicineTypeManager(CommonParam param)
            : base(param)
        {

        }

        [Logger]
        public ApiResultObject<List<HIS_MEDICINE_TYPE>> Get(HisMedicineTypeFilterQuery filter)
        {
            ApiResultObject<List<HIS_MEDICINE_TYPE>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_MEDICINE_TYPE> resultData = null;
                if (valid)
                {
                    resultData = new HisMedicineTypeGet(param).Get(filter);
                    if (IsNotNullOrEmpty(resultData))
                    {
                        //xu ly de ko bi reference khi convert sang json truyen xuong client
                        //==> dan den loi overload
                        resultData.ForEach(o =>
                        {
                            o.HIS_MEDICINE_TYPE1 = null;
                            o.HIS_MEDICINE_TYPE2 = null;
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
        public ApiResultObject<List<V_HIS_MEDICINE_TYPE>> GetView(HisMedicineTypeViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_MEDICINE_TYPE>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_MEDICINE_TYPE> resultData = null;
                if (valid)
                {
                    resultData = new HisMedicineTypeGet(param).GetView(filter);
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
        public ApiResultObject<List<HisMedicineTypeInStockSDO>> GetInStockMedicineType(HisMedicineTypeStockViewFilter filter)
        {
            ApiResultObject<List<HisMedicineTypeInStockSDO>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HisMedicineTypeInStockSDO> resultData = null;
                if (valid)
                {
                    resultData = new HisMedicineTypeGet(param).GetInStockMedicineType(filter);
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
        public ApiResultObject<HIS_MEDICINE_TYPE> Create(HIS_MEDICINE_TYPE data)
        {
            ApiResultObject<HIS_MEDICINE_TYPE> result = new ApiResultObject<HIS_MEDICINE_TYPE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MEDICINE_TYPE resultData = null;
                if (valid && new HisMedicineTypeCreate(param).Create(data))
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
        public ApiResultObject<HIS_MEDICINE_TYPE> Update(HIS_MEDICINE_TYPE data)
        {
            ApiResultObject<HIS_MEDICINE_TYPE> result = new ApiResultObject<HIS_MEDICINE_TYPE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MEDICINE_TYPE resultData = null;
                if (valid && new HisMedicineTypeUpdate(param).Update(data))
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
        public ApiResultObject<HIS_MEDICINE_TYPE> Lock(HIS_MEDICINE_TYPE data)
        {
            ApiResultObject<HIS_MEDICINE_TYPE> result = new ApiResultObject<HIS_MEDICINE_TYPE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MEDICINE_TYPE resultData = null;
                if (valid && new HisMedicineTypeLock(param).Lock(data))
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
        public ApiResultObject<HIS_MEDICINE_TYPE> Unlock(HIS_MEDICINE_TYPE data)
        {
            ApiResultObject<HIS_MEDICINE_TYPE> result = new ApiResultObject<HIS_MEDICINE_TYPE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MEDICINE_TYPE resultData = null;
                if (valid && new HisMedicineTypeLock(param).Unlock(data))
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
        public ApiResultObject<bool> Delete(HIS_MEDICINE_TYPE data)
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
                    resultData = new HisMedicineTypeTruncate(param).Truncate(data);
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
        public ApiResultObject<HIS_MEDICINE_TYPE> UpdateSdo(HisMedicineTypeSDO data)
        {
            ApiResultObject<HIS_MEDICINE_TYPE> result = new ApiResultObject<HIS_MEDICINE_TYPE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                valid = valid && IsNotNull(data.HisMedicineType);
                HIS_MEDICINE_TYPE resultData = null;
                if (valid && new HisMedicineTypeUpdate(param).UpdateSdo(data))
                {
                    resultData = data.HisMedicineType;
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
        public ApiResultObject<List<HIS_MEDICINE_TYPE>> CreateList(List<HIS_MEDICINE_TYPE> listData)
        {
            ApiResultObject<List<HIS_MEDICINE_TYPE>> result = new ApiResultObject<List<HIS_MEDICINE_TYPE>>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNullOrEmpty(listData);
                List<HIS_MEDICINE_TYPE> resultData = null;
                if (valid && new HisMedicineTypeCreateSql(param).Run(listData))
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
        public ApiResultObject<List<HisMedicineTypeView1SDO>> GetPriceLists(HisMedicineTypeView1SDOFilter filter)
        {
            ApiResultObject<List<HisMedicineTypeView1SDO>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HisMedicineTypeView1SDO> resultData = null;
                if (valid)
                {
                    resultData = new HisMedicineTypeGet(param).GetPriceLists(filter);
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
        public ApiResultObject<HIS_MEDICINE_TYPE> CreateParent(HIS_MEDICINE_TYPE data)
        {
            ApiResultObject<HIS_MEDICINE_TYPE> result = new ApiResultObject<HIS_MEDICINE_TYPE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MEDICINE_TYPE resultData = null;
                if (valid && new HisMedicineTypeCreate(param).CreateParent(data))
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
        public ApiResultObject<List<HisMedicineTypeInStockSDO>> GetInStockMedicineTypeWithImpStock(HisMetyStockWithImpStockViewFilter filter)
        {
            ApiResultObject<List<HisMedicineTypeInStockSDO>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HisMedicineTypeInStockSDO> resultData = null;
                if (valid)
                {
                    resultData = new HisMedicineTypeGet(param).GetInStockMedicineTypeWithImpStock(filter);
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
        public ApiResultObject<List<HisMedicineTypeInStockSDO>> GetInStockMedicineTypeWithBaseInfo(HisMetyStockWithBaseInfoViewFilter filter)
        {
            ApiResultObject<List<HisMedicineTypeInStockSDO>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HisMedicineTypeInStockSDO> resultData = null;
                if (valid)
                {
                    resultData = new HisMedicineTypeGet(param).GetInStockMedicineTypeWithBaseInfo(filter);
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
        public ApiResultObject<MedicineTypeInHospitalSDO> GetInHospitalMedicineType(HisMedicineTypeHospitalViewFilter filter)
        {
            ApiResultObject<MedicineTypeInHospitalSDO> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                MedicineTypeInHospitalSDO resultData = null;
                if (valid)
                {
                    resultData = new HisMedicineTypeGet(param).GetInHospitalMedicineType(filter);
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
    }
}
