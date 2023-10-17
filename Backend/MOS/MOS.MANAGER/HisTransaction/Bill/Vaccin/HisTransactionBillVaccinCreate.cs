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
using MOS.MANAGER.HisExpMest.Common.Get;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.HisVaccination;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTransaction.Bill.Vaccin
{
    class HisTransactionBillVaccinCreate : BusinessBase
    {
        private HisTransactionCreate hisTransactionCreate;
        private HisBillGoodsCreate hisBillGoodsCreate;
        private HisVaccinationUpdate hisVaccinationUpdate;

        private HIS_TRANSACTION recentHisTransaction;

        internal HisTransactionBillVaccinCreate()
            : base()
        {
            this.Init();
        }

        internal HisTransactionBillVaccinCreate(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisTransactionCreate = new HisTransactionCreate(param);
            this.hisBillGoodsCreate = new HisBillGoodsCreate(param);
            this.hisVaccinationUpdate = new HisVaccinationUpdate(param);
        }

        internal bool Run(HisTransactionBillVaccinSDO data, ref V_HIS_TRANSACTION resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                List<HIS_EXP_MEST> expMests = new List<HIS_EXP_MEST>();
                List<HIS_VACCINATION> vaccinations = new List<HIS_VACCINATION>();
                V_HIS_ACCOUNT_BOOK hisAccountBook = null;
                HisTransactionCheck checker = new HisTransactionCheck(param);
                HisTransactionBillCheck billChecker = new HisTransactionBillCheck(param);
                HisVaccinationCheck vaccinChecker = new HisVaccinationCheck(param);

                valid = valid && IsNotNull(data);
                valid = valid && IsNotNull(data.HisTransaction);
                valid = valid && IsNotNullOrEmpty(data.HisBillGoods);
                valid = valid && IsNotNullOrEmpty(data.VaccinationIds);
                valid = valid && checker.HasNotFinancePeriod(data.HisTransaction);
                valid = valid && checker.IsUnlockAccountBook(data.HisTransaction.ACCOUNT_BOOK_ID, ref hisAccountBook);
                valid = valid && billChecker.IsGetNumOrderFromOldSystem(data.HisTransaction, hisAccountBook);
                valid = valid && checker.IsValidNumOrder(data.HisTransaction, hisAccountBook);
                valid = valid && vaccinChecker.VerifyIds(data.VaccinationIds, vaccinations);
                valid = valid && vaccinChecker.IsUnLock(vaccinations);
                valid = valid && vaccinChecker.HasNotBill(vaccinations);
                valid = valid && this.ValidData(vaccinations, ref expMests);
                if (valid)
                {
                    this.ProcessHisTransaction(data, hisAccountBook, vaccinations, expMests);
                    this.ProcessBillGoods(data);
                    this.ProcessVaccination(vaccinations);
                    this.PassResult(ref resultData);
                    result = true;
                    new EventLogGenerator(EventLog.Enum.HisTransaction_TaoHoaDonTiemVaccin, this.GetEventLog(vaccinations, expMests)).TransactionCode(this.recentHisTransaction.TRANSACTION_CODE).PatientCode(this.recentHisTransaction.TDL_PATIENT_CODE).Run();
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

        private void ProcessHisTransaction(HisTransactionBillVaccinSDO data, V_HIS_ACCOUNT_BOOK accountBook, List<HIS_VACCINATION> vaccinations, List<HIS_EXP_MEST> expMests)
        {
            decimal totalExpAmount = 0;
            decimal totalExpPrice = 0;

            decimal totalGoodAmount = IsNotNull(data.HisBillGoods) ? data.HisBillGoods.Sum(s => s.AMOUNT) : 0;
            decimal totalGoodPrice = IsNotNull(data.HisBillGoods) ? data.HisBillGoods.Sum(s => ((s.PRICE * s.AMOUNT) - (s.DISCOUNT ?? 0))) : 0;

            List<HIS_EXP_MEST_MEDICINE> listExpMestMedicine = new HisExpMestMedicineGet().GetByExpMestIds(expMests.Select(s => s.ID).ToList());
            List<HIS_EXP_MEST_MATERIAL> listExpMestMaterial = new HisExpMestMaterialGet().GetByExpMestIds(expMests.Select(s => s.ID).ToList());

            if (IsNotNullOrEmpty(listExpMestMedicine))
            {
                foreach (var item in listExpMestMedicine)
                {
                    decimal expAmount = (item.AMOUNT - (item.TH_AMOUNT ?? 0));
                    decimal expPrice = expAmount * ((item.PRICE ?? 0) * (1 + (item.VAT_RATIO ?? 0)));
                    totalExpAmount += expAmount;
                    totalExpPrice += (expPrice - (item.DISCOUNT ?? 0));
                }
            }
            if (IsNotNullOrEmpty(listExpMestMaterial))
            {
                foreach (var item in listExpMestMaterial)
                {
                    decimal expAmount = (item.AMOUNT - (item.TH_AMOUNT ?? 0));
                    decimal expPrice = expAmount * ((item.PRICE ?? 0) * (1 + (item.VAT_RATIO ?? 0)));
                    totalExpAmount += expAmount;
                    totalExpPrice += (expPrice - (item.DISCOUNT ?? 0));
                }
            }

            if (totalExpAmount != totalGoodAmount)
            {
                BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                throw new Exception("So luong trong expMest Khac voi so luong trong Goods: ExpMestAmount: " + totalExpAmount + "; GoodsAmount: " + totalGoodAmount);
            }
            if (totalExpPrice != totalGoodPrice)
            {
                BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                throw new Exception("Tong tien trong expMest Khac voi so luong trong Goods: ExpMestPrice: " + totalExpPrice + "; GoodsPrice: " + totalGoodPrice);
            }

            HIS_TRANSACTION hisTransaction = data.HisTransaction;
            hisTransaction.TRANSACTION_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__TT;
            hisTransaction.SALE_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_SALE_TYPE.ID__SALE_VACCIN;
            hisTransaction.AMOUNT = totalGoodPrice - (expMests.Sum(o => (o.DISCOUNT ?? 0)));
            hisTransaction.TREATMENT_ID = expMests.FirstOrDefault().TDL_TREATMENT_ID;

            HisTransactionUtil.SetTdl(hisTransaction, vaccinations.FirstOrDefault());
            hisTransaction.SERE_SERV_AMOUNT = 0;
            if (!hisTransactionCreate.Create(hisTransaction, null))
            {
                throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
            }
            this.recentHisTransaction = hisTransaction;
        }

        private void ProcessBillGoods(HisTransactionBillVaccinSDO data)
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

        private void ProcessVaccination(List<HIS_VACCINATION> vaccinations)
        {
            Mapper.CreateMap<HIS_VACCINATION, HIS_VACCINATION>();
            List<HIS_VACCINATION> befores = Mapper.Map<List<HIS_VACCINATION>>(vaccinations);
            vaccinations.ForEach(o =>
            {
                o.BILL_ID = this.recentHisTransaction.ID;
            });

            if (!this.hisVaccinationUpdate.UpdateList(vaccinations, befores))
            {
                throw new Exception("Update BILL_ID cho HIS_VACCINATION that bai. Du lieu se bi Rollback.");
            }
        }

        private bool ValidData(List<HIS_VACCINATION> vaccinations, ref List<HIS_EXP_MEST> expMests)
        {
            bool result = true;
            try
            {
                if (vaccinations.GroupBy(g => g.PATIENT_ID).Count() > 1)
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("Cac yeu cau tiem khong cung mot benh nhan");
                }

                expMests = new HisExpMestGet().GetByVaccinationIds(vaccinations.Select(s => s.ID).ToList());

                if (expMests.Any(a => a.EXP_MEST_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__VACCIN))
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("Phieu xuat khong phai la xuat vaccin");
                }

                if (expMests.Any(a => a.EXP_MEST_STT_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__DRAFT || a.EXP_MEST_STT_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__REJECT))
                {
                    long sttId = expMests.FirstOrDefault(o => o.EXP_MEST_STT_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__DRAFT || o.EXP_MEST_STT_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__REJECT).EXP_MEST_STT_ID;
                    string sttName = HisExpMestSttCFG.DATA.FirstOrDefault(o => o.ID == sttId).EXP_MEST_STT_NAME;
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisExpMest_PhieuXuatDangOTrangThai, sttName);
                    throw new Exception("Khong cho phep thanh toan khi phieu xuat ban o trang thai nhap hoac khong duyet STT_ID: " + sttId);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private void PassResult(ref V_HIS_TRANSACTION resultData)
        {
            resultData = new HisTransactionGet().GetViewById(this.recentHisTransaction.ID);
        }

        private string GetEventLog(List<HIS_VACCINATION> vaccinations, List<HIS_EXP_MEST> expMests)
        {
            string log = "";
            try
            {
                List<string> logs = new List<string>();
                foreach (var item in vaccinations)
                {
                    string expMestCode = expMests.FirstOrDefault(o => o.VACCINATION_ID == item.ID).EXP_MEST_CODE;
                    logs.Add(string.Format("VACCINATION_CODE: {0} (EXP_MEST_CODE: {1})", item.VACCINATION_CODE, expMestCode));
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
            this.hisVaccinationUpdate.RollbackData();
            this.hisBillGoodsCreate.RollbackData();
            this.hisTransactionCreate.RollbackData();
        }
    }
}
