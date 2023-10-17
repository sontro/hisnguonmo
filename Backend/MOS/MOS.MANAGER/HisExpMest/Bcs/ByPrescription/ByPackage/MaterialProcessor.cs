using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.MANAGER.HisExpMestMatyReq;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Chms.Bcs
{
    class MaterialProcessor : BusinessBase
    {
        private HisExpMestMatyReqCreate hisExpMestMatyReqCreate;

        internal MaterialProcessor(CommonParam param)
            : base(param)
        {
            this.hisExpMestMatyReqCreate = new HisExpMestMatyReq.HisExpMestMatyReqCreate(param);
        }

        internal bool Run(HisExpMestBcsSDO data, HIS_EXP_MEST expMest, List<HIS_EXP_MEST_MATERIAL> approves, List<HIS_EXP_MEST_MATY_REQ> requests, ref List<HIS_EXP_MEST_MATY_REQ> matyRequests)
        {
            bool result = false;
            try
            {
                Dictionary<long, Dictionary<long, ExpMaterialTypeSDO>> dicSdo = this.MakeExpMestMaterialTypeSdo(data, approves, requests);
                this.ProcessMatyReq(dicSdo, expMest, ref matyRequests);
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

        private Dictionary<long, Dictionary<long, ExpMaterialTypeSDO>> MakeExpMestMaterialTypeSdo(HisExpMestBcsSDO data, List<HIS_EXP_MEST_MATERIAL> approves, List<HIS_EXP_MEST_MATY_REQ> requests)
        {
            Dictionary<long, Dictionary<long, ExpMaterialTypeSDO>> dicMaterialType = new Dictionary<long, Dictionary<long, ExpMaterialTypeSDO>>();

            if (IsNotNullOrEmpty(approves))
            {
                var Groups = approves.GroupBy(g => new { g.TDL_MATERIAL_TYPE_ID, g.TDL_TREATMENT_ID }).ToList();
                foreach (var group in Groups)
                {
                    var listApp = group.ToList();
                    long treatmentId = group.Key.TDL_TREATMENT_ID ?? 0;
                    long medicineTypeId = group.Key.TDL_MATERIAL_TYPE_ID ?? 0;
                    if (!dicMaterialType.ContainsKey(treatmentId))
                        dicMaterialType[treatmentId] = new Dictionary<long, ExpMaterialTypeSDO>();

                    ExpMaterialTypeSDO sdo = new ExpMaterialTypeSDO();
                    if (dicMaterialType[treatmentId].ContainsKey(medicineTypeId))
                    {
                        sdo = dicMaterialType[treatmentId][medicineTypeId];
                        sdo.Amount = sdo.Amount + listApp.Sum(s => s.AMOUNT);
                        sdo.MaterialTypeId = medicineTypeId;
                    }
                    else
                    {
                        sdo.Amount = listApp.Sum(s => s.AMOUNT);
                        sdo.MaterialTypeId = medicineTypeId;
                        dicMaterialType[treatmentId][medicineTypeId] = sdo;
                    }
                }
            }

            if (IsNotNullOrEmpty(requests))
            {
                var Groups = requests.GroupBy(g => new { g.MATERIAL_TYPE_ID, g.TREATMENT_ID }).ToList();
                foreach (var group in Groups)
                {
                    var listApp = group.ToList();
                    long treatmentId = group.Key.TREATMENT_ID ?? 0;
                    long medicineTypeId = group.Key.MATERIAL_TYPE_ID;

                    decimal amount = listApp.Sum(s => (s.AMOUNT - (s.DD_AMOUNT ?? 0)));
                    if (amount > 0)
                    {
                        if (!dicMaterialType.ContainsKey(treatmentId))
                            dicMaterialType[treatmentId] = new Dictionary<long, ExpMaterialTypeSDO>();

                        ExpMaterialTypeSDO sdo = null;
                        if (dicMaterialType[treatmentId].ContainsKey(medicineTypeId))
                        {
                            sdo = dicMaterialType[treatmentId][medicineTypeId];
                        }
                        else
                        {
                            sdo = new ExpMaterialTypeSDO();
                            sdo.MaterialTypeId = medicineTypeId;
                            dicMaterialType[treatmentId][medicineTypeId] = sdo;
                        }

                        sdo.Amount += amount;
                    }
                }
            }

            return dicMaterialType;
        }

        private void ProcessMatyReq(Dictionary<long, Dictionary<long, ExpMaterialTypeSDO>> dicMaterialType, HIS_EXP_MEST expMest, ref List<HIS_EXP_MEST_MATY_REQ> matyDatas)
        {
            List<HIS_EXP_MEST_MATY_REQ> makeDatas = null;

            if (dicMaterialType.Count > 0)
            {
                makeDatas = new List<HIS_EXP_MEST_MATY_REQ>();
                foreach (var dic in dicMaterialType)
                {
                    if (IsNotNullOrEmpty(dic.Value))
                    {
                        foreach (ExpMaterialTypeSDO sdo in dic.Value.Select(s => s.Value).ToList())
                        {
                            HIS_EXP_MEST_MATY_REQ exp = new HIS_EXP_MEST_MATY_REQ();
                            exp.EXP_MEST_ID = expMest.ID;
                            exp.AMOUNT = sdo.Amount;
                            exp.MATERIAL_TYPE_ID = sdo.MaterialTypeId;
                            exp.NUM_ORDER = sdo.NumOrder;
                            exp.DESCRIPTION = sdo.Description;
                            exp.TDL_MEDI_STOCK_ID = expMest.MEDI_STOCK_ID;
                            exp.TREATMENT_ID = dic.Key > 0 ? (long?)dic.Key : null; ;
                            makeDatas.Add(exp);
                        }
                    }
                }
                if (IsNotNullOrEmpty(makeDatas))
                {
                    if (!this.hisExpMestMatyReqCreate.CreateList(makeDatas))
                    {
                        throw new Exception("hisExpMestMatyReqCreate. Ket thuc nghiep vu");
                    }
                }
            }

            matyDatas = makeDatas;
        }

        internal void Rollback()
        {
            try
            {
                this.hisExpMestMatyReqCreate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
