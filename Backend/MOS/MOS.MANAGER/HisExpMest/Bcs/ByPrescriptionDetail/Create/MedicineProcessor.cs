using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisBcsMetyReqDt;
using MOS.MANAGER.HisBcsMetyReqReq;
using MOS.MANAGER.HisExpMestMatyReq;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.MANAGER.HisExpMestMetyReq;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Base.BaseCompensation.Create
{
    class MedicineProcessor : BusinessBase
    {
        private HisExpMestMetyReqCreate hisExpMestMetyReqCreate;
        private HisExpMestMedicineIncreaseBcsReqAmount increaseMedicine;
        private HisExpMestMetyReqIncreaseBcsReqAmount increaseMetyReq;
        private HisBcsMetyReqDtCreate hisBcsMetyReqDtCreate;
        private HisBcsMetyReqReqCreate hisBcsMetyReqReqCreate;

        internal MedicineProcessor(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisExpMestMetyReqCreate = new HisExpMestMetyReqCreate(param);
            this.increaseMedicine = new HisExpMestMedicineIncreaseBcsReqAmount(param);
            this.increaseMetyReq = new HisExpMestMetyReqIncreaseBcsReqAmount(param);
            this.hisBcsMetyReqDtCreate = new HisBcsMetyReqDtCreate(param);
            this.hisBcsMetyReqReqCreate = new HisBcsMetyReqReqCreate(param);
        }

        private Dictionary<long, decimal> dicIncreaseMedicine = new Dictionary<long, decimal>();
        private Dictionary<long, decimal> dicIncreaseMetyReq = new Dictionary<long, decimal>();

        private Dictionary<HIS_EXP_MEST_METY_REQ, List<HIS_BCS_METY_REQ_DT>> bcsMetyDts = new Dictionary<HIS_EXP_MEST_METY_REQ, List<HIS_BCS_METY_REQ_DT>>();
        private Dictionary<HIS_EXP_MEST_METY_REQ, List<HIS_BCS_METY_REQ_REQ>> bcsMetyReqs = new Dictionary<HIS_EXP_MEST_METY_REQ, List<HIS_BCS_METY_REQ_REQ>>();

        internal bool Run(Dictionary<HIS_EXP_MEST, ExpMestDetail> dicExpMest, List<HIS_EXP_MEST_MEDICINE> expMedicines, List<HIS_EXP_MEST_METY_REQ> metyReqs)
        {
            bool result = false;
            try
            {

                List<HIS_EXP_MEST_METY_REQ> inserteds = new List<HIS_EXP_MEST_METY_REQ>();
                foreach (var dic in dicExpMest)
                {
                    if (IsNotNullOrEmpty(dic.Value.Medicines))
                    {
                        List<HIS_EXP_MEST_METY_REQ> datas = this.MakeExpMestMetyReq(dic.Key, dic.Value.Medicines, expMedicines, metyReqs);
                        inserteds.AddRange(datas);
                    }
                }

                if (IsNotNullOrEmpty(inserteds))
                {
                    if (!this.hisExpMestMetyReqCreate.CreateListSql(inserteds))
                    {
                        throw new Exception("hisExpMestMetyReqCreate. Ket thuc nghiep vu");
                    }

                    if (dicIncreaseMedicine.Count > 0)
                    {
                        if (!this.increaseMedicine.Run(dicIncreaseMedicine))
                        {
                            throw new Exception("increaseMedicine. Ket thuc nghiep vu");
                        }
                    }
                    if (dicIncreaseMetyReq.Count > 0)
                    {
                        if (!this.increaseMetyReq.Run(dicIncreaseMetyReq))
                        {
                            throw new Exception("increaseMetyReq. Ket thuc nghiep vu");
                        }
                    }

                    if (bcsMetyDts.Count > 0)
                    {
                        List<HIS_BCS_METY_REQ_DT> reqDts = new List<HIS_BCS_METY_REQ_DT>();
                        foreach (var dic in bcsMetyDts)
                        {
                            dic.Value.ForEach(o => o.EXP_MEST_METY_REQ_ID = dic.Key.ID);
                            reqDts.AddRange(dic.Value);
                        }
                        if (!this.hisBcsMetyReqDtCreate.CreateListSql(reqDts))
                        {
                            throw new Exception("hisBcsMetyReqDtCreate. Ket thuc nghiep vu");
                        }
                    }
                    if (bcsMetyReqs.Count > 0)
                    {
                        List<HIS_BCS_METY_REQ_REQ> reqDts = new List<HIS_BCS_METY_REQ_REQ>();
                        foreach (var dic in bcsMetyReqs)
                        {
                            dic.Value.ForEach(o => o.EXP_MEST_METY_REQ_ID = dic.Key.ID);
                            reqDts.AddRange(dic.Value);
                        }
                        if (!this.hisBcsMetyReqReqCreate.CreateListSql(reqDts))
                        {
                            throw new Exception("hisBcsMetyReqReqCreate. Ket thuc nghiep vu");
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

        private List<HIS_EXP_MEST_METY_REQ> MakeExpMestMetyReq(HIS_EXP_MEST expMest, List<BaseMedicineTypeSDO> data, List<HIS_EXP_MEST_MEDICINE> expMedicines, List<HIS_EXP_MEST_METY_REQ> metyReqs)
        {
            List<HIS_EXP_MEST_METY_REQ> reqDatas = new List<HIS_EXP_MEST_METY_REQ>();

            foreach (BaseMedicineTypeSDO reqSdo in data)
            {
                Dictionary<long, HIS_EXP_MEST_METY_REQ> dicReq = new Dictionary<long, HIS_EXP_MEST_METY_REQ>();
                decimal reqAmount = reqSdo.Amount;
                List<HIS_EXP_MEST_MEDICINE> approves = expMedicines != null ? expMedicines.Where(o => reqSdo.ExpMestMedicineIds != null && reqSdo.ExpMestMedicineIds.Contains(o.ID)).ToList() : null;
                List<HIS_EXP_MEST_METY_REQ> requests = metyReqs != null ? metyReqs.Where(o => reqSdo.ExpMestMetyReqIds != null && reqSdo.ExpMestMetyReqIds.Contains(o.ID)).ToList() : null;

                if (IsNotNullOrEmpty(requests))
                {
                    var Groups = requests.GroupBy(g => (g.TREATMENT_ID ?? 0)).ToList();
                    Groups = Groups.OrderBy(o => o.Sum(s => s.AMOUNT - ((s.DD_AMOUNT ?? 0) + (s.BCS_REQ_AMOUNT ?? 0)))).ToList();
                    foreach (var group in Groups)
                    {
                        if (reqAmount <= 0)
                        {
                            break;
                        }
                        HIS_EXP_MEST_METY_REQ r = new HIS_EXP_MEST_METY_REQ();
                        if (dicReq.ContainsKey(group.Key)) r = dicReq[group.Key];
                        else dicReq[group.Key] = r;
                        r.MEDICINE_TYPE_ID = reqSdo.MedicineTypeId;
                        r.TDL_MEDI_STOCK_ID = expMest.MEDI_STOCK_ID;
                        r.EXP_MEST_ID = expMest.ID;
                        if (group.Key > 0)
                        {
                            r.TREATMENT_ID = group.Key;
                        }
                        if (!bcsMetyReqs.ContainsKey(r))
                            bcsMetyReqs[r] = new List<HIS_BCS_METY_REQ_REQ>();
                        //reqDatas.Add(r);

                        var list = group.ToList();
                        foreach (var item in list)
                        {
                            if (reqAmount <= 0)
                            {
                                break;
                            }

                            decimal availAmount = item.AMOUNT - ((item.DD_AMOUNT ?? 0) + (item.BCS_REQ_AMOUNT ?? 0));
                            if (availAmount <= 0)
                            {
                                continue;
                            }
                            if (availAmount >= reqAmount)
                            {
                                r.AMOUNT += reqAmount;
                                dicIncreaseMetyReq[item.ID] = reqAmount;
                                HIS_BCS_METY_REQ_REQ rr = new HIS_BCS_METY_REQ_REQ();
                                rr.AMOUNT = reqAmount;
                                rr.PRE_EXP_MEST_METY_REQ_ID = item.ID;
                                rr.TDL_XBTT_EXP_MEST_ID = expMest.ID;
                                rr.TDL_XBTT_EXP_MEST_CODE = expMest.EXP_MEST_CODE;
                                bcsMetyReqs[r].Add(rr);
                                reqAmount = 0;
                            }
                            else
                            {
                                r.AMOUNT += availAmount;
                                dicIncreaseMetyReq[item.ID] = availAmount;
                                HIS_BCS_METY_REQ_REQ rr = new HIS_BCS_METY_REQ_REQ();
                                rr.AMOUNT = availAmount;
                                rr.PRE_EXP_MEST_METY_REQ_ID = item.ID;
                                rr.TDL_XBTT_EXP_MEST_ID = expMest.ID;
                                rr.TDL_XBTT_EXP_MEST_CODE = expMest.EXP_MEST_CODE;
                                bcsMetyReqs[r].Add(rr);
                                reqAmount = reqAmount - availAmount;
                            }
                        }
                    }
                }

                if (IsNotNullOrEmpty(approves) && reqAmount > 0)
                {
                    var Groups = approves.GroupBy(g => (g.TDL_TREATMENT_ID ?? 0)).ToList();
                    Groups = Groups.OrderBy(o => o.Sum(s => s.AMOUNT - (s.BCS_REQ_AMOUNT ?? 0))).ToList();
                    foreach (var group in Groups)
                    {
                        if (reqAmount <= 0)
                        {
                            break;
                        }
                        HIS_EXP_MEST_METY_REQ r = new HIS_EXP_MEST_METY_REQ();
                        if (dicReq.ContainsKey(group.Key)) r = dicReq[group.Key];
                        else dicReq[group.Key] = r;
                        r.MEDICINE_TYPE_ID = reqSdo.MedicineTypeId;
                        r.TDL_MEDI_STOCK_ID = expMest.MEDI_STOCK_ID;
                        r.EXP_MEST_ID = expMest.ID;
                        if (group.Key > 0)
                        {
                            r.TREATMENT_ID = group.Key;
                        }
                        if (!bcsMetyDts.ContainsKey(r))
                            bcsMetyDts[r] = new List<HIS_BCS_METY_REQ_DT>();
                        //reqDatas.Add(r);

                        var list = group.ToList();
                        foreach (var item in list)
                        {
                            if (reqAmount <= 0)
                            {
                                break;
                            }

                            decimal availAmount = item.AMOUNT - (item.BCS_REQ_AMOUNT ?? 0);
                            if (availAmount <= 0)
                            {
                                continue;
                            }
                            if (availAmount >= reqAmount)
                            {
                                r.AMOUNT += reqAmount;
                                dicIncreaseMedicine[item.ID] = reqAmount;
                                HIS_BCS_METY_REQ_DT rr = new HIS_BCS_METY_REQ_DT();
                                rr.AMOUNT = reqAmount;
                                rr.EXP_MEST_MEDICINE_ID = item.ID;
                                rr.TDL_XBTT_EXP_MEST_ID = expMest.ID;
                                rr.TDL_XBTT_EXP_MEST_CODE = expMest.EXP_MEST_CODE;
                                bcsMetyDts[r].Add(rr);
                                reqAmount = 0;
                            }
                            else
                            {
                                r.AMOUNT += availAmount;
                                dicIncreaseMedicine[item.ID] = availAmount;
                                HIS_BCS_METY_REQ_DT rr = new HIS_BCS_METY_REQ_DT();
                                rr.AMOUNT = availAmount;
                                rr.EXP_MEST_MEDICINE_ID = item.ID;
                                rr.TDL_XBTT_EXP_MEST_ID = expMest.ID;
                                rr.TDL_XBTT_EXP_MEST_CODE = expMest.EXP_MEST_CODE;
                                bcsMetyDts[r].Add(rr);
                                reqAmount = reqAmount - availAmount;
                            }
                        }
                    }
                }
                if (reqAmount > 0)
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                    throw new Exception("So luong yeu cau bu lon hon so luong co the bu trong ExpMestMedicine va ExpMestMetyReq: " + LogUtil.TraceData("ReqSDO", reqSdo));
                }
                reqDatas.AddRange(dicReq.Values.ToList());
            }

            return reqDatas;
        }

        internal void Rollback()
        {
            this.hisBcsMetyReqReqCreate.RollbackData();
            this.hisBcsMetyReqDtCreate.RollbackData();
            this.increaseMetyReq.Rollback();
            this.increaseMedicine.RollbackData();
            this.hisExpMestMetyReqCreate.RollbackData();
        }
    }
}
