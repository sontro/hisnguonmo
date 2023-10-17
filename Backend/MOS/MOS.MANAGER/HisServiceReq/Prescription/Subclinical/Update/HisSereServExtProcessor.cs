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

namespace MOS.MANAGER.HisServiceReq.Prescription.Subclinical.Update
{
    class HisSereServExtProcessor : BusinessBase
    {
        private HisSereServExtCreate hisSereServExtCreate;
        private HisSereServExtUpdate hisSereServExtUpdate;

        internal HisSereServExtProcessor(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.hisSereServExtCreate = new HisSereServExtCreate(param);
            this.hisSereServExtUpdate = new HisSereServExtUpdate(param);
        }

        internal bool Run(SubclinicalPresSDO data, List<HIS_EXP_MEST_MATERIAL> deleted, HIS_SERVICE_REQ prescription)
        {
            try
            {
                List<long> sereServParentIds = IsNotNullOrEmpty(data.Materials) ? data.Materials.Where(o => o.SereServParentId.HasValue).Select(o => o.SereServParentId.Value).Distinct().ToList() : null;
                if (IsNotNullOrEmpty(sereServParentIds))
                {
                    List<HIS_SERE_SERV_EXT> sereServExts = new HisSereServExtGet().GetBySereServIds(sereServParentIds);
                    List<V_HIS_EXP_MEST_MATERIAL_2> expMestMaterials = new HisExpMestMaterialGet().GetView2BySereServParentIds(sereServParentIds);

                    List<HIS_SERE_SERV_EXT> toUpdates = new List<HIS_SERE_SERV_EXT>();
                    List<HIS_SERE_SERV_EXT> toInserts = new List<HIS_SERE_SERV_EXT>();

                    Mapper.CreateMap<HIS_SERE_SERV_EXT, HIS_SERE_SERV_EXT>();

                    foreach (long id in sereServParentIds)
                    {
                        List<V_HIS_EXP_MEST_MATERIAL_2> films = IsNotNullOrEmpty(expMestMaterials) ? expMestMaterials
                            .Where(o => o.SERE_SERV_PARENT_ID == id && o.IS_FILM == Constant.IS_TRUE && (deleted == null || !deleted.Exists(t => t.ID == o.ID))).ToList() : null;
                        
                        V_HIS_EXP_MEST_MATERIAL_2 f = IsNotNullOrEmpty(films) ? films.Where(o => o.FILM_SIZE_ID.HasValue).OrderBy(o => o.CREATE_TIME).FirstOrDefault() : null;
                        long? filmSizeId = f != null ? f.FILM_SIZE_ID : null;
                        decimal? numberOfFilm = IsNotNullOrEmpty(films) ? (decimal?)films.Sum(o => o.AMOUNT) : null;
                        decimal? numberOfFailedFilm = IsNotNullOrEmpty(films) ? (decimal?)films.Sum(o => o.FAILED_AMOUNT ?? 0) : null;

                        HIS_SERE_SERV_EXT ssExt = IsNotNullOrEmpty(sereServExts) ? sereServExts.Where(o => o.SERE_SERV_ID == id).FirstOrDefault() : null;

                        if (ssExt != null)
                        {
                            HIS_SERE_SERV_EXT before = Mapper.Map<HIS_SERE_SERV_EXT>(ssExt);
                            ssExt.FILM_SIZE_ID = filmSizeId;
                            ssExt.NUMBER_OF_FILM = (long?)numberOfFilm;
                            ssExt.NUMBER_OF_FAIL_FILM = (long?)numberOfFailedFilm;
                            ssExt.SUBCLINICAL_PRES_LOGINNAME = data.RequestLoginName;
                            ssExt.SUBCLINICAL_PRES_USERNAME = data.RequestUserName;
                            ssExt.SUBCLINICAL_PRES_ID = prescription != null ? (long?)prescription.ID : null;

                            if (Inventec.Common.ObjectChecker.ValueChecker.IsPrimitiveDiff<HIS_SERE_SERV_EXT>(before, ssExt))
                            {
                                toUpdates.Add(ssExt);
                            }
                        }
                        else
                        {
                            ssExt = new HIS_SERE_SERV_EXT();
                            ssExt.SERE_SERV_ID = id;
                            ssExt.FILM_SIZE_ID = filmSizeId;
                            ssExt.NUMBER_OF_FILM = (long?)numberOfFilm;
                            ssExt.NUMBER_OF_FAIL_FILM = (long?)numberOfFailedFilm;
                            ssExt.SUBCLINICAL_PRES_LOGINNAME = data.RequestLoginName;
                            ssExt.SUBCLINICAL_PRES_USERNAME = data.RequestUserName;
                            ssExt.SUBCLINICAL_PRES_ID = prescription != null ? (long?)prescription.ID : null;
                            toInserts.Add(ssExt);
                        }
                    }

                    if (IsNotNullOrEmpty(toInserts))
                    {
                        List<long> sereServIds = toInserts.Select(o => o.SERE_SERV_ID).ToList();
                        List<HIS_SERE_SERV> ss = new HisSereServGet().GetByIds(sereServIds);
                        foreach (HIS_SERE_SERV_EXT ext in toInserts)
                        {
                            HIS_SERE_SERV s = IsNotNullOrEmpty(ss) ? ss.Where(o => o.ID == ext.SERE_SERV_ID).FirstOrDefault() : null;
                            if (s != null)
                            {
                                ext.TDL_SERVICE_REQ_ID = s.SERVICE_REQ_ID;
                                ext.TDL_TREATMENT_ID = s.TDL_TREATMENT_ID;
                            }
                        }
                        if (!this.hisSereServExtCreate.CreateList(toInserts))
                        {
                            throw new Exception("Tao thong tin his_sere_serv_ext that bai");
                        }
                    }
                    if (IsNotNullOrEmpty(toUpdates) && !this.hisSereServExtUpdate.UpdateList(toUpdates))
                    {
                        throw new Exception("Update thong tin his_sere_serv_ext that bai");
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
            this.hisSereServExtCreate.RollbackData();
            this.hisSereServExtUpdate.RollbackData();
        }
    }
}
