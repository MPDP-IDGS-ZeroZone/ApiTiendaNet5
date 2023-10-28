using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace ApiTienda.Data.Models
{
    public partial class Socio
    {
        public Socio()
        {
            UsuariosSocios = new HashSet<UsuariosSocio>();
        }

        public int Idsocio { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public DateTime FechaNacimiento { get; set; }
        
        [JsonIgnore]
        public virtual ICollection<UsuariosSocio> UsuariosSocios { get; set; }
    }
}
