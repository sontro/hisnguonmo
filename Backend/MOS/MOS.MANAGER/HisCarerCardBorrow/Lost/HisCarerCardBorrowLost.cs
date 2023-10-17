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
    class HisCarerCardBorrowLost : BusinessBase
    {
        private HIS_CARER_CARD_BORROW beforeUpdateCarerCardBorrow;
        private List<HIS_SERVICE_REQ> beforeUpdateServiceReqs;
        private List<HIS_SERE_SERV> beforeUpdateSereServs;

        internal HisCarerCardBorrowLost()
            : base()
        {
            this.Init();
        }

        internal HisCarerCardBorrowLost(CommonParam param)
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

        internal bool Lost(HisCarerCardBorrowLostSDO data, ref HIS_CARER_CARD_BORROW resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_CARER_CARD_BORROW cardBorrowed = null;
                HIS_TREATMENT treatment = null;
                WorkPlaceSDO workplace = null;
                List<HIS_SERVICE_REQ> reqs = null;
                List<HIS_SERE_SERV> ss = null;

                HisTreatmentCheck treatChecker = new HisTreatmentCheck(param);
                HisCarerCardBorrowLostCheck checker = new HisCarerCardBorrowLostCheck(param);
                HisCarerCardBorrowCheck commonChecker = new HisCarerCardBorrowCheck(param);

                valid = valid && checker.VerifyRequireField(data);
                valid = valid && commonChecker.VerifyId(data.CarerCardBorrowId, ref cardBorrowed);
                valid = valid && commonChecker.IsUnLost(cardBorrowed);
                valid = valid && this.HasWorkPlaceInfo(data.RequestRoomId, ref workplace);
                valid = valid && treatChecker.VerifyId(cardBorrowed.TREATMENT_ID, ref treatment);
                valid = valid && treatChecker.IsUnLock(treatment);
                valid = valid && commonChecker.IsValidLostTime(data.GiveBackTime);

                if (valid)
                {
                    this.ProcessCarerCardBorrow(cardBorrowed, data.GiveBackTime);
                    this.ProcessServiceReq(cardBorrowed.ID, ref reqs);
                    this.ProcessSereServ(reqs, treatment, cardBorrowed.BORROW_TIME, data.GiveBackTime, ref ss);

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

        private void ProcessSereServ(List<HIS_SERVICE_REQ> reqs, HIS_TREATMENT treatment,long borrowTime, long giveBackTime, ref List<HIS_SERE_SERV> ss)
        {
            ss = IsNotNullOrEmpty(reqs) ? new HisSereServGet().GetByServiceReqIds(reqs.Select(o => o.ID).ToList()) : null;
            if (IsNotNullOrEmpty(ss))
            {
                Mapper.CreateMap<HIS_SERE_SERV, HIS_SERE_SERV>();
                this.beforeUpdateSereServs = Mapper.Map<List<HIS_SERE_SERV>>(ss);

                foreach (var s in ss)
                {
                    if (treatment.IS_PAUSE == Constant.IS_TRUE)
                        HisCarerCardBorrowUtil.ChangeAmountWithBorrowAndGiveBackTime(s, borrowTime, treatment.OUT_TIME.Value);
                    else
                        HisCarerCardBorrowUtil.ChangeAmountWithBorrowAndGiveBackTime(s, borrowTime, giveBackTime);
                }

                if (!DAOWorker.HisSereServDAO.UpdateList(ss))
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSereServ_CapNhatThatBai);
                    throw new Exception("Cap nhat thong tin HisSereServ khi bao mat the that bai." + LogUtil.TraceData("data", ss));
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
                    o.SERVICE_REQ_STT_ID = IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__HT;
                    o.EXECUTE_LOGINNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                    o.EXECUTE_USERNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
                });

                if (!DAOWorker.HisServiceReqDAO.UpdateList(reqs))
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisServiceReq_CapNhatThatBai);
                    throw new Exception("Cap nhat thong tin HisServiceReqs khi bao mat the that bai." + LogUtil.TraceData("data", reqs));
                }
            }
        }

        private void ProcessCarerCardBorrow(HIS_CARER_CARD_BORROW cardBorrowed, long giveBackTime)
        {
            Mapper.CreateMap<HIS_CARER_CARD_BORROW, HIS_CARER_CARD_BORROW>();
            this.beforeUpdateCarerCardBorrow = Mapper.Map<HIS_CARER_CARD_BORROW>(cardBorrowed);

            cardBorrowed.GIVE_BACK_TIME = giveBackTime;
            cardBorrowed.IS_LOST = Constant.IS_TRUE;

            if (!DAOWorker.HisCarerCardBorrowDAO.Update(cardBorrowed))
            {
                MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisCarerCardBorrow_CapNhatThatBai);
                throw new Exception("Cap nhat thong tin HisCarerCardBorrow khi bao mat the that bai." + LogUtil.TraceData("data", cardBorrowed));
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

