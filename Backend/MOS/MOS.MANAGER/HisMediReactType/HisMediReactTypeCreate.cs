using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMediReactType
{
    class HisMediReactTypeCreate : BusinessBase
    {
        internal HisMediReactTypeCreate()
            : base()
        {

        }

        internal HisMediReactTypeCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_MEDI_REACT_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMediReactTypeCheck checker = new HisMediReactTypeCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.MEDI_REACT_TYPE_CODE, null);
                if (valid)
                {
                    result = DAOWorker.HisMediReactTypeDAO.Create(data);
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

        internal bool CreateList(List<HIS_MEDI_REACT_TYPE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMediReactTypeCheck checker = new HisMediReactTypeCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.MEDI_REACT_TYPE_CODE, null);
                }
                if (valid)
                {
                    result = DAOWorker.HisMediReactTypeDAO.CreateList(listData);
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
