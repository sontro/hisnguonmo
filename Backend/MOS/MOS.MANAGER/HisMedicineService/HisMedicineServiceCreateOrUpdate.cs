using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisMedicineService
{
    partial class HisMedicineServiceCreateOrUpdate : BusinessBase
    {
		private List<HIS_MEDICINE_SERVICE> beforeUpdateHisMedicineServices = new List<HIS_MEDICINE_SERVICE>();
        private HisMedicineServiceTruncate medicineServiceTruncate;
        private HisMedicineServiceCreate medicineServiceCreate;
        internal HisMedicineServiceCreateOrUpdate()
            : base()
        {
            this.Init();
        }

        internal HisMedicineServiceCreateOrUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {
            this.Init();
        }

        private void Init()
        {
            this.medicineServiceTruncate = new HisMedicineServiceTruncate(param);
            this.medicineServiceCreate = new HisMedicineServiceCreate(param);
        }

        internal bool Run(List<HIS_MEDICINE_SERVICE> listData, ref List<HIS_MEDICINE_SERVICE> resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMedicineServiceCheck checker = new HisMedicineServiceCheck(param);
                List<long> listId = listData.Select(o => o.ID).ToList();
                HisMedicineServiceFilterQuery filter = new HisMedicineServiceFilterQuery();
                filter.IDs = listId;
                List<HIS_MEDICINE_SERVICE> listRaw = new HisMedicineServiceGet().Get(filter);

                valid = valid && checker.IsValidDataTypeAndTestIndexId(listData);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (IsNotNullOrEmpty(listRaw))
                    {
                        //Xoa du lieu cu
                        if (!medicineServiceTruncate.TruncateList(listRaw))
                        {
                            throw new Exception("Xoa du lieu List<HIS_MEDICINE_SERVICE> cu that bai");
                        }
                    }

                    if (IsNotNullOrEmpty(listData))
                    {
                        if (!this.medicineServiceCreate.CreateList(listData))
                        {
                            LogSystem.Warn("tao moi List<HIS_MEDICINE_SERVICE> that bai");
                            throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
                        }
                        resultData = listData;
                    }
                    else
                    {
                        resultData = listRaw;
                    } 
                    result = true;
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

        internal void RollbackData()
        {
            this.medicineServiceCreate.RollbackData();
        }
    }
}
