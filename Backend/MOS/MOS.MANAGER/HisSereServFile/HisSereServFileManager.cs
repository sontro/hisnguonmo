using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSereServFile
{
    public class HisSereServFileManager : BusinessBase
    {
        public HisSereServFileManager()
            : base()
        {

        }
        
        public HisSereServFileManager(CommonParam param)
            : base(param)
        {

        }

        [Logger]
        public ApiResultObject<List<HIS_SERE_SERV_FILE>> Get(HisSereServFileFilterQuery filter)
        {
            ApiResultObject<List<HIS_SERE_SERV_FILE>> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_SERE_SERV_FILE> resultData = null;
                if (valid)
                {
                    resultData = new HisSereServFileGet(param).Get(filter);
                }
                result = this.PackSuccess(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<HIS_SERE_SERV_FILE> GetById(long id)
        {
            ApiResultObject<HIS_SERE_SERV_FILE> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsGreaterThanZero(id);
                HIS_SERE_SERV_FILE resultData = null;
                if (valid)
                {
                    HisSereServFileFilterQuery filter = new HisSereServFileFilterQuery();
                    resultData = new HisSereServFileGet(param).GetById(id, filter);
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<HIS_SERE_SERV_FILE> Create(HIS_SERE_SERV_FILE data)
        {
            ApiResultObject<HIS_SERE_SERV_FILE> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SERE_SERV_FILE resultData = null;
                if (valid && new HisSereServFileCreate(param).Create(data))
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
        public ApiResultObject<HIS_SERE_SERV_FILE> Update(HIS_SERE_SERV_FILE data)
        {
            ApiResultObject<HIS_SERE_SERV_FILE> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SERE_SERV_FILE resultData = null;
                if (valid && new HisSereServFileUpdate(param).Update(data))
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
        public ApiResultObject<HIS_SERE_SERV_FILE> ChangeLock(HIS_SERE_SERV_FILE data)
        {
            ApiResultObject<HIS_SERE_SERV_FILE> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsGreaterThanZero(data.ID);
                HIS_SERE_SERV_FILE resultData = null;
                if (valid && new HisSereServFileLock(param).ChangeLock(data))
                {
                    resultData = data;
                }
                this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }
            
            return result;
        }

        [Logger]
        public ApiResultObject<bool> Delete(HIS_SERE_SERV_FILE data)
        {
            ApiResultObject<bool> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                bool resultData = false;
                if (valid)
                {
                    resultData = new HisSereServFileTruncate(param).Truncate(data);
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

        public FileHolder GetFile(long id)
        {
            FileHolder result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsGreaterThanZero(id);
                if (valid)
                {
                    result = new HisSereServFileGet(param).GetFile(id);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }

            return result;
        }
    }
}
