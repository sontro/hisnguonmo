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
    class HisCarerCardBorrowUnGiveBack : BusinessBase
    {
        private HIS_CARER_CARD_BORROW beforeUpdateCarerCardBorrow;
        private List<HIS_SERVICE_REQ> beforeUpdateServiceReqs;
        private List<HIS_SERE_SERV> beforeUpdateSereServs;

        internal HisCarerCardBorrowUnGiveBack()
            : base()
        {
            this.Init();
        }

        internal HisCarerCardBorrowUnGiveBack(CommonParam param)
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

        internal bool UnGiveBack(HisCarerCardBorrowUnGiveBackSDO data, ref HIS_CARER_CARD_BORROW resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_CARER_CARD_BORROW cardBorrowed = null;
                HIS_TREATMENT treatment = null;
                WorkPlaceSDO workplace = null;;
                List<HIS_SERVICE_REQ> reqs = null;
                List<HIS_SERE_SERV> ss = null;

                HisTreatmentCheck treatChecker = new HisTreatmentCheck(param);
                HisCarerCardBorrowUnGiveBackCheck checker = new HisCarerCardBorrowUnGiveBackCheck(param);
                HisCarerCardBorrowCheck commonChecker = new HisCarerCardBorrowCheck(param);

                valid = valid && checker.VerifyRequireField(data);
                valid = valid && commonChecker.VerifyId(data.CarerCardBorrowId, ref cardBorrowed);
                valid = valid && commonChecker.HasGiveBackInfo(cardBorrowed);
                valid = valid && commonChecker.IsUnLost(cardBorrowed);
                valid = valid && this.HasWorkPlaceInfo(data.RequestRoomId, ref workplace);
                valid = valid && treatChecker.VerifyId(cardBorrowed.TREATMENT_ID, ref treatment);
                valid = valid && treatChecker.IsUnLock(treatment);

                if (valid)
                {
                    this.ProcessCarerCardBorrow(cardBorrowed);
                    this.ProcessServiceReq(cardBorrowed.ID, ref reqs);
                    this.ProcessSereServ(reqs, cardBorrowed.BORROW_TIME, ref ss);

                    resultData = cardBorrowed;
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

        private void ProcessSereServ(List<HIS_SERVICE_REQ> reqs, long borrowTime, ref List<HIS_SERE_SERV> ss)
        {
            ss = IsNotNullOrEmpty(reqs) ? new HisSereServGet().GetByServiceReqIds(reqs.Select(o => o.ID).ToList()) : null;
            if (IsNotNullOrEmpty(ss))
            {
                Mapper.CreateMap<HIS_SERE_SERV, HIS_SERE_SERV>();
                this.beforeUpdateSereServs = Mapper.Map<List<HIS_SERE_SERV>>(ss);

                foreach (var s in ss)
                {
                    HisCarerCardBorrowUtil.ChangeAmountWithBorrowTime(s, borrowTime);
                }

                if (!DAOWorker.HisSereServDAO.UpdateList(ss))
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSereServ_CapNhatThatBai);
                    throw new Exception("Cap nhat thong tin HisSereServ khi huy bao mat the that bai." + LogUtil.TraceData("data", ss));
                }
            }
        }

        private void ProcessServiceReq(long carerCardBorrowId, ref List<HIS_SERVICE_REQ> reqs)
        {
            reqs = new HisServiceReqGet().GetByCarerCardBorrowId(carerCardBorrowId);
            if (IsNotNullOrEmpty(reqs))
            {
                Mapper.CreateMap<HIS_SERVICE_REQ, HIS_SERVICE_REQ>();
                this.beforeUpdateServiceReqs = Mapper.Map<List<HIS_SERVICE_REQ>>(reqs);

                reqs.ForEach(o =>
                {
                    o.SERVICE_REQ_STT_ID = IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__CXL;
                });

                if (!DAOWorker.HisServiceReqDAO.UpdateList(reqs))
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisServiceReq_CapNhatThatBai);
                    throw new Exception("Cap nhat thong tin HisServiceReqs khi huy tra the that bai." + LogUtil.TraceData("data", reqs));
                }
            }
        }

        private void ProcessCarerCardBorrow(HIS_CARER_CARD_BORROW cardBorrowed)
        {
            Mapper.CreateMap<HIS_CARER_CARD_BORROW, HIS_CARER_CARD_BORROW>();
            this.beforeUpdateCarerCardBorrow = Mapper.Map<HIS_CARER_CARD_BORROW>(cardBorrowed);

            cardBorrowed.GIVE_BACK_TIME = null;
            cardBorrowed.RECEIVING_LOGINNAME = null;
            cardBorrowed.RECEIVING_USERNAME = null;
            if (!DAOWorker.HisCarerCardBorrowDAO.Update(cardBorrowed))
            {
                MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisCarerCardBorrow_CapNhatThatBai);
                throw new Exception("Cap nhat thong tin HisCarerCardBorrow khi huy tra the that bai." + LogUtil.TraceData("data", cardBorrowed));
            }
        }

        internal void RollbackData()
        {
            if (IsNotNull(this.beforeUpdateSereServs))
            {
                if (!DAOWorker.HisSereServDAO.UpdateList(this.beforeUpdateSereServs))
                {
                    LogSystem.Warn("Rollback du lieu HisSereServ that bai, can kiem tra lai." + LogUtil.TraceData("HisSereServ", this.beforeUpdateSereServs));
                }
                this.beforeUpdateSereServs = null;
            }

            if (IsNotNull(this.beforeUpdateServiceReqs))
            {
                if (!DAOWorker.HisServiceReqDAO.UpdateList(this.beforeUpdateServiceReqs))
                {
                    LogSystem.Warn("Rollback du lieu HisServiceReqs that bai, can kiem tra lai." + LogUtil.TraceData("HisServiceReqs", this.beforeUpdateServiceReqs));
                }
                this.beforeUpdateServiceReqs = null;
            }

            if (IsNotNull(this.beforeUpdateCarerCardBorrow))
            {
                if (!DAOWorker.HisCarerCardBorrowDAO.Update(this.beforeUpdateCarerCardBorrow))
                {
                    LogSystem.Warn("Rollback du lieu HisCarerCardBorrow that bai, can kiem tra lai." + LogUtil.TraceData("HisCarerCardBorrow", this.beforeUpdateCarerCardBorrow));
                }
                this.beforeUpdateCarerCardBorrow = null;
            }
        }
    }
}

