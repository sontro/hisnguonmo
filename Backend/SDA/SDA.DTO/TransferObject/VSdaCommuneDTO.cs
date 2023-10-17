using Inventec.Common.Repository;
using SDA.EFMODEL.DataModels;
using System;
using System.Reflection;

namespace SDA.DTO.TransferObject
{
    public class VSdaCommuneDTO : SDA_V_SDA_COMMUNE
    {
        public VSdaCommuneDTO()
        {
        }

        public VSdaCommuneDTO CreateDTO(SDA_V_SDA_COMMUNE raw)
        {
            try
            {
                if (raw != null)
                {
                    VSdaCommuneDTO dto = new VSdaCommuneDTO();
                    PropertyInfo[] pi = Properties.Get<SDA_V_SDA_COMMUNE>();
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
