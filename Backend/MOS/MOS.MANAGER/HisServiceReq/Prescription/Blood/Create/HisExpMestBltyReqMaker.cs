using AutoMapper;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpMestBltyReq;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Prescription.Blood
{
    class HisExpMestBltyReqMaker : BusinessBase
    {
        private HisExpMestBltyReqCreate hisExpMestBltyReqCreate;

        internal HisExpMestBltyReqMaker()
            : base()
        {
            this.Init();
        }

        internal HisExpMestBltyReqMaker(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisExpMestBltyReqCreate = new HisExpMestBltyReqCreate(param);
        }

        internal bool Run(PatientBloodPresSDO data, HIS_EXP_MEST expMest, ref List<HIS_EXP_MEST_BLTY_REQ> resultData)
        {
            bool result = false;
            try
            {
                if (IsNotNullOrEmpty(data.ExpMestBltyReqs) && IsNotNull(expMest))
                {
                    Mapper.CreateMap<HIS_EXP_MEST_BLTY_REQ, HIS_EXP_MEST_BLTY_REQ>();
                    List<HIS_EXP_MEST_BLTY_REQ> createList = Mapper.Map<List<HIS_EXP_MEST_BLTY_REQ>>(data.ExpMestBltyReqs);
                    foreach(HIS_EXP_MEST_BLTY_REQ req in createList)
                    {
                        req.EXP_MEST_ID = expMest.ID;
                        req.TDL_MEDI_STOCK_ID = expMest.MEDI_STOCK_ID;
                        req.IS_OUT_PARENT_FEE = !req.SERE_SERV_PARENT_ID.HasValue ? null : req.IS_OUT_PARENT_FEE;
                    }
                    if (!this.hisExpMestBltyReqCreate.CreateList(createList))
                    {
                        throw new Exception("Ket thuc nghiep vu. Rollback du lieu.");
                    }

                    resultData = createList;
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

        internal void Rollback()
        {
            this.hisExpMestBltyReqCreate.RollbackData();
        }
    }
}
