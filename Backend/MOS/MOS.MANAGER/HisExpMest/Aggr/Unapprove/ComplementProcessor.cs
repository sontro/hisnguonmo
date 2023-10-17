using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisExpMest.Common.Get;
using MOS.MANAGER.HisMaterialBean.Handle;
using MOS.MANAGER.HisMaterialBean.Update;
using MOS.MANAGER.HisMedicineBean.Handle;
using MOS.MANAGER.HisMedicineBean.Update;
using MOS.UTILITY;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Aggr.Unapprove
{
    /// <summary>
    /// Xoa phieu bu le
    /// Mo khoa cac bean tuong ung
    /// </summary>
    class ComplementProcessor : BusinessBase
    {
        internal ComplementProcessor()
            : base()
        {
        }

        internal ComplementProcessor(CommonParam paramCreate)
            : base(paramCreate)
        {
        }

        internal bool Run(long parentId, List<HIS_EXP_MEST_MEDICINE> medicines, List<HIS_EXP_MEST_MATERIAL> materials, ref List<HIS_EXP_MEST_MEDICINE> deleteMedicines, ref List<HIS_EXP_MEST_MATERIAL> deleteMaterials, ref List<string> sqls)
        {
            try
            {
                //Lấy danh sach phieu bu le thuoc phieu linh
                HisExpMestFilterQuery filter = new HisExpMestFilterQuery();
                filter.EXP_MEST_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__BL;
                filter.AGGR_EXP_MEST_ID = parentId;
                List<HIS_EXP_MEST> complements = new HisExpMestGet().Get(filter);
                List<long> complementExpMestIds = IsNotNullOrEmpty(complements) ? complements.Select(o => o.ID).ToList() : null;

                this.ProcessMedicine(parentId, medicines, complementExpMestIds, ref deleteMedicines, ref sqls);
                this.ProcessMaterial(parentId, materials, complementExpMestIds, ref deleteMaterials, ref sqls);

                if (IsNotNullOrEmpty(complementExpMestIds))
                {
                    string sqlUpdateExpMest = DAOWorker.SqlDAO.AddInClause(complementExpMestIds, string.Format("UPDATE HIS_EXP_MEST SET EXP_MEST_STT_ID = {0} WHERE %IN_CLAUSE% ", IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__REQUEST), "ID");
                    sqls.Add(sqlUpdateExpMest);
                    string sqlDelExpMest = DAOWorker.SqlDAO.AddInClause(complementExpMestIds, "DELETE HIS_EXP_MEST WHERE %IN_CLAUSE% ", "ID");
                    sqls.Add(sqlDelExpMest);
                }

                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
        }

        private void ProcessMedicine(long parentId, List<HIS_EXP_MEST_MEDICINE> medicines, List<long> complementExpMestIds, ref List<HIS_EXP_MEST_MEDICINE> deleteMedicines, ref List<string> sqls)
        {
            if (HisMediStockCFG.ODD_MATERIAL_MANAGEMENT_OPTION == HisMediStockCFG.OddManagementOption.MANAGEMENT_ALLOCATE
                || HisMediStockCFG.ODD_MATERIAL_MANAGEMENT_OPTION == HisMediStockCFG.OddManagementOption.MANAGEMENT_PATIENT)
            {
                List<HIS_EXP_MEST_MEDICINE> complementMedicines = IsNotNullOrEmpty(medicines) ? medicines
                    .Where(o => o.IS_NOT_PRES.HasValue 
                        && o.IS_NOT_PRES.Value == Constant.IS_TRUE
                        //chi lay ra cac du lieu duoc tao ra khi duyet phieu linh de xoa, tranh xoa cac phan bu duoc tao ra khi ke don
                        && o.IS_CREATED_BY_APPROVAL == Constant.IS_TRUE)
                    .ToList() : null;

                if (IsNotNullOrEmpty(complementMedicines))
                {
                    List<long> expMedicineIds = complementMedicines.Select(o => o.ID).ToList();
                    string sqlUpdateBean = new HisMedicineBeanUnlockByExpMest(param).GenSql(expMedicineIds);

                    string sqlDeleteExpMestMedicine = DAOWorker.SqlDAO.AddInClause(expMedicineIds, "UPDATE HIS_EXP_MEST_MEDICINE SET IS_DELETE = 1, EXP_MEST_ID = NULL, MEDICINE_ID = NULL, TDL_MEDI_STOCK_ID = NULL, TDL_MEDICINE_TYPE_ID = NULL WHERE %IN_CLAUSE% ", "ID");

                    //Luu y: can cap nhat bean truoc khi xoa exp_mest_medicine (tranh loi FK)
                    sqls.Add(sqlUpdateBean);
                    sqls.Add(sqlDeleteExpMestMedicine);
                    deleteMedicines = complementMedicines;
                }
            }
            else
            {
                //Lay thông tin chi tiết bù lẻ: thuộc phiếu bù lẻ hoặc gắn trực tiếp vào phiếu lĩnh
                List<HIS_EXP_MEST_MEDICINE> complementMedicines = IsNotNullOrEmpty(medicines) ? medicines
                    .Where(o => (complementExpMestIds != null && complementExpMestIds.Contains(o.EXP_MEST_ID.Value))
                        || o.EXP_MEST_ID == parentId
                    ).ToList() : null;

                if (IsNotNullOrEmpty(complementMedicines))
                {
                    List<long> expMedicineIds = complementMedicines.Select(o => o.ID).ToList();
                    string sqlUpdateBean = new HisMedicineBeanUnlockByExpMest(param).GenSql(expMedicineIds);

                    string sqlDeleteExpMestMedicine = DAOWorker.SqlDAO.AddInClause(expMedicineIds, "UPDATE HIS_EXP_MEST_MEDICINE SET IS_DELETE = 1, EXP_MEST_ID = NULL, MEDICINE_ID = NULL, TDL_MEDI_STOCK_ID = NULL, TDL_MEDICINE_TYPE_ID = NULL WHERE %IN_CLAUSE% ", "ID");

                    //Luu y: can cap nhat bean truoc khi xoa exp_mest_medicine (tranh loi FK)
                    sqls.Add(sqlUpdateBean);
                    sqls.Add(sqlDeleteExpMestMedicine);
                }

            }
        }

        private void ProcessMaterial(long parentId, List<HIS_EXP_MEST_MATERIAL> materials, List<long> complementExpMestIds, ref List<HIS_EXP_MEST_MATERIAL> deleteMaterials, ref List<string> sqls)
        {
            if (HisMediStockCFG.ODD_MATERIAL_MANAGEMENT_OPTION == HisMediStockCFG.OddManagementOption.MANAGEMENT_ALLOCATE
                || HisMediStockCFG.ODD_MATERIAL_MANAGEMENT_OPTION == HisMediStockCFG.OddManagementOption.MANAGEMENT_PATIENT)
            {
                List<HIS_EXP_MEST_MATERIAL> complementMaterials = IsNotNullOrEmpty(materials) ? materials
                    .Where(o => o.IS_NOT_PRES.HasValue
                        && o.IS_NOT_PRES.Value == Constant.IS_TRUE
                        //chi lay ra cac du lieu duoc tao ra khi duyet phieu linh de xoa, tranh xoa cac phan bu duoc tao ra khi ke don
                        && o.IS_CREATED_BY_APPROVAL == Constant.IS_TRUE).ToList() : null;
                if (IsNotNullOrEmpty(complementMaterials))
                {
                    List<long> expMaterialIds = complementMaterials.Select(o => o.ID).ToList();
                    string sqlUpdateBean = new HisMaterialBeanUnlockByExpMest(param).GenSql(expMaterialIds);

                    string sqlDeleteExpMestMaterial = DAOWorker.SqlDAO.AddInClause(expMaterialIds, "UPDATE HIS_EXP_MEST_MATERIAL SET IS_DELETE = 1, EXP_MEST_ID = NULL, MATERIAL_ID = NULL, TDL_MEDI_STOCK_ID = NULL, TDL_MATERIAL_TYPE_ID = NULL WHERE %IN_CLAUSE% ", "ID");

                    //Luu y: can cap nhat bean truoc khi xoa exp_mest_material (tranh loi FK)
                    sqls.Add(sqlUpdateBean);
                    sqls.Add(sqlDeleteExpMestMaterial);
                    deleteMaterials = complementMaterials;
                }
            }
            else
            {
                List<HIS_EXP_MEST_MATERIAL> complementMaterials = IsNotNullOrEmpty(materials) ? materials
                   .Where(o => (complementExpMestIds != null && complementExpMestIds.Contains(o.EXP_MEST_ID.Value))
                       || o.EXP_MEST_ID == parentId
                   ).ToList() : null;

                if (IsNotNullOrEmpty(complementMaterials))
                {
                    List<long> expMaterialIds = complementMaterials.Select(o => o.ID).ToList();
                    string sqlUpdateBean = new HisMaterialBeanUnlockByExpMest(param).GenSql(expMaterialIds);

                    string sqlDeleteExpMestMaterial = DAOWorker.SqlDAO.AddInClause(expMaterialIds, "UPDATE HIS_EXP_MEST_MATERIAL SET IS_DELETE = 1, EXP_MEST_ID = NULL, MATERIAL_ID = NULL, TDL_MEDI_STOCK_ID = NULL, TDL_MATERIAL_TYPE_ID = NULL WHERE %IN_CLAUSE% ", "ID");

                    //Luu y: can cap nhat bean truoc khi xoa exp_mest_material (tranh loi FK)
                    sqls.Add(sqlUpdateBean);
                    sqls.Add(sqlDeleteExpMestMaterial);
                }
            }
        }
    }
}
