using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisBillGoods;
using MOS.MANAGER.HisExpMest.Common.Update;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.MANAGER.HisTransaction;
using MOS.MANAGER.HisTransaction.Bill;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisImpMest.Moba.Sale
{
    class TransactionProcessor : BusinessBase
    {
        private HisTransactionCreate hisTransactionCreate;
        private HisBillGoodsCreate hisBillGoodsCreate;
        private HisTransactionUpdate hisTransactionUpdate;

        internal TransactionProcessor(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisBillGoodsCreate = new HisBillGoodsCreate(param);
            this.hisTransactionCreate = new HisTransactionCreate(param);
            this.hisTransactionUpdate = new HisTransactionUpdate(param);
        }

        internal bool Run(HIS_TRANSACTION oldTransaction, List<HIS_EXP_MEST> expMests, long? cancelTime, V_HIS_ACCOUNT_BOOK accountBook, ref List<string> sqls, ref HIS_TRANSACTION resultData)
        {
            bool result = false;
            try
            {
                if (oldTransaction == null)
                {
                    return true;
                }
                HIS_TRANSACTION newTran = null;
                List<HIS_BILL_GOODS> billGoods = new List<HIS_BILL_GOODS>();
                this.MakeTransaction(oldTransaction, accountBook, ref newTran);
                this.MakeBillGoods(newTran, expMests, ref billGoods);

                this.ProcessCancelOldTransaction(oldTransaction, cancelTime, ref sqls);

                if (IsNotNullOrEmpty(billGoods))
                {
                    bool valid = true;
                    HisTransactionBillCheck billChecker = new HisTransactionBillCheck(param);
                    HisTransactionCheck checker = new HisTransactionCheck(param);
                    valid = valid && billChecker.IsGetNumOrderFromOldSystem(newTran, accountBook);
                    valid = valid && checker.IsValidNumOrder(newTran, accountBook);
                    if (valid)
                    {
                        newTran.AMOUNT = billGoods.Sum(a => ((a.AMOUNT * a.PRICE) - (a.DISCOUNT ?? 0)));
                        newTran.TRANSACTION_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__TT;
                        newTran.SALE_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_SALE_TYPE.ID__SALE_EXP;
                        newTran.SERE_SERV_AMOUNT = 0;
                        if (!this.hisTransactionCreate.Create(newTran, null))
                        {
                            throw new Exception("hisTransactionCreate. ket thuc nghiep vu");
                        }

                        billGoods.ForEach(o => o.BILL_ID = newTran.ID);
                        if (!this.hisBillGoodsCreate.CreateList(billGoods))
                        {
                            throw new Exception("hisBillGoodsCreate. ket thuc nghiep vu");
                        }

                        string sql = String.Format("UPDATE HIS_EXP_MEST SET BILL_ID = {0}, CASHIER_LOGINNAME = '{1}', CASHIER_USERNAME = '{2}' WHERE %IN_CLAUSE% ", newTran.ID, newTran.CASHIER_LOGINNAME ?? "", newTran.CASHIER_USERNAME ?? "");
                        sqls.Add(DAOWorker.SqlDAO.AddInClause(expMests.Select(s => s.ID).ToList(), sql, "ID"));
                    }
                }
                else
                {
                    LogSystem.Warn("Khong toa duoc HIS_BILL_GOODS cho giao dich moi. Co the phieu xuat ban da duc thuo hoi het");
                }
                result = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private void MakeTransaction(HIS_TRANSACTION oldTransaction, V_HIS_ACCOUNT_BOOK accountBook, ref HIS_TRANSACTION newTran)
        {
            newTran = new HIS_TRANSACTION();
            newTran.CASHIER_ROOM_ID = oldTransaction.CASHIER_ROOM_ID;
            newTran.CASHIER_LOGINNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
            newTran.CASHIER_USERNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
            newTran.ACCOUNT_BOOK_ID = oldTransaction.ACCOUNT_BOOK_ID;
            newTran.BILL_TYPE_ID = accountBook.BILL_TYPE_ID;
            newTran.BUYER_ACCOUNT_NUMBER = oldTransaction.BUYER_ACCOUNT_NUMBER;
            newTran.BUYER_ADDRESS = oldTransaction.BUYER_ADDRESS;
            newTran.BUYER_NAME = oldTransaction.BUYER_NAME;
            newTran.BUYER_ORGANIZATION = oldTransaction.BUYER_ORGANIZATION;
            newTran.BUYER_PHONE = oldTransaction.BUYER_TAX_CODE;
            newTran.PAY_FORM_ID = oldTransaction.PAY_FORM_ID;
            newTran.SALE_TYPE_ID = oldTransaction.SALE_TYPE_ID;
            newTran.SELLER_ACCOUNT_NUMBER = oldTransaction.SELLER_ACCOUNT_NUMBER;
            newTran.SELLER_ADDRESS = oldTransaction.SELLER_ADDRESS;
            newTran.SELLER_NAME = oldTransaction.SELLER_NAME;
            newTran.SELLER_PHONE = oldTransaction.SELLER_PHONE;
            newTran.SELLER_TAX_CODE = oldTransaction.SELLER_TAX_CODE;
            newTran.TDL_PATIENT_ADDRESS = oldTransaction.TDL_PATIENT_ADDRESS;
            newTran.TDL_PATIENT_CAREER_NAME = oldTransaction.TDL_PATIENT_CAREER_NAME;
            newTran.TDL_PATIENT_CODE = oldTransaction.TDL_PATIENT_CODE;
            newTran.TDL_PATIENT_COMMUNE_CODE = oldTransaction.TDL_PATIENT_COMMUNE_CODE;
            newTran.TDL_PATIENT_DISTRICT_CODE = oldTransaction.TDL_PATIENT_DISTRICT_CODE;
            newTran.TDL_PATIENT_DOB = oldTransaction.TDL_PATIENT_DOB;
            newTran.TDL_PATIENT_FIRST_NAME = oldTransaction.TDL_PATIENT_FIRST_NAME;
            newTran.TDL_PATIENT_GENDER_ID = oldTransaction.TDL_PATIENT_GENDER_ID;
            newTran.TDL_PATIENT_GENDER_NAME = oldTransaction.TDL_PATIENT_GENDER_NAME;
            newTran.TDL_PATIENT_ID = oldTransaction.TDL_PATIENT_ID;
            newTran.TDL_PATIENT_IS_HAS_NOT_DAY_DOB = oldTransaction.TDL_PATIENT_IS_HAS_NOT_DAY_DOB;
            newTran.TDL_PATIENT_LAST_NAME = oldTransaction.TDL_PATIENT_LAST_NAME;
            newTran.TDL_PATIENT_MILITARY_RANK_NAME = oldTransaction.TDL_PATIENT_MILITARY_RANK_NAME;
            newTran.TDL_PATIENT_NAME = oldTransaction.TDL_PATIENT_NAME;
            newTran.TDL_PATIENT_NATIONAL_NAME = oldTransaction.TDL_PATIENT_NATIONAL_NAME;
            newTran.TDL_PATIENT_PROVINCE_CODE = oldTransaction.TDL_PATIENT_PROVINCE_CODE;
            newTran.TDL_PATIENT_WORK_PLACE = oldTransaction.TDL_PATIENT_WORK_PLACE;
            newTran.TDL_PATIENT_WORK_PLACE_NAME = oldTransaction.TDL_PATIENT_WORK_PLACE_NAME;
            newTran.TDL_TREATMENT_CODE = oldTransaction.TDL_TREATMENT_CODE;
            newTran.TREATMENT_ID = oldTransaction.TREATMENT_ID;
            newTran.TRANSACTION_TIME = Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmmss"));
            newTran.TRANSACTION_TYPE_ID = oldTransaction.TRANSACTION_TYPE_ID;
            if (accountBook.IS_NOT_GEN_TRANSACTION_ORDER == Constant.IS_TRUE)
            {
                newTran.NUM_ORDER = (long)((accountBook.CURRENT_NUM_ORDER ?? 0) + 1);
            }
        }

        private void MakeBillGoods(HIS_TRANSACTION newTran, List<HIS_EXP_MEST> expMests, ref List<HIS_BILL_GOODS> billGoods)
        {
            List<V_HIS_EXP_MEST_MEDICINE> listExpMestMedicine = new HisExpMestMedicineGet().GetViewByExpMestIds(expMests.Select(s => s.ID).ToList());
            List<V_HIS_EXP_MEST_MATERIAL> listExpMestMaterial = new HisExpMestMaterialGet().GetViewByExpMestIds(expMests.Select(s => s.ID).ToList());
            if (IsNotNullOrEmpty(listExpMestMedicine))
            {
                var Groups = listExpMestMedicine.GroupBy(g => new { g.EXP_MEST_ID, g.PRICE, g.VAT_RATIO, g.MEDICINE_ID }).ToList();
                foreach (var group in Groups)
                {
                    var first = group.FirstOrDefault();
                    HIS_BILL_GOODS goods = new HIS_BILL_GOODS();
                    goods.AMOUNT = group.Sum(s => (s.AMOUNT - (s.TH_AMOUNT ?? 0)));
                    goods.DISCOUNT = group.Sum(s => s.DISCOUNT ?? 0);
                    goods.PRICE = (first.PRICE ?? 0) * (1 + (first.VAT_RATIO ?? 0));
                    goods.GOODS_NAME = first.MEDICINE_TYPE_NAME;
                    goods.GOODS_UNIT_NAME = first.SERVICE_UNIT_NAME;
                    if (goods.AMOUNT > 0 && goods.PRICE > 0)
                    {
                        billGoods.Add(goods);
                    }
                }
            }

            if (IsNotNullOrEmpty(listExpMestMaterial))
            {
                var Groups = listExpMestMaterial.GroupBy(g => new { g.EXP_MEST_ID, g.PRICE, g.VAT_RATIO, g.MATERIAL_ID }).ToList();
                foreach (var group in Groups)
                {
                    var first = group.FirstOrDefault();
                    HIS_BILL_GOODS goods = new HIS_BILL_GOODS();
                    goods.AMOUNT = group.Sum(s => (s.AMOUNT - (s.TH_AMOUNT ?? 0)));
                    goods.DISCOUNT = group.Sum(s => s.DISCOUNT ?? 0);
                    goods.PRICE = (first.PRICE ?? 0) * (1 + (first.VAT_RATIO ?? 0));
                    goods.GOODS_NAME = first.MATERIAL_TYPE_NAME;
                    goods.GOODS_UNIT_NAME = first.SERVICE_UNIT_NAME;
                    if (goods.AMOUNT > 0 && goods.PRICE > 0)
                    {
                        billGoods.Add(goods);
                    }
                }
            }
        }

        private void ProcessCancelOldTransaction(HIS_TRANSACTION oldTransaction, long? cancelTime, ref List<string> sqls)
        {
            Mapper.CreateMap<HIS_TRANSACTION, HIS_TRANSACTION>();
            HIS_TRANSACTION before = Mapper.Map<HIS_TRANSACTION>(oldTransaction);

            oldTransaction.CANCEL_REASON = MessageUtil.GetMessage(LibraryMessage.Message.Enum.HisTransaction_ThuHoiDonXuatBan, param.LanguageCode);
            oldTransaction.IS_CANCEL = MOS.UTILITY.Constant.IS_TRUE;
            oldTransaction.CANCEL_CASHIER_ROOM_ID = oldTransaction.CASHIER_ROOM_ID;
            oldTransaction.CANCEL_LOGINNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
            oldTransaction.CANCEL_USERNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
            oldTransaction.CANCEL_TIME = cancelTime.Value;

            if (!this.hisTransactionUpdate.UpdateWithoutCheckLock(oldTransaction, before))
            {
                throw new Exception("hisTransactionUpdate. Ket thuc nghiep vu");
            }

            sqls.Add(String.Format("UPDATE HIS_EXP_MEST SET BILL_ID = NULL, CASHIER_LOGINNAME = NULL, CASHIER_USERNAME = NULL WHERE BILL_ID = {0}", oldTransaction.ID));
        }

        internal void Rollback()
        {
            try
            {
                this.hisBillGoodsCreate.RollbackData();
                this.hisTransactionCreate.RollbackData();
                this.hisTransactionUpdate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
