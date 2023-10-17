using AutoMapper;
using Inventec.Common.Logging;
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

namespace MOS.MANAGER.HisExpMest.Aggr.Unexport
{
    class ChildProcessor : BusinessBase
    {
        private HisExpMestUpdate hisExpMestUpdate;
        private HisServiceReqUpdate hisServiceReqUpdate;

        internal ChildProcessor()
            : base()
        {
            this.Init();
        }

        internal ChildProcessor(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisExpMestUpdate = new HisExpMestUpdate(param);
            this.hisServiceReqUpdate = new HisServiceReqUpdate(param);
        }

        internal bool Run(List<HIS_EXP_MEST> children, ref List<HIS_SERVICE_REQ> serviceReqs)
        {
            try
            {
                if (IsNotNullOrEmpty(children))
                {
                    this.UnfinishExpMest(children);
                    this.UnfinishServiceReq(children, ref serviceReqs);
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


        private void UnfinishExpMest(List<HIS_EXP_MEST> children)
        {
            if (IsNotNullOrEmpty(children))
            {
                Mapper.CreateMap<HIS_EXP_MEST, HIS_EXP_MEST>();
                List<HIS_EXP_MEST> beforeUpdates = Mapper.Map<List<HIS_EXP_MEST>>(children);
                //Su dung truong nay de huy thuc xuat chứ ko update trực tiếp trường trạng thái để
                //chạy trigger nhằm tránh trường hợp 2 người cùng thực hiện hủy thực xuất đồng thời trên 1 phiếu
                children.ForEach(o => o.IS_HTX = MOS.UTILITY.Constant.IS_TRUE);

                if (!this.hisExpMestUpdate.UpdateList(children, beforeUpdates))
                {
                    throw new Exception("Rollback du lieu");
                }
            }
        }

        private void UnfinishServiceReq(List<HIS_EXP_MEST> children, ref List<HIS_SERVICE_REQ> serviceReqs)
        {
            List<long> serviceReqIds = IsNotNullOrEmpty(children) ? children.Where(o => o.SERVICE_REQ_ID.HasValue).Select(o => o.SERVICE_REQ_ID.Value).ToList() : null;

            if (IsNotNullOrEmpty(serviceReqIds))
            {
                var tmpServiceReqs = new HisServiceReqGet().GetByIds(serviceReqIds);

                if (IsNotNullOrEmpty(tmpServiceReqs))
                {
                    Mapper.CreateMap<HIS_SERVICE_REQ, HIS_SERVICE_REQ>();
                    List<HIS_SERVICE_REQ> beforeUpdates = Mapper.Map<List<HIS_SERVICE_REQ>>(tmpServiceReqs);

                    tmpServiceReqs.ForEach(o =>
                    {
                        o.SERVICE_REQ_STT_ID = IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__DXL;
                        o.FINISH_TIME = null;
                        o.EXECUTE_LOGINNAME = null;
                        o.EXECUTE_USERNAME = null;
                        o.EXECUTE_USER_TITLE = null;
                    });

                    if (!this.hisServiceReqUpdate.UpdateList(tmpServiceReqs, beforeUpdates))
                    {
                        throw new Exception("Rollback du lieu");
                    }
                    serviceReqs = tmpServiceReqs;
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
