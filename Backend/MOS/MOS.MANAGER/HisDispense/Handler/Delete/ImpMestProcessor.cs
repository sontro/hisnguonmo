using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisImpMestMedicine;
using MOS.MANAGER.HisMedicine;
using MOS.MANAGER.HisMedicinePaty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisDispense.Handler.Delete
{
    class ImpMestProcessor : BusinessBase
    {
        internal ImpMestProcessor()
            : base()
        {

        }

        internal ImpMestProcessor(CommonParam param)
            : base(param)
        {

        }

        internal bool Run(HIS_IMP_MEST impMest, ref List<string> sqls)
        {
            bool result = false;
            try
            {
                List<HIS_IMP_MEST_MEDICINE> impMestMedicines = new HisImpMestMedicineGet().GetByImpMestId(impMest.ID);
                if (IsNotNullOrEmpty(impMestMedicines))
                {
                    this.ProcessImpMestMedicine(impMest, impMestMedicines, ref sqls);
                    this.ProcessMedicineBean(impMestMedicines, ref sqls);
                    this.ProcessMedicine(impMestMedicines, ref sqls);
                }
                sqls.Add(String.Format("DELETE HIS_IMP_MEST WHERE ID = {0}", impMest.ID));
                result = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }


        private void ProcessImpMestMedicine(HIS_IMP_MEST impMest, List<HIS_IMP_MEST_MEDICINE> impMestMedicines, ref List<string> sqls)
        {
            bool valid = true;
            HisImpMestMedicineCheck checker = new HisImpMestMedicineCheck(param);
            foreach (var item in impMestMedicines)
            {
                valid = valid && checker.IsUnLock(item);
            }
            if (!valid)
            {
                throw new Exception("HisImpMestMedicineCheck. Ket thuc nghiep vu.");
            }
            string sqlDelete = String.Format("DELETE HIS_IMP_MEST_MEDICINE WHERE IMP_MEST_ID = {0}", impMest.ID);
            sqls.Add(sqlDelete);
        }

        private void ProcessMedicineBean(List<HIS_IMP_MEST_MEDICINE> impMestMedicines, ref List<string> sqls)
        {
            string deleteMedicineBean = DAOWorker.SqlDAO.AddInClause(impMestMedicines.Select(s => s.MEDICINE_ID).ToList(), "DELETE HIS_MEDICINE_BEAN WHERE %IN_CLAUSE% ", "MEDICINE_ID");
            sqls.Add(deleteMedicineBean);
        }

        private void ProcessMedicine(List<HIS_IMP_MEST_MEDICINE> impMestMedicines, ref List<string> sqls)
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
                sqls.Add(deleteMedicinePaty);
            }

            List<HIS_MEDICINE> listMedicine = new HisMedicineGet().GetByIds(listMedicineId);
            if (listMedicine == null || listMedicine.Count == 0)
            {
                BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                throw new Exception("Khong lay duoc HIS_MEDICINE theo listMedicineId" + LogUtil.TraceData("listMedicineId", listMedicineId));
            }

            HisMedicineCheck materialChecker = new HisMedicineCheck(param);
            foreach (var material in listMedicine)
            {
                valid = valid && IsNotNull(material) && IsGreaterThanZero(material.ID);
                valid = valid && materialChecker.IsUnLock(material);
            }
            if (!valid)
            {
                throw new Exception("Ket thuc nghiep vu");
            }
            string deleteMedicine = DAOWorker.SqlDAO.AddInClause(listMedicineId, "DELETE HIS_MEDICINE WHERE %IN_CLAUSE% ", "ID");
            sqls.Add(deleteMedicine);
        }

    }
}
