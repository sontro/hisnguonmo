using AutoMapper;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpMestMatyReq;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Base.BaseAddition.Update
{
    class MaterialProcessor : BusinessBase
    {
        private HisExpMestMatyReqMaker hisExpMestMatyReqMaker;
        private HisExpMestMatyReqUpdate hisExpMestMatyReqUpdate;

        internal MaterialProcessor(CommonParam param)
            : base(param)
        {
            this.hisExpMestMatyReqMaker = new HisExpMestMatyReqMaker(param);
            this.hisExpMestMatyReqUpdate = new HisExpMestMatyReqUpdate(param);
        }

        internal bool Run(List<ExpMaterialTypeSDO> medicines, HIS_EXP_MEST expMest, List<HIS_EXP_MEST_MATY_REQ> oldReqs, ref List<HIS_EXP_MEST_MATY_REQ> resultData, ref List<string> sqls)
        {
            try
            {
                List<HIS_EXP_MEST_MATY_REQ> returnDatas = new List<HIS_EXP_MEST_MATY_REQ>();

                List<HIS_EXP_MEST_MATY_REQ> updates = new List<HIS_EXP_MEST_MATY_REQ>();
                List<HIS_EXP_MEST_MATY_REQ> beforeUpdates = new List<HIS_EXP_MEST_MATY_REQ>();
                List<HIS_EXP_MEST_MATY_REQ> creates = new List<HIS_EXP_MEST_MATY_REQ>();
                List<HIS_EXP_MEST_MATY_REQ> deletes = new List<HIS_EXP_MEST_MATY_REQ>();
                List<ExpMaterialTypeSDO> newSdos = new List<ExpMaterialTypeSDO>();

                if (IsNotNullOrEmpty(medicines))
                {
                    Mapper.CreateMap<HIS_EXP_MEST_MATY_REQ, HIS_EXP_MEST_MATY_REQ>();
                    foreach (ExpMaterialTypeSDO sdo in medicines)
                    {
                        HIS_EXP_MEST_MATY_REQ old = oldReqs != null ? oldReqs.FirstOrDefault(o => o.MATERIAL_TYPE_ID == sdo.MaterialTypeId) : null;
                        if (old != null)
                        {
                            HIS_EXP_MEST_MATY_REQ before = Mapper.Map<HIS_EXP_MEST_MATY_REQ>(old);
                            if (old.AMOUNT != sdo.Amount)
                            {
                                old.AMOUNT = sdo.Amount;
                                updates.Add(old);
                                beforeUpdates.Add(before);
                            }
                            returnDatas.Add(old);
                        }
                        else
                        {
                            newSdos.Add(sdo);
                        }
                    }
                }

                deletes = oldReqs != null ? oldReqs.Where(o => returnDatas == null || !returnDatas.Any(a => a.ID == o.ID)).ToList() : null;

                if (IsNotNullOrEmpty(newSdos))
                {
                    List<HIS_EXP_MEST_MATY_REQ> datas = null;
                    if (!this.hisExpMestMatyReqMaker.Run(newSdos, expMest, ref datas))
                    {
                        throw new Exception("hisExpMestMatyReqMaker. Ket thuc nghiep vu");
                    }
                    returnDatas.AddRange(datas);
                }

                if (IsNotNullOrEmpty(updates))
                {
                    if (!this.hisExpMestMatyReqUpdate.UpdateList(updates, beforeUpdates))
                    {
                        throw new Exception("hisExpMestMatyReqUpdate. Ket thuc nghiep vu");
                    }
                }

                if (IsNotNullOrEmpty(deletes))
                {
                    string sql = DAOWorker.SqlDAO.AddInClause(deletes.Select(s => s.ID).ToList(), "DELETE HIS_EXP_MEST_MATY_REQ WHERE %IN_CLAUSE% ", "ID");
                    sqls.Add(sql);
                }

                if (IsNotNullOrEmpty(returnDatas))
                {
                    resultData = returnDatas;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return false;
            }
            return true;
        }

        internal void Rollback()
        {
            this.hisExpMestMatyReqUpdate.RollbackData();
            this.hisExpMestMatyReqMaker.Rollback();
        }
    }
}
