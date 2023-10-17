using Inventec.Common.Repository;
using SDA.EFMODEL.DataModels;
using System;
using System.Reflection;

namespace SDA.DTO.TransferObject
{
    public class VSdaDistrictDTO : SDA_V_SDA_DISTRICT
    {
        public VSdaDistrictDTO()
        {
        }

        public VSdaDistrictDTO CreateDTO(SDA_V_SDA_DISTRICT raw)
        {
            try
            {
                if (raw != null)
                {
                    VSdaDistrictDTO dto = new VSdaDistrictDTO();
                    PropertyInfo[] pi = Properties.Get<SDA_V_SDA_DISTRICT>();
                    foreach (var item in pi)
                    {
                        item.SetValue(dto, (item.GetValue(raw)));
                    }
                    return dto;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
