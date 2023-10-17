using HIS.Desktop.Common;
using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.MedicineVaccinBill.MedicineVaccinBill
{
    class MedicineVaccinBillBehavior : Tool<IDesktopToolContext>, IMedicineVaccinBill
    {
        object[] entity;

        internal MedicineVaccinBillBehavior()
            : base()
        {

        }

        internal MedicineVaccinBillBehavior(CommonParam param, object[] data)
            : base()
        {
            entity = data;
        }

        object IMedicineVaccinBill.Run()
        {
            object result = null;
            try
            {
                Inventec.Desktop.Common.Modules.Module moduleData = null;
                long? _roomThuNganId = null;
                DelegateSelectData delegateSelectData = null;

                V_HIS_VACCINATION _vaccin = null;
                HIS_VACCINATION _vaccinRegister = null;
                string patientCodeForSearch = null;

                if (entity != null && entity.Count() > 0)
                {
                    for (int i = 0; i < entity.Count(); i++)
                    {
                        if (entity[i] is Inventec.Desktop.Common.Modules.Module)
                        {
                            moduleData = (Inventec.Desktop.Common.Modules.Module)entity[i];
                        }
                        else if (entity[i] is DelegateSelectData)
                        {
                            delegateSelectData = (DelegateSelectData)entity[i];
                        }
                        else if (entity[i] is V_HIS_VACCINATION)
                        {
                            _vaccin = (V_HIS_VACCINATION)entity[i];
                        }
                        else if (entity[i] is HIS_VACCINATION)
                        {
                            _vaccinRegister = (HIS_VACCINATION)entity[i];
                            // Inventec.Common.Mapper.DataObjectMapper.Map<V_HIS_VACCINATION>(_vaccin, _vaccinRegister);
                        }
                        else if (entity[i] is long)
                        {
                            _roomThuNganId = (long)entity[i];
                        }
                        else if (entity[i] is string)
                        {
                            patientCodeForSearch = (string)entity[i];
                        }
                    }
                }
                if (moduleData != null && _vaccin != null)
                {
                    return new frmMedicineVaccinBill(moduleData, _vaccin, delegateSelectData, patientCodeForSearch);
                }
                else if (moduleData != null && _vaccinRegister != null && _roomThuNganId != null)
                {
                    return new frmMedicineVaccinBill(moduleData, _vaccinRegister, _roomThuNganId, delegateSelectData, patientCodeForSearch);
                }
                else if (moduleData != null)
                {
                    return new frmMedicineVaccinBill(moduleData, patientCodeForSearch);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }
            return result;
        }
    }
}
