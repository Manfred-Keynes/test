//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace solicitudescooitza.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class TblSolicitudes
    {
        public long idTblSolicitudes { get; set; }
        public Nullable<long> idCatProveedores { get; set; }
        public Nullable<long> idCatRubros { get; set; }
        public string razon { get; set; }
        public Nullable<decimal> monto { get; set; }
        public Nullable<System.DateTime> fecha { get; set; }
        public Nullable<long> cantidad { get; set; }
        public Nullable<decimal> totalQuetzales { get; set; }
        public Nullable<decimal> totalDolares { get; set; }
        public string imagen { get; set; }
        public Nullable<long> idTblUsuario { get; set; }
        public Nullable<System.DateTime> fechaAlta { get; set; }
        public Nullable<long> idCatEstados { get; set; }
    
        public virtual CatProveedores CatProveedores { get; set; }
        public virtual CatRubros CatRubros { get; set; }
        public virtual TblUsuarios TblUsuarios { get; set; }
    }
}
