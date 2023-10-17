using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisPackageDetail;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisPackage.UpdateDetail
{
    partial class HisPackageUpdateDetail : BusinessBase
    {
		private HisPackageDetailCreate hisPackageDetailCreate;
		private HisPackageDetailTruncate hisPackageDetailTruncate;
        private HisPackageDetailUpdate hisPackageDetailUpdate;
		
        internal HisPackageUpdateDetail()
            : base()
        {
			this.Init();
        }

        internal HisPackageUpdateDetail(CommonParam paramUpdate)
            : base(paramUpdate)
        {
			this.Init();
        }
		
		private void Init()
		{
			this.hisPackageDetailCreate = new HisPackageDetailCreate(param);
			this.hisPackageDetailTruncate = new HisPackageDetailTruncate(param);
            this.hisPackageDetailUpdate = new HisPackageDetailUpdate(param);
		}

        internal bool Run(HisPackageSDO data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisPackageCheck checker = new HisPackageCheck(param);
				HisPackageUpdateDetailCheck detailChecker = new HisPackageUpdateDetailCheck(param);
                HIS_PACKAGE raw = null;
                valid = valid && checker.VerifyId(data.PackageId, ref raw);
                valid = valid && detailChecker.IsFixedService(raw);
                valid = valid && detailChecker.IsNotDuplicated(data.Details);

                if (valid)
                {
                    //Lay du lieu cu
                    List<HIS_PACKAGE_DETAIL> existedDetails = new HisPackageDetailGet().GetByPackageId(data.PackageId);

					List<HIS_PACKAGE_DETAIL> toInserts = IsNotNullOrEmpty(data.Details) ?
                        data.Details.Where(o => existedDetails == null || !existedDetails.Exists(t => t.SERVICE_ID == o.ServiceId))
                        .Select(o => new HIS_PACKAGE_DETAIL {
                            AMOUNT = o.Amount,
                            SERVICE_ID = o.ServiceId,
                            PACKAGE_ID = data.PackageId
                        }).ToList() : null;

                    List<HIS_PACKAGE_DETAIL> toUpdates = new List<HIS_PACKAGE_DETAIL>();
                    List<HIS_PACKAGE_DETAIL> toDeletes = new List<HIS_PACKAGE_DETAIL>();
                    
                    if(IsNotNullOrEmpty(existedDetails))
                    {
                        foreach(HIS_PACKAGE_DETAIL dt in existedDetails)
                        {
                            HisPackageDetailSDO sdo = data.Details != null ? data.Details.Where(t => t.ServiceId == dt.SERVICE_ID).FirstOrDefault() : null;
                            if (sdo != null && sdo.Amount != dt.AMOUNT)
                            {
                                dt.AMOUNT = sdo.Amount;
                                toUpdates.Add(dt);
                            }
                            else if (sdo == null)
                            {
                                toDeletes.Add(dt);
                            }
                        }
                    }
                    
                    if (IsNotNullOrEmpty(toInserts) && !this.hisPackageDetailCreate.CreateList(toInserts))
					{
                        LogSystem.Error("Insert du lieu HIS_PACKAGE_DETAIL that bai");
					}

                    if (IsNotNullOrEmpty(toUpdates) && !this.hisPackageDetailUpdate.UpdateList(toUpdates))
                    {
                        LogSystem.Error("Update du lieu HIS_PACKAGE_DETAIL that bai");
                    }

                    if (IsNotNullOrEmpty(toDeletes) && !this.hisPackageDetailTruncate.TruncateList(toDeletes))
                    {
                        LogSystem.Error("Xoa du lieu HIS_PACKAGE_DETAIL that bai");
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
            this.hisPackageDetailUpdate.RollbackData();
            this.hisPackageDetailCreate.RollbackData();
        }
    }
}
