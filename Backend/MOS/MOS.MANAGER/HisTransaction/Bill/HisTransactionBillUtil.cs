using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisTreatment;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTransaction.Bill
{
    class HisTransactionBillUtil
    {
        /// <summary>
        /// Tinh so tien ket chuyen trong giao dich trong truong hop nguoi dung thuc hien giao dich thanh toan 
        /// trong khi dang thừa tiền tạm ứng (==> hệ thống tự kết chuyển 1 phần)
        /// </summary>
        /// <param name="data"></param>
        /// <param name="transactionAmount"></param>
        /// <returns></returns>
        public static decimal? CalcTransferAmount(CommonParam param, long? treatmentId, decimal payAmount, decimal exemption, decimal fundPaidTotal, decimal transactionAmount)
        {
            decimal? transferAmount = null;

            //So tien benh nhan phai tra
            decimal mustPaid = transactionAmount - fundPaidTotal - exemption;

            //Neu so tien thanh toan khac so tien can giao dich ==> co ket chuyen
            if (payAmount != mustPaid && treatmentId.HasValue)
            {
                V_HIS_TREATMENT_FEE_1 treatmentFee = new HisTreatmentGet().GetFeeView1ById(treatmentId.Value);

                //Tong so tien cho phep dung de ket chuyen (so tien Hiện dư)
                decimal? availableAmount = new HisTreatmentGet().GetAvailableAmount(treatmentFee);
                decimal calcPayAmount = 0;

                //Nếu Hiện dư >= Số tiền (tổng số tiền các dịch vụ cần thanh toán) --> "Cần thu" = 0, KC_AMOUNT = "Số tiền"
                //Nếu Hiện dư < Số tiền (tổng số tiền các dịch vụ cần thanh toán) --> "Cần thu" = Số tiền - Hiện dư, KC_AMOUNT = Hiện dư
                //Nếu Hiện dư=0 --> KC_amount=null
                if (availableAmount <= 0)
                {
                    transferAmount = null;
                    calcPayAmount = mustPaid;
                }
                else if (availableAmount >= mustPaid)
                {
                    calcPayAmount = 0;
                    transferAmount = mustPaid;
                }
                else
                {
                    calcPayAmount = mustPaid - availableAmount.Value;
                    transferAmount = availableAmount;
                }

                //Neu so tien server tinh lech so voi so tien client tinh qua 0.0001 thi ko cho thanh toan
                //(so sanh voi 0.0001 la vi de tranh truong hop client lam tron den 4 chu so sau phan thap phan)
                if (Math.Abs(calcPayAmount - payAmount) > Constant.PRICE_DIFFERENCE)
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("So tien can thu do server tinh la: " + calcPayAmount + " khac voi so tien do client y/c la: " + payAmount);
                }
            }
            return transferAmount;
        }
    }
}
