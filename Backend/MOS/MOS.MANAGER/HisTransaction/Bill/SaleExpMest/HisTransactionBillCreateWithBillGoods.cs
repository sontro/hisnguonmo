using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisAccountBook;
using MOS.MANAGER.HisBillGoods;
using MOS.MANAGER.HisExpMest;
using MOS.MANAGER.HisExpMest.Common;
using MOS.MANAGER.HisExpMest.Common.Auto;
using MOS.MANAGER.HisExpMest.Common.Export;
using MOS.MANAGER.HisExpMest.Common.Get;
using MOS.MANAGER.HisExpMest.Common.Update;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.MANAGER.HisTransactionExp;
using MOS.MANAGER.HisTreatment;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTransaction.Bill.SaleExpMest
{
    class HisTransactionBillCreateWithBillGoods : BusinessBase
    {
        private HisTransactionCreate hisTransactionCreate;
        private HisBillGoodsCreate hisBillGoodsCreate;
        private HisExpMestUpdate hisExpMestUpdate;
        private HisTransactionExpCreate hisTransactionExpCreate;

        private HIS_TRANSACTION recentHisTransaction;

        internal HisTransactionBillCreateWithBillGoods()
            : base()
        {
            this.Init();
        }

        internal HisTransactionBillCreateWithBillGoods(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisTransactionCreate = new HisTransactionCreate(param);
            this.hisBillGoodsCreate = new HisBillGoodsCreate(param);
            this.hisExpMestUpdate = new HisExpMestUpdate(param);
            this.hisTransactionExpCreate = new HisTransactionExpCreate(param);
        }

        internal bool Run(HisTransactionBillGoodsSDO data, ref V_HIS_TRANSACTION resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                List<HIS_EXP_MEST> expMests = new List<HIS_EXP_MEST>();
                V_HIS_ACCOUNT_BOOK hisAccountBook = null;
                HIS_TREATMENT treatment = null;
                HisTreatmentCheck treatmentChecker = new HisTreatmentCheck(param);
                HisTransactionCheck checker = new HisTransactionCheck(param);
                HisTransactionBillCheck billChecker = new HisTransactionBillCheck(param);
                HisTransactionBillCreateWithBillGoodsCheck billGoodsChecker = new HisTransactionBillCreateWithBillGoodsCheck(param);
                valid = valid && IsNotNull(data);
                valid = valid && IsNotNull(data.HisTransaction);
                valid = valid && IsNotNullOrEmpty(data.HisBillGoods);
                valid = valid && IsNotNullOrEmpty(data.ExpMestIds);
                valid = valid && billGoodsChecker.IsValidData(data, ref expMests);
                valid = valid && checker.HasNotFinancePeriod(data.HisTransaction);
                valid = valid && checker.IsUnlockAccountBook(data.HisTransaction.ACCOUNT_BOOK_ID, ref hisAccountBook);
                valid = valid && (!data.HisTransaction.TREATMENT_ID.HasValue || treatmentChecker.VerifyId(data.HisTransaction.TREATMENT_ID.Value, ref treatment));
                valid = valid && billChecker.IsGetNumOrderFromOldSystem(data.HisTransaction, hisAccountBook);
                valid = valid && checker.IsValidNumOrder(data.HisTransaction, hisAccountBook);
                if (valid)
                {
                    this.ProcessHisTransaction(data, hisAccountBook, treatment, ref expMests);
                    this.ProcessHisTransactionExp(expMests);
                    this.ProcessBillGoods(data);
                    this.ProcessExpMestSale(expMests);
                    this.PassResult(ref resultData);
                    result = true;
                    new EventLogGenerator(EventLog.Enum.HisTransaction_TaoHoaDonPhieuXuatBan, this.GetEventLog(expMests)).TransactionCode(this.recentHisTransaction.TRANSACTION_CODE).Run();
                    this.AutoExportExpMest(expMests);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                this.RollbackData();
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private void ProcessHisTransactionExp(List<HIS_EXP_MEST> expMests)
        {
            if (IsNotNullOrEmpty(expMests))
            {
                List<HIS_TRANSACTION_EXP> transactionExps = expMests.Select(o => new HIS_TRANSACTION_EXP
                {
                    EXP_MEST_ID = o.ID,
                    TDL_EXP_MEST_CODE = o.EXP_MEST_CODE,
                    TDL_MEDI_STOCK_ID = o.MEDI_STOCK_ID,
                    TRANSACTION_ID = this.recentHisTransaction.ID
                }).ToList();

                if (!this.hisTransactionExpCreate.CreateList(transactionExps))
                {
                    throw new Exception("Rollback du lieu");
                }
            }
        }

        private void ProcessHisTransaction(HisTransactionBillGoodsSDO data, V_HIS_ACCOUNT_BOOK accountBook, HIS_TREATMENT treatment, ref  List<HIS_EXP_MEST> expMests)
        {
            decimal totalGoodPrice = IsNotNull(data.HisBillGoods) ? data.HisBillGoods.Sum(s => ((s.PRICE * s.AMOUNT * (1 + (s.VAT_RATIO ?? 0))) - (s.DISCOUNT ?? 0))) : 0;

            HIS_TRANSACTION hisTransaction = data.HisTransaction;
            hisTransaction.TRANSACTION_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__TT;
            hisTransaction.SALE_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_SALE_TYPE.ID__SALE_EXP;
            hisTransaction.AMOUNT = totalGoodPrice - (expMests.Sum(o => (o.DISCOUNT ?? 0)));
            hisTransaction.TREATMENT_ID = expMests.FirstOrDefault().TDL_TREATMENT_ID;
            hisTransaction.SERE_SERV_AMOUNT = 0;
            HisTransactionUtil.SetTdl(hisTransaction, expMests.FirstOrDefault());
            if (!hisTransactionCreate.Create(hisTransaction, treatment))
            {
                throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
            }
            this.recentHisTransaction = hisTransaction;
        }

        private void ProcessBillGoods(HisTransactionBillGoodsSDO data)
        {
            if (IsNotNullOrEmpty(data.HisBillGoods))
            {
                data.HisBillGoods.ForEach(o => o.BILL_ID = this.recentHisTransaction.ID);
                if (!this.hisBillGoodsCreate.CreateList(data.HisBillGoods))
                {
                    throw new Exception("Khong tao duoc HisBillGoods cho giao dich thanh toan. Du lieu se bi Rollback.");
                }
            }
        }

        private void ProcessExpMestSale(List<HIS_EXP_MEST> expMests)
        {
            if (IsNotNullOrEmpty(expMests))
            {
                List<long> expMestIds = expMests.Select(o => o.ID).ToList();
                string query = DAOWorker.SqlDAO.AddInClause(expMestIds, "UPDATE HIS_EXP_MEST SET BILL_ID = :param1, CASHIER_LOGINNAME = :param2, CASHIER_USERNAME = :param3 WHERE %IN_CLAUSE% ", "ID");
                if (!DAOWorker.SqlDAO.Execute(query, this.recentHisTransaction.ID, this.recentHisTransaction.CASHIER_LOGINNAME, this.recentHisTransaction.CASHIER_USERNAME))
                {
                    throw new Exception("Update BILL_ID cho SaleExpMest that bai. Du lieu se bi Rollback.");
                }
            }
        }

        private void AutoExportExpMest(List<HIS_EXP_MEST> expMests)
        {
            try
            {
                if (!HisExpMestCFG.IS_AUTO_EXPORT_EXP_MEST_SALE) return;
                foreach (var expMest in expMests)
                {
                    try
                    {
                        if (expMest.EXP_MEST_STT_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__REQUEST)
                        {
                            HisExpMestResultSDO resultData = null;
                            bool approve = new HisExpMestAutoProcess().Run(expMest, ref resultData, true);
                            if (!approve)
                            {
                                MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisExpMest_TuDongDuyetThatBai);
                                return;
                            }
                            expMest.EXP_MEST_STT_ID = resultData.ExpMest.EXP_MEST_STT_ID;
                        }

                        if (expMest.EXP_MEST_STT_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__EXECUTE)
                        {
                            HIS_EXP_MEST resultData = null;
                            HisExpMestExportSDO sdo = new HisExpMestExportSDO();
                            sdo.ExpMestId = expMest.ID;
                            sdo.ReqRoomId = expMest.REQ_ROOM_ID;
                            sdo.IsFinish = true;
                            if (!new HisExpMestExport().Export(sdo, true, ref resultData))
                            {
                                MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisExpMest_TuDongXuatThatBai);
                                return;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Inventec.Common.Logging.LogSystem.Error(LogUtil.TraceData("ExpMest", expMest), ex);
                    }
                }

            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void PassResult(ref V_HIS_TRANSACTION resultData)
        {
            resultData = new HisTransactionGet().GetViewById(this.recentHisTransaction.ID);
        }

        private string GetEventLog(List<HIS_EXP_MEST> expMests)
        {
            string log = "";
            try
            {
                List<string> logs = new List<string>();
                foreach (var item in expMests)
                {
                    logs.Add(string.Format("EXP_MEST_CODE: {0}", item.EXP_MEST_CODE));
                }
                log = String.Join(". ", logs);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                log = "";
            }
            return log;
        }

        internal void RollbackData()
        {
            this.hisExpMestUpdate.RollbackData();
            this.hisBillGoodsCreate.RollbackData();
            this.hisTransactionExpCreate.RollbackData();
            this.hisTransactionCreate.RollbackData();
        }
    }
}
