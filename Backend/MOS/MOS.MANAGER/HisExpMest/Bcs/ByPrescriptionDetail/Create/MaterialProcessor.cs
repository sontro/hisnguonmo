using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisBcsMatyReqDt;
using MOS.MANAGER.HisBcsMatyReqReq;
using MOS.MANAGER.HisExpMestMatyReq;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Common.Logging;

namespace MOS.MANAGER.HisExpMest.Base.BaseCompensation.Create
{
    class MaterialProcessor : BusinessBase
    {
        private HisExpMestMatyReqCreate hisExpMestMatyReqCreate;
        private HisExpMestMaterialIncreaseBcsReqAmount increaseMaterial;
        private HisExpMestMatyReqIncreaseBcsReqAmount increaseMatyReq;
        private HisBcsMatyReqDtCreate hisBcsMatyReqDtCreate;
        private HisBcsMatyReqReqCreate hisBcsMatyReqReqCreate;

        internal MaterialProcessor(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisExpMestMatyReqCreate = new HisExpMestMatyReqCreate(param);
            this.increaseMaterial = new HisExpMestMaterialIncreaseBcsReqAmount(param);
            this.increaseMatyReq = new HisExpMestMatyReqIncreaseBcsReqAmount(param);
            this.hisBcsMatyReqDtCreate = new HisBcsMatyReqDtCreate(param);
            this.hisBcsMatyReqReqCreate = new HisBcsMatyReqReqCreate(param);
        }

        private Dictionary<long, decimal> dicIncreaseMaterial = new Dictionary<long, decimal>();
        private Dictionary<long, decimal> dicIncreaseMatyReq = new Dictionary<long, decimal>();

        private Dictionary<HIS_EXP_MEST_MATY_REQ, List<HIS_BCS_MATY_REQ_DT>> bcsMatyDts = new Dictionary<HIS_EXP_MEST_MATY_REQ, List<HIS_BCS_MATY_REQ_DT>>();
        private Dictionary<HIS_EXP_MEST_MATY_REQ, List<HIS_BCS_MATY_REQ_REQ>> bcsMatyReqs = new Dictionary<HIS_EXP_MEST_MATY_REQ, List<HIS_BCS_MATY_REQ_REQ>>();

        internal bool Run(Dictionary<HIS_EXP_MEST, ExpMestDetail> dicExpMest, List<HIS_EXP_MEST_MATERIAL> expMaterials, List<HIS_EXP_MEST_MATY_REQ> matyReqs)
        {
            bool result = false;
            try
            {

                List<HIS_EXP_MEST_MATY_REQ> inserteds = new List<HIS_EXP_MEST_MATY_REQ>();
                foreach (var dic in dicExpMest)
                {
                    if (IsNotNullOrEmpty(dic.Value.Materials))
                    {
                        List<HIS_EXP_MEST_MATY_REQ> datas = this.MakeExpMestMatyReq(dic.Key, dic.Value.Materials, expMaterials, matyReqs);
                        inserteds.AddRange(datas);
                    }
                }

                if (IsNotNullOrEmpty(inserteds))
                {
                    if (!this.hisExpMestMatyReqCreate.CreateListSql(inserteds))
                    {
                        throw new Exception("hisExpMestMatyReqCreate. Ket thuc nghiep vu");
                    }

                    if (dicIncreaseMaterial.Count > 0)
                    {
                        if (!this.increaseMaterial.Run(dicIncreaseMaterial))
                        {
                            throw new Exception("increaseMaterial. Ket thuc nghiep vu");
                        }
                    }
                    if (dicIncreaseMatyReq.Count > 0)
                    {
                        if (!this.increaseMatyReq.Run(dicIncreaseMatyReq))
                        {
                            throw new Exception("increaseMatyReq. Ket thuc nghiep vu");
                        }
                    }

                    if (bcsMatyDts.Count > 0)
                    {
                        List<HIS_BCS_MATY_REQ_DT> reqDts = new List<HIS_BCS_MATY_REQ_DT>();
                        foreach (var dic in bcsMatyDts)
                        {
                            dic.Value.ForEach(o => o.EXP_MEST_MATY_REQ_ID = dic.Key.ID);
                            reqDts.AddRange(dic.Value);
                        }
                        if (!this.hisBcsMatyReqDtCreate.CreateListSql(reqDts))
                        {
                            throw new Exception("hisBcsMatyReqDtCreate. Ket thuc nghiep vu");
                        }
                    }
                    if (bcsMatyReqs.Count > 0)
                    {
                        List<HIS_BCS_MATY_REQ_REQ> reqDts = new List<HIS_BCS_MATY_REQ_REQ>();
                        foreach (var dic in bcsMatyReqs)
                        {
                            dic.Value.ForEach(o => o.EXP_MEST_MATY_REQ_ID = dic.Key.ID);
                            reqDts.AddRange(dic.Value);
                        }
                        if (!this.hisBcsMatyReqReqCreate.CreateListSql(reqDts))
                        {
                            throw new Exception("hisBcsMatyReqReqCreate. Ket thuc nghiep vu");
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

        private List<HIS_EXP_MEST_MATY_REQ> MakeExpMestMatyReq(HIS_EXP_MEST expMest, List<BaseMaterialTypeSDO> data, List<HIS_EXP_MEST_MATERIAL> expMaterials, List<HIS_EXP_MEST_MATY_REQ> matyReqs)
        {
            List<HIS_EXP_MEST_MATY_REQ> reqDatas = new List<HIS_EXP_MEST_MATY_REQ>();

            foreach (BaseMaterialTypeSDO reqSdo in data)
            {
                Dictionary<long, HIS_EXP_MEST_MATY_REQ> dicReq = new Dictionary<long, HIS_EXP_MEST_MATY_REQ>();
                decimal reqAmount = reqSdo.Amount;
                List<HIS_EXP_MEST_MATERIAL> approves = expMaterials != null ? expMaterials.Where(o => reqSdo.ExpMestMaterialIds != null && reqSdo.ExpMestMaterialIds.Contains(o.ID)).ToList() : null;
                List<HIS_EXP_MEST_MATY_REQ> requests = matyReqs != null ? matyReqs.Where(o => reqSdo.ExpMestMatyReqIds != null && reqSdo.ExpMestMatyReqIds.Contains(o.ID)).ToList() : null;

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
                        HIS_EXP_MEST_MATY_REQ r = new HIS_EXP_MEST_MATY_REQ();
                        if (dicReq.ContainsKey(group.Key)) r = dicReq[group.Key];
                        else dicReq[group.Key] = r;
                        r.MATERIAL_TYPE_ID = reqSdo.MaterialTypeId;
                        r.TDL_MEDI_STOCK_ID = expMest.MEDI_STOCK_ID;
                        r.EXP_MEST_ID = expMest.ID;
                        if (group.Key > 0)
                        {
                            r.TREATMENT_ID = group.Key;
                        }
                        if (!bcsMatyReqs.ContainsKey(r))
                            bcsMatyReqs[r] = new List<HIS_BCS_MATY_REQ_REQ>();
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
                                dicIncreaseMatyReq[item.ID] = reqAmount;
                                HIS_BCS_MATY_REQ_REQ rr = new HIS_BCS_MATY_REQ_REQ();
                                rr.AMOUNT = reqAmount;
                                rr.PRE_EXP_MEST_MATY_REQ_ID = item.ID;
                                rr.TDL_XBTT_EXP_MEST_ID = expMest.ID;
                                rr.TDL_XBTT_EXP_MEST_CODE = expMest.EXP_MEST_CODE;
                                bcsMatyReqs[r].Add(rr);
                                reqAmount = 0;
                            }
                            else
                            {
                                r.AMOUNT += availAmount;
                                dicIncreaseMatyReq[item.ID] = availAmount;
                                HIS_BCS_MATY_REQ_REQ rr = new HIS_BCS_MATY_REQ_REQ();
                                rr.AMOUNT = availAmount;
                                rr.PRE_EXP_MEST_MATY_REQ_ID = item.ID;
                                rr.TDL_XBTT_EXP_MEST_ID = expMest.ID;
                                rr.TDL_XBTT_EXP_MEST_CODE = expMest.EXP_MEST_CODE;
                                bcsMatyReqs[r].Add(rr);
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
                        HIS_EXP_MEST_MATY_REQ r = new HIS_EXP_MEST_MATY_REQ();
                        if (dicReq.ContainsKey(group.Key)) r = dicReq[group.Key];
                        else dicReq[group.Key] = r;
                        r.MATERIAL_TYPE_ID = reqSdo.MaterialTypeId;
                        r.TDL_MEDI_STOCK_ID = expMest.MEDI_STOCK_ID;
                        r.EXP_MEST_ID = expMest.ID;
                        if (group.Key > 0)
                        {
                            r.TREATMENT_ID = group.Key;
                        }
                        if (!bcsMatyDts.ContainsKey(r))
                            bcsMatyDts[r] = new List<HIS_BCS_MATY_REQ_DT>();
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
                                dicIncreaseMaterial[item.ID] = reqAmount;
                                HIS_BCS_MATY_REQ_DT rr = new HIS_BCS_MATY_REQ_DT();
                                rr.AMOUNT = reqAmount;
                                rr.EXP_MEST_MATERIAL_ID = item.ID;
                                rr.TDL_XBTT_EXP_MEST_ID = expMest.ID;
                                rr.TDL_XBTT_EXP_MEST_CODE = expMest.EXP_MEST_CODE;
                                bcsMatyDts[r].Add(rr);
                                reqAmount = 0;
                            }
                            else
                            {
                                r.AMOUNT += availAmount;
                                dicIncreaseMaterial[item.ID] = availAmount;
                                HIS_BCS_MATY_REQ_DT rr = new HIS_BCS_MATY_REQ_DT();
                                rr.AMOUNT = availAmount;
                                rr.EXP_MEST_MATERIAL_ID = item.ID;
                                rr.TDL_XBTT_EXP_MEST_ID = expMest.ID;
                                rr.TDL_XBTT_EXP_MEST_CODE = expMest.EXP_MEST_CODE;
                                bcsMatyDts[r].Add(rr);
                                reqAmount = reqAmount - availAmount;
                            }
                        }
                    }
                }
                if (reqAmount > 0)
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                    throw new Exception("So luong yeu cau bu lon hon so luong co the bu trong ExpMestMaterial va ExpMestMatyReq: " + LogUtil.TraceData("ReqSDO", reqSdo));
                }
                reqDatas.AddRange(dicReq.Values.ToList());
            }

            return reqDatas;
        }

        internal void Rollback()
        {
            this.hisBcsMatyReqDtCreate.RollbackData();
            this.hisBcsMatyReqReqCreate.RollbackData();
            this.increaseMatyReq.Rollback();
            this.increaseMaterial.RollbackData();
            this.hisExpMestMatyReqCreate.RollbackData();
        }
    }
}
