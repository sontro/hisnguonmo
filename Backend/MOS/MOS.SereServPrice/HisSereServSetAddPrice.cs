using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryHein.Bhyt;
using MOS.LibraryHein.Common;
using MOS.SereServPrice;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisSereServ
{
    internal partial class HisSereServSetAddPrice
    {
        private CommonParam param { get; set; }

        public HisSereServSetAddPrice()
            : base()
        {
            param = new CommonParam();
        }

        public HisSereServSetAddPrice(CommonParam paramBusiness)
            : base()
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
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
                if (hisSereServs != null && hisSereServs.Count > 0)
                {
                    //Lay ra cac dich vu chinh (dich vu cha) cua cac goi dich vu va co cau hinh chinh sach gia goi (co price_policy_id)
                    List<HIS_SERE_SERV> parentSereServs = hisSereServs
                        .Where(o => o.PACKAGE_ID.HasValue && o.IS_EXPEND != ConfigDataLoader.IS_TRUE).ToList();

                    if (parentSereServs != null && parentSereServs.Count > 0)
                    {
                        foreach (HIS_SERE_SERV s in parentSereServs)
                        {
                            V_HIS_SERVICE service = ConfigDataLoader.SERVICES
                                .Where(o => o.ID == s.SERVICE_ID && o.PACKAGE_ID == s.PACKAGE_ID.Value)
                                .FirstOrDefault();
                            if (service != null)
                            {
                                //Tong gia tien hien tai cua goi
                                decimal packageTotalPrice = 0;
                                foreach (HIS_SERE_SERV tmp in hisSereServs)
                                {
                                    if ((tmp.PARENT_ID == s.ID || s.ID == tmp.ID) && (!tmp.IS_OUT_PARENT_FEE.HasValue || tmp.IS_OUT_PARENT_FEE != ConfigDataLoader.IS_TRUE))
                                    {
                                        if (tmp.IS_NO_EXECUTE != ConfigDataLoader.IS_TRUE && tmp.IS_EXPEND != ConfigDataLoader.IS_TRUE)
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
                                    s.ADD_PRICE = (service.PACKAGE_PRICE * s.AMOUNT - packageTotalPrice + (!s.ADD_PRICE.HasValue ? 0 : s.ADD_PRICE.Value) * s.AMOUNT) / s.AMOUNT;
                                }
                                else
                                {
                                    s.ADD_PRICE = 0;
                                }

                                if (s.ADD_PRICE < 0)
                                {
                                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisSereServ_SoTienTrongChiPhiVuotQuaGioiHanQuyDinh);
                                    LogSystem.Warn("So tien trong chi phi vuot qua gioi han dan den add_price bi am.");
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
