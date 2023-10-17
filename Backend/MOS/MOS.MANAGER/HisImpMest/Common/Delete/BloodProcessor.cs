using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisBlood;
using MOS.MANAGER.HisBloodGiver;
using MOS.MANAGER.HisExpMestBlood;
using MOS.MANAGER.HisImpMestBlood;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisImpMest.Common.Delete
{
    class BloodProcessor : BusinessBase
    {
        internal BloodProcessor(CommonParam param)
            : base(param)
        {

        }

        internal bool Run(HIS_IMP_MEST impMest, List<HIS_IMP_MEST_BLOOD> impMestBloods, List<HIS_EXP_MEST_BLOOD> expMestBloods, ref List<string> executeSqls)
        {
            bool result = false;
            try
            {
                if (IsNotNullOrEmpty(impMestBloods))
                {
                    List<long> listBloodId = impMestBloods.Select(s => s.BLOOD_ID).ToList();

                    this.ProcessImpMestBlood(impMestBloods, ref executeSqls);

                    if (HisImpMestContanst.TYPE_MUST_CREATE_PACKAGE_IDS.Contains(impMest.IMP_MEST_TYPE_ID))
                    {
                        this.ProcessBlood(listBloodId, ref executeSqls);
                    }

                    if (impMest.IMP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__DMTL)
                    {
                        this.ProcessExpMestBlood(listBloodId, expMestBloods, ref executeSqls);
                    }

                    if (impMest.IMP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__HM)
                    {
                        this.ProcessBloodGiver(impMest, ref executeSqls);
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

        private void ProcessBloodGiver(HIS_IMP_MEST impMest, ref List<string> executeSqls)
        {
            HisBloodGiverFilterQuery bloodGiverFilter = new HisBloodGiverFilterQuery();
            bloodGiverFilter.IMP_MEST_ID = impMest.ID;
            List<HIS_BLOOD_GIVER> bloodGivers = new HisBloodGiverGet().Get(bloodGiverFilter);
            if (IsNotNullOrEmpty(bloodGivers))
            {
                string deleteBloodGivers = DAOWorker.SqlDAO.AddInClause(bloodGivers.Select(s => s.ID).ToList(), "DELETE HIS_BLOOD_GIVER WHERE %IN_CLAUSE% ", "ID");
                executeSqls.Add(deleteBloodGivers);
            }
        }

        private void ProcessImpMestBlood(List<HIS_IMP_MEST_BLOOD> impMestBloods, ref  List<string> executeSqls)
        {
            bool valid = true;
            HisImpMestBloodCheck checker = new HisImpMestBloodCheck(param);
            foreach (var item in impMestBloods)
            {
                valid = valid && checker.IsUnLock(item);
            }
            if (!valid)
            {
                throw new Exception("Ket thuc nghiep vu.");
            }
            string deleteImpMestBlood = DAOWorker.SqlDAO.AddInClause(impMestBloods.Select(s => s.ID).ToList(), "DELETE HIS_IMP_MEST_BLOOD WHERE %IN_CLAUSE% ", "ID");
            executeSqls.Add(deleteImpMestBlood);
        }

        private void ProcessBlood(List<long> listBloodId, ref  List<string> executeSqls)
        {
            List<HIS_BLOOD> listBlood = new HisBloodGet().GetByIds(listBloodId);

            bool valid = true;

            if (listBlood == null || listBlood.Count == 0)
            {
                BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                throw new Exception("Khong lay duoc HIS_BLOOD theo listBloodId" + LogUtil.TraceData("listBloodId", listBloodId));
            }

            HisBloodCheck bloodChecker = new HisBloodCheck(param);
            foreach (var blood in listBlood)
            {
                valid = valid && bloodChecker.IsUnLock(blood);
            }
            if (!valid)
            {
                throw new Exception("Ket thuc nghiep vu");
            }

            string deleteBlood = DAOWorker.SqlDAO.AddInClause(listBloodId, "DELETE HIS_BLOOD WHERE %IN_CLAUSE% ", "ID");
            executeSqls.Add(deleteBlood);
        }

        private void ProcessExpMestBlood(List<long> listBloodId, List<HIS_EXP_MEST_BLOOD> expMestBloods, ref List<string> executeSqls)
        {
            bool valid = true;
            expMestBloods = expMestBloods != null ? expMestBloods.Where(o => listBloodId.Contains(o.BLOOD_ID)).ToList() : null;
            if (!IsNotNullOrEmpty(expMestBloods))
            {
                BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                throw new Exception("Don mau tra lai. Khong lay duoc HIS_EXP_MEST_BLOOD theo BloodIds" + LogUtil.TraceData("BloodIds", listBloodId));
            }
            HisExpMestBloodCheck expBloodChecker = new HisExpMestBloodCheck(param);
            foreach (var expBlood in expMestBloods)
            {
                valid = valid && expBloodChecker.IsUnLock(expBlood);
            }
            if (!valid)
            {
                throw new Exception("Du lieu da bi khoa");
            }

            string updateExpMestBlood = DAOWorker.SqlDAO.AddInClause(expMestBloods.Select(s => s.ID).ToList(), "UPDATE HIS_EXP_MEST_BLOOD SET IS_TH = NULL WHERE %IN_CLAUSE% ", "ID");
            executeSqls.Add(updateExpMestBlood);
        }
    }
}
