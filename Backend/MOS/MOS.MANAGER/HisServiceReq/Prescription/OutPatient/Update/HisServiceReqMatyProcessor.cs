using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisServiceReqMaty;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Prescription.OutPatient.Update
{
    class HisServiceReqMatyProcessor: BusinessBase
    {
        private HisServiceReqMatyCreate hisServiceReqMatyCreate;
        private HisServiceReqMatyUpdate hisServiceReqMatyUpdate;

        internal HisServiceReqMatyProcessor(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.hisServiceReqMatyCreate = new HisServiceReqMatyCreate(param);
            this.hisServiceReqMatyUpdate = new HisServiceReqMatyUpdate(param);
        }

        internal bool Run(List<PresOutStockMatySDO> serviceReqMaties, List<PresMaterialSDO> materials, HIS_SERVICE_REQ serviceReq, ref List<HIS_SERVICE_REQ_MATY> resultData, ref List<string> sqls)
        {
            try
            {
                List<HIS_SERVICE_REQ_MATY> inserts = new List<HIS_SERVICE_REQ_MATY>();
                List<HIS_SERVICE_REQ_MATY> deletes = new List<HIS_SERVICE_REQ_MATY>();
                List<HIS_SERVICE_REQ_MATY> updates = new List<HIS_SERVICE_REQ_MATY>();
                List<HIS_SERVICE_REQ_MATY> beforeUpdates = new List<HIS_SERVICE_REQ_MATY>();

                //Lay ra danh sach thong tin cu
                List<HIS_SERVICE_REQ_MATY> olds = new HisServiceReqMatyGet().GetByServiceReqId(serviceReq.ID);

                //Don ngoai kho
                List<HIS_SERVICE_REQ_MATY> oldOutPres = IsNotNullOrEmpty(olds) ? olds.Where(o => o.IS_SUB_PRES != Constant.IS_TRUE).ToList() : null;
                
                List<HIS_SERVICE_REQ_MATY> news = serviceReqMaties != null ? serviceReqMaties.Select(o => new HIS_SERVICE_REQ_MATY
                {
                    AMOUNT = o.Amount,
                    MATERIAL_TYPE_ID = o.MaterialTypeId,
                    MATERIAL_TYPE_NAME = o.MaterialTypeName,
                    NUM_ORDER = o.NumOrder,
                    PRICE = o.Price,
                    SERVICE_REQ_ID = serviceReq.ID,
                    UNIT_NAME = o.UnitName,
                    TUTORIAL = o.Tutorial,
                    TDL_TREATMENT_ID = serviceReq.TREATMENT_ID,
                    PRES_AMOUNT = o.PresAmount,
                    EXCEED_LIMIT_IN_PRES_REASON = o.ExceedLimitInPresReason,
                    EXCEED_LIMIT_IN_DAY_REASON = o.ExceedLimitInDayReason,
                }).ToList() : null;

                this.GetDiff(oldOutPres, news, ref inserts, ref deletes, ref updates, ref beforeUpdates);

                //Don phu
                List<HIS_SERVICE_REQ_MATY> oldSubPres = IsNotNullOrEmpty(olds) ? olds.Where(o => o.IS_SUB_PRES == Constant.IS_TRUE).ToList() : null;
                List<HIS_SERVICE_REQ_MATY> newSubPres = new HisServiceReqOutPatientPresUtil().MakeMaty(materials, new List<HIS_SERVICE_REQ>(){serviceReq});

                this.GetDiff(oldSubPres, newSubPres, ref inserts, ref deletes, ref updates, ref beforeUpdates);

                
                if (IsNotNullOrEmpty(inserts))
                {
                    inserts.ForEach(o => o.SERVICE_REQ_ID = serviceReq.ID);
                    if (!this.hisServiceReqMatyCreate.CreateList(inserts))
                    {
                        throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                    }
                }

                if (IsNotNullOrEmpty(updates) && !this.hisServiceReqMatyUpdate.UpdateList(updates, beforeUpdates))
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
                    string sql = DAOWorker.SqlDAO.AddInClause(deleteIds, "DELETE FROM HIS_SERVICE_REQ_MATY WHERE %IN_CLAUSE%", "ID");
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

        private void PassResult(List<HIS_SERVICE_REQ_MATY> olds, List<HIS_SERVICE_REQ_MATY> inserts, List<HIS_SERVICE_REQ_MATY> updates, List<HIS_SERVICE_REQ_MATY> deletes, ref List<HIS_SERVICE_REQ_MATY> resultData)
        {
            if (IsNotNullOrEmpty(inserts) || IsNotNullOrEmpty(olds))
            {
                resultData = new List<HIS_SERVICE_REQ_MATY>();
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
                    List<HIS_SERVICE_REQ_MATY> remains = olds;
                    remains.RemoveAll(o => deletes != null && deletes.Exists(t => t.ID == o.ID));
                    remains.RemoveAll(o => updates != null && updates.Exists(t => t.ID == o.ID));
                    resultData.AddRange(remains);
                }
            }
        }

        private void GetDiff(List<HIS_SERVICE_REQ_MATY> olds, List<HIS_SERVICE_REQ_MATY> news, ref List<HIS_SERVICE_REQ_MATY> inserts, ref List<HIS_SERVICE_REQ_MATY> deletes, ref List<HIS_SERVICE_REQ_MATY> updates, ref List<HIS_SERVICE_REQ_MATY> oldOfUpdates)
        {
            //Duyet du lieu truyen len de kiem tra thong tin thay doi
            if (!IsNotNullOrEmpty(news))
            {
                if (IsNotNullOrEmpty(olds))
                {
                    deletes.AddRange(olds);
                }
            }
            else if (!IsNotNullOrEmpty(olds))
            {
                if (IsNotNullOrEmpty(news))
                {
                    inserts.AddRange(news);
                }
            }
            else
            {
                Mapper.CreateMap<HIS_SERVICE_REQ_MATY, HIS_SERVICE_REQ_MATY>();

                //Duyet danh sach moi, nhung du lieu co trong moi ma ko co trong cu ==> can them moi
                foreach (HIS_SERVICE_REQ_MATY newMaty in news)
                {
                    HIS_SERVICE_REQ_MATY old = olds
                        .Where(t => t.MATERIAL_TYPE_ID == newMaty.MATERIAL_TYPE_ID
                            && t.MATERIAL_TYPE_NAME == newMaty.MATERIAL_TYPE_NAME
                        ).FirstOrDefault();

                    if (old == null)
                    {
                        inserts.Add(newMaty);
                    }
                    else if (old.NUM_ORDER != newMaty.NUM_ORDER
                        || old.UNIT_NAME != newMaty.UNIT_NAME
                        || old.AMOUNT != newMaty.AMOUNT
                        || old.TUTORIAL != newMaty.TUTORIAL
                        || old.PRES_AMOUNT != newMaty.PRES_AMOUNT
                        || old.EXCEED_LIMIT_IN_PRES_REASON != newMaty.EXCEED_LIMIT_IN_PRES_REASON
                        || old.EXCEED_LIMIT_IN_DAY_REASON != newMaty.EXCEED_LIMIT_IN_DAY_REASON)
                    {
                        HIS_SERVICE_REQ_MATY oldOfUpdate = Mapper.Map<HIS_SERVICE_REQ_MATY>(old);
                        old.NUM_ORDER = newMaty.NUM_ORDER;
                        old.UNIT_NAME = newMaty.UNIT_NAME;
                        old.AMOUNT = newMaty.AMOUNT;
                        old.TUTORIAL = newMaty.TUTORIAL;
                        old.PRES_AMOUNT = newMaty.PRES_AMOUNT;
                        old.EXCEED_LIMIT_IN_PRES_REASON = newMaty.EXCEED_LIMIT_IN_PRES_REASON;
                        old.EXCEED_LIMIT_IN_DAY_REASON = newMaty.EXCEED_LIMIT_IN_DAY_REASON;

                        oldOfUpdates.Add(oldOfUpdate);
                        updates.Add(old);
                    }
                }

                //Duyet danh sach cu, nhung du lieu co trong cu ma ko co trong moi ==> can xoa
                foreach (HIS_SERVICE_REQ_MATY old in olds)
                {
                    HIS_SERVICE_REQ_MATY newMaty = news
                        .Where(t => t.MATERIAL_TYPE_ID == old.MATERIAL_TYPE_ID
                            && t.MATERIAL_TYPE_NAME == old.MATERIAL_TYPE_NAME
                        ).FirstOrDefault();

                    if (newMaty == null)
                    {
                        deletes.Add(old);
                    }
                }
            }
        }

        internal void Rollback()
        {
            this.hisServiceReqMatyCreate.RollbackData();
            this.hisServiceReqMatyUpdate.RollbackData();
        }
    }
}
