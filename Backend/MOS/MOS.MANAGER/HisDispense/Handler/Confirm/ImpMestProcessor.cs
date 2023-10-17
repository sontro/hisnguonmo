using AutoMapper;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisImpMest;
using MOS.MANAGER.HisImpMestMedicine;
using MOS.MANAGER.HisMedicine;
using MOS.MANAGER.HisMedicineBean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisDispense.Handler.Confirm
{
    class ImpMestProcessor : BusinessBase
    {
        private HisImpMestUpdate hisImpMestUpdate;

        private HisMedicineUpdate hisMedicineUpdate;
        private HisMedicineBeanCreate hisMedicineBeanCreate;

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
            this.hisMedicineBeanCreate = new HisMedicineBeanCreate(param);
            this.hisMedicineUpdate = new HisMedicineUpdate(param);
        }

        internal bool Run(HIS_DISPENSE dispense, HIS_IMP_MEST impMest, string loginname, string username, ref HIS_MEDICINE medicine, ref List<string> sqls)
        {
            bool result = false;
            try
            {
                HIS_MEDICINE hisMedicine = null;
                this.ProcessHisImpMest(dispense, impMest, loginname, username);
                this.ProcessMedicine(impMest, ref hisMedicine);
                this.ProcessMedicineBean(impMest, hisMedicine, ref sqls);
                medicine = hisMedicine;
                result = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        private void ProcessHisImpMest(HIS_DISPENSE dispense, HIS_IMP_MEST impMest, string loginname, string username)
        {
            Mapper.CreateMap<HIS_IMP_MEST, HIS_IMP_MEST>();
            HIS_IMP_MEST before = Mapper.Map<HIS_IMP_MEST>(impMest);
            impMest.IMP_MEST_STT_ID = IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_STT.ID__IMPORT;
            impMest.APPROVAL_LOGINNAME = loginname;
            impMest.APPROVAL_USERNAME = username;
            impMest.APPROVAL_TIME = dispense.DISPENSE_TIME;
            impMest.IMP_LOGINNAME = loginname;
            impMest.IMP_USERNAME = username;
            impMest.IMP_TIME = dispense.DISPENSE_TIME;
            if (!this.hisImpMestUpdate.Update(impMest, before))
            {
                throw new Exception("hisImpMestUpdate. ket thuc nghiep vu");
            }
        }

        private void ProcessMedicine(HIS_IMP_MEST impMest, ref HIS_MEDICINE medicine)
        {
            List<HIS_IMP_MEST_MEDICINE> impMestMedicines = new HisImpMestMedicineGet().GetByImpMestId(impMest.ID);
            if (impMestMedicines == null || impMestMedicines.Count != 1)
            {
                BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                throw new Exception("So luong HIS_IMP_MEST_MEDICINE theo ImpMestId khac 1");
            }
            HIS_IMP_MEST_MEDICINE impMestMedicine = impMestMedicines[0];
            HIS_MEDICINE hisMedicine = new HisMedicineGet().GetById(impMestMedicine.MEDICINE_ID);
            Mapper.CreateMap<HIS_MEDICINE, HIS_MEDICINE>();
            HIS_MEDICINE before = Mapper.Map<HIS_MEDICINE>(hisMedicine);
            hisMedicine.IS_PREGNANT = null;
            hisMedicine.IMP_TIME = impMest.IMP_TIME;
            if (!this.hisMedicineUpdate.Update(hisMedicine, before))
            {
                throw new Exception("hisMedicineUpdate. Ket thuc nghiep vu");
            }
            medicine = hisMedicine;
        }

        private void ProcessMedicineBean(HIS_IMP_MEST impMest, HIS_MEDICINE medicine, ref List<string> sqls)
        {
            HIS_MEDICINE_BEAN bean = new HIS_MEDICINE_BEAN();
            bean.MEDICINE_ID = medicine.ID;
            bean.MEDI_STOCK_ID = impMest.MEDI_STOCK_ID;
            bean.AMOUNT = medicine.AMOUNT;
            bean.IS_ACTIVE = IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__FALSE;
            HisMedicineBeanUtil.SetTdl(bean, medicine);
            if (!this.hisMedicineBeanCreate.Create(bean))
            {
                throw new Exception("hisMedicineBeanCreate. Ket thuc nghiep vu");
            }

            if (sqls == null) sqls = new List<string>();
            sqls.Add(String.Format("UPDATE HIS_MEDICINE_BEAN SET IS_ACTIVE = 1 WHERE ID = {0}", bean.ID));
        }

        internal void RollbackData()
        {
            try
            {
                this.hisMedicineBeanCreate.RollbackData();
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
