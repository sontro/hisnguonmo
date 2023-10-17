using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisRestRetrType;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisSaroExro
{
    partial class HisSaroExroTruncate : BusinessBase
    {
        internal HisSaroExroTruncate()
            : base()
        {

        }

        internal HisSaroExroTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(HIS_SARO_EXRO data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisSaroExroCheck checker = new HisSaroExroCheck(param);
                valid = valid && IsNotNull(data);
                HIS_SARO_EXRO raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisSaroExroDAO.Truncate(data);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        internal bool TruncateList(List<long> ids)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(ids);
                HisSaroExroCheck checker = new HisSaroExroCheck(param);
                List<HIS_SARO_EXRO> listRaw = new List<HIS_SARO_EXRO>();
                valid = valid && checker.VerifyIds(ids, listRaw);
                if (valid)
                {
                    result = this.TruncateList(listRaw);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        internal bool TruncateList(List<HIS_SARO_EXRO> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisSaroExroCheck checker = new HisSaroExroCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                    valid = valid && checker.IsUnLock(data.ID);
                }
                if (valid)
                {
                    result = DAOWorker.HisSaroExroDAO.TruncateList(listData);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        internal bool TruncateBySampleRoomId(long id)
        {
            bool result = true;
            try
            {
                List<HIS_SARO_EXRO> hisSaroExros = new HisSaroExroGet().GetBySampleRoomId(id);
                if (IsNotNullOrEmpty(hisSaroExros))
                {
                    result = this.TruncateList(hisSaroExros);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        internal bool TruncateByExecuteRoomId(long id)
        {
            bool result = true;
            try
            {
                List<HIS_SARO_EXRO> hisSaroExros = new HisSaroExroGet().GetByExecuteRoomId(id);
                if (IsNotNullOrEmpty(hisSaroExros))
                {
                    result = this.TruncateList(hisSaroExros);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }
    }
}
