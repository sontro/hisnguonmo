using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisPatient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisKskContract.Import
{
    class PatientProcessor : BusinessBase
    {
        private HisPatientCreate hisPatientCreate;
        private HisPatientUpdate hisPatientUpdate;

        internal PatientProcessor()
            : base()
        {
            this.Init();
        }

        internal PatientProcessor(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisPatientCreate = new HisPatientCreate(param);
            this.hisPatientUpdate = new HisPatientUpdate(param);
        }

        internal bool Run(PrepareData prepareData, ref string desc)
        {
            bool result = false;
            try
            {
                if (prepareData != null)
                {
                    //Patient chua co thi moi thuc hien tao
                    if (prepareData.Patient.ID <= 0 && !this.hisPatientCreate.Create(prepareData.Patient))
                    {
                        desc = !String.IsNullOrWhiteSpace(param.GetMessage()) ? param.GetMessage() : param.GetBugCode();
                        result = false;
                    }
                    else
                    {
                        if (this.hisPatientUpdate.Update(prepareData.Patient))
                        {
                            result = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        internal void Rollback()
        {
            this.hisPatientCreate.RollbackData();
            this.hisPatientUpdate.RollbackData();
        }
    }
}
