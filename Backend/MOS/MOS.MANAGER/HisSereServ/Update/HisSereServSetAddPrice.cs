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
using MOS.MANAGER.HisTreatment;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisSereServ
{
    internal partial class HisSereServSetAddPrice : BusinessBase
    {
        public HisSereServSetAddPrice()
            : base()
        {
        }

        internal HisSereServSetAddPrice(CommonParam param)
            : base(param)
        {

        }

        /// <summary>
        /// Cap nhat giá phụ thu đối với dịch vụ
        /// Nghiệp vụ đối với các nghiệp vụ có cấu hình giá cho cả 1 gói
        /// </summary>
        /// <param name="hisSereServs"></param>
        /// <returns></returns>
        public bool UpdateAddprice(List<HIS_SERE_SERV> hisSereServs)
        {
            bool result = true;
            try
            {
                if (IsNotNullOrEmpty(hisSereServs))
                {
                    //Lay ra cac dich vu co add_price nhung ko co "package_id"
                    List<HIS_SERE_SERV> noPackageSereServs = hisSereServs
                        .Where(o => !o.PACKAGE_ID.HasValue && o.ADD_PRICE.HasValue).ToList();
                    if (IsNotNullOrEmpty(noPackageSereServs))
                    {
                        noPackageSereServs.ForEach(o => o.ADD_PRICE = null);
                    }

                    //Lay ra cac dich vu chinh (dich vu cha) cua cac goi dich vu va co cau hinh chinh sach gia goi (co price_policy_id)
                    List<HIS_SERE_SERV> parentSereServs = hisSereServs
                        .Where(o => o.PACKAGE_ID.HasValue 
                            && HisPackageCFG.NOT_FIXED_SERVICE_PACKAGE_IDS != null
                            && HisPackageCFG.NOT_FIXED_SERVICE_PACKAGE_IDS.Contains(o.PACKAGE_ID.Value)
                            && o.IS_EXPEND != MOS.UTILITY.Constant.IS_TRUE).ToList();

                    if (IsNotNullOrEmpty(parentSereServs))
                    {
                        foreach (HIS_SERE_SERV s in parentSereServs)
                        {
                            decimal? packagePrice = s.PACKAGE_PRICE;

                            if (!packagePrice.HasValue)
                            {
                                V_HIS_SERVICE service = HisServiceCFG.DATA_VIEW
                                .Where(o => o.ID == s.SERVICE_ID && o.PACKAGE_ID == s.PACKAGE_ID.Value)
                                .FirstOrDefault();
                                packagePrice = service != null ? service.PACKAGE_PRICE : null;
                            }

                            if (packagePrice.HasValue)
                            {
                                //Tong gia tien hien tai cua goi
                                decimal packageTotalPrice = 0;
                                foreach (HIS_SERE_SERV tmp in hisSereServs)
                                {
                                    if ((tmp.PARENT_ID == s.ID && tmp.IS_OUT_PARENT_FEE != MOS.UTILITY.Constant.IS_TRUE) || s.ID == tmp.ID)
                                    {
                                        if (tmp.IS_NO_EXECUTE != MOS.UTILITY.Constant.IS_TRUE && tmp.IS_EXPEND != MOS.UTILITY.Constant.IS_TRUE)
                                        {
                                            //tu tinh chu ko dung truong "vir_price", vi voi cac dich vu chua duoc them vao DB thi vir_price = null
                                            decimal price = (tmp.PRICE + (tmp.ADD_PRICE ?? 0)) * (1 + tmp.VAT_RATIO) * tmp.AMOUNT;
                                            packageTotalPrice += price;
                                        }
                                    }
                                }

                                //Tinh lai so tien phu thu tuong ung voi dich vu
                                //so tien phu thu = tong so tien duoc cau hinh - so tien hien tai cua goi + so tien phu thu truoc khi tinh cua dich vu
                                if (s.AMOUNT > 0)
                                {
                                    s.ADD_PRICE = (packagePrice * s.AMOUNT - packageTotalPrice + (!s.ADD_PRICE.HasValue ? 0 : s.ADD_PRICE.Value) * s.AMOUNT) / s.AMOUNT;
                                }
                                else
                                {
                                    s.ADD_PRICE = 0;
                                }

                                if (s.ADD_PRICE < 0)
                                {
                                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisSereServ_SoTienTrongChiPhiVuotQuaGioiHanQuyDinh);
                                }
                            }
                        }
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
    }
}
