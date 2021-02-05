using System;
using EFProductControl.Domain;
using EFProductControl.ValueObjects;

namespace EFProductControl
{
    class Program
    {
        static void Main(string[] args)
        {
            InserirDados();
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
