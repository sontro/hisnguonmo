using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTransaction.Bill
{
    class HisTransactionBillCreateList : BusinessBase
    {
        private List<HisTransactionBillCreate> hisTransactionCreates;

        internal HisTransactionBillCreateList()
            : base()
        {

        }

        internal HisTransactionBillCreateList(CommonParam param)
            : base(param)
        {

        }

        internal bool Run(List<HisTransactionBillSDO> data, ref List<V_HIS_TRANSACTION> resultData)
        {
            bool result = false;
            try
            {
                if (IsNotNullOrEmpty(data))
                {
                    this.hisTransactionCreates = new List<HisTransactionBillCreate>();
                    resultData = new List<V_HIS_TRANSACTION>();
                    foreach (HisTransactionBillSDO sdo in data)
                    {
                        HisTransactionBillCreate creator = new HisTransactionBillCreate(param);
                        HisTransactionBillResultSDO resultSDO = null;
                        if (!creator.CreateBill(sdo, ref resultSDO))
                        {
                            throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
                        }
                        this.hisTransactionCreates.Add(creator);
                        resultData.Add(resultSDO.TransactionBill);
                    }
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                this.Rollback();
                result = false;
            }
            return result;
        }

        private void Rollback()
        {
            try
            {
                if (IsNotNullOrEmpty(this.hisTransactionCreates))
                {
                    foreach (var creator in this.hisTransactionCreates)
                    {
                        creator.RollbackData();
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
