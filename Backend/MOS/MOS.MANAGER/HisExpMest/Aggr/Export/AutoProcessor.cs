using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisImpMest.Chms;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Aggr.Export
{
    /// <summary>
    /// Xu ly nghiep vu tu dong khi thuc xuat phieu linh:
    /// - Neu phieu lĩnh có chứa phiếu "bù lẻ" và có cấu hình tự động tạo phiếu nhập chuyển kho thì tự động tạo phiếu nhập chuyển kho tương ứng với phiếu "bù lẻ"
    /// </summary>
    class AutoProcessor : BusinessBase
    {
        private HisImpMestChmsCreate hisImpMestChmsCreate;

        private List<long> recentExpMestIds;
        private List<long> recentServiceReqIds;

        internal AutoProcessor()
            : base()
        {
            this.hisImpMestChmsCreate = new HisImpMestChmsCreate(param);
        }

        internal AutoProcessor(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.hisImpMestChmsCreate = new HisImpMestChmsCreate(param);
        }

        internal bool Run(List<HIS_EXP_MEST> children, long finishTime)
        {
            try
            {
                //Kiem tra xem co phieu bu le nao khong
                List<HIS_EXP_MEST> complements = IsNotNullOrEmpty(children) ? children
                    .Where(o => o.EXP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__BL
                        && o.IMP_MEDI_STOCK_ID.HasValue
                        && HisMediStockCFG.DATA.Exists(t => t.ID == o.IMP_MEDI_STOCK_ID.Value && t.IS_AUTO_CREATE_CHMS_IMP == Constant.IS_TRUE)
                    ).ToList() : null;

                if (IsNotNullOrEmpty(complements))
                {
                    foreach (HIS_EXP_MEST expMest in complements)
                    {
                        HIS_IMP_MEST impMest = new HIS_IMP_MEST();
                        impMest.MEDI_STOCK_ID = expMest.IMP_MEDI_STOCK_ID.Value;
                        impMest.IMP_MEST_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__BL;
                        impMest.REQ_ROOM_ID = expMest.REQ_ROOM_ID;
                        impMest.REQ_LOGINNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                        impMest.REQ_USERNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
                        impMest.IMP_MEST_STT_ID = IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_STT.ID__REQUEST;
                        impMest.CHMS_EXP_MEST_ID = expMest.ID;
                        impMest.TDL_CHMS_EXP_MEST_CODE = expMest.EXP_MEST_CODE;

                        HisImpMestResultSDO resultData = null;

                        if (!this.hisImpMestChmsCreate.Create(impMest, ref resultData))
                        {
                            MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisExpMest_TuDongTaoPhieuNhapThatBai);
                        }
                    }
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


        private void FinishExpMest(List<HIS_EXP_MEST> children, long finishTime)
        {
            if (IsNotNullOrEmpty(children))
            {
                List<long> expMestIds = children.Select(o => o.ID).ToList();
                string query = DAOWorker.SqlDAO.AddInClause(expMestIds, "UPDATE HIS_EXP_MEST SET EXP_MEST_STT_ID = :param1, FINISH_TIME = :param2 WHERE %IN_CLAUSE% ", "ID");
                if (!DAOWorker.SqlDAO.Execute(query, IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__DONE, finishTime))
                {
                    throw new Exception("Cap nhat trang thai exp_mest cac phieu con that bai. Rollback du lieu. Ket thuc nghiep vu");
                }
                this.recentExpMestIds = expMestIds; //phuc vu rollback
            }
        }

        private void FinishServiceReq(List<HIS_EXP_MEST> children, long finishTime)
        {
            List<long> serviceReqIds = children
                .Where(o => o.SERVICE_REQ_ID.HasValue)
                .Select(o => o.SERVICE_REQ_ID.Value).ToList();
            if (IsNotNullOrEmpty(serviceReqIds))
            {
                string query = DAOWorker.SqlDAO.AddInClause(serviceReqIds, "UPDATE HIS_SERVICE_REQ SET SERVICE_REQ_STT_ID = :param1, FINISH_TIME = :param2, EXECUTE_LOGINNAME = :param3, EXECUTE_USERNAME = :param4 WHERE %IN_CLAUSE% ", "ID");

                long sttId = IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__HT;
                string loginName = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                string userName = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
                if (!DAOWorker.SqlDAO.Execute(query, sttId, finishTime, loginName, userName))
                {
                    throw new Exception("Cap nhat trang thai service_req cac phieu con that bai. Rollback du lieu. Ket thuc nghiep vu");
                }
                this.recentServiceReqIds = serviceReqIds; //phuc vu rollback
            }
        }

        internal void Rollback()
        {
            try
            {
                this.UnfinishExpMest(this.recentExpMestIds);
                this.UnfinishServiceReq(this.recentServiceReqIds);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }

        private void UnfinishExpMest(List<long> expMestIds)
        {
            if (IsNotNullOrEmpty(expMestIds))
            {
                string query = DAOWorker.SqlDAO.AddInClause(expMestIds, "UPDATE HIS_EXP_MEST SET EXP_MEST_STT_ID = :param1, FINISH_TIME = NULL WHERE %IN_CLAUSE% ", "ID");
                if (!DAOWorker.SqlDAO.Execute(query, IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__EXECUTE))
                {
                    LogSystem.Warn("Rollback trang thai exp_mest cac phieu con that bai. Rollback du lieu. Ket thuc nghiep vu");
                }
                this.recentExpMestIds = null;
            }
        }

        private void UnfinishServiceReq(List<long> serviceReqIds)
        {
            if (IsNotNullOrEmpty(serviceReqIds))
            {
                string query = DAOWorker.SqlDAO.AddInClause(serviceReqIds, "UPDATE HIS_SERVICE_REQ SET SERVICE_REQ_STT_ID = :param1, FINISH_TIME = NULL, EXECUTE_LOGINNAME = NULL, EXECUTE_USERNAME = NULL WHERE %IN_CLAUSE% ", "ID");
                if (!DAOWorker.SqlDAO.Execute(query, IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__DXL))
                {
                    LogSystem.Warn("Rollback trang thai service_req cac phieu con that bai. Rollback du lieu. Ket thuc nghiep vu");
                }
                this.recentServiceReqIds = null;
            }
        }
    }
}
