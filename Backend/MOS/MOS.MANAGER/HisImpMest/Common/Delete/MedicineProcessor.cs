using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.MANAGER.HisImpMestMedicine;
using MOS.MANAGER.HisMedicine;
using MOS.MANAGER.HisMedicinePaty;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisImpMest.Common.Delete
{
    class MedicineProcessor : BusinessBase
    {
        private HisExpMestMedicineDecreaseThAmount hisExpMestMedicineDecreaseThAmount;

        internal MedicineProcessor(CommonParam param)
            : base(param)
        {
            this.hisExpMestMedicineDecreaseThAmount = new HisExpMestMedicineDecreaseThAmount(param);
        }

        internal bool Run(HIS_IMP_MEST impMest, List<HIS_IMP_MEST_MEDICINE> impMestMedicines, List<HIS_EXP_MEST_MEDICINE> expMestMedicines, ref List<string> executeSqls, ref Dictionary<HIS_EXP_MEST_MEDICINE, decimal> dicMedicineThAmount)
        {
            bool result = false;
            try
            {
                if (IsNotNullOrEmpty(impMestMedicines))
                {
                    if (HisImpMestContanst.TYPE_CHMS_IDS.Contains(impMest.IMP_MEST_TYPE_ID))
                    {
                        this.ProcessChms(impMestMedicines, expMestMedicines, ref executeSqls);
                    }

                    this.ProcessImpMestMedicine(impMest, impMestMedicines, ref executeSqls);

                    if (HisImpMestContanst.TYPE_MUST_CREATE_PACKAGE_IDS.Contains(impMest.IMP_MEST_TYPE_ID))
                    {
                        this.ProcessMedicineBean(impMestMedicines, ref executeSqls);

                        this.ProcessMedicine(impMestMedicines, ref executeSqls);
                    }
                    if (HisImpMestContanst.TYPE_MOBA_IDS.Contains(impMest.IMP_MEST_TYPE_ID) || impMest.IMP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__DONKTL)
                    {
                        this.ProcessThAmount(impMestMedicines, expMestMedicines, ref executeSqls, ref dicMedicineThAmount);
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

        private void ProcessChms(List<HIS_IMP_MEST_MEDICINE> impMestMedicines, List<HIS_EXP_MEST_MEDICINE> expMestMedicines, ref List<string> executeSqls)
        {
            if (!IsNotNullOrEmpty(expMestMedicines))
            {
                BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                throw new Exception("Nhap chuyen kho, BCS co ImpMestMedicine nhung khong co ExpMestMedicine");
            }
            List<HIS_EXP_MEST_MEDICINE> updateList = new List<HIS_EXP_MEST_MEDICINE>();
            foreach (var imp in impMestMedicines)
            {
                List<HIS_EXP_MEST_MEDICINE> exps = expMestMedicines.Where(o => o.CK_IMP_MEST_MEDICINE_ID == imp.ID).ToList();
                if (!IsNotNullOrEmpty(exps))
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                    throw new Exception("Co ImpMestMedicine CK nhung khong co ExpMestMedicine CK tuong ung " + LogUtil.TraceData("ExpMestMedicines", exps));
                }
                if (exps.Exists(o => o.IS_ACTIVE == IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__FALSE))
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.DuLieuDangBiKhoa);
                    throw new Exception("Ton tai ExpMestMedicine dang bi khoa: " + LogUtil.TraceData("ExpMestMedicines", exps));
                }
                updateList.AddRange(exps);
            }
            string updateSql = DAOWorker.SqlDAO.AddInClause(updateList.Select(s => s.ID).ToList(), "UPDATE HIS_EXP_MEST_MEDICINE SET CK_IMP_MEST_MEDICINE_ID = NULL WHERE %IN_CLAUSE% ", "ID");
            executeSqls.Add(updateSql);
        }

        private void ProcessImpMestMedicine(HIS_IMP_MEST impMest, List<HIS_IMP_MEST_MEDICINE> impMestMedicines, ref List<string> executeSqls)
        {
            bool valid = true;
            HisImpMestMedicineCheck checker = new HisImpMestMedicineCheck(param);
            foreach (var item in impMestMedicines)
            {
                valid = valid && checker.IsUnLock(item);
            }
            if (!valid)
            {
                throw new Exception("Ket thuc nghiep vu.");
            }
            string deleteImpMestMedicineSql = new StringBuilder().Append("DELETE HIS_IMP_MEST_MEDICINE WHERE IMP_MEST_ID = ").Append(impMest.ID).ToString();
            executeSqls.Add(deleteImpMestMedicineSql);
        }

        private void ProcessMedicineBean(List<HIS_IMP_MEST_MEDICINE> impMestMedicines, ref List<string> executeSqls)
        {
            string deleteMedicineBean = DAOWorker.SqlDAO.AddInClause(impMestMedicines.Select(s => s.MEDICINE_ID).ToList(), "DELETE HIS_MEDICINE_BEAN WHERE %IN_CLAUSE% ", "MEDICINE_ID");
            executeSqls.Add(deleteMedicineBean);
        }

        private void ProcessMedicine(List<HIS_IMP_MEST_MEDICINE> impMestMedicines, ref List<string> executeSqls)
        {
            bool valid = true;
            List<long> listMedicineId = impMestMedicines.Select(s => s.MEDICINE_ID).ToList();
            List<HIS_MEDICINE_PATY> listMedicinePaty = new HisMedicinePatyGet().GetByMedicineIds(listMedicineId);
            if (IsNotNullOrEmpty(listMedicinePaty))
            {
                HisMedicinePatyCheck patyChecker = new HisMedicinePatyCheck(param);
                foreach (var paty in listMedicinePaty)
                {
                    valid = valid && IsNotNull(paty) && IsGreaterThanZero(paty.ID);
                    valid = valid && patyChecker.IsUnLock(paty.ID);
                }
                if (!valid)
                {
                    throw new Exception("Ket thuc nghiep vu.");
                }
                string deleteMedicinePaty = DAOWorker.SqlDAO.AddInClause(listMedicineId, "DELETE HIS_MEDICINE_PATY WHERE %IN_CLAUSE% ", "MEDICINE_ID");
                executeSqls.Add(deleteMedicinePaty);
            }

            List<HIS_MEDICINE> listMedicine = new HisMedicineGet().GetByIds(listMedicineId);
            if (listMedicine == null || listMedicine.Count == 0)
            {
                BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                throw new Exception("Khong lay duoc HIS_MEDICINE theo listMedicineId" + LogUtil.TraceData("listMedicineId", listMedicineId));
            }

            HisMedicineCheck medicineChecker = new HisMedicineCheck(param);
            foreach (var medicine in listMedicine)
            {
                valid = valid && IsNotNull(medicine) && IsGreaterThanZero(medicine.ID);
                valid = valid && medicineChecker.IsUnLock(medicine);
            }
            if (!valid)
            {
                throw new Exception("Ket thuc nghiep vu");
            }
            string deleteMedicine = DAOWorker.SqlDAO.AddInClause(listMedicineId, "DELETE HIS_MEDICINE WHERE %IN_CLAUSE% ", "ID");
            executeSqls.Add(deleteMedicine);
        }

        private void ProcessThAmount(List<HIS_IMP_MEST_MEDICINE> impMestMedicines, List<HIS_EXP_MEST_MEDICINE> expMestMedicines, ref List<string> executeSqls, ref Dictionary<HIS_EXP_MEST_MEDICINE, decimal> dicMedicineThAmount)
        {
            if (!IsNotNullOrEmpty(expMestMedicines))
            {
                BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                throw new Exception("Phieu thu hoi tra lai co ImpMestMedicince nhung khong co ExpMestMedicine");
            }

            Dictionary<long, decimal> dicDecrease = new Dictionary<long, decimal>();

            var GroupImp = impMestMedicines.GroupBy(g => g.TH_EXP_MEST_MEDICINE_ID).ToList();

            foreach (var imp in GroupImp)
            {
                List<HIS_IMP_MEST_MEDICINE> listImpByMedicine = imp.ToList();
                decimal amountImp = listImpByMedicine.Sum(s => s.AMOUNT);
                HIS_EXP_MEST_MEDICINE exp = expMestMedicines.FirstOrDefault(o => o.ID == imp.Key);
                if (exp == null)
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                    throw new Exception("khong lay duoc HIS_EXP_MEST_MEDICINE  theo medicine_id: " + imp.Key);
                }

                if (!exp.TH_AMOUNT.HasValue || exp.TH_AMOUNT.Value < amountImp)
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                    throw new Exception("So luong thuoc thu hoi cua chi tiet xuat nho hon so luong nhap thu hoi cua phieu nhap MedicineId: " + imp.Key);
                }
                dicDecrease[exp.ID] = amountImp;

                //executeSqls.Add(String.Format("UPDATE HIS_EXP_MEST_MEDICINE SET TH_AMOUNT = NVL(TH_AMOUNT,0) - {0} WHERE ID = {1}", CommonUtil.ToString(amountImp), exp.ID));
                dicMedicineThAmount[exp] = amountImp;
            }
            if (dicDecrease.Count > 0)
            {
                if (!this.hisExpMestMedicineDecreaseThAmount.Run(dicDecrease))
                {
                    throw new Exception("hisExpMestMedicineDecreaseThAmount. Ket thuc nghiep vu");
                }
            }
        }

        internal void Rollback()
        {
            try
            {
                this.hisExpMestMedicineDecreaseThAmount.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
