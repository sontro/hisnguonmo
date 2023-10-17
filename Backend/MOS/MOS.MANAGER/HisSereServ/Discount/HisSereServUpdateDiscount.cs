using AutoMapper;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisTreatment;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisSereServ.Discount
{
    class HisSereServUpdateDiscount : BusinessBase
    {

        private HisSereServUpdate hisSereServUpdate;
        private HisTreatmentUpdate hisTreatmentUpdate;

        internal HisSereServUpdateDiscount()
            : base()
        {
            this.hisSereServUpdate = new HisSereServUpdate(param);
            this.hisTreatmentUpdate = new HisTreatmentUpdate(param);
        }

        internal HisSereServUpdateDiscount(CommonParam param)
            : base(param)
        {
            this.hisSereServUpdate = new HisSereServUpdate(param);
            this.hisTreatmentUpdate = new HisTreatmentUpdate(param);
        }

        internal bool Run(HIS_SERE_SERV data, ref HIS_SERE_SERV resultData)
        {
            bool result = false;
            try
            {
                HIS_SERE_SERV existedData = new HisSereServGet().GetById(data.ID);
                HIS_TREATMENT hisTreatment = null;
                HisTreatmentCheck treatmentCheck = new HisTreatmentCheck(param);
                HisSereServCheck sereServCheck = new HisSereServCheck(param);
                bool valid = treatmentCheck.VerifyId(existedData.TDL_TREATMENT_ID.Value, ref hisTreatment);
                valid = valid && sereServCheck.HasNoBill(existedData);
                valid = valid && sereServCheck.HasExecute(existedData);
                valid = valid && treatmentCheck.IsUnLock(hisTreatment);
                valid = valid && treatmentCheck.IsUnTemporaryLock(hisTreatment);
                valid = valid && treatmentCheck.IsUnLockHein(hisTreatment);
                if (valid)
                {
                    Mapper.CreateMap<HIS_SERE_SERV, HIS_SERE_SERV>();
                    //luu lai de phuc vu rollback
                    HIS_SERE_SERV beforeUpdateRaw = Mapper.Map<HIS_SERE_SERV>(existedData);
                    existedData.DISCOUNT = data.DISCOUNT;

                    if (this.hisSereServUpdate.Update(existedData, beforeUpdateRaw))
                    {
                        resultData = existedData;
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        internal bool Run(List<HIS_SERE_SERV> listData, ref List<HIS_SERE_SERV> resultData)
        {
            bool result = false;
            try
            {
                List<HIS_SERE_SERV> listRaw = new List<HIS_SERE_SERV>();
                HIS_TREATMENT hisTreatment = null;
                long treatmentId = 0;
                HisTreatmentCheck treatmentCheck = new HisTreatmentCheck(param);
                HisSereServCheck sereServCheck = new HisSereServCheck(param);
                bool valid = true;
                valid = valid && this.ValidData(listData, ref treatmentId);
                valid = valid && sereServCheck.VerifyIds(listData.Select(s => s.ID).ToList(), listRaw);
                valid = valid && treatmentCheck.VerifyId(treatmentId, ref hisTreatment);
                valid = valid && sereServCheck.HasNoBill(listRaw);
                valid = valid && sereServCheck.HasExecute(listRaw);
                valid = valid && treatmentCheck.IsUnLock(hisTreatment);
                valid = valid && treatmentCheck.IsUnTemporaryLock(hisTreatment);
                valid = valid && treatmentCheck.IsUnLockHein(hisTreatment);
                if (valid)
                {
                    Mapper.CreateMap<HIS_SERE_SERV, HIS_SERE_SERV>();
                    //luu lai de phuc vu rollback
                    List<HIS_SERE_SERV> listBefore = Mapper.Map<List<HIS_SERE_SERV>>(listRaw);
                    foreach (HIS_SERE_SERV ss in listData)
                    {
                        HIS_SERE_SERV old = listRaw.FirstOrDefault(o => o.ID == ss.ID);
                        old.DISCOUNT = ss.DISCOUNT;
                    }

                    if (this.hisSereServUpdate.UpdateList(listRaw, listBefore, false))
                    {
                        resultData = listRaw;
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                this.RollbackData();
                result = false;
            }
            return result;
        }

        internal bool Run(HisSereServDiscountSDO data, ref HisSereServDiscountSDO resultData)
        {
            bool result = false;
            try
            {
                List<HIS_SERE_SERV> listRaw = new List<HIS_SERE_SERV>();
                HIS_TREATMENT hisTreatment = null;
                HisTreatmentCheck treatmentCheck = new HisTreatmentCheck(param);
                bool valid = true;
                valid = valid && IsNotNull(data);
                //valid = valid && IsNotNullOrEmpty(data.HisSereServs);
                valid = valid && IsGreaterThanZero(data.TreatmentId);
                valid = valid && treatmentCheck.VerifyId(data.TreatmentId, ref hisTreatment);
                valid = valid && this.ValidData(data);
                valid = valid && treatmentCheck.IsUnLock(hisTreatment);
                valid = valid && treatmentCheck.IsUnTemporaryLock(hisTreatment);
                valid = valid && treatmentCheck.IsUnLockHein(hisTreatment);
                if (valid)
                {
                    this.ProcessSereServ(data, listRaw);

                    this.ProcessTreatment(data, hisTreatment);

                    result = true;
                    this.PassResult(data, listRaw, ref resultData);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                this.RollbackData();
                result = false;
            }
            return result;
        }

        private void ProcessTreatment(HisSereServDiscountSDO data, HIS_TREATMENT hisTreatment)
        {

            bool IsUpdateTreatment = false;

            Mapper.CreateMap<HIS_TREATMENT, HIS_TREATMENT>();
            //luu lai de phuc vu rollback
            HIS_TREATMENT treatBefore = Mapper.Map<HIS_TREATMENT>(hisTreatment);

            if ((hisTreatment.IS_AUTO_DISCOUNT == Constant.IS_TRUE && (!data.IsAutoDiscount.HasValue || !data.IsAutoDiscount.Value || hisTreatment.AUTO_DISCOUNT_RATIO != data.AutoDiscountRatio))
                || (hisTreatment.IS_AUTO_DISCOUNT != Constant.IS_TRUE && (data.IsAutoDiscount.HasValue && data.IsAutoDiscount.Value)))
            {
                IsUpdateTreatment = true;
            }

            if (IsUpdateTreatment)
            {
                hisTreatment.IS_AUTO_DISCOUNT = (data.IsAutoDiscount.HasValue && data.IsAutoDiscount.Value) ? new Nullable<short>(Constant.IS_TRUE) : null;
                hisTreatment.AUTO_DISCOUNT_RATIO = (data.IsAutoDiscount.HasValue && data.IsAutoDiscount.Value) ? data.AutoDiscountRatio : null;

                if (!this.hisTreatmentUpdate.Update(hisTreatment, treatBefore))
                {
                    throw new Exception("hisSereServUpdate. Ket thuc nghiep vu");
                }

                if (hisTreatment.IS_AUTO_DISCOUNT == Constant.IS_TRUE)
                {
                    new EventLogGenerator(EventLog.Enum.HisTreatment_TuDongMienGiam, data.AutoDiscountRatio,"","").TreatmentCode(hisTreatment.TREATMENT_CODE).Run();
                }
                else
                {
                    new EventLogGenerator(EventLog.Enum.HisTreatment_HuyTuDongMienGiam).TreatmentCode(hisTreatment.TREATMENT_CODE).Run();
                }
            }
        }

        private void ProcessSereServ(HisSereServDiscountSDO data, List<HIS_SERE_SERV> listRaw)
        {
            if (IsNotNullOrEmpty(data.HisSereServs))
            {
                HisSereServCheck sereServCheck = new HisSereServCheck(param);
                bool valid = true;
                valid = valid && sereServCheck.VerifyIds(data.HisSereServs.Select(s => s.ID).ToList(), listRaw);
                valid = valid && sereServCheck.HasNoBill(listRaw);
                valid = valid && sereServCheck.HasExecute(listRaw);
                if (!valid)
                {
                    throw new Exception("valid false");
                }
                if (listRaw.Exists(e => e.TDL_TREATMENT_ID != data.TreatmentId))
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("Cac HIS_SERE_SERV khong cung mot Ho So Dieu Tri");
                }

                Mapper.CreateMap<HIS_SERE_SERV, HIS_SERE_SERV>();
                //luu lai de phuc vu rollback
                List<HIS_SERE_SERV> listBefore = Mapper.Map<List<HIS_SERE_SERV>>(listRaw);
                foreach (HIS_SERE_SERV ss in data.HisSereServs)
                {
                    HIS_SERE_SERV old = listRaw.FirstOrDefault(o => o.ID == ss.ID);
                    if (old.DISCOUNT != ss.DISCOUNT)
                    {
                        old.DISCOUNT = ss.DISCOUNT;
                        old.DISCOUNT_LOGINNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                        old.DISCOUNT_USERNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
                    }
                }

                if (!this.hisSereServUpdate.UpdateList(listRaw, listBefore, false))
                {
                    throw new Exception("hisSereServUpdate. Ket thuc nghiep vu");
                }
            }
        }

        private void PassResult(HisSereServDiscountSDO data, List<HIS_SERE_SERV> listRaw, ref HisSereServDiscountSDO resultData)
        {
            resultData = new HisSereServDiscountSDO();
            resultData.AutoDiscountRatio = data.AutoDiscountRatio;
            resultData.IsAutoDiscount = data.IsAutoDiscount;
            resultData.TreatmentId = data.TreatmentId;
            resultData.HisSereServs = listRaw;
        }

        private bool ValidData(List<HIS_SERE_SERV> listData, ref long treatmentId)
        {
            bool valid = true;
            try
            {
                valid = valid && IsNotNullOrEmpty(listData);
                if (listData.GroupBy(g => g.TDL_TREATMENT_ID).Count() > 1)
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("Cac HIS_SERE_SERV khong cung mot Ho So Dieu Tri");
                }
                treatmentId = listData.FirstOrDefault().TDL_TREATMENT_ID.Value;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        private bool ValidData(HisSereServDiscountSDO data)
        {
            bool valid = true;
            try
            {
                if (data.IsAutoDiscount.HasValue && data.IsAutoDiscount.Value)
                {
                    if (data.AutoDiscountRatio <= 0 || data.AutoDiscountRatio > 1)
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception("IsAutoDiscount = true and AutoDiscountRatio invalid");
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        private void RollbackData()
        {
            try
            {
                this.hisTreatmentUpdate.RollbackData();
                this.hisSereServUpdate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
