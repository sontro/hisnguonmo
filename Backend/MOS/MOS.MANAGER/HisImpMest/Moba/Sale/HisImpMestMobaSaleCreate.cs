using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisAccountBook;
using MOS.MANAGER.HisExpMest.Common;
using MOS.MANAGER.HisExpMest.Common.Get;
using MOS.MANAGER.HisImpMestMaterial;
using MOS.MANAGER.HisImpMestMedicine;
using MOS.MANAGER.HisTransaction;
using MOS.MANAGER.HisTransaction.Bill;
using MOS.MANAGER.HisUserRoom;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisImpMest.Moba.Sale
{
    class HisImpMestMobaSaleCreate : BusinessBase
    {
        private List<HIS_IMP_MEST_MATERIAL> recentHisImpMestMaterials;
        private List<HIS_IMP_MEST_MEDICINE> recentHisImpMestMedicines;
        private HIS_IMP_MEST recentHisImpMest;
        private HIS_EXP_MEST expMest;
        private HIS_TRANSACTION recentTransaction = null;

        private HisImpMestCreate hisImpMestCreate;
        private ImpMestMaterialProcessor impMestMaterialProcessor;
        private ImpMestMedicineProcessor impMestMedicineProcessor;
        private TransactionProcessor transactionProcessor;
        private HisImpMestAutoProcess hisImpMestAutoProcess;

        internal HisImpMestMobaSaleCreate()
            : base()
        {
            this.Init();
        }

        internal HisImpMestMobaSaleCreate(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisImpMestCreate = new HisImpMestCreate(param);
            this.impMestMaterialProcessor = new ImpMestMaterialProcessor(param);
            this.impMestMedicineProcessor = new ImpMestMedicineProcessor(param);
            this.transactionProcessor = new TransactionProcessor(param);
            this.hisImpMestAutoProcess = new HisImpMestAutoProcess(param);
        }

        internal bool Create(HisImpMestMobaSaleSDO data, ref HisImpMestResultSDO resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisImpMestMobaCheck checker = new HisImpMestMobaCheck(param);
                HIS_TRANSACTION oldTransaction = null;
                V_HIS_ACCOUNT_BOOK accountBook = null;
                List<HIS_EXP_MEST> expMestForTrans = null;
                long? cancelTime = null;
                HIS_IMP_MEST impMest = new HIS_IMP_MEST();
                valid = valid && checker.ValidateDataSale(data);
                valid = valid && checker.VerifyExpMestId(impMest, data.ExpMestId, null, ref this.expMest);
                valid = valid && checker.CheckValidMobaSale(impMest, this.expMest);
                valid = valid && checker.CheckValidRequestRoom(impMest, this.expMest, data.RequestRoomId);
                valid = valid && this.ValidDataTransaction(data, impMest, ref oldTransaction, ref expMestForTrans, ref cancelTime, ref accountBook);
                if (valid)
                {
                    List<string> sqls = new List<string>();
                    this.ProcessHisImpMest(data, impMest);

                    if (!this.impMestMaterialProcessor.Run(this.recentHisImpMest, data.MobaMaterials, this.expMest, ref this.recentHisImpMestMaterials))
                    {
                        throw new Exception("impMestMaterialProcessor. Ket thuc nghiep vu. Rollback du lieu");
                    }

                    if (!this.impMestMedicineProcessor.Run(this.recentHisImpMest, data.MobaMedicines, this.expMest, ref this.recentHisImpMestMedicines))
                    {
                        throw new Exception("impMestMedicineProcessor. Ket thuc nghiep vu. Rollback du lieu");
                    }

                    if (!this.transactionProcessor.Run(oldTransaction, expMestForTrans, cancelTime, accountBook, ref sqls, ref this.recentTransaction))
                    {
                        throw new Exception("transactionProcessor. Ket thuc nghiep vu. Rollback du lieu");
                    }

                    if (IsNotNullOrEmpty(sqls) && !DAOWorker.SqlDAO.Execute(sqls))
                    {
                        throw new Exception("sqls: " + sqls.ToString());
                    }

                    this.PassResult(ref resultData);
                    result = true;

                    this.ProcessEventLog(oldTransaction);

                    this.ProcessAuto();

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

        /// <summary>
        /// Xu ly thong tin phieu nhap
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private void ProcessHisImpMest(HisImpMestMobaSaleSDO data, HIS_IMP_MEST impMest)
        {
            impMest.IMP_MEST_STT_ID = IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_STT.ID__REQUEST;
            impMest.TDL_MOBA_EXP_MEST_CODE = this.expMest.EXP_MEST_CODE;
            impMest.DESCRIPTION = data.Description;
            impMest.TDL_PATIENT_NAME = expMest.TDL_PATIENT_NAME;
            HisImpMestUtil.SetTdl(impMest, expMest);
            if (!this.hisImpMestCreate.Create(impMest))
            {
                throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
            }
            this.recentHisImpMest = impMest;
        }

        private bool ValidDataTransaction(HisImpMestMobaSaleSDO data, HIS_IMP_MEST impMest, ref HIS_TRANSACTION transaction, ref List<HIS_EXP_MEST> expMestForTrans, ref long? cancelTime, ref V_HIS_ACCOUNT_BOOK accountBook)
        {
            bool valid = true;
            try
            {
                if (!HisImpMestCFG.IS_AUTO_CREATE_TRAN_WHEN_MOBA_EXP_MEST_SALE)
                {
                    return true;
                }

                if (!this.expMest.BILL_ID.HasValue)
                {
                    return true;
                }

                HIS_TRANSACTION raw = null;
                string loginname = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                HisTransactionCheck checker = new HisTransactionCheck(param);
                HisExpMestCheck expMestChecker = new HisExpMestCheck(param);
                cancelTime = Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmmss"));
                valid = valid && checker.VerifyId(expMest.BILL_ID.Value, ref raw);
                valid = valid && checker.IsNotCollect(raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.IsUnCancel(raw);
                valid = valid && checker.HasNotFinancePeriod(raw);
                valid = valid && checker.HasNoNationalCode(raw);
                valid = valid && this.HasNotTransactionSaleExpMest(this.expMest, raw);
                valid = valid && this.IsValidTime(cancelTime.Value, raw.TRANSACTION_TIME);
                valid = valid && this.ValidCashierRoom(raw.CASHIER_ROOM_ID, loginname);
                valid = valid && checker.IsUnlockAccountBook(raw.ACCOUNT_BOOK_ID, ref accountBook);
                valid = valid && this.ValidAccountBook(accountBook, raw.CASHIER_ROOM_ID, loginname);
                if (valid)
                {
                    expMestForTrans = new HisExpMestGet().GetByBillId(raw.ID);
                    if (!IsNotNullOrEmpty(expMestForTrans))
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                        throw new Exception(" expMestForTrans is null. TransactionId: " + raw.ID);
                    }
                    valid = valid && expMestChecker.IsUnlock(expMestForTrans);
                    if (valid) transaction = raw;
                }

            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                valid = false;
            }
            return valid;
        }

        private bool HasNotTransactionSaleExpMest(HIS_EXP_MEST expMest, HIS_TRANSACTION raw)
        {
            bool valid = true;
            try
            {
                if (!this.expMest.BILL_ID.HasValue || raw == null)
                    return true;
                if (HisExpMestCFG.DO_NOT_ALLOW_MOBA_HAS_TRANSACTION_SALE_EXP_MEST)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_PhieuXuatDaDuocThanhToan, expMest.EXP_MEST_CODE, raw.TRANSACTION_CODE);
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        private bool IsValidTime(long cancelTime, long transactionTime)
        {
            bool valid = true;
            try
            {
                if (cancelTime < transactionTime)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTransaction_ThoiGianHuyGiaoDichNhoHonThoiGianGiaoDich);
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        private bool ValidAccountBook(V_HIS_ACCOUNT_BOOK accountBook, long cashierRoomId, string loginname)
        {
            bool valid = true;
            try
            {
                HisAccountBookViewFilterQuery filter = new HisAccountBookViewFilterQuery();
                filter.LOGINNAME = loginname;
                filter.CASHIER_ROOM_ID = cashierRoomId;
                List<V_HIS_ACCOUNT_BOOK> books = new HisAccountBookGet().GetView(filter);
                if (books == null || !books.Any(a => a.ID == accountBook.ID))
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTransaction_NguoiDungKhongCoQuyenSuDungSoThuChi, accountBook.ACCOUNT_BOOK_NAME);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                valid = false;
            }
            return valid;
        }

        private bool ValidCashierRoom(long cashierRoomId, string loginname)
        {
            bool valid = true;
            try
            {
                var cashierRoom = HisCashierRoomCFG.DATA.FirstOrDefault(o => o.ID == cashierRoomId);
                HisUserRoomFilterQuery filter = new HisUserRoomFilterQuery();
                filter.LOGINNAME__EXACT = loginname;
                filter.ROOM_ID = cashierRoom.ROOM_ID;
                List<HIS_USER_ROOM> userRooms = new HisUserRoomGet().Get(filter);
                if (userRooms == null || userRooms.Count <= 0)
                {
                    MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTransaction_NguoiDungKhongCoQuyenSuDungPhong, cashierRoom.CASHIER_ROOM_NAME);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                valid = false;
            }
            return valid;
        }

        private void ProcessEventLog(HIS_TRANSACTION oldTransaction)
        {
            try
            {
                string oldCode = "";
                string newCode = "";
                if (oldTransaction != null)
                {
                    oldCode = String.Format("TRANSACTION_CODE: {0}", oldTransaction.TRANSACTION_CODE);
                }
                if (this.recentTransaction != null)
                {
                    newCode = String.Format("TRANSACTION_CODE: {0}", this.recentTransaction.TRANSACTION_CODE);
                }
                else
                {
                    newCode = LogCommonUtil.GetEventLogContent(EventLog.Enum.KhongTaoHoaDonMoi);
                }
                if (oldTransaction != null)
                {
                    new EventLogGenerator(EventLog.Enum.HisImpMest_TaoPhieuNhapThuHoiDonXuatBanVaHuyGiaoDich, oldCode, newCode).ImpMestCode(this.recentHisImpMest.IMP_MEST_CODE).ExpMestCode(this.expMest.EXP_MEST_CODE).Run();
                }
                else
                {
                    new EventLogGenerator(EventLog.Enum.HisImpMest_TaoPhieuNhapThuHoiDonXuatBan).ImpMestCode(this.recentHisImpMest.IMP_MEST_CODE).ExpMestCode(this.expMest.EXP_MEST_CODE).Run();
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void ProcessAuto()
        {
            try
            {
                this.hisImpMestAutoProcess.Run(this.recentHisImpMest);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void PassResult(ref HisImpMestResultSDO resultData)
        {
            resultData = new HisImpMestResultSDO();
            resultData.ImpMest = new HisImpMestGet().GetViewById(this.recentHisImpMest.ID);
            if (IsNotNullOrEmpty(this.recentHisImpMestMedicines))
            {
                resultData.ImpMedicines = new HisImpMestMedicineGet().GetViewByImpMestId(this.recentHisImpMest.ID);
            }

            if (IsNotNullOrEmpty(this.recentHisImpMestMaterials))
            {
                resultData.ImpMaterials = new HisImpMestMaterialGet().GetViewByImpMestId(this.recentHisImpMest.ID);
            }
        }

        internal void RollbackData()
        {
            this.transactionProcessor.Rollback();
            this.impMestMedicineProcessor.Rollback();
            this.impMestMaterialProcessor.Rollback();
            this.hisImpMestCreate.RollbackData();
        }
    }
}
