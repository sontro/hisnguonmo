using AutoMapper;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpMestMetyReq;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Base.BaseReduction.Update
{
    class MedicineProcessor : BusinessBase
    {
        private HisExpMestMetyReqMaker hisExpMestMetyReqMaker;
        private HisExpMestMetyReqUpdate hisExpMestMetyReqUpdate;

        internal MedicineProcessor(CommonParam param)
            : base(param)
        {
            this.hisExpMestMetyReqMaker = new HisExpMestMetyReqMaker(param);
            this.hisExpMestMetyReqUpdate = new HisExpMestMetyReqUpdate(param);
        }

        internal bool Run(List<ExpMedicineTypeSDO> medicines, HIS_EXP_MEST expMest, List<HIS_EXP_MEST_METY_REQ> oldReqs, ref List<HIS_EXP_MEST_METY_REQ> resultData, ref List<string> sqls)
        {
            try
            {
                List<HIS_EXP_MEST_METY_REQ> returnDatas = new List<HIS_EXP_MEST_METY_REQ>();

                List<HIS_EXP_MEST_METY_REQ> updates = new List<HIS_EXP_MEST_METY_REQ>();
                List<HIS_EXP_MEST_METY_REQ> beforeUpdates = new List<HIS_EXP_MEST_METY_REQ>();
                List<HIS_EXP_MEST_METY_REQ> creates = new List<HIS_EXP_MEST_METY_REQ>();
                List<HIS_EXP_MEST_METY_REQ> deletes = new List<HIS_EXP_MEST_METY_REQ>();
                List<ExpMedicineTypeSDO> newSdos = new List<ExpMedicineTypeSDO>();

                if (IsNotNullOrEmpty(medicines))
                {
                    Mapper.CreateMap<HIS_EXP_MEST_METY_REQ, HIS_EXP_MEST_METY_REQ>();
                    foreach (ExpMedicineTypeSDO sdo in medicines)
                    {
                        HIS_EXP_MEST_METY_REQ old = oldReqs != null ? oldReqs.FirstOrDefault(o => o.MEDICINE_TYPE_ID == sdo.MedicineTypeId) : null;
                        if (old != null)
                        {
                            HIS_EXP_MEST_METY_REQ before = Mapper.Map<HIS_EXP_MEST_METY_REQ>(old);
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
                    List<HIS_EXP_MEST_METY_REQ> datas = null;
                    if (!this.hisExpMestMetyReqMaker.Run(newSdos, expMest, ref datas))
                    {
                        throw new Exception("hisExpMestMetyReqMaker. Ket thuc nghiep vu");
                    }
                    returnDatas.AddRange(datas);
                }

                if (IsNotNullOrEmpty(updates))
                {
                    if (!this.hisExpMestMetyReqUpdate.UpdateList(updates, beforeUpdates))
                    {
                        throw new Exception("hisExpMestMetyReqUpdate. Ket thuc nghiep vu");
                    }
                }

                if (IsNotNullOrEmpty(deletes))
                {
                    string sql = DAOWorker.SqlDAO.AddInClause(deletes.Select(s => s.ID).ToList(), "DELETE HIS_EXP_MEST_METY_REQ WHERE %IN_CLAUSE% ", "ID");
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
            this.hisExpMestMetyReqUpdate.RollbackData();
            this.hisExpMestMetyReqMaker.Rollback();
        }
    }
}
