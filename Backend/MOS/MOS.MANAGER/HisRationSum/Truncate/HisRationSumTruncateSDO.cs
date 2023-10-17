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

namespace MOS.MANAGER.HisRationSum.Truncate
{
    class HisRationSumTruncateSDO : BusinessBase
    {
        private HisServiceReqUpdate hisServiceReqUpdate;
        private HisSereServDeleteSql hisSereServDeleteSql;

        internal HisRationSumTruncateSDO()
            : base()
        {
            this.Init();
        }

        internal HisRationSumTruncateSDO(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisServiceReqUpdate = new HisServiceReqUpdate(param);
            this.hisSereServDeleteSql = new HisSereServDeleteSql(param);
        }

        internal bool Run(HisRationSumSDO data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_RATION_SUM raw = null;
                WorkPlaceSDO wp = null;
                HisRationSumCheck checker = new HisRationSumCheck(param);

                valid = valid && data.Id.HasValue;
                valid = valid && IsGreaterThanZero(data.Id.Value);
                valid = valid && checker.VerifyId(data.Id.Value, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.IsSttAllowDelete(raw);
                //valid = valid && this.HasWorkPlaceInfo(raw.ROOM_ID, ref wp);
                valid = valid && checker.CheckCreatorOrAdmin(raw);
                if (valid)
                {
                    HisServiceReqFilterQuery filter = new HisServiceReqFilterQuery();
                    filter.RATION_SUM_ID = data.Id.Value;
                    List<HIS_SERVICE_REQ> listReq = new HisServiceReqGet().Get(filter);

                    if (IsNotNullOrEmpty(listReq))
                    {
                        this.ProcessSereServ(listReq);
                        this.ProcessServiceReq(listReq);
                    }

                    this.ProcessRationSum(raw);
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                this.Rollback();
                result = false;
            }
            return result;
        }

        private void ProcessSereServ(List<HIS_SERVICE_REQ> listReq)
        {
            List<HIS_SERE_SERV> hisSereServs = new HisSereServGet().GetByServiceReqIds(listReq.Select(s => s.ID).ToList());
            if (IsNotNullOrEmpty(hisSereServs))
            {
                if (!this.hisSereServDeleteSql.Run(hisSereServs))
                {
                    throw new Exception("hisSereServDeleteSql. Ket thuc nghiep vu");
                }
            }
        }

        private void ProcessServiceReq(List<HIS_SERVICE_REQ> listReq)
        {
            Mapper.CreateMap<List<HIS_SERVICE_REQ>, List<HIS_SERVICE_REQ>>();
            List<HIS_SERVICE_REQ> listBefore = Mapper.Map<List<HIS_SERVICE_REQ>>(listReq);
            listReq.ForEach(o =>
            {
                o.RATION_SUM_ID = null;
                o.SERVICE_REQ_STT_ID = IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__CXL;
            });

            if (!this.hisServiceReqUpdate.UpdateList(listReq, listBefore))
            {
                throw new Exception("hisServiceReqUpdate. Ket thuc nghiep vu");
            }
        }

        private void ProcessRationSum(HIS_RATION_SUM raw)
        {
            if (!DAOWorker.HisRationSumDAO.Truncate(raw))
            {
                throw new Exception("Xoa HIS_RATION_SUM that bai. Ket thuc nghiep vu");
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
