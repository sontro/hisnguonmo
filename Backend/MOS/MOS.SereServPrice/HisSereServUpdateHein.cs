using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Common.ObjectChecker;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryHein.Bhyt;
using MOS.LibraryHein.Common;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisKskContract;
using MOS.MANAGER.HisPatientType;
using MOS.MANAGER.HisPatientTypeAlter;
using MOS.MANAGER.HisSereServ.Update;
using MOS.MANAGER.HisServiceReq;
using MOS.MANAGER.HisTreatment;
using MOS.UTILITY;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisSereServ
{
    internal partial class HisSereServUpdateHein : BusinessBase
    {
        private HisSereServCreate hisSereServCreate;
        private HisSereServUpdateSql hisSereServUpdate;
        private HisSereServDelete hisSereServDelete;
        private SetPriceWithBhytPolicy setPriceWithBhytPolicy;
        private HisSereServSetInfo addInfo;
        private List<HIS_SERE_SERV> beforeUpdates;
        private List<HIS_PATIENT_TYPE_ALTER> patientTypeAlters;
        private bool verifyTreatment;
        private HIS_TREATMENT treatment;

        internal HisSereServUpdateHein(CommonParam paramUpdate, HIS_TREATMENT treatment, bool verifyTreatment)
            : base(paramUpdate)
        {
            this.patientTypeAlters = new HisPatientTypeAlterGet().GetByTreatmentId(treatment.ID);
            this.Init(treatment, verifyTreatment);
        }

        internal HisSereServUpdateHein(CommonParam paramUpdate, HIS_TREATMENT treatment, List<HIS_PATIENT_TYPE_ALTER> ptas, bool verifyTreatment)
            : base(paramUpdate)
        {
            this.patientTypeAlters = ptas;
            this.Init(treatment, verifyTreatment);
        }

        private void Init(HIS_TREATMENT treatment, bool verifyTreatment)
        {
            this.hisSereServCreate = new HisSereServCreate(param);
            this.hisSereServUpdate = new HisSereServUpdateSql(param);
            this.hisSereServDelete = new HisSereServDelete(param);
            this.setPriceWithBhytPolicy = new SetPriceWithBhytPolicy(param);
            this.addInfo = new HisSereServSetInfo(param, this.patientTypeAlters);
            this.verifyTreatment = verifyTreatment;
            this.treatment = treatment;
        }

        /// <summary>
        /// Cap nhat toan bo thong tin lien quan den gia va thong tin BHYT cua tat ca cac
        /// sere_serv lien quan trong cung treatment
        /// </summary>
        /// <param name="treatmentId"></param>
        /// <returns></returns>
        public bool Update()
        {
            bool result = true;
            try
            {
                List<HIS_SERE_SERV> listRaw = this.GetSereServToUse(this.treatment.ID);
                List<HIS_SERE_SERV> changeRecords = null;
                List<HIS_SERE_SERV> oldOfChangeRecords = null;
                if (this.Update(listRaw, ref changeRecords, ref oldOfChangeRecords))
                {
                    if (IsNotNullOrEmpty(changeRecords))
                    {
                        this.beforeUpdates = oldOfChangeRecords;//phuc vu rollback

                        List<HIS_SERE_SERV> listToUpdate = changeRecords.Where(o => o.IS_DELETE != 1).ToList();
                        List<HIS_SERE_SERV> listToDelete = changeRecords.Where(o => o.IS_DELETE == 1).ToList();

                        if (IsNotNullOrEmpty(listToUpdate))
                        {
                            if (!this.hisSereServUpdate.Run(listToUpdate))
                            {
                                throw new Exception("Ket thuc nghiep vu");
                            }
                        }
                        if (IsNotNullOrEmpty(listToDelete))
                        {
                            if (!this.hisSereServDelete.DeleteList(listToDelete, true))
                            {
                                throw new Exception("Ket thuc nghiep vu");
                            }
                        }
                    }
                }
                else
                {
                    result = false;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        public bool Update(List<HIS_SERE_SERV> sereServs)
        {
            List<HIS_SERE_SERV> changeRecords = null;
            List<HIS_SERE_SERV> oldOfChangeRecords = null;
            return this.Update(sereServs, ref changeRecords, ref oldOfChangeRecords);
        }

        /// <summary>
        /// Cap nhat toan bo thong tin lien quan den gia va thong tin BHYT cua tat ca cac sere_serv lien quan trong cung treatment
        /// </summary>
        /// <param name="treatmentId"></param>
        /// <returns></returns>
        public bool Update(List<HIS_SERE_SERV> sereServs, ref List<HIS_SERE_SERV> changeRecords, ref List<HIS_SERE_SERV> oldOfChangeRecords)
        {
            bool result = false;
            try
            {
                //neu danh sach sere_serv rong thi ket thuc xu ly
                if (!IsNotNullOrEmpty(sereServs))
                {
                    return true;
                }

                bool valid = true;

                //chi check treatment khi co y/c.
                //Tranh check treatment nhieu lan trong 1 nghiep vu (treatment, service_req, sere_serv, ...)
                if (this.verifyTreatment)
                {
                    HisTreatmentCheck treatmentCheck = new HisTreatmentCheck(param);
                    valid = valid && treatmentCheck.IsUnLock(this.treatment);
                    valid = valid && treatmentCheck.IsUnTemporaryLock(treatment);
                    valid = valid && treatmentCheck.IsUnLockHein(this.treatment);
                }

                if (valid)
                {
                    //Xac dinh loai dieu tri cua BN hien tai la j (kham hay dieu tri)
                    HIS_PATIENT_TYPE_ALTER lastPatientTypeAlter = this.GetLastPatientTypeAlter();
                    if (lastPatientTypeAlter != null)
                    {
                        //Ko xu ly cac du lieu "ko thuc hien"
                        sereServs = sereServs
                            .Where(o => o.IS_NO_EXECUTE == null || o.IS_NO_EXECUTE != MOS.UTILITY.Constant.IS_TRUE)
                            .Where(o => o.SERVICE_REQ_ID.HasValue && o.IS_DELETE != Constant.IS_TRUE)
                            .ToList();
                        if (IsNotNullOrEmpty(sereServs))
                        {
                            Mapper.CreateMap<HIS_SERE_SERV, HIS_SERE_SERV>();
                            List<HIS_SERE_SERV> beforeChanges = Mapper.Map<List<HIS_SERE_SERV>>(sereServs);

                            //this.UpdatePrice(sereServs);
                            
                            //Xu ly nghiep vu dinh muc hao phi
                            this.UpdateExpendQuota(sereServs);

                            //lay cac dich vu ko tinh dich vu luu so tien vuot hao phi de xu ly tiep
                            List<HIS_SERE_SERV> noExpendOvers = sereServs.Where(o => o.SERVICE_ID != HisServiceCFG.SERVICE_ID__OVER_EXPEND).ToList();

                            //Set lai gia va cac thong tin lien quan cho sere_serv
                            this.AddInfo(noExpendOvers);

                            //Cap nhat cho cac dich vu BHYT
                            this.UpdateBhyt(noExpendOvers, treatment, lastPatientTypeAlter);

                            //Cap nhat cho cac dich vu con lai
                            this.UpdateOther(noExpendOvers, lastPatientTypeAlter);

                            //Cap nhat gia phụ thu cho sere_serv
                            //(luu y: cap nhat gia phu thu can xu ly cuoi cung, sau khi xu ly cac tinh toan tien BHYT, KSK, ...)
                            this.UpdateAddprice(noExpendOvers);

                            List<HIS_SERE_SERV> afterChanges = Mapper.Map<List<HIS_SERE_SERV>>(sereServs);
                            HisSereServUtil.GetChangeRecord(beforeChanges, afterChanges, ref changeRecords, ref oldOfChangeRecords);
                        }
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private void UpdateExpendQuota(List<HIS_SERE_SERV> sereServs)
        {
            //neu co cau hinh dich vu de luu so tien trong truong hop vuot qua dinh muc hao phi
            //thi moi tiep tuc xu ly
            if (HisServiceCFG.SERVICE_ID__OVER_EXPEND > 0 && IsNotNullOrEmpty(sereServs))
            {
                //lay dich vu chinh ma co chua cac dich vu hao phi
                List<long> parentIds = sereServs.Where(o => o.IS_EXPEND == MOS.UTILITY.Constant.IS_TRUE && o.PARENT_ID.HasValue).Select(o => o.PARENT_ID.Value).Distinct().ToList();
                if (IsNotNullOrEmpty(parentIds))
                {
                    foreach (long parentId in parentIds)
                    {
                        HIS_SERE_SERV parent = sereServs.Where(o => o.ID == parentId).FirstOrDefault();
                        //kiem tra xem da co dich vu nao de luu so tien vuot hao phi chua
                        HIS_SERE_SERV overExpendService = sereServs.Where(o => o.SERVICE_ID == HisServiceCFG.SERVICE_ID__OVER_EXPEND && o.PARENT_ID == parentId).FirstOrDefault();
                        V_HIS_SERVICE parentService = HisServiceCFG.DATA_VIEW.Where(o => o.ID == parent.SERVICE_ID).FirstOrDefault();
                        if (parent != null && parentService != null && parentService.MAX_EXPEND.HasValue)
                        {
                            //Lay ra gia cac dich vu hao phi gan voi dich vu
                            decimal totalExpendChildren = sereServs
                                .Where(o => o.PARENT_ID.HasValue
                                    && o.PARENT_ID.Value == parentId
                                    && o.SERVICE_ID != HisServiceCFG.SERVICE_ID__OVER_EXPEND
                                    && o.IS_EXPEND == MOS.UTILITY.Constant.IS_TRUE).Sum(o => o.PRICE * o.AMOUNT);

                            //neu tong so tien hao phi vuot qua dinh muc hao phi cua dich vu chinh
                            decimal exceedPrice = totalExpendChildren - parentService.MAX_EXPEND.Value;

                            if (exceedPrice > 0)
                            {
                                //neu co dich vu nay, thi gan lai so tien
                                if (overExpendService != null)
                                {
                                    overExpendService.PRICE = exceedPrice;
                                }
                                else
                                {
                                    HIS_SERVICE_REQ serviceReq = new HisServiceReqGet().GetById(parent.SERVICE_REQ_ID.Value);
                                    HIS_SERE_SERV overExpendSereServ = new HIS_SERE_SERV();
                                    overExpendSereServ.PARENT_ID = parent.ID;
                                    overExpendSereServ.SERVICE_REQ_ID = parent.SERVICE_REQ_ID;
                                    overExpendSereServ.PRICE = exceedPrice;
                                    overExpendSereServ.ORIGINAL_PRICE = exceedPrice;
                                    overExpendSereServ.SERVICE_ID = HisServiceCFG.SERVICE_ID__OVER_EXPEND;
                                    overExpendSereServ.PATIENT_TYPE_ID = HisPatientTypeCFG.PATIENT_TYPE_ID__OVER_EXPEND;
                                    overExpendSereServ.AMOUNT = 1;
                                    this.hisSereServCreate.Create(overExpendSereServ, serviceReq, false);
                                }
                            }
                            else if (overExpendService != null) //neu so tien chua vuot, nhung ton tai dich vu luu so tien hao phi
                            {
                                overExpendService.IS_DELETE = 1;//de xoa dich vu nay
                            }
                        }
                        //neu dich vu cha bi xoa hoac ko co cau hinh gioi han hao phi nua thi xoa
                        else if (overExpendService != null)
                        {
                            overExpendService.IS_DELETE = 1;//de xoa dich vu nay
                        }
                    }
                }
            }
        }

        private void UpdateAddprice(List<HIS_SERE_SERV> listRaw)
        {
            if (IsNotNullOrEmpty(listRaw))
            {
                if (!new HisSereServSetAddPrice(param).UpdateAddprice(listRaw))
                {
                    throw new Exception("Ket thuc nghiep vu, rollback du lieu");
                }
            }
        }

        /// <summary>
        /// Set gia va cac thong tin lien quan cho sere_serv
        /// </summary>
        /// <param name="hisSereServs"></param>
        /// <param name="treatmentId"></param>
        private void AddInfo(List<HIS_SERE_SERV> hisSereServs)
        {
            if (IsNotNullOrEmpty(hisSereServs))
            {
                foreach (HIS_SERE_SERV hisSereServ in hisSereServs)
                {
                    if (!addInfo.AddInfo(hisSereServ))
                    {
                        throw new Exception("Ket thuc nghiep vu, rollback du lieu");
                    }
                }
            }
        }

        internal void RollbackData()
        {
            if (IsNotNullOrEmpty(this.beforeUpdates))
            {
                if (!this.hisSereServUpdate.Run(this.beforeUpdates))
                {
                    LogSystem.Error("Rollback sere_serv that bai");
                }
            }
        }

        private List<HIS_SERE_SERV> GetSereServToUse(long treatmentId)
        {
            HisSereServFilterQuery sereServFilter = new HisSereServFilterQuery();
            sereServFilter.TREATMENT_ID = treatmentId;
            sereServFilter.HAS_EXECUTE = true;//ko lay dich vu khong thuc hien
            return new HisSereServGet().Get(sereServFilter);
        }

        private HIS_PATIENT_TYPE_ALTER GetLastPatientTypeAlter()
        {
            if (IsNotNullOrEmpty(this.patientTypeAlters))
            {
                return this.patientTypeAlters
                    .OrderByDescending(o => o.LOG_TIME)
                    .ThenByDescending(o => o.ID)
                    .FirstOrDefault();
            }
            return null;
        }

        //private void UpdatePrice(List<HIS_SERE_SERV> hisSereServs)
        //{
        //    if (IsNotNullOrEmpty(hisSereServs))
        //    {
        //        HisSereServSetPrice priceAdder = new HisSereServSetPrice(param, treatment);
        //        foreach (HIS_SERE_SERV ss in hisSereServs)
        //        {
        //            priceAdder.AddPrice(ss, ss.TDL_INTRUCTION_TIME, ss.TDL_EXECUTE_BRANCH_ID, ss.TDL_REQUEST_ROOM_ID, ss.TDL_REQUEST_DEPARTMENT_ID, ss.TDL_EXECUTE_ROOM_ID);
        //        }
        //    }
        //}
    }
}
