using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using System;

namespace MOS.MANAGER.HisMediReact
{
    class HisMediReactUpdate : BusinessBase
    {
        internal HisMediReactUpdate()
            : base()
        {

        }

        internal HisMediReactUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_MEDI_REACT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMediReactCheck checker = new HisMediReactCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && IsGreaterThanZero(data.ID);
                if (valid)
                {
                    result = DAOWorker.HisMediReactDAO.Update(data);
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

        /// <summary>
        /// Thuc hien
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool Execute(HIS_MEDI_REACT data, ref HIS_MEDI_REACT resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMediReactCheck checker = new HisMediReactCheck(param);
                HIS_MEDI_REACT raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    if (valid)
                    {
                        if (this.Update(raw))
                        {
                            resultData = raw;
                            result = true;
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

        /// <summary>
        /// Thuc hien
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool UnExecute(HIS_MEDI_REACT data, ref HIS_MEDI_REACT resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMediReactCheck checker = new HisMediReactCheck(param);
                HIS_MEDI_REACT raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    if (valid)
                    {
                        raw.EXECUTE_LOGINNAME = null;
                        raw.EXECUTE_USERNAME = null;
                        raw.EXECUTE_TIME = null;
                        if (this.Update(raw))
                        {
                            resultData = raw;
                            result = true;
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

        /// <summary>
        /// Kiem tra
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool Check(HIS_MEDI_REACT data, ref HIS_MEDI_REACT resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMediReactCheck checker = new HisMediReactCheck(param);
                HIS_MEDI_REACT raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && IsNotNullOrEmpty(data.CHECK_RESULT);
                if (valid)
                {
                    if (valid)
                    {
                        raw.CHECK_LOGINNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                        raw.CHECK_USERNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
                        raw.CHECK_RESULT = data.CHECK_RESULT;
                        raw.CHECK_TIME = data.CHECK_TIME;
                        if (this.Update(raw))
                        {
                            resultData = raw;
                            result = true;
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

        /// <summary>
        /// Kiem tra
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool UnCheck(HIS_MEDI_REACT data, ref HIS_MEDI_REACT resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMediReactCheck checker = new HisMediReactCheck(param);
                HIS_MEDI_REACT raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    if (valid)
                    {
                        raw.CHECK_LOGINNAME = null;
                        raw.CHECK_USERNAME = null;
                        raw.CHECK_RESULT = null;
                        raw.CHECK_TIME = null;
                        if (this.Update(raw))
                        {
                            resultData = raw;
                            result = true;
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
    }
}
