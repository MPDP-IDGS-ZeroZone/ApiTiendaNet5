using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace ApiTienda.Data.Models
{
    public partial class UsuariosSocio
    {
        public UsuariosSocio()
        {
            Productos = new HashSet<Producto>();
        }

        public int Idusuariosocio { get; set; }
        public string Pasword { get; set; }
        public string Rol { get; set; }
        public string Mail { get; set; }
        public int Idsocio { get; set; }
        
        [JsonIgnore]
        public virtual Socio IdsocioNavigation { get; set; }
        
        [JsonIgnore]
        public virtual ICollection<Producto> Productos { get; set; }
    }
}
