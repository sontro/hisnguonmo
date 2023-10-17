using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisSereServ.Truncate
{
    class HisSereServExamTruncateCheck : BusinessBase
    {
        internal HisSereServExamTruncateCheck()
            : base()
        {

        }

        internal HisSereServExamTruncateCheck(CommonParam param)
            : base(param)
        {

        }


        internal bool IsExam(HIS_SERE_SERV raw)
        {
            bool valid = true;
            try
            {
                if (raw.TDL_SERVICE_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__KH)
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisSereServ_KhongPhaiDichVuKham);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool IsCreatorOrAdmin(HIS_SERVICE_REQ req)
        {
            bool valid = true;
            try
            {
                string loginname = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                if (req.CREATOR == loginname)
                {
                    return true;
                }

                HIS_EMPLOYEE emp = HisEmployeeCFG.DATA.FirstOrDefault(o => o.LOGINNAME == loginname);
                if (emp != null && emp.IS_ADMIN == Constant.IS_TRUE)
                {
                    return true;
                }

                MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.Common_BanKhongCoQuyenThucHienChucNangNay);
                return false;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

    }
}
