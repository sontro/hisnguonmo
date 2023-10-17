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

namespace MOS.MANAGER.HisServiceReq.Prescription.Subclinical.CreateByConfig
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

        internal bool Run(List<HIS_SERE_SERV_EXT> sereServExts, HIS_SERE_SERV parentSereServ, HIS_SERVICE_REQ prescription, bool hasMaterial, ref HIS_SERE_SERV_EXT sereServExtResult)
        {
            try
            {
                HIS_SERE_SERV_EXT sereServExt = IsNotNullOrEmpty(sereServExts) ? sereServExts.Where(o => o.SERE_SERV_ID == parentSereServ.ID).FirstOrDefault() : null;
                List<V_HIS_EXP_MEST_MATERIAL_2> expMestMaterials = hasMaterial ? new HisExpMestMaterialGet().GetView2BySereServParentId(parentSereServ.ID) : null;

                Mapper.CreateMap<HIS_SERE_SERV_EXT, HIS_SERE_SERV_EXT>();

                List<V_HIS_EXP_MEST_MATERIAL_2> films = IsNotNullOrEmpty(expMestMaterials) ? expMestMaterials.Where(o => o.IS_FILM == Constant.IS_TRUE).ToList() : null;

                V_HIS_EXP_MEST_MATERIAL_2 f = IsNotNullOrEmpty(films) ? films.Where(o => o.FILM_SIZE_ID.HasValue).OrderBy(o => o.CREATE_TIME).FirstOrDefault() : null;
                long? filmSizeId = f != null ? f.FILM_SIZE_ID : null;
                decimal? numberOfFilm = IsNotNullOrEmpty(films) ? (decimal?)films.Sum(o => o.AMOUNT) : null;
                decimal? numberOfFailedFilm = IsNotNullOrEmpty(films) ? (decimal?)films.Sum(o => o.FAILED_AMOUNT ?? 0) : null;

                if (sereServExt != null)
                {
                    HIS_SERE_SERV_EXT before = Mapper.Map<HIS_SERE_SERV_EXT>(sereServExt);
                    sereServExt.FILM_SIZE_ID = filmSizeId;
                    sereServExt.NUMBER_OF_FILM = (long?)numberOfFilm;
                    sereServExt.NUMBER_OF_FAIL_FILM = (long?)numberOfFailedFilm;
                    sereServExt.SUBCLINICAL_PRES_LOGINNAME = prescription.REQUEST_LOGINNAME;
                    sereServExt.SUBCLINICAL_PRES_USERNAME = prescription.REQUEST_USERNAME;
                    sereServExt.SUBCLINICAL_PRES_ID = prescription.ID;

                    if (Inventec.Common.ObjectChecker.ValueChecker.IsPrimitiveDiff<HIS_SERE_SERV_EXT>(before, sereServExt))
                    {
                        if (!this.hisSereServExtUpdate.Update(sereServExt, false))
                        {
                            throw new Exception("Update thong tin his_sere_serv_ext that bai");
                        }
                    }
                }
                else
                {
                    sereServExt = new HIS_SERE_SERV_EXT();
                    sereServExt.SERE_SERV_ID = parentSereServ.ID;
                    sereServExt.FILM_SIZE_ID = filmSizeId;
                    sereServExt.NUMBER_OF_FILM = (long?)numberOfFilm;
                    sereServExt.NUMBER_OF_FAIL_FILM = (long?)numberOfFailedFilm;
                    sereServExt.SUBCLINICAL_PRES_LOGINNAME = prescription.REQUEST_LOGINNAME;
                    sereServExt.SUBCLINICAL_PRES_USERNAME = prescription.REQUEST_USERNAME;
                    sereServExt.SUBCLINICAL_PRES_ID = prescription.ID;
                    sereServExt.TDL_SERVICE_REQ_ID = parentSereServ.SERVICE_REQ_ID;
                    sereServExt.TDL_TREATMENT_ID = parentSereServ.TDL_TREATMENT_ID;

                    if (!this.hisSereServExtCreate.Create(sereServExt))
                    {
                        throw new Exception("Tao thong tin his_sere_serv_ext that bai");
                    }
                }
                sereServExtResult = sereServExt;
                return true;
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
