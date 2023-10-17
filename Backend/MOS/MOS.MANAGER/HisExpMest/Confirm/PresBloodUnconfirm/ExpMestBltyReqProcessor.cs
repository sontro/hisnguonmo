﻿using AutoMapper;
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

namespace MOS.MANAGER.HisExpMest.Confirm.PresBloodUnconfirm
{
    class ExpMestBltyReqProcessor : BusinessBase
    {
        private HisExpMestBltyReqUpdate hisExpMestBltyReqUpdate;

        internal ExpMestBltyReqProcessor(CommonParam param)
            : base(param)
        {
            this.hisExpMestBltyReqUpdate = new HisExpMestBltyReqUpdate(param);
        }

        internal bool Run(List<HIS_EXP_MEST_BLTY_REQ> expBltyReqs)
        {
            bool result = false;
            try
            {
                List<HIS_EXP_MEST_BLTY_REQ> updates = new List<HIS_EXP_MEST_BLTY_REQ>();
                List<HIS_EXP_MEST_BLTY_REQ> befores = new List<HIS_EXP_MEST_BLTY_REQ>();
                Mapper.CreateMap<HIS_EXP_MEST_BLTY_REQ, HIS_EXP_MEST_BLTY_REQ>();
                foreach (HIS_EXP_MEST_BLTY_REQ req in expBltyReqs)
                {
                    if (req.REQ_BLOOD_TYPE_ID.HasValue
                        && req.REQ_AMOUNT.HasValue)
                    {
                        HIS_EXP_MEST_BLTY_REQ before = Mapper.Map<HIS_EXP_MEST_BLTY_REQ>(req);
                        req.AMOUNT = req.REQ_AMOUNT.Value;
                        req.BLOOD_ABO_ID = req.REQ_BLOOD_ABO_ID;
                        req.BLOOD_RH_ID = req.REQ_BLOOD_RH_ID;
                        req.BLOOD_TYPE_ID = req.REQ_BLOOD_TYPE_ID.Value;
                        req.REQ_AMOUNT = null;
                        req.REQ_BLOOD_ABO_ID = null;
                        req.REQ_BLOOD_RH_ID = null;
                        req.REQ_BLOOD_TYPE_ID = null;
                        updates.Add(req);
                        befores.Add(before);
                    }
                }

                if (IsNotNullOrEmpty(updates) && !this.hisExpMestBltyReqUpdate.UpdateList(updates, befores))
                {
                    throw new Exception("hisExpMestBltyReqUpdate. Ket thuc nghiep vu");
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
                this.hisExpMestBltyReqUpdate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
