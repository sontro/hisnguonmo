using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisImpMest;
using MOS.MANAGER.HisMaterialBean.Handle;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Base.BaseHandler.Unexport
{
    class ImpMaterialProcessor : BusinessBase
    {
        private HisMaterialBeanSplit hisMaterialBeanSplit;
        private HisMaterialBeanUnimport hisMaterialBeanUnimport;

        internal ImpMaterialProcessor(CommonParam param)
            : base(param)
        {
            this.hisMaterialBeanSplit = new HisMaterialBeanSplit(param);
            this.hisMaterialBeanUnimport = new HisMaterialBeanUnimport(param);
        }

        internal bool Run(HIS_IMP_MEST impMest, List<HIS_IMP_MEST_MATERIAL> impMaterials, ref List<string> sqls)
        {
            bool result = false;
            try
            {
                if (IsNotNullOrEmpty(impMaterials))
                {
                    HisImpMestCheck checker = new HisImpMestCheck(param);
                    List<ExpMaterialSDO> medicineSDOs = new List<ExpMaterialSDO>();
                    var Groups = impMaterials.GroupBy(g => g.MATERIAL_ID).ToList();
                    foreach (var group in Groups)
                    {
                        if (!checker.CheckCorrectImpExpMaterial(group.Key, impMest.MEDI_STOCK_ID, impMest.ID))
                        {
                            MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisImpMest_ThuocVatTuDaDuocXuat);
                            throw new Exception("Du lieu nhap, xuat khong con tinh dung dan khi huy nhap. MaterialId: " + group.Key);
                        }
                        decimal totalAmount = group.Sum(s => s.AMOUNT);
                        if (totalAmount <= 0) continue;
                        ExpMaterialSDO sdo = new ExpMaterialSDO();
                        sdo.Amount = totalAmount;
                        sdo.MaterialId = group.Key;
                        medicineSDOs.Add(sdo);
                    }
                    List<HIS_MATERIAL_BEAN> hisMaterialBeans = null;
                    if (!this.hisMaterialBeanSplit.SplitByMaterial(medicineSDOs, impMest.MEDI_STOCK_ID, ref hisMaterialBeans))
                    {
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisImpMest_ThuocVatTuDaDuocXuat);
                        throw new Exception("Khong tach du bean theo medicine, co the do khong du so luong " + LogUtil.TraceData("medicineSDOs", medicineSDOs));
                    }

                    if (!this.hisMaterialBeanUnimport.Run(hisMaterialBeans.Select(s => s.ID).ToList(), impMest.MEDI_STOCK_ID))
                    {
                        throw new Exception("Update IS_ACTIVE, MEDI_STOCK_ID cho MATERIAL_BEAN that bai");
                    }

                    string sqlUpdate = DAOWorker.SqlDAO.AddInClause(impMaterials.Select(s => s.ID).ToList(), "UPDATE HIS_EXP_MEST_MATERIAL SET CK_IMP_MEST_MATERIAL_ID = NULL WHERE %IN_CLAUSE% ", "CK_IMP_MEST_MATERIAL_ID");
                    string sql = String.Format("DELETE HIS_IMP_MEST_MATERIAL WHERE IMP_MEST_ID = {0}", impMest.ID);
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
                this.hisMaterialBeanUnimport.Rollback();
                this.hisMaterialBeanSplit.RollBack();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
