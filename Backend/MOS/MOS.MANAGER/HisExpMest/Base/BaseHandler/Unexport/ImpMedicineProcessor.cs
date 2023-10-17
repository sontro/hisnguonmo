using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisImpMest;
using MOS.MANAGER.HisMedicineBean.Handle;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Base.BaseHandler.Unexport
{
    class ImpMedicineProcessor : BusinessBase
    {
        private HisMedicineBeanSplit hisMedicineBeanSplit;
        private HisMedicineBeanUnimport hisMedicineBeanUnimport;

        internal ImpMedicineProcessor(CommonParam param)
            : base(param)
        {
            this.hisMedicineBeanSplit = new HisMedicineBeanSplit(param);
            this.hisMedicineBeanUnimport = new HisMedicineBeanUnimport(param);
        }

        internal bool Run(HIS_IMP_MEST impMest, List<HIS_IMP_MEST_MEDICINE> impMedicines, ref List<string> sqls)
        {
            bool result = false;
            try
            {
                if (IsNotNullOrEmpty(impMedicines))
                {
                    HisImpMestCheck checker = new HisImpMestCheck(param);
                    List<ExpMedicineSDO> medicineSDOs = new List<ExpMedicineSDO>();
                    var Groups = impMedicines.GroupBy(g => g.MEDICINE_ID).ToList();
                    foreach (var group in Groups)
                    {
                        if (!checker.CheckCorrectImpExpMedicine(group.Key, impMest.MEDI_STOCK_ID, impMest.ID))
                        {
                            MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisImpMest_ThuocVatTuDaDuocXuat);
                            throw new Exception("Du lieu nhap, xuat khong con tinh dung dan khi huy nhap. MedicineId: " + group.Key);
                        }
                        decimal totalAmount = group.Sum(s => s.AMOUNT);
                        if (totalAmount <= 0) continue;
                        ExpMedicineSDO sdo = new ExpMedicineSDO();
                        sdo.Amount = totalAmount;
                        sdo.MedicineId = group.Key;
                        medicineSDOs.Add(sdo);
                    }
                    List<HIS_MEDICINE_BEAN> hisMedicineBeans = null;
                    if (!this.hisMedicineBeanSplit.SplitByMedicine(medicineSDOs, impMest.MEDI_STOCK_ID, ref hisMedicineBeans))
                    {
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisImpMest_ThuocVatTuDaDuocXuat);
                        throw new Exception("Khong tach du bean theo medicine, co the do khong du so luong " + LogUtil.TraceData("medicineSDOs", medicineSDOs));
                    }

                    if (!this.hisMedicineBeanUnimport.Run(hisMedicineBeans.Select(s => s.ID).ToList(), impMest.MEDI_STOCK_ID))
                    {
                        throw new Exception("Update IS_ACTIVE, MEDI_STOCK_ID cho MEDICINE_BEAN that bai");
                    }

                    string sqlUpdate = DAOWorker.SqlDAO.AddInClause(impMedicines.Select(s => s.ID).ToList(), "UPDATE HIS_EXP_MEST_MEDICINE SET CK_IMP_MEST_MEDICINE_ID = NULL WHERE %IN_CLAUSE% ", "CK_IMP_MEST_MEDICINE_ID");
                    string sql = String.Format("DELETE HIS_IMP_MEST_MEDICINE WHERE IMP_MEST_ID = {0}", impMest.ID);
                    sqls.Add(sqlUpdate);
                    sqls.Add(sql);
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


        internal void Rollback()
        {
            try
            {
                this.hisMedicineBeanUnimport.Rollback();
                this.hisMedicineBeanSplit.RollBack();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
