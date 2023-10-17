using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisServiceReqMety;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Prescription.InPatient.Update
{
    class HisServiceReqMetyProcessor : BusinessBase
    {
        private HisServiceReqMetyCreate hisServiceReqMetyCreate;
        private HisServiceReqMetyUpdate hisServiceReqMetyUpdate;

        internal HisServiceReqMetyProcessor(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.hisServiceReqMetyCreate = new HisServiceReqMetyCreate(param);
            this.hisServiceReqMetyUpdate = new HisServiceReqMetyUpdate(param);
        }

        internal bool Run(List<PresOutStockMetySDO> serviceReqMeties, long serviceReqId, long treatmentId, ref List<HIS_SERVICE_REQ_METY> resultData, ref List<string> sqls)
        {
            try
            {
                List<HIS_SERVICE_REQ_METY> inserts = new List<HIS_SERVICE_REQ_METY>();
                List<HIS_SERVICE_REQ_METY> deletes = new List<HIS_SERVICE_REQ_METY>();
                List<HIS_SERVICE_REQ_METY> updates = new List<HIS_SERVICE_REQ_METY>();
                List<HIS_SERVICE_REQ_METY> beforeUpdates = new List<HIS_SERVICE_REQ_METY>();

                //Lay ra danh sach thong tin cu
                List<HIS_SERVICE_REQ_METY> olds = new HisServiceReqMetyGet().GetByServiceReqId(serviceReqId);
                List<HIS_SERVICE_REQ_METY> news = new List<HIS_SERVICE_REQ_METY>();

                foreach (var sdo in serviceReqMeties)
                {
                    HIS_SERVICE_REQ_METY srMety = new HIS_SERVICE_REQ_METY();
                    srMety.AMOUNT = sdo.Amount;
                    srMety.MEDICINE_TYPE_ID = sdo.MedicineTypeId;
                    srMety.MEDICINE_TYPE_NAME = sdo.MedicineTypeName;
                    srMety.NUM_ORDER = sdo.NumOrder;
                    srMety.PRICE = sdo.Price;
                    srMety.SERVICE_REQ_ID = serviceReqId;
                    srMety.UNIT_NAME = sdo.UnitName;
                    srMety.MEDICINE_USE_FORM_ID = sdo.MedicineUseFormId;
                    srMety.SPEED = sdo.Speed;
                    srMety.USE_TIME_TO = sdo.UseTimeTo;
                    srMety.TUTORIAL = sdo.Tutorial;
                    srMety.NOON = sdo.Noon;
                    srMety.AFTERNOON = sdo.Afternoon;
                    srMety.EVENING = sdo.Evening;
                    srMety.MORNING = sdo.Morning;
                    srMety.HTU_ID = sdo.HtuId;
                    srMety.TDL_TREATMENT_ID = treatmentId;
                    srMety.PRES_AMOUNT = sdo.PresAmount;
                    srMety.PREVIOUS_USING_COUNT = sdo.PreviousUsingCount;
                    srMety.EXCEED_LIMIT_IN_PRES_REASON = sdo.ExceedLimitInPresReason;
                    srMety.EXCEED_LIMIT_IN_DAY_REASON = sdo.ExceedLimitInDayReason;
                    srMety.ODD_PRES_REASON = sdo.OddPresReason;
                    if (IsNotNullOrEmpty(sdo.MedicineInfoSdos))
                    {
                        HIS_SERVICE_REQ sr = new HisServiceReqGet().GetById(serviceReqId);
                        foreach (var item in sdo.MedicineInfoSdos)
                        {
                            if (!item.IsNoPrescription && sr != null && item.IntructionTime == sr.INTRUCTION_TIME)
                            {
                                srMety.OVER_KIDNEY_REASON = item.OverKidneyReason;
                                srMety.OVER_RESULT_TEST_REASON = item.OverResultTestReason;
                            }
                        }
                    }
                    news.Add(srMety);
                }

                this.GetDiff(olds, news, ref inserts, ref deletes, ref updates, ref beforeUpdates);

                if (IsNotNullOrEmpty(inserts))
                {
                    inserts.ForEach(o => o.SERVICE_REQ_ID = serviceReqId);
                    if (!this.hisServiceReqMetyCreate.CreateList(inserts))
                    {
                        throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                    }
                }

                if (IsNotNullOrEmpty(updates) && !this.hisServiceReqMetyUpdate.UpdateList(updates, beforeUpdates))
                {
                    throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                }

                if (sqls == null)
                {
                    sqls = new List<string>();
                }

                if (IsNotNullOrEmpty(deletes))
                {
                    List<long> deleteIds = deletes.Select(o => o.ID).ToList();
                    string sql = DAOWorker.SqlDAO.AddInClause(deleteIds, "DELETE FROM HIS_SERVICE_REQ_METY WHERE %IN_CLAUSE%", "ID");
                    sqls.Add(sql);
                }

                this.PassResult(olds, inserts, updates, deletes, ref resultData);

                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
        }

        private void PassResult(List<HIS_SERVICE_REQ_METY> olds, List<HIS_SERVICE_REQ_METY> inserts, List<HIS_SERVICE_REQ_METY> updates, List<HIS_SERVICE_REQ_METY> deletes, ref List<HIS_SERVICE_REQ_METY> resultData)
        {
            if (IsNotNullOrEmpty(inserts) || IsNotNullOrEmpty(olds))
            {
                resultData = new List<HIS_SERVICE_REQ_METY>();
                if (IsNotNullOrEmpty(inserts))
                {
                    resultData.AddRange(inserts);
                }
                if (IsNotNullOrEmpty(updates))
                {
                    resultData.AddRange(updates);
                }
                if (IsNotNullOrEmpty(olds))
                {
                    List<HIS_SERVICE_REQ_METY> remains = olds;
                    remains.RemoveAll(o => deletes != null && deletes.Exists(t => t.ID == o.ID));
                    remains.RemoveAll(o => updates != null && updates.Exists(t => t.ID == o.ID));
                    resultData.AddRange(remains);
                }
            }
        }

        private void GetDiff(List<HIS_SERVICE_REQ_METY> olds, List<HIS_SERVICE_REQ_METY> news, ref List<HIS_SERVICE_REQ_METY> inserts, ref List<HIS_SERVICE_REQ_METY> deletes, ref List<HIS_SERVICE_REQ_METY> updates, ref List<HIS_SERVICE_REQ_METY> oldOfUpdates)
        {
            //Duyet du lieu truyen len de kiem tra thong tin thay doi
            if (!IsNotNullOrEmpty(news))
            {
                deletes = olds;
            }
            else if (!IsNotNullOrEmpty(olds))
            {
                inserts = news;
            }
            else
            {
                Mapper.CreateMap<HIS_SERVICE_REQ_METY, HIS_SERVICE_REQ_METY>();

                //Duyet danh sach moi, nhung du lieu co trong moi ma ko co trong cu ==> can them moi
                foreach (HIS_SERVICE_REQ_METY newMety in news)
                {
                    HIS_SERVICE_REQ_METY old = olds
                        .Where(t => t.MEDICINE_TYPE_ID == newMety.MEDICINE_TYPE_ID
                            && t.MEDICINE_TYPE_NAME == newMety.MEDICINE_TYPE_NAME
                        ).FirstOrDefault();

                    if (old == null)
                    {
                        inserts.Add(newMety);
                    }
                    else if (old.NUM_ORDER != newMety.NUM_ORDER
                        || old.UNIT_NAME != newMety.UNIT_NAME
                        || old.AMOUNT != newMety.AMOUNT
                        || old.TUTORIAL != newMety.TUTORIAL
                        || old.MORNING != newMety.MORNING
                        || old.NOON != newMety.NOON
                        || old.AFTERNOON != newMety.AFTERNOON
                        || old.EVENING != newMety.EVENING
                        || old.HTU_ID != newMety.HTU_ID
                        || old.USE_TIME_TO != newMety.USE_TIME_TO
                        || old.MEDICINE_USE_FORM_ID != newMety.MEDICINE_USE_FORM_ID
                        || old.PRES_AMOUNT != newMety.PRES_AMOUNT
                        || old.PREVIOUS_USING_COUNT != newMety.PREVIOUS_USING_COUNT
                        || old.EXCEED_LIMIT_IN_PRES_REASON != newMety.EXCEED_LIMIT_IN_PRES_REASON
                        || old.EXCEED_LIMIT_IN_DAY_REASON != newMety.EXCEED_LIMIT_IN_DAY_REASON
                        || old.ODD_PRES_REASON != newMety.ODD_PRES_REASON
                        || old.OVER_KIDNEY_REASON != newMety.OVER_KIDNEY_REASON
                        || old.OVER_RESULT_TEST_REASON != newMety.OVER_RESULT_TEST_REASON
                        )
                    {
                        HIS_SERVICE_REQ_METY oldOfUpdate = Mapper.Map<HIS_SERVICE_REQ_METY>(old);
                        old.NUM_ORDER = newMety.NUM_ORDER;
                        old.UNIT_NAME = newMety.UNIT_NAME;
                        old.AMOUNT = newMety.AMOUNT;
                        old.TUTORIAL = newMety.TUTORIAL;
                        old.USE_TIME_TO = newMety.USE_TIME_TO;
                        old.MEDICINE_USE_FORM_ID = newMety.MEDICINE_USE_FORM_ID;
                        old.PRES_AMOUNT = newMety.PRES_AMOUNT;
                        old.PREVIOUS_USING_COUNT = newMety.PREVIOUS_USING_COUNT;
                        old.EXCEED_LIMIT_IN_PRES_REASON = newMety.EXCEED_LIMIT_IN_PRES_REASON;
                        old.EXCEED_LIMIT_IN_DAY_REASON = newMety.EXCEED_LIMIT_IN_DAY_REASON;
                        old.ODD_PRES_REASON = newMety.ODD_PRES_REASON;
                        old.OVER_KIDNEY_REASON = newMety.OVER_KIDNEY_REASON;
                        old.OVER_RESULT_TEST_REASON = newMety.OVER_RESULT_TEST_REASON;

                        oldOfUpdates.Add(oldOfUpdate);
                        updates.Add(old);
                    }
                }

                //Duyet danh sach cu, nhung du lieu co trong cu ma ko co trong moi ==> can xoa
                foreach (HIS_SERVICE_REQ_METY old in olds)
                {
                    HIS_SERVICE_REQ_METY newMety = news
                        .Where(t => t.MEDICINE_TYPE_ID == old.MEDICINE_TYPE_ID
                            && t.MEDICINE_TYPE_NAME == old.MEDICINE_TYPE_NAME
                        ).FirstOrDefault();

                    if (newMety == null)
                    {
                        deletes.Add(old);
                    }
                }
            }
        }

        internal void Rollback()
        {
            this.hisServiceReqMetyCreate.RollbackData();
            this.hisServiceReqMetyUpdate.RollbackData();
        }
    }
}
