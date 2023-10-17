using HIS.Desktop.LocalStorage.BackendData;
using HIS.Desktop.LocalStorage.BackendData.Core.ServiceCombo;
using Inventec.Common.Logging;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace HIS.Desktop.Modules.Main
{
    internal abstract class ProcessBase
    {
        protected void ResetDataExt(string type)
        {
            try
            {
                if (type == (typeof(MOS.EFMODEL.DataModels.V_HIS_SERVICE)).ToString()
                    || type == (typeof(MOS.EFMODEL.DataModels.HIS_SERVICE)).ToString()
                    || type == (typeof(MOS.EFMODEL.DataModels.V_HIS_SERVICE_PATY)).ToString()
                    || type == (typeof(MOS.EFMODEL.DataModels.V_HIS_SERVICE_ROOM)).ToString())
                {
                    ServiceComboDataWorker.DicServiceCombo = new Dictionary<long, LocalStorage.BackendData.ADO.ServiceComboADO>();
                }

                if (type == (typeof(MOS.EFMODEL.DataModels.V_HIS_MEDICINE_TYPE)).ToString()
                    || type == (typeof(MOS.EFMODEL.DataModels.V_HIS_MATERIAL_TYPE)).ToString())
                {
                    BackendDataWorker.Reset<HIS.Desktop.LocalStorage.BackendData.ADO.MedicineMaterialTypeComboADO>();
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        /// <summary>
        /// Gets a all Type instances matching the specified class name with just non-namespace qualified class name.
        /// </summary>
        /// <param name="className">Name of the class sought.</param>
        /// <returns>Types that have the class name specified. They may not be in the same namespace.</returns>
        protected Type GetTypeByName(string className)
        {
            Type returnVal = null;
            try
            {
                bool finded = false;
                foreach (Assembly a in AppDomain.CurrentDomain.GetAssemblies())
                {
                    Type[] assemblyTypes = a.GetTypes();
                    for (int j = 0; j < assemblyTypes.Length; j++)
                    {
                        if (assemblyTypes[j].FullName == className)
                        {
                            returnVal = assemblyTypes[j];
                            finded = true;
                            break;
                        }
                    }
                    if (finded)
                        break;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Warn(ex);
            }

            return returnVal;
        }
    }
}
