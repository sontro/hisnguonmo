
using Inventec.Common.Logging;
using Newtonsoft.Json;
using System;
namespace MOS.LibraryHein.Common
{
    public class RequestServiceData
    {
        /// <summary>
        /// Id cua y/c
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Id cua dich vu chinh (trong truong hop cac y/c nam trong cung 1 goi)
        /// </summary>
        public long? ParentId { get; set; }
        /// <summary>
        /// Gia dich vu
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// Gia dich vu
        /// </summary>
        public decimal OriginalPrice { get; set; }
        /// <summary>
        /// Gia BH gioi han doi voi tung dich vu
        /// </summary>
        public decimal? LimitPrice { get; set; }
        /// <summary>
        /// Ti le BH chi tra
        /// </summary>
        public decimal HeinRatio { get; set; }
        /// <summary>
        /// So tien BH chi tra (trong truong hop co su phan bo lai so tien
        // do tong so tien vuot qua gioi han cua don vi bao hiem quy dinh,
        // dan den so tien bao hiem chi tra tinh theo cong thuc price * hein_ratio ko con dung nua)
        // Neu hein_price co gia tri thi se uu tien su dung hein_price thay vi su dung hein_ratio * price
        /// </summary>
        public decimal? HeinPrice { get; set; }
        /// <summary>
        /// Benh nhan tu tra
        /// </summary>
        public decimal? PatientPrice { get; set; }
        /// <summary>
        /// So luong
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// Chuoi json luu thong tin doi tuong thanh toan
        /// </summary>
        public string JsonPatientTypeData { get; set; }

        public RequestServiceData()
        {
        }

        public RequestServiceData(RequestServiceData data)
        {
            if (data != null)
            {
                this.Id = data.Id;
                this.ParentId = data.ParentId;
                this.Price = data.Price;
                this.LimitPrice = data.LimitPrice;
                this.HeinPrice = data.HeinPrice;
                this.HeinRatio = data.HeinRatio;
                this.Amount = data.Amount;
                this.OriginalPrice = data.OriginalPrice;
            }
        }

        public RequestServiceData(string jsonPatientTypeData)
        {
            this.JsonPatientTypeData = jsonPatientTypeData;
        }

        public RequestServiceData(long id, long? parentId, decimal price, decimal? limitPrice, decimal amount, string jsonPatientTypeData, decimal originalPrice)
        {
            this.Id = id;
            this.ParentId = parentId;
            this.Price = price;
            this.LimitPrice = limitPrice;
            this.Amount = amount;
            this.OriginalPrice = originalPrice;
            this.JsonPatientTypeData = jsonPatientTypeData;
        }
    }
}
