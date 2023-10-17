using Inventec.Core;
using MOS.MANAGER.Base;
using MOS.MANAGER.CodeGenerator.HisMediRecord;
using MOS.MANAGER.CodeGenerator.HisServiceReq;
using MOS.MANAGER.CodeGenerator.HisTreatment;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.CodeGenerator
{
    public partial class CodeGenerationManager : BusinessBase
    {
        public CodeGenerationManager()
            : base()
        {

        }

        public CodeGenerationManager(CommonParam param)
            : base(param)
        {

        }

        [Logger]
        public ApiResultObject<string> InCodeGetNext(string seedCode)
        {
            ApiResultObject<string> result = null;
            try
            {
                result = this.PackSuccess(InCodeGenerator.GetNext(seedCode));
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
        public ApiResultObject<bool> InCodeFinishUpdateDB(string inCode)
        {
            ApiResultObject<bool> result = null;
            try
            {
                result = this.PackSingleResult(InCodeGenerator.FinishUpdateDB(inCode));
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
        public ApiResultObject<string> ExtraEndCodeGetNext(string seedCode)
        {
            ApiResultObject<string> result = null;
            try
            {
                result = this.PackSuccess(ExtraEndCodeGenerator.GetNext(seedCode));
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
        public ApiResultObject<bool> ExtraEndCodeFinishUpdateDB(string inCode)
        {
            ApiResultObject<bool> result = null;
            try
            {
                result = this.PackSingleResult(ExtraEndCodeGenerator.FinishUpdateDB(inCode));
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
        public ApiResultObject<string> StoreCodeGetNextOption1(long seedTime)
        {
            ApiResultObject<string> result = null;
            try
            {
                result = this.PackSuccess(StoreCodeGenerator.GetNextOption1(seedTime));
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
        public ApiResultObject<string> StoreCodeGetNextOption2(StoreCodeGenerateSDO data)
        {
            ApiResultObject<string> result = null;
            try
            {
                result = this.PackSuccess(StoreCodeGenerator.GetNextOption2(data.SeedCode, data.SeedTime));
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
        public ApiResultObject<string> StoreCodeGetNextOption34(StoreCodeGenerateSDO data)
        {
            ApiResultObject<string> result = null;
            try
            {
                result = this.PackSuccess(StoreCodeGenerator.GetNextOption3_4(data.SeedCode, data.SeedTime));
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
        public ApiResultObject<bool> StoreCodeFinishUpdateDB(List<string> storeCodes)
        {
            ApiResultObject<bool> result = null;
            try
            {
                result = this.PackSingleResult(StoreCodeGenerator.FinishUpdateDB(storeCodes));
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
        public ApiResultObject<string> MediRecordStoreCodeGetNextOption1(long seedTime)
        {
            ApiResultObject<string> result = null;
            try
            {
                result = this.PackSuccess(MediRecordStoreCodeGenerator.GetNextOption1(seedTime));
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
        public ApiResultObject<string> MediRecordStoreCodeGetNextOption2(MediRecordStoreCodeGenerateSDO data)
        {
            ApiResultObject<string> result = null;
            try
            {
                result = this.PackSuccess(MediRecordStoreCodeGenerator.GetNextOption2(data.SeedCode, data.SeedTime));
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
        public ApiResultObject<string> MediRecordStoreCodeGetNextOption3(MediRecordStoreCodeGenerateSDO data)
        {
            ApiResultObject<string> result = null;
            try
            {
                result = this.PackSuccess(MediRecordStoreCodeGenerator.GetNextOption3(data.SeedCode, data.SeedTime));
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
        public ApiResultObject<string> MediRecordStoreCodeGetNextOption4(MediRecordStoreCodeGenerateSDO data)
        {
            ApiResultObject<string> result = null;
            try
            {
                result = this.PackSuccess(MediRecordStoreCodeGenerator.GetNextOption4(data.DataStoreId, data.SeedTime));
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
        public ApiResultObject<string> MediRecordStoreCodeGetNextOption5(MediRecordStoreCodeGenerateSDO data)
        {
            ApiResultObject<string> result = null;
            try
            {
                result = this.PackSuccess(MediRecordStoreCodeGenerator.GetNextOption5(data.DataStoreId, data.SeedTime));
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
        public ApiResultObject<bool> MediRecordStoreCodeFinishUpdateDB(List<string> storeCodes)
        {
            ApiResultObject<bool> result = null;
            try
            {
                result = this.PackSingleResult(MediRecordStoreCodeGenerator.FinishUpdateDB(storeCodes));
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
        public ApiResultObject<string> BarcodeGetNext(long intructionTime)
        {
            ApiResultObject<string> result = null;
            try
            {
                result = this.PackSuccess(BarcodeGenerator.GetNext(intructionTime));
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
        public ApiResultObject<bool> BarcodeFinishUpdateDB(List<string> barcodes)
        {
            ApiResultObject<bool> result = null;
            try
            {
                result = this.PackSingleResult(BarcodeGenerator.FinishUpdateDB(barcodes));
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
