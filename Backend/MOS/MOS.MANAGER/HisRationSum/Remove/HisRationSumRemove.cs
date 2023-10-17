using AutoMapper;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisSereServ.Delete;
using MOS.MANAGER.HisServiceReq;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisRationSum.Remove
{
    class HisRationSumRemove : BusinessBase
    {
        private HisServiceReqUpdate hisServiceReqUpdate;
        private HisSereServDeleteSql hisSereServDeleteSql;

        internal HisRationSumRemove()
            : base()
        {
            this.Init();
        }

        internal HisRationSumRemove(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisSereServDeleteSql = new HisSereServDeleteSql(param);
            this.hisServiceReqUpdate = new HisServiceReqUpdate(param);
        }

        internal bool Run(HisRationSumUpdateSDO data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_RATION_SUM raw = null;
                HIS_SERVICE_REQ serviceReq = null;
                WorkPlaceSDO workplaceSDO = null;
                HisRationSumCheck checker = new HisRationSumCheck(param);
                HisServiceReqCheck serviceReqChecker = new HisServiceReqCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && data.ServiceReqId.HasValue;
                valid = valid && checker.VerifyId(data.RationSumId, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.IsSttAllowApproveOrReject(raw);
                valid = valid && serviceReqChecker.VerifyId(data.ServiceReqId.Value, ref serviceReq);
                valid = valid && serviceReqChecker.IsUnLock(serviceReq);
                valid = valid && checker.CheckServiceReq(raw, serviceReq);
                valid = valid && checker.HasWorkPlaceInfo(data.WorkingRoomId, ref workplaceSDO);
                //valid = valid && checker.IsWorkingAtRoom(raw.ROOM_ID, data.WorkingRoomId);
                if (valid)
                {
                    this.ProcessServiceReq(serviceReq);
                    this.ProcessSereServ(serviceReq);
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
                this.Rollback();
                param.HasException = true;
            }
            return result;
        }

        private void ProcessServiceReq(HIS_SERVICE_REQ serviceReq)
        {
            Mapper.CreateMap<HIS_SERVICE_REQ, HIS_SERVICE_REQ>();
            HIS_SERVICE_REQ before = Mapper.Map<HIS_SERVICE_REQ>(serviceReq);
            serviceReq.RATION_SUM_ID = null;
            serviceReq.SERVICE_REQ_STT_ID = IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__CXL;
            if (!this.hisServiceReqUpdate.Update(serviceReq, before, false))
            {
                throw new Exception("hisServiceReqUpdate. Ket thuc nghiep vu");
            }
        }

        private void ProcessSereServ(HIS_SERVICE_REQ serviceReq)
        {
            List<HIS_SERE_SERV> hisSereServs = new HisSereServGet().GetByServiceReqId(serviceReq.ID);
            if (IsNotNullOrEmpty(hisSereServs))
            {
                if (!this.hisSereServDeleteSql.Run(hisSereServs))
                {
                    throw new Exception("hisSereServDeleteSql. Ket thuc nghiep vu");
                }
            }
        }

        private void Rollback()
        {
            try
            {
                this.hisServiceReqUpdate.RollbackData();
                this.hisSereServDeleteSql.Rollback();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
