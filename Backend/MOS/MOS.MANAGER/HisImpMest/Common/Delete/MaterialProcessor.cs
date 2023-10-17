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

namespace MOS.MANAGER.HisImpMest.Common.Delete
{
    class MaterialProcessor : BusinessBase
    {
        HisExpMestMaterialDecreaseThAmount hisExpMestMaterialDecreaseThAmount;

        internal MaterialProcessor(CommonParam param)
            : base(param)
        {
            this.hisExpMestMaterialDecreaseThAmount = new HisExpMestMaterialDecreaseThAmount(param);
        }

        internal bool Run(HIS_IMP_MEST impMest, List<HIS_IMP_MEST_MATERIAL> impMestMaterials, List<HIS_EXP_MEST_MATERIAL> expMestMaterials, ref List<string> executeSqls, ref Dictionary<HIS_EXP_MEST_MATERIAL, decimal> dicMaterialThAmount)
        {
            bool result = false;
            try
            {
                if (IsNotNullOrEmpty(impMestMaterials))
                {
                    if (HisImpMestContanst.TYPE_CHMS_IDS.Contains(impMest.IMP_MEST_TYPE_ID))
                    {
                        this.ProcessChms(impMestMaterials, expMestMaterials, ref executeSqls);
                    }

                    this.ProcessImpMestMaterial(impMest, impMestMaterials, ref executeSqls);

                    if (HisImpMestContanst.TYPE_MUST_CREATE_PACKAGE_IDS.Contains(impMest.IMP_MEST_TYPE_ID))
                    {
                        this.ProcessMaterialBean(impMestMaterials, ref executeSqls);

                        this.ProcessMaterial(impMestMaterials, ref executeSqls);
                    }
                    if (HisImpMestContanst.TYPE_MOBA_IDS.Contains(impMest.IMP_MEST_TYPE_ID) || impMest.IMP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__DONKTL)
                    {
                        this.ProcessThAmount(impMestMaterials, expMestMaterials, ref executeSqls, ref dicMaterialThAmount);
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

        private void ProcessChms(List<HIS_IMP_MEST_MATERIAL> impMestMaterials, List<HIS_EXP_MEST_MATERIAL> expMestMaterials, ref List<string> executeSqls)
        {
            if (!IsNotNullOrEmpty(expMestMaterials))
            {
                BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                throw new Exception("Nhap chuyen kho, BCS co ImpMestMaterial nhung khong co ExpMestMaterial");
            }
            List<HIS_EXP_MEST_MATERIAL> updateList = new List<HIS_EXP_MEST_MATERIAL>();
            foreach (var imp in impMestMaterials)
            {
                List<HIS_EXP_MEST_MATERIAL> exps = expMestMaterials.Where(o => o.CK_IMP_MEST_MATERIAL_ID == imp.ID).ToList();
                if (!IsNotNullOrEmpty(exps))
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                    throw new Exception("Co ImpMestMaterial CK nhung khong co ExpMestMaterial CK tuong ung " + LogUtil.TraceData("ExpMestMaterials", exps));
                }
                if (exps.Exists(o => o.IS_ACTIVE == IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__FALSE))
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.DuLieuDangBiKhoa);
                    throw new Exception("Ton tai ExpMestMaterial dang bi khoa: " + LogUtil.TraceData("ExpMestMaterials", exps));
                }
                updateList.AddRange(exps);
            }
            string updateSql = DAOWorker.SqlDAO.AddInClause(updateList.Select(s => s.ID).ToList(), "UPDATE HIS_EXP_MEST_MATERIAL SET CK_IMP_MEST_MATERIAL_ID = NULL WHERE %IN_CLAUSE% ", "ID");
            executeSqls.Add(updateSql);
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

        private void ProcessThAmount(List<HIS_IMP_MEST_MATERIAL> impMestMaterials, List<HIS_EXP_MEST_MATERIAL> expMestMaterials, ref List<string> executeSqls, ref Dictionary<HIS_EXP_MEST_MATERIAL, decimal> dicMaterialThAmount)
        {
            if (!IsNotNullOrEmpty(expMestMaterials))
            {
                BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                throw new Exception("Phieu thu hoi tra lai co ImpMestMaterial nhung khong co ExpMestMaterial");
            }

            Dictionary<long, decimal> dicDecrease = new Dictionary<long, decimal>();

            var GroupImp = impMestMaterials.GroupBy(g => g.TH_EXP_MEST_MATERIAL_ID).ToList();

            foreach (var imp in GroupImp)
            {
                List<HIS_IMP_MEST_MATERIAL> listImpByMaterial = imp.ToList();
                decimal amountImp = listImpByMaterial.Sum(s => s.AMOUNT);
                HIS_EXP_MEST_MATERIAL exp = expMestMaterials.FirstOrDefault(o => o.ID == imp.Key);
                if (exp == null)
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                    throw new Exception("khong lay duoc HIS_EXP_MEST_MATERIAL  theo material_id: " + imp.Key);
                }

                if (!exp.TH_AMOUNT.HasValue || exp.TH_AMOUNT.Value < amountImp)
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                    throw new Exception("So luong thuoc thu hoi cua chi tiet xuat nho hon so luong nhap thu hoi cua phieu  nhap MaterialId: " + imp.Key);
                }
                dicDecrease[exp.ID] = amountImp;
                //executeSqls.Add(String.Format("UPDATE HIS_EXP_MEST_MATERIAL SET TH_AMOUNT = NVL(TH_AMOUNT,0) - {0} WHERE ID = {1}", CommonUtil.ToString(amountImp), exp.ID));
                dicMaterialThAmount[exp] = amountImp;
            }
            if (dicDecrease.Count > 0)
            {
                if (!this.hisExpMestMaterialDecreaseThAmount.Run(dicDecrease))
                {
                    throw new Exception("hisExpMestMaterialDecreaseThAmount. Ket thuc nghiep vu");
                }
            }
        }

        internal void Rollback()
        {
            try
            {
                this.hisExpMestMaterialDecreaseThAmount.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
