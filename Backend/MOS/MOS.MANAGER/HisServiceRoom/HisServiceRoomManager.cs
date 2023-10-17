using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisServiceRoom
{
	public partial class HisServiceRoomManager : BusinessBase
	{
		public HisServiceRoomManager()
			: base()
		{

		}
		
		public HisServiceRoomManager(CommonParam param)
			: base(param)
		{

		}
		
		[Logger]
		public ApiResultObject<List<HIS_SERVICE_ROOM>> Get(HisServiceRoomFilterQuery filter)
		{
			ApiResultObject<List<HIS_SERVICE_ROOM>> result = null;
			try
			{
				bool valid = true;
				valid = valid && IsNotNull(param);
				valid = valid && IsNotNull(filter);
				List<HIS_SERVICE_ROOM> resultData = null;
				if (valid)
				{
					resultData = new HisServiceRoomGet(param).Get(filter);
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
		public ApiResultObject<List<V_HIS_SERVICE_ROOM>> GetView(HisServiceRoomViewFilterQuery filter)
		{
			ApiResultObject<List<V_HIS_SERVICE_ROOM>> result = null;
			try
			{
				bool valid = true;
				valid = valid && IsNotNull(param);
				valid = valid && IsNotNull(filter);
				List<V_HIS_SERVICE_ROOM> resultData = null;
				if (valid)
				{
					resultData = new HisServiceRoomGet(param).GetView(filter);
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
		public ApiResultObject<HIS_SERVICE_ROOM> Create(HIS_SERVICE_ROOM data)
		{
			ApiResultObject<HIS_SERVICE_ROOM> result = new ApiResultObject<HIS_SERVICE_ROOM>(null);
			try
			{
				bool valid = true;
				valid = valid && IsNotNull(param);
				valid = valid && IsNotNull(data);
				HIS_SERVICE_ROOM resultData = null;
				if (valid && new HisServiceRoomCreate(param).Create(data))
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
		public ApiResultObject<List<HIS_SERVICE_ROOM>> CreateList(List<HIS_SERVICE_ROOM> data)
		{
			ApiResultObject<List<HIS_SERVICE_ROOM>> result = new ApiResultObject<List<HIS_SERVICE_ROOM>>(null);

			try
			{
				bool valid = true;
				valid = valid && IsNotNull(param);
				valid = valid && IsNotNull(data);
				List<HIS_SERVICE_ROOM> resultData = null;
				if (valid && new HisServiceRoomCreate(param).CreateList(data))
				{
					resultData = data;
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
		public ApiResultObject<HIS_SERVICE_ROOM> Update(HIS_SERVICE_ROOM data)
		{
			ApiResultObject<HIS_SERVICE_ROOM> result = new ApiResultObject<HIS_SERVICE_ROOM>(null);
			try
			{
				bool valid = true;
				valid = valid && IsNotNull(param);
				valid = valid && IsNotNull(data);
				HIS_SERVICE_ROOM resultData = null;
				if (valid && new HisServiceRoomUpdate(param).Update(data))
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
        public ApiResultObject<List<HIS_SERVICE_ROOM>> UpdateList(List<HIS_SERVICE_ROOM> data)
        {
            ApiResultObject<List<HIS_SERVICE_ROOM>> result = new ApiResultObject<List<HIS_SERVICE_ROOM>>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<HIS_SERVICE_ROOM> resultData = null;
                if (valid && new HisServiceRoomUpdate(param).UpdateList(data))
                {
                    resultData = data;
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
		public ApiResultObject<HIS_SERVICE_ROOM> ChangeLock(HIS_SERVICE_ROOM data)
		{
			ApiResultObject<HIS_SERVICE_ROOM> result = new ApiResultObject<HIS_SERVICE_ROOM>(null);
			try
			{
				bool valid = true;
				valid = valid && IsNotNull(param);
				valid = valid && IsNotNull(data);
				HIS_SERVICE_ROOM resultData = null;
				if (valid && new HisServiceRoomLock(param).ChangeLock(data))
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
		public ApiResultObject<bool> Delete(HIS_SERVICE_ROOM data)
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
					resultData = new HisServiceRoomTruncate(param).Truncate(data);
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
					resultData = new HisServiceRoomTruncate(param).TruncateList(ids);
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
		public ApiResultObject<List<HIS_SERVICE_ROOM>> CopyByService(HisServiceRoomCopyByServiceSDO data)
		{
			ApiResultObject<List<HIS_SERVICE_ROOM>> result = new ApiResultObject<List<HIS_SERVICE_ROOM>>(null);

			try
			{
				bool valid = true;
				valid = valid && IsNotNull(param);
				valid = valid && IsNotNull(data);
				List<HIS_SERVICE_ROOM> resultData = null;
				if (valid)
				{
					new HisServiceRoomCopyByService(param).Run(data, ref resultData);
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
		public ApiResultObject<List<HIS_SERVICE_ROOM>> CopyByRoom(HisServiceRoomCopyByRoomSDO data)
		{
			ApiResultObject<List<HIS_SERVICE_ROOM>> result = new ApiResultObject<List<HIS_SERVICE_ROOM>>(null);

			try
			{
				bool valid = true;
				valid = valid && IsNotNull(param);
				valid = valid && IsNotNull(data);
				List<HIS_SERVICE_ROOM> resultData = null;
				if (valid)
				{
					new HisServiceRoomCopyByRoom(param).Run(data, ref resultData);
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
