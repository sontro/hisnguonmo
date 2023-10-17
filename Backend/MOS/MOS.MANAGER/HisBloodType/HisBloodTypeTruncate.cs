using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisRestRetrType;
using MOS.MANAGER.HisService;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisBloodType
{
    partial class HisBloodTypeTruncate : BusinessBase
    {
        internal HisBloodTypeTruncate()
            : base()
        {

        }

        internal HisBloodTypeTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(long id)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisBloodTypeCheck checker = new HisBloodTypeCheck(param);
                HisServiceCheck serviceChecker = new HisServiceCheck(param);
                HIS_BLOOD_TYPE raw = null;
                valid = valid && checker.VerifyId(id, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.CheckConstraint(id);
                valid = valid && serviceChecker.CheckConstraint(raw.SERVICE_ID);
                if (valid)
                {
                    long? parentId = raw.PARENT_ID;
                    if (DAOWorker.HisBloodTypeDAO.Truncate(raw))
                    {
                        result = new HisServiceTruncate(param).Truncate(raw.SERVICE_ID);
                        if (parentId.HasValue)
                        {
                            HIS_BLOOD_TYPE oldParent = new HisBloodTypeGet().GetById(parentId.Value);
                            List<HIS_BLOOD_TYPE> children = new HisBloodTypeGet().GetByParentId(parentId.Value);
                            if (!IsNotNullOrEmpty(children))
                            {
                                oldParent.IS_LEAF = MOS.UTILITY.Constant.IS_TRUE;
                                if (!new HisBloodTypeUpdate(param).Update(oldParent))
                                {
                                    LogSystem.Warn("Cap nhat IS_LEAF du lieu oldParent that bai. Kiem tra lai du lieu Id: " + oldParent.ID);
                                }
                            }
                        }
                    }
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

        internal bool TruncateList(List<HIS_BLOOD_TYPE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisBloodTypeCheck checker = new HisBloodTypeCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                    valid = valid && checker.IsUnLock(data.ID);
                    valid = valid && checker.CheckConstraint(data.ID);
                }
                if (valid)
                {
                    result = DAOWorker.HisBloodTypeDAO.TruncateList(listData);
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
