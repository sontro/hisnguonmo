using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisSereServ.Delete;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Aggr.Unapprove
{
    class SereServProcessor : BusinessBase
    {
        private HisSereServDeleteSql hisSereServDeleteSql;

        internal SereServProcessor()
            : base()
        {
            this.hisSereServDeleteSql = new HisSereServDeleteSql(param);
        }

        internal SereServProcessor(CommonParam param)
            : base(param)
        {
            this.hisSereServDeleteSql = new HisSereServDeleteSql(param);
        }

        internal bool Run(List<HIS_EXP_MEST_MEDICINE> deleteMedicines, List<HIS_EXP_MEST_MATERIAL> deleteMaterials)
        {
            bool result = false;
            try
            {
                if (IsNotNullOrEmpty(deleteMedicines) || IsNotNullOrEmpty(deleteMaterials))
                {
                    List<HIS_SERE_SERV> deletes = null;
                    if (!this.IsValidSereServ(deleteMedicines, deleteMaterials, ref deletes))
                    {
                        return false;
                    }
                    if (!this.hisSereServDeleteSql.Run(deletes))
                    {
                        throw new Exception("hisSereServDeleteSql. Ket thuc nghiep vu");
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

        private bool IsValidSereServ(List<HIS_EXP_MEST_MEDICINE> deleteMedicines, List<HIS_EXP_MEST_MATERIAL> deleteMaterials, ref List<HIS_SERE_SERV> sereServsToDelete)
        {
            try
            {
                List<long> serviceReqIds = new List<long>();
                int count = 0;
                if (IsNotNullOrEmpty(deleteMaterials))
                {
                    serviceReqIds.AddRange(deleteMaterials.Select(s => s.TDL_SERVICE_REQ_ID ?? 0).Distinct().ToList());
                    count += deleteMaterials.Count;
                }
                if (IsNotNullOrEmpty(deleteMedicines))
                {
                    serviceReqIds.AddRange(deleteMedicines.Select(s => s.TDL_SERVICE_REQ_ID ?? 0).Distinct().ToList());
                    count += deleteMedicines.Count;
                }
                serviceReqIds = serviceReqIds.Distinct().ToList();

                var sereServs = new HisSereServGet(param).GetByServiceReqIds(serviceReqIds);
                var deleteSereServs = sereServs != null ? sereServs.Where(o => o.IS_NOT_PRES.HasValue && o.IS_NOT_PRES.Value == Constant.IS_TRUE && ((o.EXP_MEST_MEDICINE_ID.HasValue && deleteMedicines != null && deleteMedicines.Any(a => a.ID == o.EXP_MEST_MEDICINE_ID.Value)) || (o.EXP_MEST_MATERIAL_ID.HasValue && deleteMaterials != null && deleteMaterials.Any(a => a.ID == o.EXP_MEST_MATERIAL_ID.Value)))).ToList() : null;
                var deleteSereServIds = deleteSereServs != null ? deleteSereServs.Select(o => o.ID).ToList() : null;

                if (!IsNotNullOrEmpty(deleteSereServs) || deleteSereServs.Count != count)
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                    throw new Exception("so luong xoa bu le SereServ khac voi so luong xoa bu le exp_mest_material + exp_mest_medicine");
                }

                sereServsToDelete = deleteSereServs;
                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
        }

        internal void Rollback()
        {
            this.hisSereServDeleteSql.Rollback();
        }
    }
}
