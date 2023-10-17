using AutoMapper;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.MANAGER.HisSereServExt;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.AssignService.TruncateReq
{
    class SereServExtProcessor : BusinessBase
    {
        private HisSereServExtUpdate hisSereServExtUpdate;

        internal SereServExtProcessor()
            : base()
        {
            this.hisSereServExtUpdate = new HisSereServExtUpdate(param);
        }

        internal SereServExtProcessor(CommonParam param)
            : base(param)
        {
            this.hisSereServExtUpdate = new HisSereServExtUpdate(param);
        }
        internal bool Run(List<HIS_SERVICE_REQ> lstServiceReqTruncate, List<HIS_SERE_SERV> lstSereServTruncate)
        {
            bool result = false;
            try
            {
                List<HIS_SERVICE_REQ> lstPress = IsNotNullOrEmpty(lstServiceReqTruncate) ? lstServiceReqTruncate.Where(o => o.PRESCRIPTION_TYPE_ID.HasValue && o.PRESCRIPTION_TYPE_ID.Value == (short)PrescriptionType.SUBCLINICAL).ToList() : null;

                List<long> sereServParentIds = IsNotNullOrEmpty(lstSereServTruncate) ? lstSereServTruncate.Where(o => lstPress != null && lstPress.Any(a => a.ID == o.SERVICE_REQ_ID) && o.PARENT_ID.HasValue).Select(o => o.PARENT_ID.Value).ToList() : null;
                List<long> deletedExpMestMaterialIds = IsNotNullOrEmpty(lstSereServTruncate) ? lstSereServTruncate.Where(o => lstPress != null && lstPress.Any(a => a.ID == o.SERVICE_REQ_ID) && o.EXP_MEST_MATERIAL_ID.HasValue).Select(o => o.EXP_MEST_MATERIAL_ID.Value).ToList() : null;

                if (IsNotNullOrEmpty(lstPress) && IsNotNullOrEmpty(sereServParentIds) && IsNotNullOrEmpty(deletedExpMestMaterialIds))
                {
                    List<HIS_SERE_SERV_EXT> sereServExts = new HisSereServExtGet().GetBySereServIds(sereServParentIds);
                    List<V_HIS_EXP_MEST_MATERIAL_2> expMestMaterials = new HisExpMestMaterialGet().GetView2BySereServParentIds(sereServParentIds);

                    if (IsNotNullOrEmpty(sereServExts))
                    {
                        List<HIS_SERE_SERV_EXT> toUpdates = new List<HIS_SERE_SERV_EXT>();
                        foreach (HIS_SERE_SERV_EXT sereServExt in sereServExts)
                        {
                            Mapper.CreateMap<HIS_SERE_SERV_EXT, HIS_SERE_SERV_EXT>();

                            List<V_HIS_EXP_MEST_MATERIAL_2> films = IsNotNullOrEmpty(expMestMaterials) ? expMestMaterials.Where(o => o.IS_FILM == Constant.IS_TRUE && !deletedExpMestMaterialIds.Contains(o.ID)).ToList() : null;

                            V_HIS_EXP_MEST_MATERIAL_2 f = IsNotNullOrEmpty(films) ? films.Where(o => o.FILM_SIZE_ID.HasValue).OrderBy(o => o.CREATE_TIME).FirstOrDefault() : null;
                            long? filmSizeId = f != null ? f.FILM_SIZE_ID : null;
                            decimal? numberOfFilm = IsNotNullOrEmpty(films) ? (decimal?)films.Sum(o => o.AMOUNT) : null;
                            decimal? numberOfFailedFilm = IsNotNullOrEmpty(films) ? (decimal?)films.Sum(o => o.FAILED_AMOUNT ?? 0) : null;

                            if (sereServExt != null)
                            {
                                HIS_SERE_SERV_EXT before = Mapper.Map<HIS_SERE_SERV_EXT>(sereServExt);
                                sereServExt.NUMBER_OF_FILM = (long?)numberOfFilm;
                                sereServExt.NUMBER_OF_FAIL_FILM = (long?)numberOfFailedFilm;

                                if (Inventec.Common.ObjectChecker.ValueChecker.IsPrimitiveDiff<HIS_SERE_SERV_EXT>(before, sereServExt))
                                {
                                    toUpdates.Add(sereServExt);
                                }
                            }
                        }

                        if (IsNotNullOrEmpty(toUpdates) && !this.hisSereServExtUpdate.UpdateList(toUpdates))
                        {
                            throw new Exception("Update thong tin his_sere_serv_ext that bai");
                        }
                    }

                }
                result = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
                param.HasException = true;
            }
            return result;
        }

        internal void Rollback()
        {
            try
            {
                this.hisSereServExtUpdate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
