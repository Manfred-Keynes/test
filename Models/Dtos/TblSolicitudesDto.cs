using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace solicitudescooitza.Models.Dtos
{
    public class TblSolicitudesDto
    {
        public long idTblSolicitudes { get; set; }
        public Nullable<long> idCatProveedores { get; set; }
        public Nullable<long> idCatRubros { get; set; }
        public string razon { get; set; }
        public Nullable<decimal> monto { get; set; }
        public string fecha { get; set; }
        public Nullable<long> cantidad { get; set; }
        public string imagen { get; set; }
    }
}