using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisDebtGoods;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisSereServDebt;
using MOS.MANAGER.HisTreatment;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTransaction.Debt.DrugStoreCreate
{
    class HisTransactionDrugStoreDebtCreate : BusinessBase
    {
        private HisTransactionCreate hisTransactionCreate;
        private HisDebtGoodsCreate hisDebtGoodsCreate;

        internal HisTransactionDrugStoreDebtCreate()
            : base()
        {
            this.Init();
        }

        internal HisTransactionDrugStoreDebtCreate(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisTransactionCreate = new HisTransactionCreate(param);
            this.hisDebtGoodsCreate = new HisDebtGoodsCreate(param);
        }

        internal bool Run(HisTransactionDrugStoreDebtSDO data, ref HisDrugStoreDebtResultSDO resultData)
        {
            bool result = false;
            try
            {
                V_HIS_CASHIER_ROOM cashierRoom = null;
                V_HIS_ACCOUNT_BOOK hisAccountBook = null;
                List<D_HIS_EXP_MEST_DETAIL_1> details = null;
                this.SetServerTime(data);
                data.Transaction.TRANSACTION_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__NO;

                HisTransactionDrugStoreDebtCheck checker = new HisTransactionDrugStoreDebtCheck(param);
                HisTransactionCheck commonChecker = new HisTransactionCheck(param);
                HisSereServCheck sereServChecker = new HisSereServCheck(param);

                bool valid = true;
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.AllowRoom(data.RequestRoomId, ref cashierRoom);
                valid = valid && commonChecker.IsUnlockAccountBook(data.Transaction.ACCOUNT_BOOK_ID, ref hisAccountBook);
                valid = valid && commonChecker.IsValidNumOrder(data.Transaction, hisAccountBook);
                valid = valid && commonChecker.HasNotFinancePeriod(data.Transaction);
                valid = valid && checker.IsValidData(data, ref details);

                if (valid)
                {
                    data.Transaction.CASHIER_ROOM_ID = cashierRoom.ID;
                    data.Transaction.PAY_FORM_ID = IMSys.DbConfig.HIS_RS.HIS_PAY_FORM.ID__TM;
                    data.Transaction.DEBT_TYPE = IMSys.DbConfig.HIS_RS.HIS_TRANSACTION.DEBT_TYPE__DRUG_STORE;

                    HIS_TRANSACTION debt = null;
                    List<HIS_DEBT_GOODS> debtGoods = null;

                    this.ProcessTransactionDebt(data, details, ref debt);

                    this.ProcessDebtGoods(data, details, debt, ref debtGoods);

                    //can dat cuoi cung, do su dung update sql
                    this.ProcessExpMest(data, debt);

                    this.PassResult(debt, debtGoods, ref resultData);

                    result = true;

                    new EventLogGenerator(LibraryEventLog.EventLog.Enum.HisTransaction_TaoGiaoDichChotNo, debt.AMOUNT).TransactionCode(debt.TRANSACTION_CODE).Run();
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                this.Rollback();
                result = false;
            }
            return result;
        }

        private void ProcessExpMest(HisTransactionDrugStoreDebtSDO data, HIS_TRANSACTION debt)
        {
            string ids = string.Join(",", data.ExpMestIds);
            string sql = string.Format("UPDATE HIS_EXP_MEST SET DEBT_ID = {0} WHERE ID IN ({1})", debt.ID, ids);
            if (!DAOWorker.SqlDAO.Execute(sql))
            {
                throw new Exception("Cap nhat du lieu DEBT_ID cho HIS_EXP_MEST that bai");
            }
        }

        private void ProcessTransactionDebt(HisTransactionDrugStoreDebtSDO data, List<D_HIS_EXP_MEST_DETAIL_1> details, ref HIS_TRANSACTION debt)
        {
            data.Transaction.TDL_PATIENT_ADDRESS = details[0].TDL_PATIENT_ADDRESS;
            data.Transaction.TDL_PATIENT_NAME = details[0].TDL_PATIENT_NAME;
            data.Transaction.TDL_TREATMENT_CODE = details[0].TDL_TREATMENT_CODE;
            data.Transaction.TDL_PATIENT_CODE = details[0].TDL_PATIENT_CODE;
            data.Transaction.TDL_PATIENT_GENDER_NAME = details[0].TDL_PATIENT_GENDER_NAME;
            data.Transaction.TDL_PATIENT_DOB = details[0].TDL_PATIENT_DOB;
            data.Transaction.TDL_PATIENT_IS_HAS_NOT_DAY_DOB = details[0].TDL_PATIENT_IS_HAS_NOT_DAY_DOB;

            if (!this.hisTransactionCreate.Create(data.Transaction, null))
            {
                throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
            }
            debt = data.Transaction;
        }

        private void ProcessDebtGoods(HisTransactionDrugStoreDebtSDO data, List<D_HIS_EXP_MEST_DETAIL_1> details, HIS_TRANSACTION debt, ref List<HIS_DEBT_GOODS> debtGoods)
        {
            if (IsNotNullOrEmpty(details) && debt != null)
            {
                List<HIS_DEBT_GOODS> goods = new List<HIS_DEBT_GOODS>();

                foreach (D_HIS_EXP_MEST_DETAIL_1 dt in details)
                {
                    HIS_DEBT_GOODS g = new HIS_DEBT_GOODS();
                    g.DEBT_ID = debt.ID;
                    g.AMOUNT = dt.AMOUNT.Value;
                    g.DISCOUNT = dt.DISCOUNT;
                    g.GOODS_NAME = dt.ITEM_NAME;
                    g.GOODS_UNIT_NAME = dt.SERVICE_UNIT_NAME;
                    g.SERVICE_UNIT_ID = dt.TDL_SERVICE_UNIT_ID;
                    g.PRICE = dt.PRICE.Value;
                    g.VAT_RATIO = dt.VAT_RATIO;
                    if (dt.IS_MEDICINE == Constant.IS_TRUE)
                    {
                        g.MEDICINE_TYPE_ID = dt.TYPE_ID;
                    }
                    else
                    {
                        g.MATERIAL_TYPE_ID = dt.TYPE_ID;
                    }

                    goods.Add(g);
                }
                

                if (!this.hisDebtGoodsCreate.CreateList(goods))
                {
                    throw new Exception("Tao thong tin HIS_DEBT_GOODS that bai. Du lieu se bi rollback");
                }
                debtGoods = goods;
            }
        }

        private void PassResult(HIS_TRANSACTION debt, List<HIS_DEBT_GOODS> debtGoods, ref HisDrugStoreDebtResultSDO resultData)
        {
            resultData = new HisDrugStoreDebtResultSDO();
            //can truy van lai de lay thong tin num_order (do trigger sinh ra, nhung ko the set identity do van co truong hop cho phep nguoi dung nhap)
            resultData.Debt = new HisTransactionGet().GetViewById(debt.ID);
            resultData.DebtGoods = debtGoods;
        }

        /// <summary>
        /// Xu ly de gan cac thong tin thoi gian theo gio server
        /// </summary>
        /// <param name="data"></param>
        private void SetServerTime(HisTransactionDrugStoreDebtSDO data)
        {
            //Neu cau hinh su dung gio server thi gan lai theo gio server cac du lieu thoi gian truyen len
            if (SystemCFG.IS_USING_SERVER_TIME && data != null)
            {
                long now = Inventec.Common.DateTime.Get.Now().Value;
                if (data.Transaction != null)
                {
                    data.Transaction.TRANSACTION_TIME = now;
                }
            }
        }

        private void Rollback()
        {
            this.hisDebtGoodsCreate.RollbackData();
            this.hisTransactionCreate.RollbackData();
        }
    }
}
