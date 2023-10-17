using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.MANAGER.HisExpMestMetyReq;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Chms.Bcs
{
    class MedicineProcessor : BusinessBase
    {
        private HisExpMestMetyReqCreate hisExpMestMetyReqCreate;

        internal MedicineProcessor(CommonParam param)
            : base(param)
        {
            this.hisExpMestMetyReqCreate = new HisExpMestMetyReqCreate(param);
        }

        internal bool Run(HisExpMestBcsSDO data, HIS_EXP_MEST expMest, List<HIS_EXP_MEST_MEDICINE> approves, List<HIS_EXP_MEST_METY_REQ> requests, ref List<HIS_EXP_MEST_METY_REQ> metyRequests)
        {
            bool result = false;
            try
            {
                Dictionary<long, Dictionary<long, ExpMedicineTypeSDO>> dicSdo = this.MakeExpMestMedicineTypeSdo(data, approves, requests);
                this.ProcessMetyReq(dicSdo, expMest, ref metyRequests);
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

        private Dictionary<long, Dictionary<long, ExpMedicineTypeSDO>> MakeExpMestMedicineTypeSdo(HisExpMestBcsSDO data, List<HIS_EXP_MEST_MEDICINE> approves, List<HIS_EXP_MEST_METY_REQ> requests)
        {
            Dictionary<long, Dictionary<long, ExpMedicineTypeSDO>> dicMedicineType = new Dictionary<long, Dictionary<long, ExpMedicineTypeSDO>>();

            if (IsNotNullOrEmpty(approves))
            {
                var Groups = approves.GroupBy(g => new { g.TDL_MEDICINE_TYPE_ID, g.TDL_TREATMENT_ID }).ToList();
                foreach (var group in Groups)
                {
                    var listApp = group.ToList();
                    long treatmentId = group.Key.TDL_TREATMENT_ID ?? 0;
                    long medicineTypeId = group.Key.TDL_MEDICINE_TYPE_ID ?? 0;
                    if (!dicMedicineType.ContainsKey(treatmentId))
                        dicMedicineType[treatmentId] = new Dictionary<long, ExpMedicineTypeSDO>();

                    ExpMedicineTypeSDO sdo = new ExpMedicineTypeSDO();
                    if (dicMedicineType[treatmentId].ContainsKey(medicineTypeId))
                    {
                        sdo = dicMedicineType[treatmentId][medicineTypeId];
                        sdo.Amount = sdo.Amount + listApp.Sum(s => s.AMOUNT);
                        sdo.MedicineTypeId = medicineTypeId;
                    }
                    else
                    {
                        sdo.Amount = listApp.Sum(s => s.AMOUNT);
                        sdo.MedicineTypeId = medicineTypeId;
                        dicMedicineType[treatmentId][medicineTypeId] = sdo;
                    }
                }
            }

            if (IsNotNullOrEmpty(requests))
            {
                var Groups = requests.GroupBy(g => new { g.MEDICINE_TYPE_ID, g.TREATMENT_ID }).ToList();
                foreach (var group in Groups)
                {
                    var listApp = group.ToList();
                    long treatmentId = group.Key.TREATMENT_ID ?? 0;
                    long medicineTypeId = group.Key.MEDICINE_TYPE_ID;

                    decimal amount = listApp.Sum(s => (s.AMOUNT - (s.DD_AMOUNT ?? 0)));
                    if (amount > 0)
                    {
                        if (!dicMedicineType.ContainsKey(treatmentId))
                            dicMedicineType[treatmentId] = new Dictionary<long, ExpMedicineTypeSDO>();

                        ExpMedicineTypeSDO sdo = null;
                        if (dicMedicineType[treatmentId].ContainsKey(medicineTypeId))
                        {
                            sdo = dicMedicineType[treatmentId][medicineTypeId];
                        }
                        else
                        {
                            sdo = new ExpMedicineTypeSDO();
                            sdo.MedicineTypeId = medicineTypeId;
                            dicMedicineType[treatmentId][medicineTypeId] = sdo;
                        }

                        sdo.Amount += amount;
                    }
                }
            }

            return dicMedicineType;
        }

        private void ProcessMetyReq(Dictionary<long, Dictionary<long, ExpMedicineTypeSDO>> dicMedicineType, HIS_EXP_MEST expMest, ref List<HIS_EXP_MEST_METY_REQ> metyDatas)
        {
            List<HIS_EXP_MEST_METY_REQ> makeDatas = null;

            if (dicMedicineType.Count > 0)
            {
                makeDatas = new List<HIS_EXP_MEST_METY_REQ>();
                foreach (var dic in dicMedicineType)
                {
                    if (IsNotNullOrEmpty(dic.Value))
                    {
                        foreach (ExpMedicineTypeSDO sdo in dic.Value.Select(s => s.Value).ToList())
                        {
                            HIS_EXP_MEST_METY_REQ exp = new HIS_EXP_MEST_METY_REQ();
                            exp.EXP_MEST_ID = expMest.ID;
                            exp.AMOUNT = sdo.Amount;
                            exp.MEDICINE_TYPE_ID = sdo.MedicineTypeId;
                            exp.NUM_ORDER = sdo.NumOrder;
                            exp.DESCRIPTION = sdo.Description;
                            exp.TDL_MEDI_STOCK_ID = expMest.MEDI_STOCK_ID;
                            exp.TREATMENT_ID = dic.Key > 0 ? (long?)dic.Key : null;
                            makeDatas.Add(exp);
                        }
                    }
                }
                if (IsNotNullOrEmpty(makeDatas))
                {
                    if (!this.hisExpMestMetyReqCreate.CreateList(makeDatas))
                    {
                        throw new Exception("hisExpMestMetyReqCreate. Ket thuc nghiep vu");
                    }
                }
            }

            metyDatas = makeDatas;
        }

        internal void Rollback()
        {
            try
            {
                this.hisExpMestMetyReqCreate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
