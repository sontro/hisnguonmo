using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSupplier
{
    class HisSupplierCreate : BusinessBase
    {
        internal HisSupplierCreate()
            : base()
        {

        }

        internal HisSupplierCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_SUPPLIER data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisSupplierCheck checker = new HisSupplierCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.SUPPLIER_CODE, null);
                if (valid)
                {
                    result = DAOWorker.HisSupplierDAO.Create(data);
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

        internal bool CreateList(List<HIS_SUPPLIER> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisSupplierCheck checker = new HisSupplierCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.SUPPLIER_CODE, null);
                }
                if (valid)
                {
                    result = DAOWorker.HisSupplierDAO.CreateList(listData);
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
