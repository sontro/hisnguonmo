using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpMest.Common.Get;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.AggrExam.Unapprove
{
    class ChildrenProcessor : BusinessBase
    {
        internal ChildrenProcessor()
            : base()
        {
        }

        internal ChildrenProcessor(CommonParam paramCreate)
            : base(paramCreate)
        {
        }

        /// <summary>
        /// Cap nhat trang thai cua cac don noi tru va service_req tuong ung
        /// </summary>
        /// <param name="expMest"></param>
        internal bool Run(long parentId, ref List<string> sqls)
        {
            try
            {
                //Cap nhat trang thai cua service-req tuong ung voi cac don phong kham
                string sqlServiceReq = "UPDATE HIS_SERVICE_REQ SR "
                                        + " SET SR.EXECUTE_LOGINNAME = NULL, SR.EXECUTE_USERNAME = NULL, SR.START_TIME = NULL, SR.SERVICE_REQ_STT_ID = {0} "
                                        + " WHERE EXISTS (SELECT 1 FROM HIS_EXP_MEST EXP "
                                        + "              WHERE EXP.AGGR_EXP_MEST_ID = {1} "
                                        + "                AND EXP.EXP_MEST_TYPE_ID = {2} "
                                        + "                AND EXP.SERVICE_REQ_ID = SR.ID)";

                sqls.Add(string.Format(sqlServiceReq, IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__CXL, parentId, IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DPK));

                //Cap nhat trang thai cua cac don phong kham
                string sqlExpMest = "UPDATE HIS_EXP_MEST SET EXP_MEST_STT_ID = {0} "
                                    + " WHERE AGGR_EXP_MEST_ID = {1} AND EXP_MEST_TYPE_ID = {2} ";

                sqls.Add(string.Format(sqlExpMest, IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__REQUEST, parentId, IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DPK));
                
                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
        }
    }
}
