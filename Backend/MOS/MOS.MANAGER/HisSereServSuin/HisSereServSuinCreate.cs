using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSereServSuin
{
    partial class HisSereServSuinCreate : BusinessBase
    {
        internal HisSereServSuinCreate()
            : base()
        {

        }

        internal HisSereServSuinCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_SERE_SERV_SUIN data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisSereServSuinCheck checker = new HisSereServSuinCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
                    result = DAOWorker.HisSereServSuinDAO.Create(data);
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

        internal bool CreateList(List<HIS_SERE_SERV_SUIN> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisSereServSuinCheck checker = new HisSereServSuinCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    result = DAOWorker.HisSereServSuinDAO.CreateList(listData);
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
