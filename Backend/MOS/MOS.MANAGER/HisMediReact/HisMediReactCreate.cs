using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.Token;
using MOS.SDO;
using System;

namespace MOS.MANAGER.HisMediReact
{
    class HisMediReactCreate : BusinessBase
    {
        internal HisMediReactCreate()
            : base()
        {

        }

        internal HisMediReactCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_MEDI_REACT data)
        {
            bool result = false;
            try
            {
                result = this.CreateInfo(data);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private bool CreateInfo(HIS_MEDI_REACT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMediReactCheck checker = new HisMediReactCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
                    result = DAOWorker.HisMediReactDAO.Create(data);
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
