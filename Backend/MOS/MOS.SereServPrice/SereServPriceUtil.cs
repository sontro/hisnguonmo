using MOS.EFMODEL.DataModels;
using MOS.LibraryHein.Bhyt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SereServPrice
{
    /// <summary>
    /// Thong tin ve gia duoc them khi thuc hien tao moi sere_serv
    /// </summary>
    public class SereServPriceUtil
    {
        private long patientTypeIdBhyt;
        private short isTrue;
        private long inTime;

        public SereServPriceUtil(long patientTypeIdBhyt, short isTrue, long inTime)
        {
            this.patientTypeIdBhyt = patientTypeIdBhyt;
            this.isTrue = isTrue;
            this.inTime = inTime;
        }

        public void SetBhytPrice(HIS_SERE_SERV data, long instructionTime, V_HIS_SERVICE hisService)
        {
            if (data.PATIENT_TYPE_ID == this.patientTypeIdBhyt)
            {
                if (string.IsNullOrWhiteSpace(hisService.HEIN_SERVICE_BHYT_CODE) || string.IsNullOrWhiteSpace(hisService.HEIN_SERVICE_BHYT_NAME))
                {
                    throw new Exception("Dich vu khong duoc cau hinh thong tin BHYT (HEIN_SERVICE_BHYT_CODE, HEIN_SERVICE_BHYT_NAME)");
                }

                decimal? heinLimitRatio = null;
                decimal? heinLimitPrice = null;

                this.GetHeinLimitPrice(hisService, instructionTime, this.inTime, ref heinLimitPrice, ref heinLimitRatio);

                //Chi set gia tran, ti le tran khi tran < gia quy dinh
                if (heinLimitPrice.HasValue || heinLimitRatio.HasValue)
                {
                    //can review lai code ==> bo sung nghiep vu thanh toan theo ti le
                    //Neu la dich vu BHYT thiet lap ti le tran (thuoc/vat tu thanh toan theo ti le)
                    if (heinLimitPrice.HasValue && heinLimitPrice <= data.PRICE)
                    {
                        data.HEIN_LIMIT_PRICE = heinLimitPrice;
                        //neu co gia tran thi set lai bang gia tran
                        data.ORIGINAL_PRICE = data.HEIN_LIMIT_PRICE.Value;
                    }
                    else if (heinLimitRatio.HasValue && heinLimitRatio.Value < 1)
                    {
                        data.HEIN_LIMIT_PRICE = heinLimitRatio.Value * data.PRICE * (1 + data.VAT_RATIO);
                    }
                }
                else
                {
                    data.HEIN_LIMIT_PRICE = null;
                }

                //Neu nguoi dung chon "ko tinh gia chenh lech" thi gan lai gia
                if (data.HEIN_LIMIT_PRICE.HasValue && data.IS_NO_HEIN_DIFFERENCE == this.isTrue)
                {
                    data.PRICE = data.HEIN_LIMIT_PRICE.Value;
                }
            }
        }

        private void GetHeinLimitPrice(V_HIS_SERVICE hisService, long instructionTime, long inTime, ref decimal? heinLimitPrice, ref decimal? heinLimitRatio)
        {
            //neu dich vu khai bao ti le tran
            if (hisService.HEIN_LIMIT_RATIO.HasValue || hisService.HEIN_LIMIT_RATIO_OLD.HasValue)
            {
                //neu gia ap dung theo ngay vao vien, thi cac benh nhan vao vien truoc ngay ap dung se lay gia cu
                if (hisService.HEIN_LIMIT_PRICE_IN_TIME.HasValue)
                {
                    heinLimitRatio = inTime < hisService.HEIN_LIMIT_PRICE_IN_TIME.Value ? hisService.HEIN_LIMIT_RATIO_OLD : hisService.HEIN_LIMIT_RATIO;
                }
                //neu ap dung theo ngay chi dinh, thi cac chi dinh truoc ngay ap dung se tinh gia cu
                else if (hisService.HEIN_LIMIT_PRICE_INTR_TIME.HasValue)
                {
                    heinLimitRatio = instructionTime < hisService.HEIN_LIMIT_PRICE_INTR_TIME.Value ? hisService.HEIN_LIMIT_RATIO_OLD : hisService.HEIN_LIMIT_RATIO;
                }
                //neu ca 2 truong ko co gia tri thi luon lay theo gia moi
                else
                {
                    heinLimitRatio = hisService.HEIN_LIMIT_RATIO;
                }
            }
            //neu dich vu khai bao gia tran
            else if (hisService.HEIN_LIMIT_PRICE.HasValue || hisService.HEIN_LIMIT_PRICE_OLD.HasValue)
            {
                //neu gia ap dung theo ngay vao vien, thi cac benh nhan vao vien truoc ngay ap dung se lay gia cu
                if (hisService.HEIN_LIMIT_PRICE_IN_TIME.HasValue)
                {
                    heinLimitPrice = inTime < hisService.HEIN_LIMIT_PRICE_IN_TIME.Value ? hisService.HEIN_LIMIT_PRICE_OLD : hisService.HEIN_LIMIT_PRICE;
                }
                //neu ap dung theo ngay chi dinh, thi cac chi dinh truoc ngay ap dung se tinh gia cu
                else if (hisService.HEIN_LIMIT_PRICE_INTR_TIME.HasValue)
                {
                    heinLimitPrice = instructionTime < hisService.HEIN_LIMIT_PRICE_INTR_TIME.Value ? hisService.HEIN_LIMIT_PRICE_OLD : hisService.HEIN_LIMIT_PRICE;
                }
                //neu ca 2 truong ko co gia tri thi luon lay theo gia moi
                else
                {
                    heinLimitPrice = hisService.HEIN_LIMIT_PRICE;
                }
            }
        }

    }
}
