﻿using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace ApiTienda.Data.Models
{
    public partial class Producto
    {
        public Producto()
        {
            Oferta = new HashSet<Oferta>();
        }

        public int Idproducto { get; set; }
        public int Idusuariosocio { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public string Foto { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int Idcategoria { get; set; }
        public string Tipo { get; set; }
        public int Stock { get; set; }
        public string Statusp { get; set; }

        [JsonIgnore]
        public virtual Categoria IdcategoriaNavigation { get; set; }
        
        [JsonIgnore]
        public virtual UsuariosSocio IdusuariosocioNavigation { get; set; }
        
        [JsonIgnore]
        public virtual ICollection<Oferta> Oferta { get; set; }
    }
}
