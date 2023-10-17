using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSuimIndexUnit
{
    class HisSuimIndexUnitCreate : BusinessBase
    {
        internal HisSuimIndexUnitCreate()
            : base()
        {

        }

        internal HisSuimIndexUnitCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_SUIM_INDEX_UNIT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisSuimIndexUnitCheck checker = new HisSuimIndexUnitCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.SUIM_INDEX_UNIT_CODE, null);
                if (valid)
                {
                    result = DAOWorker.HisSuimIndexUnitDAO.Create(data);
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

        internal bool CreateList(List<HIS_SUIM_INDEX_UNIT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisSuimIndexUnitCheck checker = new HisSuimIndexUnitCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.SUIM_INDEX_UNIT_CODE, null);
                }
                if (valid)
                {
                    result = DAOWorker.HisSuimIndexUnitDAO.CreateList(listData);
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
