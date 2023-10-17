using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisExpMest.Common.Get;
using MOS.MANAGER.HisImpMest.Import;
using MOS.MANAGER.HisMediStockImty;
using MOS.MANAGER.HisTreatment;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisImpMest.UpdateStatus
{
    class HisImpMestUpdateStatus : BusinessBase
    {
        private List<HIS_IMP_MEST> beforeUpdateHisImpMests = new List<HIS_IMP_MEST>();
        private HIS_IMP_MEST recentHisImpMest;
        private HIS_IMP_MEST beforeUpdate;
        private bool? approvalOrCancelApproval = null;

        private HisImpMestImport hisImpMestImport;

        internal HisImpMestUpdateStatus()
            : base()
        {
            this.hisImpMestImport = new HisImpMestImport(param);
        }

        internal HisImpMestUpdateStatus(CommonParam param)
            : base(param)
        {
            this.hisImpMestImport = new HisImpMestImport(param);
        }

        internal bool UpdateStatus(HIS_IMP_MEST data, bool isAuto, ref HIS_IMP_MEST resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_IMP_MEST raw = null;
                List<HIS_IMP_MEST> children = null;

                HisImpMestCheck checker = new HisImpMestCheck(param);
                HisImpMestUpdateStatusCheck statusChecker = new HisImpMestUpdateStatusCheck(param);

                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.HasNotMediStockPeriod(raw);
                valid = valid && checker.IsUnLockMediStock(raw);
                valid = valid && checker.IsAllowChangeStatus(raw.IMP_MEST_STT_ID, data.IMP_MEST_STT_ID);
                valid = valid && checker.IsValidTypeChangeStatus(raw, data.IMP_MEST_STT_ID);
                valid = valid && checker.HasNotInAggrImpMest(raw);
                valid = valid && checker.HasNoNationalCode(raw);
                valid = valid && checker.CheckMediStockPermission(raw, isAuto);
                valid = valid && statusChecker.CheckTreatmentFinished(raw, data.IMP_MEST_STT_ID);
                valid = valid && statusChecker.IsValidChildren(raw, ref children);

                if (valid)
                {
                    this.ProcessImpMest(raw, data, isAuto);
                    this.ProcessChildImpMest(children);
                    this.PassResult(ref resultData);
                    result = true;
                    if (this.approvalOrCancelApproval.HasValue)
                    {
                        if (this.approvalOrCancelApproval.Value)
                        {
                            new EventLogGenerator(EventLog.Enum.HisImpMest_DuyetPhieuNhap).ImpMestCode(this.recentHisImpMest.IMP_MEST_CODE).Run();
                        }
                        else
                        {
                            new EventLogGenerator(EventLog.Enum.HisImpMest_HuyDuyetPhieuNhap).ImpMestCode(this.recentHisImpMest.IMP_MEST_CODE).Run();
                        }
                    }
                    this.ProcessAuto();
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                this.Rollback();
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private void ProcessImpMest(HIS_IMP_MEST raw, HIS_IMP_MEST data, bool isAuto)
        {
            if (data.IMP_MEST_STT_ID == IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_STT.ID__APPROVAL
                && raw.IMP_MEST_STT_ID == IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_STT.ID__REQUEST)
            {
                this.approvalOrCancelApproval = true;
            }
            else if (data.IMP_MEST_STT_ID == IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_STT.ID__REQUEST
                && raw.IMP_MEST_STT_ID == IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_STT.ID__APPROVAL)
            {
                this.approvalOrCancelApproval = false;
            }
            Mapper.CreateMap<HIS_IMP_MEST, HIS_IMP_MEST>();
            beforeUpdate = Mapper.Map<HIS_IMP_MEST>(raw);

            raw.IMP_MEST_STT_ID = data.IMP_MEST_STT_ID;
            //neu y/c cap nhat trang thai sang phe duyet thi luu them thong tin
            if (data.IMP_MEST_STT_ID == IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_STT.ID__APPROVAL)
            {
                raw.APPROVAL_LOGINNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                raw.APPROVAL_USERNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
                raw.APPROVAL_TIME = Inventec.Common.DateTime.Convert.SystemDateTimeToTimeNumber(DateTime.Now);
            }
            //neu y/c cap nhat trang thai sang tu choi thi xoa thong tin duyet
            else if (data.IMP_MEST_STT_ID == IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_STT.ID__REJECT || data.IMP_MEST_STT_ID == IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_STT.ID__REQUEST)
            {
                raw.APPROVAL_LOGINNAME = null;
                raw.APPROVAL_USERNAME = null;
                raw.APPROVAL_TIME = null;
            }
            raw.MODIFY_TIME = Inventec.Common.DateTime.Get.Now();
            if (!DAOWorker.HisImpMestDAO.Update(raw))
            {
                MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisImpMest_CapNhatThatBai);
                throw new Exception("Cap nhat thong tin HisImpMest that bai." + LogUtil.TraceData("data", data));
            }
            this.recentHisImpMest = raw;
            this.beforeUpdateHisImpMests.Add(beforeUpdate);
        }

        private void ProcessAuto()
        {
            try
            {
                //Tu dong thuc xuat voi nhung phieu xuat duoc duyet (trang thai truoc la yeu cau)
                if (this.recentHisImpMest.IMP_MEST_STT_ID == IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_STT.ID__APPROVAL
                    && this.recentHisImpMest.IMP_MEST_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__THT)
                {
                    HIS_MEDI_STOCK_IMTY mediStockImty = new HisMediStockImtyGet().GetByMediStockIdAndImpMestTypeId(this.recentHisImpMest.MEDI_STOCK_ID, this.recentHisImpMest.IMP_MEST_TYPE_ID);

                    //Neu kho cho phep tu dong thuc xuat thi thuc hien xuat
                    if (new HisImpMestCheck().IsAutoStockTransfer(this.recentHisImpMest, this.GetExpMediStockChms(this.recentHisImpMest))
                        || (mediStockImty != null && mediStockImty.IS_AUTO_EXECUTE == MOS.UTILITY.Constant.IS_TRUE))
                    {
                        HIS_IMP_MEST resultData = null;

                        if (!new HisImpMestImport().Import(this.recentHisImpMest, true, ref resultData))
                        {
                            MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisImpMest_TuDongNhapThatBai);
                            LogSystem.Warn("Tu dong thuc nhap that bai." + LogUtil.TraceData("data", this.recentHisImpMest));
                        }
                        else
                        {
                            this.recentHisImpMest.IMP_MEST_STT_ID = resultData.IMP_MEST_STT_ID;//cap nhat lai trang thai cua du lieu truyen vao
                            this.recentHisImpMest.IMP_LOGINNAME = resultData.IMP_LOGINNAME;
                            this.recentHisImpMest.IMP_TIME = resultData.IMP_TIME;
                            this.recentHisImpMest.IMP_USERNAME = resultData.IMP_USERNAME;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }

        private void ProcessChildImpMest(List<HIS_IMP_MEST> children)
        {
            if (this.recentHisImpMest.IMP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__THT
                && IsNotNullOrEmpty(children))
            {
                Mapper.CreateMap<HIS_IMP_MEST, HIS_IMP_MEST>();
                List<HIS_IMP_MEST> befores = Mapper.Map<List<HIS_IMP_MEST>>(children);
                children.ForEach(o =>
                {
                    o.IMP_MEST_STT_ID = this.recentHisImpMest.IMP_MEST_STT_ID;
                    o.APPROVAL_LOGINNAME = this.recentHisImpMest.APPROVAL_LOGINNAME;
                    o.APPROVAL_TIME = this.recentHisImpMest.APPROVAL_TIME;
                    o.APPROVAL_USERNAME = this.recentHisImpMest.APPROVAL_USERNAME;
                    o.MODIFY_TIME = this.recentHisImpMest.MODIFY_TIME;
                });
                if (!DAOWorker.HisImpMestDAO.UpdateList(children))
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisImpMest_CapNhatThatBai);
                    throw new Exception("Cap nhat thong tin HisImpMest 'con' cua phieu nhap thong hop that bai. Rollback nghiep vu" + LogUtil.TraceData("AggrImpMest", this.recentHisImpMest));
                }
                this.beforeUpdateHisImpMests.AddRange(befores);
            }
        }

        private long? GetExpMediStockChms(HIS_IMP_MEST data)
        {
            long? result = null;
            try
            {
                if (data != null && data.IMP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__CK && data.CHMS_EXP_MEST_ID.HasValue)
                {
                    HIS_EXP_MEST expMest = new HisExpMestGet().GetById(data.CHMS_EXP_MEST_ID.Value);
                    if (expMest != null)
                    {
                        result = expMest.MEDI_STOCK_ID;
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }
            return result;
        }

        private void PassResult(ref HIS_IMP_MEST resultData)
        {
            resultData = this.recentHisImpMest;
        }

        internal void Rollback()
        {
            if (IsNotNullOrEmpty(this.beforeUpdateHisImpMests))
            {
                if (!DAOWorker.HisImpMestDAO.UpdateList(this.beforeUpdateHisImpMests))
                {
                    LogSystem.Warn("Rollback du lieu HisImpMest that bai." + LogUtil.TraceData("beforeUpdateHisImpMests", this.beforeUpdateHisImpMests));
                }
                this.beforeUpdateHisImpMests = null;
            }
        }
    }
}
