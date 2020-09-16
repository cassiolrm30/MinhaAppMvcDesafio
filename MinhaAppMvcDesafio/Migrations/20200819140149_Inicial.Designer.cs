﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MinhaAppMvcDesafio.Data;

namespace MinhaAppMvcDesafio.Migrations
{
    [DbContext(typeof(Contexto))]
    [Migration("20200819140149_Inicial")]
    partial class Inicial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MinhaAppMvcDesafio.Models.Endereco", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Bairro")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(20);

                    b.Property<string>("Cep")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(8);

                    b.Property<string>("Cidade")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(50);

                    b.Property<string>("Complemento")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(20);

                    b.Property<string>("Estado")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(2);

                    b.Property<string>("Logradouro")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(200);

                    b.Property<string>("Numero")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(5);

                    b.HasKey("Id");

                    b.ToTable("Enderecos");
                });

            modelBuilder.Entity("MinhaAppMvcDesafio.Models.Fornecedor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Ativo");

                    b.Property<string>("Documento")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(20);

                    b.Property<int>("EnderecoId");

                    b.Property<string>("Nome")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(50);

                    b.Property<int>("TipoFornecedor");

                    b.HasKey("Id");

                    b.HasIndex("EnderecoId")
                        .IsUnique();

                    b.ToTable("Fornecedores");
                });

            modelBuilder.Entity("MinhaAppMvcDesafio.Models.Produto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Ativo");

                    b.Property<DateTime>("DataCadastro");

                    b.Property<string>("Descricao")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(200);

                    b.Property<int>("FornecedorId");

                    b.Property<string>("Imagem")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Nome")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(50);

                    b.Property<decimal>("Valor");

                    b.HasKey("Id");

                    b.HasIndex("FornecedorId");

                    b.ToTable("Produtos");
                });

            modelBuilder.Entity("MinhaAppMvcDesafio.Models.Fornecedor", b =>
                {
                    b.HasOne("MinhaAppMvcDesafio.Models.Endereco", "Endereco")
                        .WithOne("Fornecedor")
                        .HasForeignKey("MinhaAppMvcDesafio.Models.Fornecedor", "EnderecoId");
                });

            modelBuilder.Entity("MinhaAppMvcDesafio.Models.Produto", b =>
                {
                    b.HasOne("MinhaAppMvcDesafio.Models.Fornecedor", "Fornecedor")
                        .WithMany("Produtos")
                        .HasForeignKey("FornecedorId");
                });
#pragma warning restore 612, 618
        }
    }
}
