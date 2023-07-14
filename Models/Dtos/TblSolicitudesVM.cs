using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace solicitudescooitza.Models.Dtos
{
    public class TblSolicitudesVM
    {
        public long idTblSolicitudes { get; set; }
        public string Proveedor { get; set; }
        public string Rubro { get; set; }
        public string razon { get; set; }
        public Nullable<decimal> monto { get; set; }
        public string fecha { get; set; }
        public Nullable<long> cantidad { get; set; }
        public string imagen { get; set; }
    }
}