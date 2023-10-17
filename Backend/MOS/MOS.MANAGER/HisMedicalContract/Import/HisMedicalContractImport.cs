using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisBid;
using MOS.MANAGER.HisSupplier;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisMedicalContract.Import
{
    class HisMedicalContractImport : BusinessBase
    {
        private List<HIS_MEDICAL_CONTRACT> recentHisMedicalContracts = null;

        internal HisMedicalContractImport()
            : base()
        {
            this.Init();
        }

        internal HisMedicalContractImport(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.recentHisMedicalContracts = new List<HIS_MEDICAL_CONTRACT>();
        }

        internal bool Run(List<HIS_MEDICAL_CONTRACT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_BID bid = null;
                HIS_SUPPLIER supplier = null;
                List<string> sqls = new List<string>();

                List<HIS_BID_MATERIAL_TYPE> lstBidMaterialType = null;
                List<HIS_BID_MEDICINE_TYPE> lstBidMedicineType = null;
                List<HIS_MATERIAL_TYPE> lstMaterialType = null;
                List<HIS_MEDICINE_TYPE> lstMedicineType = null;

                HisMedicalContractImportCheck checker = new HisMedicalContractImportCheck(param);
                HisSupplierCheck supplierChecker = new HisSupplierCheck(param);
                HisBidCheck bidChecker = new HisBidCheck(param);

                valid = valid && IsNotNullOrEmpty(listData);
                valid = valid && checker.IsDuplicateOrExists(listData);
                if (valid)
                {
                    foreach (var data in listData)
                    {
                        valid = valid && checker.VerifyRequireField(data);
                        valid = valid && checker.ValidData(data);
                        valid = valid && checker.IsDuplicateMatyAndMety(data.HIS_MEDI_CONTRACT_MATY, data.HIS_MEDI_CONTRACT_METY);
                        valid = valid && supplierChecker.VerifyId(data.SUPPLIER_ID, ref supplier);
                        valid = valid && supplierChecker.IsUnLock(supplier);
                        valid = valid && (!data.BID_ID.HasValue || bidChecker.VerifyId(data.BID_ID.Value, ref bid));
                        valid = valid && (!data.BID_ID.HasValue || bidChecker.IsUnLock(bid));
                        valid = valid && checker.VerifyBid(data, ref lstBidMaterialType, ref lstBidMedicineType);
                        valid = valid && checker.VerifyType(data, ref lstMaterialType, ref lstMedicineType, ref sqls);
                    }

                    if (valid)
                    {
                        
                        if (!DAOWorker.HisMedicalContractDAO.CreateList(listData))
                        {
                            MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMedicalContract_ThemMoiThatBai);
                            throw new Exception("Them moi danh sach thong tin HisMedicalContract that bai." + LogUtil.TraceData("listData", listData));
                        }
                        if (IsNotNullOrEmpty(sqls))
                        {
                            if (!DAOWorker.SqlDAO.Execute(sqls))
                            {
                                throw new Exception("Nhap khau hop dong duoc update db that bai sqls: " + sqls.ToString());
                            }
                        }
                        this.recentHisMedicalContracts.AddRange(listData);
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                result = false;
                param.HasException = true;
                this.Rollback();
            }
            return result;
        }

        private void Rollback()
        {
            try
            {
                if (IsNotNullOrEmpty(this.recentHisMedicalContracts))
                {
                    if (!DAOWorker.HisMedicalContractDAO.TruncateList(this.recentHisMedicalContracts))
                    {
                        LogSystem.Warn("Rollback du lieu HisMedicalContract that bai, can kiem tra lai." + LogUtil.TraceData("recentHisMedicalContracts", this.recentHisMedicalContracts));
                    }
                    this.recentHisMedicalContracts = null;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }
    }
}
