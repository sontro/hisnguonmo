using AutoMapper;
using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.HisServiceReq;
using MOS.MANAGER.HisSereServ;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisCarerCardBorrow
{
    class HisCarerCardBorrowDeleteSDOProcessor : BusinessBase
    {
        private HIS_CARER_CARD_BORROW beforeUpdateCarerCardBorrow;
        private List<HIS_SERVICE_REQ> beforeUpdateServiceReqs;
        private List<HIS_SERE_SERV> beforeUpdateSereServs;
        private bool idDeleteSS = false;
        private bool idDeleteSR = false;
        private bool idDeleteCard = false;

        internal HisCarerCardBorrowDeleteSDOProcessor()
            : base()
        {
            this.Init();
        }

        internal HisCarerCardBorrowDeleteSDOProcessor(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.beforeUpdateCarerCardBorrow = null;
            this.beforeUpdateServiceReqs = null;
            this.beforeUpdateSereServs = null;
        }

        internal bool DeleteSDO(HisCarerCardBorrowDeleteSDO data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                WorkPlaceSDO workplace = null;
                HIS_CARER_CARD_BORROW cardBorrowed = null;

                HisTreatmentCheck treatChecker = new HisTreatmentCheck(param);
                HisCarerCardBorrowDeleteSDOCheck checker = new HisCarerCardBorrowDeleteSDOCheck(param);
                HisCarerCardBorrowCheck commonChecker = new HisCarerCardBorrowCheck(param);

                valid = valid && checker.VerifyRequireField(data);
                valid = valid && commonChecker.VerifyId(data.CarerCardBorrowId, ref cardBorrowed);
                valid = valid && commonChecker.HasNoGiveBackTime(cardBorrowed);
                valid = valid && this.HasWorkPlaceInfo(data.RequestRoomId, ref workplace);

                if (valid)
                {
                    List<HIS_SERVICE_REQ> reqs = new HisServiceReqGet().GetByCarerCardBorrowId(cardBorrowed.ID);
                    this.ProcessSereServ(reqs);
                    this.ProcessServiceReq(reqs);
                    this.ProcessCarerCardBorrow(cardBorrowed);

                    result = true;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                this.RollbackData();
                result = false;
            }
            return result;
        }

        private void ProcessSereServ(List<HIS_SERVICE_REQ> reqs)
        {
            List<HIS_SERE_SERV> ss = IsNotNullOrEmpty(reqs) ? new HisSereServGet().GetByServiceReqIds(reqs.Select(o => o.ID).ToList()) : null;
            if (IsNotNullOrEmpty(ss))
            {
                Mapper.CreateMap<HIS_SERE_SERV, HIS_SERE_SERV>();
                this.beforeUpdateSereServs = Mapper.Map<List<HIS_SERE_SERV>>(ss);

                if (!DAOWorker.HisSereServDAO.TruncateList(ss))
                {
                    throw new Exception("Xoa thong tin HisSereServ khi huy muon the that bai." + LogUtil.TraceData("data", ss));
                }
                this.idDeleteSS = true;
            }
        }

        private void ProcessServiceReq(List<HIS_SERVICE_REQ> reqs)
        {
            
            if (IsNotNullOrEmpty(reqs))
            {
                Mapper.CreateMap<HIS_SERVICE_REQ, HIS_SERVICE_REQ>();
                this.beforeUpdateServiceReqs = Mapper.Map<List<HIS_SERVICE_REQ>>(reqs);

                if (!DAOWorker.HisServiceReqDAO.TruncateList(reqs))
                {
                    throw new Exception("Xoa thong tin HisServiceReqs khi huy bao mat the that bai." + LogUtil.TraceData("data", reqs));
                }
                this.idDeleteSR = true;
            }
        }

        private void ProcessCarerCardBorrow(HIS_CARER_CARD_BORROW cardBorrowed)
        {
            Mapper.CreateMap<HIS_CARER_CARD_BORROW, HIS_CARER_CARD_BORROW>();
            this.beforeUpdateCarerCardBorrow = Mapper.Map<HIS_CARER_CARD_BORROW>(cardBorrowed);
            if (!DAOWorker.HisCarerCardBorrowDAO.Truncate(cardBorrowed))
            {
                throw new Exception("Xoa thong tin HisCarerCardBorrow khi huy bao mat the that bai." + LogUtil.TraceData("data", cardBorrowed));
            }
            this.idDeleteCard = true;
        }

        internal void RollbackData()
        {
            if (this.idDeleteCard && IsNotNull(this.beforeUpdateCarerCardBorrow))
            {
                if (!DAOWorker.HisCarerCardBorrowDAO.Create(this.beforeUpdateCarerCardBorrow))
                {
                    LogSystem.Warn("Rollback du lieu HisCarerCardBorrow that bai, can kiem tra lai." + LogUtil.TraceData("HisCarerCardBorrow", this.beforeUpdateCarerCardBorrow));
                }
                this.beforeUpdateCarerCardBorrow = null;
            }

            if (this.idDeleteSR && IsNotNull(this.beforeUpdateServiceReqs))
            {
                if (!DAOWorker.HisServiceReqDAO.CreateList(this.beforeUpdateServiceReqs))
                {
                    LogSystem.Warn("Rollback du lieu HisServiceReqs that bai, can kiem tra lai." + LogUtil.TraceData("HisServiceReqs", this.beforeUpdateServiceReqs));
                }
                this.beforeUpdateServiceReqs = null;
            }

            if (this.idDeleteSS && IsNotNull(this.beforeUpdateSereServs))
            {
                if (!DAOWorker.HisSereServDAO.CreateList(this.beforeUpdateSereServs))
                {
                    LogSystem.Warn("Rollback du lieu HisSereServ that bai, can kiem tra lai." + LogUtil.TraceData("HisSereServ", this.beforeUpdateSereServs));
                }
                this.beforeUpdateSereServs = null;
            }
        }
    }
}

