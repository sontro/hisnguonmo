using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSuimIndex
{
    class HisSuimIndexCreate : BusinessBase
    {
        internal HisSuimIndexCreate()
            : base()
        {

        }

        internal HisSuimIndexCreate(Inventec.Core.CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_SUIM_INDEX data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisSuimIndexCheck checker = new HisSuimIndexCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.SUIM_INDEX_CODE, null);
                if (valid)
                {
                    result = DAOWorker.HisSuimIndexDAO.Create(data);
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

        internal bool CreateList(List<HIS_SUIM_INDEX> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisSuimIndexCheck checker = new HisSuimIndexCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.SUIM_INDEX_CODE, null);
                }
                if (valid)
                {
                    result = DAOWorker.HisSuimIndexDAO.CreateList(listData);
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
