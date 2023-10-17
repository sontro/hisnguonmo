using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisExpMest.Common.Update;
using MOS.MANAGER.HisServiceReq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Common.UnexportAndUnapprove
{
    class ExpMestProcessor : BusinessBase
    {
        private HisExpMestUpdate hisExpMestUpdate;
        private HisServiceReqUpdate hisServiceReqUpdate;

        internal ExpMestProcessor()
            : base()
        {
            this.Init();
        }

        internal ExpMestProcessor(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisExpMestUpdate = new HisExpMestUpdate(param);
            this.hisServiceReqUpdate = new HisServiceReqUpdate(param);
        }


        internal bool Run(List<HIS_EXP_MEST> expMests, ref List<HIS_SERVICE_REQ> serviceReqs, ref List<string> sqls)
        {
            try
            {
                if (IsNotNullOrEmpty(expMests))
                {
                    this.UnfinishExpMest(expMests, ref sqls);
                    this.UnfinishServiceReq(expMests, ref serviceReqs);
                }
                return true;
            }
            catch (Exception ex)
            {
                this.Rollback();
                LogSystem.Error(ex);
                return false;
            }
        }


        private void UnfinishExpMest(List<HIS_EXP_MEST> expMests, ref List<string> sqls)
        {
            Mapper.CreateMap<HIS_EXP_MEST, HIS_EXP_MEST>();
            List<HIS_EXP_MEST> beforeUpdates = Mapper.Map<List<HIS_EXP_MEST>>(expMests);

            expMests.ForEach(o =>
            {
                o.IS_HTX = MOS.UTILITY.Constant.IS_TRUE;
                o.IS_EXPORT_EQUAL_APPROVE = null;
                o.IS_EXPORT_EQUAL_REQUEST = null;
                o.LAST_EXP_LOGINNAME = null;
                o.LAST_EXP_TIME = null;
                o.LAST_EXP_USERNAME = null;
            });

            if (!this.hisExpMestUpdate.UpdateList(expMests, beforeUpdates))
            {
                throw new Exception("Rollback du lieu");
            }

            string sql = DAOWorker.SqlDAO.AddInClause(expMests.Select(s => s.ID).ToList(), "UPDATE HIS_EXP_MEST SET EXP_MEST_STT_ID = 2, LAST_APPROVAL_TIME = NULL, LAST_APPROVAL_LOGINNAME = NULL, LAST_APPROVAL_USERNAME = NULL, LAST_APPROVAL_DATE = NULL WHERE %IN_CLAUSE% ", "ID");
            sqls.Add(sql);
            expMests.ForEach(o =>
            {
                o.EXP_MEST_STT_ID = IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__REQUEST;
                o.LAST_APPROVAL_TIME = null;
                o.LAST_APPROVAL_LOGINNAME = null;
                o.LAST_APPROVAL_DATE = null;
                o.LAST_EXP_USERNAME = null;
            });
        }

        private void UnfinishServiceReq(List<HIS_EXP_MEST> expMests, ref List<HIS_SERVICE_REQ> serviceReqs)
        {
            List<HIS_SERVICE_REQ> tmpServiceReqs = null;

            foreach (var expMest in expMests)
            {
                HIS_SERVICE_REQ tmp = null;
                if (expMest.SERVICE_REQ_ID.HasValue)
                {
                    tmp = new HisServiceReqGet().GetById(expMest.SERVICE_REQ_ID.Value);
                }
                //Neu phieu xuat ban va co cau hinh tu dong tao phieu xuat ban --> khi duyet phieu xuat ban, tu dong cap nhat trang thai y lenh ke don ngoai kho
                else if (expMest.PRESCRIPTION_ID.HasValue && HisServiceReqCFG.IS_AUTO_CREATE_SALE_EXP_MEST)
                {
                    tmp = new HisServiceReqGet().GetById(expMest.PRESCRIPTION_ID.Value);
                }
                if (tmp != null)
                {
                    tmpServiceReqs.Add(tmp);
                }
            }

            if (IsNotNullOrEmpty(tmpServiceReqs))
            {
                Mapper.CreateMap<HIS_SERVICE_REQ, HIS_SERVICE_REQ>();
                List<HIS_SERVICE_REQ> beforeUpdates = Mapper.Map<List<HIS_SERVICE_REQ>>(tmpServiceReqs);

                tmpServiceReqs.ForEach(o =>
                {
                    o.SERVICE_REQ_STT_ID = IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__CXL;
                    o.FINISH_TIME = null;
                    o.EXECUTE_LOGINNAME = null;
                    o.EXECUTE_USERNAME = null;
                    o.START_TIME = null;
                });
                if (!this.hisServiceReqUpdate.UpdateList(tmpServiceReqs, beforeUpdates))
                {
                    throw new Exception("Rollback du lieu");
                }
                serviceReqs = tmpServiceReqs;
            }
        }

        internal void Rollback()
        {
            this.hisExpMestUpdate.RollbackData();
            this.hisServiceReqUpdate.RollbackData();
        }
    }
}
