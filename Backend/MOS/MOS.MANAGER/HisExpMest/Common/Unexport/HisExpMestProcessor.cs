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

namespace MOS.MANAGER.HisExpMest.Common.Unexport
{
    class HisExpMestProcessor : BusinessBase
    {
        private HisExpMestUpdate hisExpMestUpdate;
        private HisServiceReqUpdate hisServiceReqUpdate;

        internal HisExpMestProcessor()
            : base()
        {
            this.Init();
        }

        internal HisExpMestProcessor(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisExpMestUpdate = new HisExpMestUpdate(param);
            this.hisServiceReqUpdate = new HisServiceReqUpdate(param);
        }

        internal bool Run(HIS_EXP_MEST expMest, ref HIS_SERVICE_REQ serviceReq)
        {
            try
            {
                if (expMest != null)
                {
                    this.UnfinishExpMest(expMest);
                    this.UnfinishServiceReq(expMest, ref serviceReq);
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


        private void UnfinishExpMest(HIS_EXP_MEST expMest)
        {
            Mapper.CreateMap<HIS_EXP_MEST, HIS_EXP_MEST>();
            HIS_EXP_MEST beforeUpdate = Mapper.Map<HIS_EXP_MEST>(expMest);

            expMest.IS_HTX = MOS.UTILITY.Constant.IS_TRUE;
            expMest.IS_EXPORT_EQUAL_APPROVE = null;
            expMest.IS_EXPORT_EQUAL_REQUEST = null;

            if (!this.hisExpMestUpdate.Update(expMest, beforeUpdate))
            {
                throw new Exception("Rollback du lieu");
            }
        }

        private void UnfinishServiceReq(HIS_EXP_MEST expMest, ref HIS_SERVICE_REQ serviceReq)
        {
            HIS_SERVICE_REQ tmpServiceReq = null;

            if (expMest.SERVICE_REQ_ID.HasValue)
            {
                tmpServiceReq = new HisServiceReqGet().GetById(expMest.SERVICE_REQ_ID.Value);
            }
            //Neu phieu xuat ban va co cau hinh tu dong tao phieu xuat ban --> khi duyet phieu xuat ban, tu dong cap nhat trang thai y lenh ke don ngoai kho
            else if (expMest.PRESCRIPTION_ID.HasValue && HisServiceReqCFG.IS_AUTO_CREATE_SALE_EXP_MEST)
            {
                tmpServiceReq = new HisServiceReqGet().GetById(expMest.PRESCRIPTION_ID.Value);
            }
            if (tmpServiceReq != null)
            {
                Mapper.CreateMap<HIS_SERVICE_REQ, HIS_SERVICE_REQ>();
                HIS_SERVICE_REQ beforeUpdate = Mapper.Map<HIS_SERVICE_REQ>(tmpServiceReq);

                tmpServiceReq.SERVICE_REQ_STT_ID = IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__DXL;
                tmpServiceReq.FINISH_TIME = null;
                tmpServiceReq.EXECUTE_LOGINNAME = null;
                tmpServiceReq.EXECUTE_USERNAME = null;
                tmpServiceReq.EXECUTE_USER_TITLE = null;

                if (!this.hisServiceReqUpdate.Update(tmpServiceReq, beforeUpdate, false))
                {
                    throw new Exception("Rollback du lieu");
                }
                serviceReq = tmpServiceReq;
            }
        }

        internal void Rollback()
        {
            this.hisExpMestUpdate.RollbackData();
            this.hisServiceReqUpdate.RollbackData();
        }
    }
}
