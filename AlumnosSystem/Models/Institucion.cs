﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace AlumnosSystem.Models;

public partial class Institucion
{
    public int IdInstitucion { get; set; }

    public string NombreInstitucion { get; set; }

    public string RazonSocial { get; set; }

    public string Provincia { get; set; }

    public string Licalidad { get; set; }

    public string Calle { get; set; }

    public string Altura { get; set; }

    public virtual ICollection<ResponsableAcademico> ResponsableAcademicos { get; set; } = new List<ResponsableAcademico>();
}