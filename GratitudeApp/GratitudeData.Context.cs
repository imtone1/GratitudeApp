﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GratitudeApp
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class gratitudetietokantaEntities : DbContext
    {
        public gratitudetietokantaEntities()
            : base("name=gratitudetietokantaEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Kayttajat> Kayttajat { get; set; }
        public virtual DbSet<Kirjaus> Kirjaus { get; set; }
        public virtual DbSet<Talot> Talot { get; set; }
    }
}