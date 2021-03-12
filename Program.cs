using System;
using System.Collections.Generic;

namespace AppCadastroSeries
{
    class Program
    {
        static SerieRepositorio repositorio = new SerieRepositorio();
        static void Main(string[] args)
        {
            string opcaoUsuario = ObterOpcaoUsuario();

            while(opcaoUsuario.ToUpper() != "X")
            {
                switch(opcaoUsuario)
                {
                    case "1":
                        ListarSeries();
                        break;
                    case "2":
                        InserirSeries();
                        break;
                    case "3":
                        AtualizarSeries();
                        break;
                    case "4":
                        ExcluirSeries();
                        break;
                    case "5":
                        VisualizarSeries();
                        break;
                    case "C":
                        Console.Clear();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                opcaoUsuario = ObterOpcaoUsuario();
            }

            Console.WriteLine("Obrigada por utilizar nossos serviços.");
            Console.ReadLine();
        }
        private static void ListarSeries()
        {
            Console.WriteLine("Listar séries");
            
            var lista = repositorio.Lista();
            
            VerificarLista();

            foreach(var serie in lista)
            {
                var excluido = serie.retornaExcluido();
                Console.WriteLine("Código {0} - Série: {1} {2}", serie.retornaId(), serie.retornaTitulo(), (excluido ? "*Excluído*" : ""));
            }
        }
        private static void InserirSeries()
        {
            Console.WriteLine("Inserir nova série: ");

            foreach(int i in Enum.GetValues(typeof(Genero)))
            {
                Console.WriteLine("{0} - {1}", i, Enum.GetName(typeof(Genero), i));
            }

            Console.WriteLine("Digite o gênero entre as opções acima: ");
            int entradaGenero = int.Parse(Console.ReadLine());

            Console.WriteLine("Digite o título da série: ");
            string entradaTitulo = Console.ReadLine();

            Console.WriteLine("Digite o ano do início da série: ");
            int entradaAno = int.Parse(Console.ReadLine());

            Console.WriteLine("Digite a descrição da série: ");
            string entradaDescricao = Console.ReadLine();

            Serie novaSerie = new Serie(id: repositorio.ProximoId(),
                                        genero: (Genero)entradaGenero,
                                        titulo: entradaTitulo,
                                        ano: entradaAno,
                                        descricao: entradaDescricao);

            repositorio.Insere(novaSerie);
        }
        private static void AtualizarSeries()
        {
            Console.WriteLine("Atualizar série");

            if(VerificarLista())
            {   
                Console.WriteLine("Atualizar série: ");
                int indiceSerie = int.Parse(Console.ReadLine());
                var serie = repositorio.RetornaPorId(indiceSerie);
                Console.WriteLine(serie);

                foreach(int i in Enum.GetValues(typeof(Genero)))
                {
                    Console.WriteLine("{0} - {1}", i, Enum.GetName(typeof(Genero), i));
                }

                Console.WriteLine("Digite o gênero entre as opções acima: ");
                int entradaGenero = int.Parse(Console.ReadLine());

                Console.WriteLine("Digite o título da série: ");
                string entradaTitulo = Console.ReadLine();

                Console.WriteLine("Digite o ano do início da série: ");
                int entradaAno = int.Parse(Console.ReadLine());

                Console.WriteLine("Digite a descrição da série: ");
                string entradaDescricao = Console.ReadLine();

                Serie atualizarSerie = new Serie(id: indiceSerie,
                                            genero: (Genero)entradaGenero,
                                            titulo: entradaTitulo,
                                            ano: entradaAno,
                                            descricao: entradaDescricao);

                repositorio.Atualiza(indiceSerie, atualizarSerie);
            }
        }
        private static void ExcluirSeries()
        {
            Console.WriteLine("Excluir série");

            if(VerificarLista())
            {
                Console.WriteLine("Excluir série: ");
                int indiceSerie = int.Parse(Console.ReadLine());
                Console.WriteLine("Deseja excluir essa série?");
            
                string opcaoExclui = OpcaoExcluir();

                switch(opcaoExclui)
                {
                    case "1":
                        repositorio.Exclui(indiceSerie);
                        return;
                    case "2":
                        return;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

        }
        private static void VisualizarSeries()
        {
            Console.WriteLine("Visualizar série");
            
            if(VerificarLista())
            {
                Console.WriteLine("Visualizar série: ");
                int indiceSerie = int.Parse(Console.ReadLine());
                var serie = repositorio.RetornaPorId(indiceSerie);
                Console.WriteLine(serie);
            }
           
        }
        private static string ObterOpcaoUsuario()
        {
            Console.WriteLine();
            Console.WriteLine("DIO Series a seu dispor!");
            Console.WriteLine("Informe a opção desejada: ");

            Console.WriteLine("1 - Listar séries");
            Console.WriteLine("2 - Inserir nova série");
            Console.WriteLine("3 - Atualizar série");
            Console.WriteLine("4 - Excluir série");
            Console.WriteLine("5 - Visualizar série");
            Console.WriteLine("C - Limpar Tela");
            Console.WriteLine("X - Sair");
            
            Console.WriteLine();    
            string opcaoUsuario = Console.ReadLine().ToUpper();
            Console.WriteLine();
            return opcaoUsuario;
        }
        private static string OpcaoExcluir()
        {
            Console.WriteLine("1 - Sim");
            Console.WriteLine("2 - Não");

            Console.WriteLine();
            string opcaoExclui = Console.ReadLine().ToUpper();
            Console.WriteLine();
            return opcaoExclui;
        }
        private static bool VerificarLista()
        {
            var lista = repositorio.Lista();
            
            if(lista.Count == 0)
            {
                Console.WriteLine("Nenhuma série cadastrada.");
                return false;
            }
            else{
                return true;
            }
        }
    }
}
