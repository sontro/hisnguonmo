using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisPatient;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisSereServTein;
using MOS.TDO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisServiceReq.Test
{
    /// <summary>
    /// Xu ly tao thong tin xet nghiem
    /// </summary>
    partial class HisServiceReqTestCreate : BusinessBase
    {
        private List<HIS_SERE_SERV_TEIN> recentHisSereServTeins = new List<HIS_SERE_SERV_TEIN>();
        private HisSereServTeinCreate hisSereServTeinCreate;

        internal HisServiceReqTestCreate()
            : base()
        {
            this.hisSereServTeinCreate = new HisSereServTeinCreate(param);
        }

        internal HisServiceReqTestCreate(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.hisSereServTeinCreate = new HisSereServTeinCreate(param);
        }

        internal bool Create(HIS_SERVICE_REQ serviceReq, List<HIS_SERE_SERV> hisSereServs)
        {
            bool result = false;
            try
            {
                //Insert du lieu HIS_SERE_SERV_TEIN
                //Lay tat ca du lieu TestIndex trong CSDL
                List<HIS_SERE_SERV_TEIN> toInsertSereServTeins = new List<HIS_SERE_SERV_TEIN>();
                if (IsNotNullOrEmpty(HisTestIndexCFG.DATA_VIEW))
                {
                    //can cu vao danh sach HIS_SERE_SERV da duoc tao, thuc hien tao du lieu HIS_SERE_SERV_TEIN
                    foreach (HIS_SERE_SERV dto in hisSereServs)
                    {
                        List<V_HIS_TEST_INDEX> indexs = HisTestIndexCFG.DATA_VIEW
                            .Where(o => o.TEST_SERVICE_TYPE_ID == dto.SERVICE_ID && o.IS_ACTIVE == IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE).ToList();

                        if (IsNotNullOrEmpty(indexs))
                        {
                            foreach (V_HIS_TEST_INDEX d in indexs)
                            {
                                HIS_SERE_SERV_TEIN sereServTein = new HIS_SERE_SERV_TEIN();
                                sereServTein.TEST_INDEX_ID = d.ID;
                                sereServTein.SERE_SERV_ID = dto.ID;
                                sereServTein.TDL_TREATMENT_ID = dto.TDL_TREATMENT_ID;
                                sereServTein.TDL_SERVICE_REQ_ID = dto.SERVICE_REQ_ID;
                                toInsertSereServTeins.Add(sereServTein);
                            }
                        }
                    }

                    //Insert HIS_SERE_SERV_TEIN vao CSDL
                    if (IsNotNullOrEmpty(toInsertSereServTeins) && !this.hisSereServTeinCreate.CreateList(toInsertSereServTeins, serviceReq.TDL_PATIENT_DOB, serviceReq.TDL_PATIENT_GENDER_ID.Value))
                    {
                        throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                    }
                    this.recentHisSereServTeins.AddRange(toInsertSereServTeins);
                }

                result = true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

    }
}
