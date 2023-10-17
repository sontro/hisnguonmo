using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisAccountBook;
using MOS.MANAGER.HisBlood;
using MOS.MANAGER.HisExpMest;
using MOS.MANAGER.HisExpMest.Common;
using MOS.MANAGER.HisExpMest.Common.Get;
using MOS.MANAGER.HisExpMestBlood;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.MANAGER.HisImpMestBlood;
using MOS.MANAGER.HisImpMestMaterial;
using MOS.MANAGER.HisImpMestMedicine;
using MOS.MANAGER.HisImpMestUser;
using MOS.MANAGER.HisMaterial;
using MOS.MANAGER.HisMaterialPaty;
using MOS.MANAGER.HisMedicine;
using MOS.MANAGER.HisMedicinePaty;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisSereServBill;
using MOS.MANAGER.HisSereServDeposit;
using MOS.MANAGER.HisTransaction;
using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.HisUserRoom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisImpMest.Common.Delete
{
    partial class HisImpMestTruncate : BusinessBase
    {
        private MaterialProcessor materialProcessor;
        private MedicineProcessor medicineProcessor;
        private BloodProcessor bloodProcessor;
        private SereServProcessor sereServProcessor;
        private TransactionProcessor transactionProcessor;
        private AutoProcessor autoProcessor;

        internal HisImpMestTruncate()
            : base()
        {
            this.Init();
        }

        internal HisImpMestTruncate(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.materialProcessor = new MaterialProcessor(param);
            this.medicineProcessor = new MedicineProcessor(param);
            this.bloodProcessor = new BloodProcessor(param);
            this.sereServProcessor = new SereServProcessor(param);
            this.transactionProcessor = new TransactionProcessor(param);
            this.autoProcessor = new AutoProcessor();
        }

        internal bool Truncate(HIS_IMP_MEST data)
        {
            bool result = false;
            try
            {
                result = this.Truncate(data.ID, false);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        internal bool Truncate(long id, bool isAuto)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_IMP_MEST raw = null;
                List<HIS_IMP_MEST_MEDICINE> impMestMedicines = null;
                List<HIS_IMP_MEST_MATERIAL> impMestMaterials = null;
                List<HIS_IMP_MEST_BLOOD> impMestBloods = null;

                HIS_EXP_MEST expMest = null;
                List<HIS_EXP_MEST_MEDICINE> expMestMedicines = null;
                List<HIS_EXP_MEST_MATERIAL> expMestMaterials = null;
                List<HIS_EXP_MEST_BLOOD> expMestBloods = null;
                List<HIS_SERE_SERV> hisSereServs = null;
                HIS_TRANSACTION oldTransaction = null;
                V_HIS_ACCOUNT_BOOK accountBook = null;
                List<HIS_EXP_MEST> expMestForTrans = null;
                long? cancelTime = null;
                HIS_TRANSACTION newTrans = null;

                HisImpMestCheck checker = new HisImpMestCheck(param);
                valid = valid && checker.VerifyId(id, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.HasNotInAggrImpMest(raw);//thuoc phieu nhap tong hop, ko cho xoa
                valid = valid && checker.VerifyStatusForDelete(raw);
                valid = valid && checker.HasNoNationalCode(raw);
                valid = valid && this.GetData(raw, ref impMestMedicines, ref impMestMaterials, ref impMestBloods);
                valid = valid && this.CheckMobaPrescription(raw, ref expMest, ref expMestMedicines, ref expMestMaterials, ref expMestBloods, ref hisSereServs);
                valid = valid && this.CheckChms(raw, ref expMest, ref expMestMedicines, ref expMestMaterials, ref expMestBloods);
                valid = valid && this.ValidDataTransaction(expMest, ref oldTransaction, ref expMestForTrans, ref cancelTime, ref accountBook);
                if (valid)
                {
                    if (!isAuto && raw.IMP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__BL)
                    {
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisImpMest_KhongChoPhepXoaPhieuNhapBuLe);
                        throw new Exception("Khong cho phep xoa phieu nhap bu le tu phan mem frontend");
                    }
                    List<string> sqls = new List<string>();
                    Dictionary<HIS_EXP_MEST_MEDICINE, decimal> dicMedicineThAmount = new Dictionary<HIS_EXP_MEST_MEDICINE, decimal>();
                    Dictionary<HIS_EXP_MEST_MATERIAL, decimal> dicMaterialThAmount = new Dictionary<HIS_EXP_MEST_MATERIAL, decimal>();

                    if (!this.materialProcessor.Run(raw, impMestMaterials, expMestMaterials, ref sqls, ref dicMaterialThAmount))
                    {
                        throw new Exception("materialProcessor: Ket thuc nghiep vu");
                    }

                    if (!this.medicineProcessor.Run(raw, impMestMedicines, expMestMedicines, ref sqls, ref dicMedicineThAmount))
                    {
                        throw new Exception("medicineProcessor: Ket thuc nghiep vu");
                    }

                    if (!this.bloodProcessor.Run(raw, impMestBloods, expMestBloods, ref sqls))
                    {
                        throw new Exception("bloodProcessor: Ket thuc nghiep vu");
                    }

                    this.ProcessImpMestUser(raw, ref sqls);
                    this.ProcessImpMest(raw, ref sqls);

                    if (!this.sereServProcessor.Run(raw, expMest, hisSereServs, dicMedicineThAmount, dicMaterialThAmount, impMestBloods))
                    {
                        throw new Exception("sereServProcessor: Ket thuc nghiep vu");
                    }

                    if (!this.transactionProcessor.Run(oldTransaction, expMestForTrans, cancelTime, accountBook, ref sqls, ref newTrans))
                    {
                        throw new Exception("transactionProcessor: Ket thuc nghiep vu");
                    }

                    if (IsNotNullOrEmpty(sqls) && !DAOWorker.SqlDAO.Execute(sqls))
                    {
                        throw new Exception("Ket thuc nghiep vu: " + string.Join(";", sqls));
                    }

                    new EventLogGenerator(EventLog.Enum.HisImpMest_HuyPhieuNhap).ImpMestCode(raw.IMP_MEST_CODE).Run();
                    result = true;

                    //Xu ly du tong huy thuc xuat cac phieu xuat chuyen kho theo cau hinh
                    this.ProcessAuto(expMest);
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
        /// Kiem tra neu phieu la nhap thu hoi don thuoc va hsdt da ket thuc thi khong cho xoa
        /// </summary>
        /// <param name="data"></param>
        private bool CheckMobaPrescription(HIS_IMP_MEST data, ref HIS_EXP_MEST expMest, ref List<HIS_EXP_MEST_MEDICINE> expMestMedicines, ref List<HIS_EXP_MEST_MATERIAL> expMestMaterials, ref List<HIS_EXP_MEST_BLOOD> expMestBloods, ref List<HIS_SERE_SERV> hisSereServs)
        {
            bool valid = true;
            try
            {
                if (HisImpMestContanst.TYPE_MOBA_IDS.Contains(data.IMP_MEST_TYPE_ID))
                {
                    if (!data.MOBA_EXP_MEST_ID.HasValue)
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception("Imp_mest ko co moba_exp_mest_id" + LogUtil.TraceData("impMest", data));
                    }
                    expMest = new HisExpMestGet().GetById(data.MOBA_EXP_MEST_ID.Value);
                    if (expMest == null)
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                        throw new Exception("Khong lay duoc HIS_EXP_MEST theo data.MOBA_EXP_MEST_ID: " + data.MOBA_EXP_MEST_ID);
                    }
                    if (expMest != null && expMest.TDL_TREATMENT_ID.HasValue)
                    {
                        HIS_TREATMENT hisTreatment = new HisTreatmentGet().GetById(expMest.TDL_TREATMENT_ID.Value);
                        if (hisTreatment == null)
                        {
                            BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                            throw new Exception("Khong lay duoc HisTreatment theo tdl_treatment_id trong precription: " + expMest.TDL_TREATMENT_ID);
                        }
                        if (hisTreatment.IS_PAUSE == MOS.UTILITY.Constant.IS_TRUE)
                        {
                            MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisImpMest_KhongChoPhepXoaYeuCauThuHoiCuaDonThuocVoiHSDTDaKetThucMDT, hisTreatment.TREATMENT_CODE);
                            throw new Exception("Phieu thu hoi cua don thuoc ma HSDT da ket thuc" + LogUtil.TraceData("expMest", expMest));
                        }
                        hisSereServs = new HisSereServGet(param).GetByServiceReqId(expMest.SERVICE_REQ_ID ?? 0);
                    }

                    expMestMedicines = new HisExpMestMedicineGet().GetExportedByExpMestId(data.MOBA_EXP_MEST_ID.Value);
                    expMestMaterials = new HisExpMestMaterialGet().GetExportedByExpMestId(data.MOBA_EXP_MEST_ID.Value);
                    expMestBloods = new HisExpMestBloodGet().GetByExpMestId(data.MOBA_EXP_MEST_ID.Value);
                }
                else if (data.IMP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__DONKTL)
                {
                    expMestMedicines = new HisExpMestMedicineGet().GetExportedByExpMestId(data.MOBA_EXP_MEST_ID.Value);
                    expMestMaterials = new HisExpMestMaterialGet().GetExportedByExpMestId(data.MOBA_EXP_MEST_ID.Value);
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

        private bool CheckChms(HIS_IMP_MEST data, ref HIS_EXP_MEST expMest, ref List<HIS_EXP_MEST_MEDICINE> expMestMedicines, ref List<HIS_EXP_MEST_MATERIAL> expMestMaterials, ref List<HIS_EXP_MEST_BLOOD> expMestBloods)
        {
            bool valid = true;
            try
            {
                if (HisImpMestContanst.TYPE_CHMS_IDS.Contains(data.IMP_MEST_TYPE_ID))
                {
                    if (!data.CHMS_EXP_MEST_ID.HasValue)
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception("Imp_mest ko co chms_exp_mest_id" + LogUtil.TraceData("impMest", data));
                    }

                    expMest = new HisExpMestGet().GetById(data.CHMS_EXP_MEST_ID.Value);
                    if (expMest == null)
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                        throw new Exception("Khong lay duoc HIS_EXP_MEST theo data.CHMS_EXP_MEST_ID: " + data.CHMS_EXP_MEST_ID);
                    }
                    expMestMedicines = new HisExpMestMedicineGet().GetExportedByExpMestId(data.CHMS_EXP_MEST_ID.Value);
                    expMestMaterials = new HisExpMestMaterialGet().GetExportedByExpMestId(data.CHMS_EXP_MEST_ID.Value);
                    expMestBloods = new HisExpMestBloodGet().GetByExpMestId(data.CHMS_EXP_MEST_ID.Value);
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

        private bool GetData(HIS_IMP_MEST data, ref  List<HIS_IMP_MEST_MEDICINE> impMestMedicines, ref List<HIS_IMP_MEST_MATERIAL> impMestMaterials, ref List<HIS_IMP_MEST_BLOOD> impMestBloods)
        {
            impMestMedicines = new HisImpMestMedicineGet().GetByImpMestId(data.ID);
            impMestMaterials = new HisImpMestMaterialGet().GetByImpMestId(data.ID);
            impMestBloods = new HisImpMestBloodGet().GetByImpMestId(data.ID);
            return true;
        }

        private void ProcessImpMestUser(HIS_IMP_MEST data, ref List<string> executeSqls)
        {
            List<HIS_IMP_MEST_USER> listImpMestUser = new HisImpMestUserGet().GetByImpMestId(data.ID);
            if (IsNotNullOrEmpty(listImpMestUser))
            {
                bool valid = true;
                HisImpMestUserCheck checker = new HisImpMestUserCheck(param);
                foreach (var impMestUser in listImpMestUser)
                {
                    valid = valid && checker.IsUnLock(impMestUser);
                }
                if (!valid)
                {
                    throw new Exception("Ket thuc nghiep vu");
                }

                string deleteImpMestUser = new StringBuilder().Append("DELETE HIS_IMP_MEST_USER WHERE IMP_MEST_ID = ").Append(data.ID).ToString();
                executeSqls.Add(deleteImpMestUser);
            }
        }

        private void ProcessImpMest(HIS_IMP_MEST data, ref List<string> executeSqls)
        {
            if (data.IMP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__THT)
            {
                List<HIS_IMP_MEST> listChildHisImpMest = new HisImpMestGet().GetByAggrImpMestId(data.ID);
                if (IsNotNullOrEmpty(listChildHisImpMest))
                {
                    bool valid = true;
                    HisImpMestCheck childChecker = new HisImpMestCheck(param);
                    foreach (var child in listChildHisImpMest)
                    {
                        valid = valid && childChecker.IsUnLock(child);
                        valid = valid && childChecker.VerifyStatusForDelete(child);
                    }
                    if (!valid)
                    {
                        throw new Exception("Ket thuc nghiep vu");
                    }

                    string updateChild = new StringBuilder().Append("UPDATE HIS_IMP_MEST SET AGGR_IMP_MEST_ID = NULL WHERE AGGR_IMP_MEST_ID = ").Append(data.ID).ToString();
                    executeSqls.Add(updateChild);
                }
            }
            string deleteImpMest = new StringBuilder().Append("DELETE HIS_IMP_MEST WHERE ID = ").Append(data.ID).ToString();
            executeSqls.Add(deleteImpMest);
        }

        private bool ValidDataTransaction(HIS_EXP_MEST expMest, ref HIS_TRANSACTION transaction, ref List<HIS_EXP_MEST> expMestForTrans, ref long? cancelTime, ref V_HIS_ACCOUNT_BOOK accountBook)
        {
            bool valid = true;
            try
            {
                if (!HisImpMestCFG.IS_AUTO_CREATE_TRAN_WHEN_MOBA_EXP_MEST_SALE)
                {
                    return true;
                }

                if (expMest == null || expMest.EXP_MEST_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__BAN || !expMest.BILL_ID.HasValue)
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

        private void ProcessAuto(HIS_EXP_MEST expMest)
        {
            try
            {
                this.autoProcessor.Run(expMest);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void RollbackData()
        {
            this.transactionProcessor.Rollback();
            this.sereServProcessor.Rollback();
            this.medicineProcessor.Rollback();
            this.materialProcessor.Rollback();
        }
    }
}
