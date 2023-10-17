using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisImpMest.Aggr
{
    class DeleteImpMestProcessor : BusinessBase
    {
        internal DeleteImpMestProcessor()
            : base()
        {

        }

        internal DeleteImpMestProcessor(CommonParam param)
            : base(param)
        {

        }

        internal bool Run(List<HIS_IMP_MEST_MEDICINE> allMedicines, List<HIS_IMP_MEST_MATERIAL> allMaterials, List<HIS_IMP_MEST_MEDICINE> deleteMedicines, List<HIS_IMP_MEST_MATERIAL> deleteMaterials, ref List<long> idDeletes, ref List<string> sqls)
        {
            bool result = false;
            try
            {
                if (IsNotNullOrEmpty(deleteMedicines) || IsNotNullOrEmpty(deleteMaterials))
                {
                    List<long> impMestIds = new List<long>();
                    if (IsNotNullOrEmpty(deleteMaterials))
                    {
                        impMestIds.AddRange(deleteMaterials.Select(s => s.IMP_MEST_ID).Distinct().ToList());
                        sqls.Add(DAOWorker.SqlDAO.AddInClause(deleteMaterials.Select(s => s.ID).ToList(), "DELETE HIS_IMP_MEST_MATERIAL WHERE %IN_CLAUSE% ", "ID"));
                    }
                    if (IsNotNullOrEmpty(deleteMedicines))
                    {
                        impMestIds.AddRange(deleteMedicines.Select(s => s.IMP_MEST_ID).Distinct().ToList());
                        sqls.Add(DAOWorker.SqlDAO.AddInClause(deleteMedicines.Select(s => s.ID).ToList(), "DELETE HIS_IMP_MEST_MEDICINE WHERE %IN_CLAUSE% ", "ID"));
                    }
                    impMestIds = impMestIds.Distinct().ToList();
                    foreach (var impMestId in impMestIds)
                    {
                        var mediDeletes = deleteMedicines != null ? deleteMedicines.Where(o => o.IMP_MEST_ID == impMestId).ToList() : null;
                        var mateDeletes = deleteMaterials != null ? deleteMaterials.Where(o => o.IMP_MEST_ID == impMestId).ToList() : null;
                        var mediAlls = allMedicines != null ? allMedicines.Where(o => o.IMP_MEST_ID == impMestId).ToList() : null;
                        var mateAlls = allMaterials != null ? allMaterials.Where(o => o.IMP_MEST_ID == impMestId).ToList() : null;

                        if (!IsNotNullOrEmpty(mediDeletes) && IsNotNullOrEmpty(mediAlls))
                            continue;
                        if (IsNotNullOrEmpty(mediDeletes) && mediAlls.Any(a => !mediDeletes.Exists(e => e.ID == a.ID)))
                        {
                            continue;
                        }

                        if (!IsNotNullOrEmpty(mateDeletes) && IsNotNullOrEmpty(mateAlls))
                            continue;
                        if (IsNotNullOrEmpty(mateDeletes) && mateAlls.Any(a => !mateDeletes.Exists(e => e.ID == a.ID)))
                        {
                            continue;
                        }
                        sqls.Add(String.Format("DELETE HIS_IMP_MEST WHERE ID = {0}", impMestId));
                        idDeletes.Add(impMestId);
                    }
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
    }
}
