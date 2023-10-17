using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisService;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisServicePaty
{
    class HisServicePatyCheck : BusinessBase
    {
        internal HisServicePatyCheck()
            : base()
        {

        }

        internal HisServicePatyCheck(Inventec.Core.CommonParam paramCheck)
            : base(paramCheck)
        {

        }

        internal bool VerifyRequireField(HIS_SERVICE_PATY data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (!IsGreaterThanZero(data.PATIENT_TYPE_ID)) throw new ArgumentNullException("data.PATIENT_TYPE_ID");
                if (data.PRICE < 0) throw new ArgumentNullException("data.PRICE");
                if (data.SERVICE_ID <= 0) throw new ArgumentNullException("data.SERVICE_ID");
                if (data.VAT_RATIO < 0) throw new ArgumentNullException("data.VAT_RATIO");
            }
            catch (ArgumentNullException ex)
            {
                MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
                LogSystem.Warn(ex);
                valid = false;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool IsUnLock(HIS_SERVICE_PATY data)
        {
            bool valid = true;
            try
            {
                if (IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE != data.IS_ACTIVE)
                {
                    valid = false;
                    MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.DuLieuDangBiKhoa);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool IsUnLock(long id)
        {
            bool valid = true;
            try
            {
                if (!DAOWorker.HisServicePatyDAO.IsUnLock(id))
                {
                    valid = false;
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDangBiKhoa);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool CheckHeinPrice(HIS_SERVICE_PATY data)
        {
            bool valid = true;
            try
            {
                if (data != null && data.PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT)
                {
                    V_HIS_SERVICE hisService = new HisServiceGet().GetViewById(data.SERVICE_ID);
                    if (hisService != null)
                    {
                        decimal? heinLimitPrice = null;
                        this.GetHeinLimitPrice(hisService, data, ref heinLimitPrice);
                        if (heinLimitPrice.HasValue && heinLimitPrice.Value >= data.PRICE)
                        {
                            MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisServicePaty_GiaNhoHonHoacBangGiaTranBHYT, heinLimitPrice.Value.ToString(), hisService.SERVICE_CODE);
                            return false;
                        }
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

        internal bool CheckHeinPrice(HIS_SERVICE_PATY data, V_HIS_SERVICE hisService)
        {
            bool valid = true;
            try
            {
                if (data != null && data.PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT && hisService != null)
                {
                    decimal? heinLimitPrice = null;
                    this.GetHeinLimitPrice(hisService, data, ref heinLimitPrice);
                    if (heinLimitPrice.HasValue && heinLimitPrice.Value >= data.PRICE)
                    {
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisServicePaty_GiaNhoHonHoacBangGiaTranBHYT, heinLimitPrice.Value.ToString(), hisService.SERVICE_CODE);
                        return false;
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

        private void GetHeinLimitPrice(V_HIS_SERVICE hisService, HIS_SERVICE_PATY data, ref decimal? heinLimitPrice)
        {
            if (hisService.HEIN_LIMIT_PRICE.HasValue || hisService.HEIN_LIMIT_PRICE_OLD.HasValue)
            {
                //neu gia ap dung theo ngay vao vien, thi cac benh nhan vao vien truoc ngay ap dung se lay gia cu
                if (hisService.HEIN_LIMIT_PRICE_IN_TIME.HasValue)
                {
                    if ((!data.FROM_TIME.HasValue || data.FROM_TIME.Value <= hisService.HEIN_LIMIT_PRICE_IN_TIME) && (!data.TO_TIME.HasValue || data.TO_TIME.Value >= hisService.HEIN_LIMIT_PRICE_IN_TIME))
                    {
                        heinLimitPrice = hisService.HEIN_LIMIT_PRICE ?? hisService.HEIN_LIMIT_PRICE_OLD;
                    }
                    else if ((!data.TREATMENT_FROM_TIME.HasValue || data.TREATMENT_FROM_TIME.Value <= hisService.HEIN_LIMIT_PRICE_IN_TIME) && (!data.TREATMENT_TO_TIME.HasValue || data.TREATMENT_TO_TIME.Value >= hisService.HEIN_LIMIT_PRICE_IN_TIME))
                    {
                        heinLimitPrice = hisService.HEIN_LIMIT_PRICE ?? hisService.HEIN_LIMIT_PRICE_OLD;
                    }
                }
                //neu ap dung theo ngay chi dinh, thi cac chi dinh truoc ngay ap dung se tinh gia cu
                else if (hisService.HEIN_LIMIT_PRICE_INTR_TIME.HasValue)
                {
                    if ((!data.FROM_TIME.HasValue || data.FROM_TIME.Value <= hisService.HEIN_LIMIT_PRICE_INTR_TIME) && (!data.TO_TIME.HasValue || data.TO_TIME.Value >= hisService.HEIN_LIMIT_PRICE_INTR_TIME))
                    {
                        heinLimitPrice = hisService.HEIN_LIMIT_PRICE ?? hisService.HEIN_LIMIT_PRICE_OLD;
                    }
                    else if ((!data.TREATMENT_FROM_TIME.HasValue || data.TREATMENT_FROM_TIME.Value <= hisService.HEIN_LIMIT_PRICE_INTR_TIME) && (!data.TREATMENT_TO_TIME.HasValue || data.TREATMENT_TO_TIME.Value >= hisService.HEIN_LIMIT_PRICE_INTR_TIME))
                    {
                        heinLimitPrice = hisService.HEIN_LIMIT_PRICE ?? hisService.HEIN_LIMIT_PRICE_OLD;
                    }
                }
                //neu ca 2 truong ko co gia tri thi luon lay theo gia moi
                else
                {
                    heinLimitPrice = hisService.HEIN_LIMIT_PRICE ?? hisService.HEIN_LIMIT_PRICE_OLD;
                }
            }
        }


        /// <summary>
        /// Kiem tra su ton tai cua danh sach cac id dong thoi lay ve du lieu
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool VerifyIds(List<long> listId, List<HIS_SERVICE_PATY> listObject)
        {
            bool valid = true;
            try
            {
                if (listId != null && listId.Count > 0)
                {
                    HisServicePatyFilterQuery filter = new HisServicePatyFilterQuery();
                    filter.IDs = listId;
                    List<HIS_SERVICE_PATY> listData = new HisServicePatyGet().Get(filter);
                    if (listData == null || listId.Count != listData.Count)
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                        Logging("ListId invalid." + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => listData), listData) + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => listId), listId), LogType.Error);
                        valid = false;
                    }
                    else
                    {
                        listObject.AddRange(listData);
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        /// <summary>
        /// Kiem tra su ton tai cua id dong thoi lay ve du lieu
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool VerifyId(long id, ref HIS_SERVICE_PATY data)
        {
            bool valid = true;
            try
            {
                data = new HisServicePatyGet().GetById(id);
                if (data == null)
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                    Logging("Id invalid." + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => id), id), LogType.Error);
                    valid = false;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

    }
}
