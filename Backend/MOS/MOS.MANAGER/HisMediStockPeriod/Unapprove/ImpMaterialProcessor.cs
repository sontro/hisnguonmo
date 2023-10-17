using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.MANAGER.HisImpMestMaterial;
using MOS.MANAGER.HisMaterial;
using MOS.MANAGER.HisMaterialPaty;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisMediStockPeriod.Unapprove
{
    class ImpMaterialProcessor : BusinessBase
    {

        internal ImpMaterialProcessor(CommonParam param)
            : base(param)
        {

        }

        internal bool Run(HIS_IMP_MEST impMest, ref List<string> sqls)
        {
            bool result = false;
            try
            {
                if (IsNotNull(impMest))
                {
                    List<HIS_IMP_MEST_MATERIAL> impMestMaterials = new HisImpMestMaterialGet().GetByImpMestId(impMest.ID);
                    if (IsNotNullOrEmpty(impMestMaterials))
                    {
                        this.ProcessImpMestMaterial(impMest, impMestMaterials, ref sqls);

                        this.ProcessMaterialBean(impMestMaterials, ref sqls);

                        this.ProcessMaterial(impMestMaterials, ref sqls);
                    }
                }
                result = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        private void ProcessImpMestMaterial(HIS_IMP_MEST impMest, List<HIS_IMP_MEST_MATERIAL> impMestMaterials, ref List<string> executeSqls)
        {
            bool valid = true;
            HisImpMestMaterialCheck checker = new HisImpMestMaterialCheck(param);
            foreach (var item in impMestMaterials)
            {
                valid = valid && checker.IsUnLock(item);
            }
            if (!valid)
            {
                throw new Exception("Ket thuc nghiep vu.");
            }
            string deleteImpMestMaterialSql = new StringBuilder().Append("DELETE HIS_IMP_MEST_MATERIAL WHERE IMP_MEST_ID = ").Append(impMest.ID).ToString();
            executeSqls.Add(deleteImpMestMaterialSql);
        }

        private void ProcessMaterialBean(List<HIS_IMP_MEST_MATERIAL> impMestMaterials, ref List<string> executeSqls)
        {
            string deleteMaterialBean = DAOWorker.SqlDAO.AddInClause(impMestMaterials.Select(s => s.MATERIAL_ID).ToList(), "DELETE HIS_MATERIAL_BEAN WHERE %IN_CLAUSE% ", "MATERIAL_ID");
            executeSqls.Add(deleteMaterialBean);
        }

        private void ProcessMaterial(List<HIS_IMP_MEST_MATERIAL> impMestMaterials, ref List<string> executeSqls)
        {
            bool valid = true;
            List<long> listMaterialId = impMestMaterials.Select(s => s.MATERIAL_ID).ToList();
            List<HIS_MATERIAL_PATY> listMaterialPaty = new HisMaterialPatyGet().GetByMaterialIds(listMaterialId);
            if (IsNotNullOrEmpty(listMaterialPaty))
            {
                HisMaterialPatyCheck patyChecker = new HisMaterialPatyCheck(param);
                foreach (var paty in listMaterialPaty)
                {
                    valid = valid && IsNotNull(paty) && IsGreaterThanZero(paty.ID);
                    valid = valid && patyChecker.IsUnLock(paty.ID);
                }
                if (!valid)
                {
                    throw new Exception("Ket thuc nghiep vu.");
                }
                string deleteMaterialPaty = DAOWorker.SqlDAO.AddInClause(listMaterialId, "DELETE HIS_MATERIAL_PATY WHERE %IN_CLAUSE% ", "MATERIAL_ID");
                executeSqls.Add(deleteMaterialPaty);
            }

            List<HIS_MATERIAL> listMaterial = new HisMaterialGet().GetByIds(listMaterialId);
            if (listMaterial == null || listMaterial.Count == 0)
            {
                BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                throw new Exception("Khong lay duoc HIS_MATERIAL theo listMaterialId" + LogUtil.TraceData("listMaterialId", listMaterialId));
            }

            HisMaterialCheck materialChecker = new HisMaterialCheck(param);
            foreach (var material in listMaterial)
            {
                valid = valid && IsNotNull(material) && IsGreaterThanZero(material.ID);
                valid = valid && materialChecker.IsUnLock(material);
            }
            if (!valid)
            {
                throw new Exception("Ket thuc nghiep vu");
            }
            string deleteMaterial = DAOWorker.SqlDAO.AddInClause(listMaterialId, "DELETE HIS_MATERIAL WHERE %IN_CLAUSE% ", "ID");
            executeSqls.Add(deleteMaterial);
        }

    }
}
