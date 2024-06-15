﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace AlumnosSystem.Models;

public partial class Materium
{
    public int IdMateria { get; set; }

    public int IdCarrera { get; set; }

    public string NombreMateria { get; set; }

    public DateOnly Periodo { get; set; }

    public virtual ICollection<AlumnoInscriptoCursadum> AlumnoInscriptoCursada { get; set; } = new List<AlumnoInscriptoCursadum>();

    public virtual Carrera IdCarreraNavigation { get; set; }

    public virtual ICollection<Mesa> Mesas { get; set; } = new List<Mesa>();

    public virtual ICollection<ProfesorMaterium> ProfesorMateria { get; set; } = new List<ProfesorMaterium>();
}