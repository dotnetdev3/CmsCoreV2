using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using CmsCoreV2.Data;

namespace CmsCoreV2.Migrations.HostDb
{
    [DbContext(typeof(HostDbContext))]
    [Migration("20170524063059_hostDbInitialCreate")]
    partial class hostDbInitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CmsCoreV2.Models.AppTenant", b =>
                {
                    b.Property<string>("AppTenantId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConnectionString");

                    b.Property<string>("Hostname");

                    b.Property<string>("Name");

                    b.Property<string>("Theme");

                    b.Property<string>("Title");

                    b.HasKey("AppTenantId");

                    b.ToTable("AppTenants");
                });
        }
    }
}
