﻿using System;
using EFProductControl.Domain;
using EFProductControl.ValueObjects;
using System.Linq;

namespace EFProductControl
{
    class Program
    {
        static void Main(string[] args)
        {
            BuscarDados("Lucia");
        }

        private static void BuscarDados(string name)
        {
            using var db = new Data.ApplicationContext();
            var clientes = db.Clientes.Where(p => p.Nome.Contains(name)).OrderBy(p => p.Id);
            foreach(var cliente in clientes)
            {
                Console.WriteLine($"Id: {cliente.Id}");
                Console.WriteLine($"Nome: {cliente.Nome}");
                Console.WriteLine($"Telefone: {cliente.Telefone}");
                Console.WriteLine($"Cidade: {cliente.Cidade}");
                Console.WriteLine($"Cep: {cliente.CEP}");
                Console.WriteLine("---------------------------------");
            }
        }

        private static void AlteraDados()
        {
            using var db = new Data.ApplicationContext();
            var cliente = db.Clientes.Find(1);
            cliente.Nome = "Maria do Carmo";
            db.SaveChanges();
        }

        private static void InserirDadosEmMassa()
        {
            using var db = new Data.ApplicationContext();
            var clientes = new[] {
                new Cliente
                {
                    Nome = "José",
                    CEP = "06355620",
                    Cidade = "Carapicuiba",
                    Estado = "SP",
                    Telefone = "11 985019631"
                },
                new Cliente
                {
                    Nome = "Lucia",
                    CEP = "06355620",
                    Cidade = "Carapicuiba",
                    Estado = "SP",
                    Telefone = "11 985019631"
                }
            };

            db.Clientes.AddRange(clientes);
            //db.Set<Cliente>().AddRange(clientes);
            //db.AddRange(clientes);
            var total = db.SaveChanges();
            Console.WriteLine($"Total de inclusões: {total}");
        }

        private static void InserirDados()
        {
            var produto = new Produto
            {
                Descricao = "Produto Teste",
                CodigoDeBarras = "123123123",
                Valor = "$10,00",
                TipoProduto = TipoProduto.MercadoriaParaRevenda,
                Ativo = false
            };

            using( var db= new Data.ApplicationContext() )
            {
                //We can add data as following
                db.Produtos.Add(produto);
                //db.Set<Produto>().Add(produto);
                //db.Add(produto);

                var registros = db.SaveChanges();
                Console.WriteLine($"Total de registros salvos: {registros}");
            }
        }
    }
}
