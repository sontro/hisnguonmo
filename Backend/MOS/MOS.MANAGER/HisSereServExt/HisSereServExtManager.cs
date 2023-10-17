using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisSereServExt.GetLinkResult;
using System;
using System.Collections.Generic;
using MOS.SDO;
using MOS.MANAGER.HisSereServExt.Update;

namespace MOS.MANAGER.HisSereServExt
{
	public partial class HisSereServExtManager : BusinessBase
	{
		public HisSereServExtManager()
			: base()
		{

		}
		
		public HisSereServExtManager(CommonParam param)
			: base(param)
		{

		}
		
		[Logger]
		public ApiResultObject<List<HIS_SERE_SERV_EXT>> Get(HisSereServExtFilterQuery filter)
		{
			ApiResultObject<List<HIS_SERE_SERV_EXT>> result = null;
			try
			{
				bool valid = true;
				valid = valid && IsNotNull(param);
				valid = valid && IsNotNull(filter);
				List<HIS_SERE_SERV_EXT> resultData = null;
				if (valid)
				{
					resultData = new HisSereServExtGet(param).Get(filter);
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
		public ApiResultObject<HIS_SERE_SERV_EXT> Create(HIS_SERE_SERV_EXT data)
		{
			ApiResultObject<HIS_SERE_SERV_EXT> result = new ApiResultObject<HIS_SERE_SERV_EXT>(null);
			try
			{
				bool valid = true;
				valid = valid && IsNotNull(param);
				valid = valid && IsNotNull(data);
				HIS_SERE_SERV_EXT resultData = null;
				if (valid && new HisSereServExtCreate(param).Create(data))
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

		public ApiResultObject<HisSereServExtWithFileSDO> Create(HisSereServExtSDO data)
		{
			ApiResultObject<HisSereServExtWithFileSDO> result = new ApiResultObject<HisSereServExtWithFileSDO>(null);


			try
			{
				bool valid = true;
				valid = valid && IsNotNull(param);
				valid = valid && IsNotNull(data);
				HisSereServExtWithFileSDO resultData = null;
				if (valid)
				{
					new HisSereServExtCreateFile(param).Create(data, ref resultData);
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

		public ApiResultObject<HisSereServExtWithFileSDO> Update(HisSereServExtSDO data)
		{
			ApiResultObject<HisSereServExtWithFileSDO> result = new ApiResultObject<HisSereServExtWithFileSDO>(null);


			try
			{
				bool valid = true;
				valid = valid && IsNotNull(param);
				valid = valid && IsNotNull(data);
				HisSereServExtWithFileSDO resultData = null;
				if (valid)
				{
                    new HisSereServExtUpdateFile(param).Run(data, ref resultData);
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
		public ApiResultObject<HIS_SERE_SERV_EXT> Update(HIS_SERE_SERV_EXT data)
		{
			ApiResultObject<HIS_SERE_SERV_EXT> result = new ApiResultObject<HIS_SERE_SERV_EXT>(null);
			try
			{
				bool valid = true;
				valid = valid && IsNotNull(param);
				valid = valid && IsNotNull(data);
				HIS_SERE_SERV_EXT resultData = null;
				if (valid && new HisSereServExtUpdate(param).Update(data, true))
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
        public ApiResultObject<bool> UpdateForEmr(UpdateForEmrSDO data)
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
                    resultData = new HisSereServExtUpdateForEmr(param).Run(data);
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
		public ApiResultObject<HIS_SERE_SERV_EXT> SetInstructionNote(HIS_SERE_SERV_EXT data)
		{
			ApiResultObject<HIS_SERE_SERV_EXT> result = new ApiResultObject<HIS_SERE_SERV_EXT>(null);
			try
			{
				bool valid = true;
				valid = valid && IsNotNull(param);
				valid = valid && IsNotNull(data);
				HIS_SERE_SERV_EXT resultData = null;
				if (valid)
				{
					new HisSereServExtSetInstructionNote(param).Run(data, ref resultData);
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
		public ApiResultObject<HIS_SERE_SERV_EXT> UpdateJsonForm(HIS_SERE_SERV_EXT data)
		{
			ApiResultObject<HIS_SERE_SERV_EXT> result = new ApiResultObject<HIS_SERE_SERV_EXT>(null);
			try
			{
				bool valid = true;
				valid = valid && IsNotNull(param);
				valid = valid && IsNotNull(data);
				bool resultData = false;
				if (valid)
				{
					resultData = new HisSereServExtUpdateJsonForm(param).Run(data);
				}
				result = this.PackResult(data, resultData);
			}
			catch (Exception ex)
			{
				Inventec.Common.Logging.LogSystem.Error(ex);
				param.HasException = true;
			}

			return result;
		}

		[Logger]
		public ApiResultObject<HIS_SERE_SERV_EXT> SetIsFee(HisSereServExtIsFeeSDO data)
		{
			ApiResultObject<HIS_SERE_SERV_EXT> result = new ApiResultObject<HIS_SERE_SERV_EXT>(null);
			try
			{
				bool valid = true;
				valid = valid && IsNotNull(param);
				valid = valid && IsNotNull(data);
				HIS_SERE_SERV_EXT resultData = null;
				bool rs = false;
				if (valid)
				{
					rs = new HisSereServExtSetIsFee(param).Run(data, ref resultData);
				}
				result = this.PackResult(resultData, rs);
			}
			catch (Exception ex)
			{
				Inventec.Common.Logging.LogSystem.Error(ex);
				param.HasException = true;
			}

			return result;
		}

		[Logger]
		public ApiResultObject<HIS_SERE_SERV_EXT> SetIsGatherData(HisSereServExtIsGatherDataSDO data)
		{
			ApiResultObject<HIS_SERE_SERV_EXT> result = new ApiResultObject<HIS_SERE_SERV_EXT>(null);
			try
			{
				bool valid = true;
				valid = valid && IsNotNull(param);
				valid = valid && IsNotNull(data);
				HIS_SERE_SERV_EXT resultData = null;
				bool rs = false;
				if (valid)
				{
					rs = new HisSereServExtSetIsGatherData(param).Run(data, ref resultData);
				}
				result = this.PackResult(resultData, rs);
			}
			catch (Exception ex)
			{
				Inventec.Common.Logging.LogSystem.Error(ex);
				param.HasException = true;
			}

			return result;
		}

		[Logger]
		public ApiResultObject<HIS_SERE_SERV_EXT> ChangeLock(long id)
		{
			ApiResultObject<HIS_SERE_SERV_EXT> result = new ApiResultObject<HIS_SERE_SERV_EXT>(null);
			try
			{
				bool valid = true;
				valid = valid && IsNotNull(param);
				HIS_SERE_SERV_EXT resultData = null;
				if (valid)
				{
					new HisSereServExtLock(param).ChangeLock(id, ref resultData);
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
		public ApiResultObject<HIS_SERE_SERV_EXT> Lock(long id)
		{
			ApiResultObject<HIS_SERE_SERV_EXT> result = null;
			
			try
			{
				bool valid = true;
				valid = valid && IsNotNull(param);
				HIS_SERE_SERV_EXT resultData = null;
				if (valid)
				{
					new HisSereServExtLock(param).Lock(id, ref resultData);
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
		public ApiResultObject<HIS_SERE_SERV_EXT> Unlock(long id)
		{
			ApiResultObject<HIS_SERE_SERV_EXT> result = null;
			
			try
			{
				bool valid = true;
				valid = valid && IsNotNull(param);
				HIS_SERE_SERV_EXT resultData = null;
				if (valid)
				{
					new HisSereServExtLock(param).Unlock(id, ref resultData);
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
        public ApiResultObject<HIS_SERE_SERV_EXT> Delete(HisSereServDeleteConfirmNoExcuteSDO data)
		{
            ApiResultObject<HIS_SERE_SERV_EXT> result = new ApiResultObject<HIS_SERE_SERV_EXT>(null);
			try
			{
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SERE_SERV_EXT resultData = null;
                bool rs = false;
                if (valid)
                {
                    rs = new HisSereServExtTruncate(param).Truncate(data, ref resultData);
                }
                result = this.PackResult(resultData, rs);
			}
			catch (Exception ex)
			{
				Inventec.Common.Logging.LogSystem.Error(ex);
				param.HasException = true;
			}
			
			return result;
		}

        [Logger]
        public ApiResultObject<string> GetLinkResult(long ssId)
        {
            ApiResultObject<string> result = new ApiResultObject<string>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                string resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisSereServGetLinkResult(param).Run(ssId, ref resultData);
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
       
	}
}
