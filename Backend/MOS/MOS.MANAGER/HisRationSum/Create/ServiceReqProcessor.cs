using AutoMapper;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisServiceReq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisRationSum.Create
{
    class ServiceReqProcessor : BusinessBase
    {
        private HisServiceReqUpdate hisServiceReqUpdate;

        internal ServiceReqProcessor(CommonParam param)
            : base(param)
        {
            this.hisServiceReqUpdate = new HisServiceReqUpdate(param);
        }

        internal bool Run(List<HIS_RATION_SUM> rationSums, List<HIS_SERVICE_REQ> serviceReqs)
        {
            bool result = false;
            try
            {
                List<HIS_SERVICE_REQ> old = new List<HIS_SERVICE_REQ>();
                old.AddRange(serviceReqs);
                Mapper.CreateMap<HIS_SERVICE_REQ, HIS_SERVICE_REQ>();
                List<HIS_SERVICE_REQ> listBefores = Mapper.Map<List<HIS_SERVICE_REQ>>(old);
                bool isAutoSplitByIntructionDate = Config.HisRationSumCFG.AUTO_SPLIT_BY_INTRUCTION_DATE == 1;
                if (isAutoSplitByIntructionDate)
                {
                    serviceReqs.ForEach(o =>
                    {
                        o.RATION_SUM_ID = rationSums.FirstOrDefault(f => f.ROOM_ID == o.EXECUTE_ROOM_ID && f.MAX_INTRUCTION_DATE == o.INTRUCTION_DATE).ID;
                        o.SERVICE_REQ_STT_ID = IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__HT;
                    });
                }
                else
                {
                    serviceReqs.ForEach(o =>
                    {
                        o.RATION_SUM_ID = rationSums.FirstOrDefault(f => f.ROOM_ID == o.EXECUTE_ROOM_ID).ID;
                        o.SERVICE_REQ_STT_ID = IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__HT;
                    });
                }
                if (!this.hisServiceReqUpdate.UpdateList(serviceReqs, listBefores))
                {
                    throw new Exception("hisServiceReqUpdate, Ket thuc nghiep vu");
                }
                result = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        internal void Rollback()
        {
            this.hisServiceReqUpdate.RollbackData();
        }
    }
}
