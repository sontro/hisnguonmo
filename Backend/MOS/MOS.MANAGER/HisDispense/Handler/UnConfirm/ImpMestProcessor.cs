using AutoMapper;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.MANAGER.HisImpMest;
using MOS.MANAGER.HisImpMestMedicine;
using MOS.MANAGER.HisMedicine;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisDispense.Handler.UnConfirm
{
    class ImpMestProcessor : BusinessBase
    {
        private HisImpMestUpdate hisImpMestUpdate;
        private HisMedicineUpdate hisMedicineUpdate;

        internal ImpMestProcessor()
            : base()
        {
            this.Init();
        }

        internal ImpMestProcessor(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisImpMestUpdate = new HisImpMestUpdate(param);
            this.hisMedicineUpdate = new HisMedicineUpdate(param);
        }

        internal bool Run(HIS_IMP_MEST impMest, HIS_IMP_MEST_MEDICINE impMestMedicine, ref List<string> sqls)
        {
            bool result = false;
            try
            {
                this.CheckExistsImpExpMest(impMest, impMestMedicine);
                this.ProcessHisImpMest(impMest);
                this.ProcessHisMedicine(impMestMedicine);
                this.ProcessHisMedicineBean(impMestMedicine, ref sqls);
                result = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        private void ProcessHisImpMest(HIS_IMP_MEST impMest)
        {
            Mapper.CreateMap<HIS_IMP_MEST, HIS_IMP_MEST>();
            HIS_IMP_MEST before = Mapper.Map<HIS_IMP_MEST>(impMest);
            impMest.IMP_MEST_STT_ID = IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_STT.ID__REQUEST;
            impMest.APPROVAL_LOGINNAME = null;
            impMest.APPROVAL_USERNAME = null;
            impMest.APPROVAL_TIME = null;
            impMest.IMP_LOGINNAME = null;
            impMest.IMP_USERNAME = null;
            impMest.IMP_TIME = null;
            if (!this.hisImpMestUpdate.Update(impMest, before))
            {
                throw new Exception("hisImpMestUpdate. ket thuc nghiep vu");
            }
        }

        private void ProcessHisMedicine(HIS_IMP_MEST_MEDICINE impMestMedicine)
        {
            HIS_MEDICINE medicine = new HisMedicineGet().GetById(impMestMedicine.MEDICINE_ID);
            Mapper.CreateMap<HIS_MEDICINE, HIS_MEDICINE>();
            HIS_MEDICINE before = Mapper.Map<HIS_MEDICINE>(medicine);
            medicine.IMP_TIME = null;
            medicine.IS_PREGNANT = Constant.IS_TRUE;
            if (!this.hisMedicineUpdate.Update(medicine, before))
            {
                throw new Exception("hisMedicineUpdate. ket thuc nghiep vu");
            }
        }

        private void ProcessHisMedicineBean(HIS_IMP_MEST_MEDICINE impMestMedicine, ref List<string> sqls)
        {
            string sqlDelete = String.Format("DELETE HIS_MEDICINE_BEAN WHERE MEDICINE_ID = {0}", impMestMedicine.MEDICINE_ID);
            sqls.Add(sqlDelete);
        }

        private void CheckExistsImpExpMest(HIS_IMP_MEST impMest, HIS_IMP_MEST_MEDICINE impMestMedicine)
        {
            List<HIS_EXP_MEST_MEDICINE> expMestMedicines = new HisExpMestMedicineGet().GetByMedicineId(impMestMedicine.MEDICINE_ID);
            if (IsNotNullOrEmpty(expMestMedicines))
            {
                MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisImpMest_ThuocVatTuDaDuocXuat);
                throw new Exception("Thuoc thanh pham da co phieu xuat");
            }

            List<HIS_IMP_MEST_MEDICINE> impMestMedicines = new HisImpMestMedicineGet().GetByMedicineId(impMestMedicine.MEDICINE_ID);
            impMestMedicines = impMestMedicines != null ? impMestMedicines.Where(o => o.IMP_MEST_ID != impMest.ID).ToList() : null;
            if (IsNotNullOrEmpty(impMestMedicines))
            {
                MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisImpMest_ThuocVatTuDaDuocXuat);
                throw new Exception("Thuoc thanh pham da co phieu nhap khac voi phieu nhap thanh pham. Kiem tra lai du lieu");
            }
        }

        internal void RollbackData()
        {
            try
            {
                this.hisMedicineUpdate.RollbackData();
                this.hisImpMestUpdate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
