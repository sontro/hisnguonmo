using Inventec.Common.Repository;
using SDA.EFMODEL.DataModels;
using System;
using System.Reflection;

namespace SDA.DTO.TransferObject
{
    public class VSdaNationalDTO : SDA_V_SDA_NATIONAL
    {
        public VSdaNationalDTO()
        {
        }

        public VSdaNationalDTO CreateDTO(SDA_V_SDA_NATIONAL raw)
        {
            try
            {
                if (raw != null)
                {
                    VSdaNationalDTO dto = new VSdaNationalDTO();
                    PropertyInfo[] pi = Properties.Get<SDA_V_SDA_NATIONAL>();
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
