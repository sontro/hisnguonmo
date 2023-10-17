using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisMediReactType
{
    class HisMediReactTypeUpdate : BusinessBase
    {
        internal HisMediReactTypeUpdate()
            : base()
        {

        }

        internal HisMediReactTypeUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_MEDI_REACT_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMediReactTypeCheck checker = new HisMediReactTypeCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && IsGreaterThanZero(data.ID);
                HIS_MEDI_REACT_TYPE raw = null;
                valid = valid && checker.IsGreaterThanZero(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.MEDI_REACT_TYPE_CODE, data.ID);
                if (valid)
                {
                    result = DAOWorker.HisMediReactTypeDAO.Update(data);
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

        internal bool UpdateList(List<HIS_MEDI_REACT_TYPE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMediReactTypeCheck checker = new HisMediReactTypeCheck(param);
                List<HIS_MEDI_REACT_TYPE> listRaw = new List<HIS_MEDI_REACT_TYPE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.MEDI_REACT_TYPE_CODE, data.ID);
                }
                if (valid)
                {
                    result = DAOWorker.HisMediReactTypeDAO.UpdateList(listData);
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
