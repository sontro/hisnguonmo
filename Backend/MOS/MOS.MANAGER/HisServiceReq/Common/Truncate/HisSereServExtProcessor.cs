using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.MANAGER.HisIcd;
using MOS.MANAGER.HisMaterialBean;
using MOS.MANAGER.HisMedicineBean.Handle;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisSereServExt;
using MOS.MANAGER.Token;
using MOS.SDO;
using MOS.UTILITY;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Common.Truncate
{
    class HisSereServExtProcessor : BusinessBase
    {
        private HisSereServExtUpdate hisSereServExtUpdate;

        internal HisSereServExtProcessor(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.hisSereServExtUpdate = new HisSereServExtUpdate(param);
        }

        internal bool Run(HIS_SERVICE_REQ serviceReq, List<long> deletedExpMestMaterialIds, List<long> sereServParentIds)
        {
            try
            {
                if (serviceReq != null && IsNotNullOrEmpty(sereServParentIds) && IsNotNullOrEmpty(deletedExpMestMaterialIds) && serviceReq.PRESCRIPTION_TYPE_ID == (short)PrescriptionType.SUBCLINICAL)
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
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
            return false;
        }

        internal void Rollback()
        {
            this.hisSereServExtUpdate.RollbackData();
        }
    }
}
