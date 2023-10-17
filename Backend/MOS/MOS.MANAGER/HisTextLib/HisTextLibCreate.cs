using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTextLib
{
    class HisTextLibCreate : BusinessBase
    {
        internal HisTextLibCreate()
            : base()
        {

        }

        internal HisTextLibCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_TEXT_LIB data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisTextLibCheck checker = new HisTextLibCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.CheckExists(data, Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName());
                if (valid)
                {
                    result = DAOWorker.HisTextLibDAO.Create(data);
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

        internal bool CreateList(List<HIS_TEXT_LIB> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisTextLibCheck checker = new HisTextLibCheck(param);
                string loginname = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.CheckExists(data, loginname);
                }
                if (valid)
                {
                    result = DAOWorker.HisTextLibDAO.CreateList(listData);
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
