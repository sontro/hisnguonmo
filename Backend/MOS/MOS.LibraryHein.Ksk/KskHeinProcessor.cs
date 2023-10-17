using Inventec.Common.Logging;
using MOS.LibraryHein.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.LibraryHein.Ksk
{
    public class KskHeinProcessor
    {
        /// <summary>
        /// Cap nhat thong tin BHYT
        /// </summary>
        /// <param name="heinServiceDatas">Danh sach dich vu</param>
        /// <returns>True: thanh cong; False: that bai va gia tri cua heinServiceDatas duoc thay doi</returns>
        public bool UpdateHeinInfo(List<RequestServiceData> heinServiceData, long kskContractId, decimal? maxFee, decimal ratio)
        {
            bool result = true;
            try
            {
                if (heinServiceData == null || heinServiceData.Count == 0)
                {
                    throw new Exception("heinServiceData ko co du lieu");
                }

                List<KskServiceRequestData> kskServiceRequestData = heinServiceData.Select(o => new KskServiceRequestData(o)).ToList();
                //Lay cac dich vu can cap nhat
                var listToUpdate = kskServiceRequestData
                    .Where(o => o.PatientTypeData.KSK_CONTRACT_ID == kskContractId)
                    .OrderBy(o => o.Id) //sap xep theo thu tu them du lieu
                    .ToList();

                decimal total = 0;
                foreach (KskServiceRequestData d in listToUpdate)
                {
                    total += d.Price * ratio;
                    d.HeinRatio = maxFee == null || total <= maxFee.Value ? ratio : 0;
                }

                //update lai du lieu input truyen vao
                foreach (RequestServiceData data in heinServiceData)
                {
                    KskServiceRequestData kskData = kskServiceRequestData.Where(o => o.Id == data.Id).SingleOrDefault();
                    data.HeinPrice = kskData.HeinPrice;
                    data.HeinRatio = kskData.HeinRatio;
                }
            }
            catch (Exception ex)
            {
                result = false;
                LogSystem.Error(ex);
            }
            return result;
        }
    }
}
