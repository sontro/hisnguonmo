using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisExpMest;
using MOS.MANAGER.HisExpMest.Common.Update;
using MOS.MANAGER.HisServiceReq;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisExpMest.Common.Decline
{
    //Tu choi duyet
    //Chi danh cho cac loai phieu xuat co tao y/c xuat: xuat chuyen kho,
    //Mo rong them co cac phieu khong tao yeu cau:
    partial class HisExpMestDecline : BusinessBase
    {
        private HisExpMestUpdate hisExpMestUpdate;
        private HisServiceReqUpdate hisServiceReqUpdate;

        internal HisExpMestDecline()
            : base()
        {
            this.Init();
        }

        internal HisExpMestDecline(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisExpMestUpdate = new HisExpMestUpdate(param);
            this.hisServiceReqUpdate = new HisServiceReqUpdate(param);
        }

        internal bool Run(HisExpMestSDO data, ref HIS_EXP_MEST resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_EXP_MEST expMest = null;
                HisExpMestDeclineCheck checker = new HisExpMestDeclineCheck(param);
                HisExpMestCheck commonChecker = new HisExpMestCheck(param);
                valid = valid && commonChecker.VerifyRequireField(data);
                valid = valid && checker.IsAllowed(data, ref expMest);
                valid = valid && commonChecker.IsUnNotTaken(expMest);
                valid = valid && commonChecker.HasNotInExpMestAggr(expMest);
                if (valid)
                {
                    this.ProcessExpMest(expMest);
                    this.ProcessServiceReq(expMest);
                    resultData = expMest;
                    result = true;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                resultData = null;
                this.RollBack();
                result = false;
            }
            return result;
        }

        private void ProcessExpMest(HIS_EXP_MEST expMest)
        {
            //Cap nhat trang thai cua exp_mest
            Mapper.CreateMap<HIS_EXP_MEST, HIS_EXP_MEST>();
            HIS_EXP_MEST beforeUpdate = Mapper.Map<HIS_EXP_MEST>(expMest);//phuc vu rollback
            expMest.EXP_MEST_STT_ID = IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__REJECT;
            if (!this.hisExpMestUpdate.Update(expMest, beforeUpdate))
            {
                throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
            }
        }

        private void ProcessServiceReq(HIS_EXP_MEST expMest)
        {
            if (expMest != null)
            {
                HIS_SERVICE_REQ serviceReq = null;
                if (expMest.SERVICE_REQ_ID.HasValue)
                {
                    serviceReq = new HisServiceReqGet().GetById(expMest.SERVICE_REQ_ID.Value);
                }
                //Neu phieu xuat ban va co cau hinh tu dong tao phieu xuat ban --> khi duyet phieu xuat ban, tu dong cap nhat trang thai y lenh ke don ngoai kho
                else if (expMest.PRESCRIPTION_ID.HasValue && HisServiceReqCFG.IS_AUTO_CREATE_SALE_EXP_MEST)
                {
                    serviceReq = new HisServiceReqGet().GetById(expMest.PRESCRIPTION_ID.Value);
                }

                if (serviceReq != null && serviceReq.SERVICE_REQ_STT_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__CXL)
                {
                    Mapper.CreateMap<HIS_SERVICE_REQ, HIS_SERVICE_REQ>();
                    HIS_SERVICE_REQ beforeServiceReq = Mapper.Map<HIS_SERVICE_REQ>(serviceReq); //phuc vu rollback
                    serviceReq.SERVICE_REQ_STT_ID = IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__HT;
                    if (!this.hisServiceReqUpdate.Update(serviceReq, beforeServiceReq, false))
                    {
                        throw new Exception("Cap nhat trang thai service_Req sang hoan thanh that bai");
                    }
                }
            }
        }

        private void RollBack()
        {
            this.hisExpMestUpdate.RollbackData();
        }
    }
}
