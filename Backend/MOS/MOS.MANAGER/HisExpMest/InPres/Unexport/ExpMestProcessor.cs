using AutoMapper;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpMest.Common.Update;
using MOS.MANAGER.HisServiceReq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.InPres.Unexport
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

        internal ExpMestProcessor(CommonParam paramCreate)
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
                if (IsNotNull(expMest))
                {
                    this.UnfinishExpMest(expMest);
                    this.UnfinishServiceReq(expMest, ref serviceReq);
                }
                return true;
            }
            catch (Exception ex)
            {
                this.Rollback();
                Inventec.Common.Logging.LogSystem.Error(ex);
                return false;
            }
        }


        private void UnfinishExpMest(HIS_EXP_MEST expMest)
        {
            Mapper.CreateMap<HIS_EXP_MEST, HIS_EXP_MEST>();
            HIS_EXP_MEST beforeUpdate = Mapper.Map<HIS_EXP_MEST>(expMest);
            //Su dung truong nay de huy thuc xuat chứ ko update trực tiếp trường trạng thái để
            //chạy trigger nhằm tránh trường hợp 2 người cùng thực hiện hủy thực xuất đồng thời trên 1 phiếu
            expMest.IS_HTX = MOS.UTILITY.Constant.IS_TRUE;

            if (!this.hisExpMestUpdate.Update(expMest, beforeUpdate))
            {
                throw new Exception("Rollback du lieu");
            }
        }

        private void UnfinishServiceReq(HIS_EXP_MEST expMest, ref HIS_SERVICE_REQ serviceReq)
        {
            if (expMest.SERVICE_REQ_ID.HasValue)
            {
                HIS_SERVICE_REQ tmpServiceReq = new HisServiceReqGet().GetById(expMest.SERVICE_REQ_ID.Value);

                if (IsNotNull(tmpServiceReq))
                {
                    Mapper.CreateMap<HIS_SERVICE_REQ, HIS_SERVICE_REQ>();
                    HIS_SERVICE_REQ beforeUpdate = Mapper.Map<HIS_SERVICE_REQ>(tmpServiceReq);

                    tmpServiceReq.SERVICE_REQ_STT_ID = IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__DXL;
                    tmpServiceReq.FINISH_TIME = null;
                    tmpServiceReq.EXECUTE_LOGINNAME = null;
                    tmpServiceReq.EXECUTE_USERNAME = null;

                    if (!this.hisServiceReqUpdate.Update(tmpServiceReq, beforeUpdate, false))
                    {
                        throw new Exception("Rollback du lieu");
                    }
                    serviceReq = tmpServiceReq;
                }
            }
        }

        internal void Rollback()
        {
            this.hisExpMestUpdate.RollbackData();
            this.hisServiceReqUpdate.RollbackData();
        }
    }
}
