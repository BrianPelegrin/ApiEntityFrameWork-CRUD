using System;
using System.Collections.Generic;

namespace ApiEntityFrameWork.Models
{
    public partial class Producto
    {
        public int IdProducto { get; set; }
        public string? Marca { get; set; }
        public string? Descripcion { get; set; }
        public decimal? Precio { get; set; }
        public int? Stock { get; set; }
        public int? IdCategoria { get; set; }

        public virtual Categoria? oCategoria { get; set; }
    }
}
