using System;
using System.Collections.Generic;

namespace DIO.Bank
{
	class Program
	{
		static List<Conta> listContas = new List<Conta>();
		static void Main(string[] args)
		{
			string opcaoUsuario = ObterOpcaoUsuario();

			while (opcaoUsuario.ToUpper() != "X")
			{
				switch (opcaoUsuario)
				{
					case "1":
						ListarContas();
						break;
					case "2":
						InserirConta();
						break;
					case "3":
						Transferir();
						break;
					case "4":
						Sacar();
						break;
					case "5":
						Depositar();
						break;
                    case "C":
						Console.Clear();
						break;
					default:
						Console.Write("Digite uma Opção valida");
						break;
						//throw new ArgumentOutOfRangeException();
						
				}

				opcaoUsuario = ObterOpcaoUsuario();
			}
			
			Console.WriteLine("Obrigado por utilizar nossos serviços.");
			Console.ReadLine();
		}

		private static void Depositar()
		{			
			int indiceConta = -1;
			do
			{
				try
				{
					Console.Write("Digite o número da conta: ");
					indiceConta = int.Parse(Console.ReadLine());
				}
				catch
				{
					Console.Write("Erro: digite o numero de uma conta\r\n");
					Console.Write("Digite alguma tecla para repetir\r\n");
					Console.ReadKey();					
				}
				finally
				{
					Console.Clear();
				}
				
			}while (indiceConta <0);

			//int indiceConta = int.Parse(Console.ReadLine());

			Console.Write("Digite o valor a ser depositado: ");
			double valorDeposito = double.Parse(Console.ReadLine());
			
			try
			{
				listContas[indiceConta].Depositar(valorDeposito);
			}
			catch (ArgumentOutOfRangeException outOfRange)
			{
				Console.Write("Erro:{0}", outOfRange.Message);
			}
			finally
			{
				Console.Write("\r\nDigite alguma tecla para continuar\r\n");
				Console.ReadKey();
				Console.Clear();
			}

		}

		private static void Sacar()
		{
			Console.Write("Digite o número da conta: ");
			int indiceConta = int.Parse(Console.ReadLine());

			Console.Write("Digite o valor a ser sacado: ");
			double valorSaque = double.Parse(Console.ReadLine());
			
            try
			{
				listContas[indiceConta].Sacar(valorSaque);
			}
			catch (ArgumentOutOfRangeException outOfRange)
			{
				Console.Write("Erro:{0}", outOfRange.Message);
			}
			finally
			{
				Console.Write("\r\nDigite alguma tecla para continuar\r\n");
				Console.ReadKey();
				Console.Clear();
			}
		}

		private static void Transferir()
		{
			string testaContaOrigem = TestaConta("Origem");
			string testaContaDestino = TestaConta("Destino");
			try
			{				
				int indiceContaOrigem = int.Parse(testaContaOrigem);				
				int indiceContaDestino = int.Parse(testaContaDestino);

				Console.Write("Digite o valor a ser transferido: ");
				double valorTransferencia = double.Parse(Console.ReadLine());

        	
				listContas[indiceContaOrigem].Transferir(valorTransferencia, listContas[indiceContaDestino]);			
				Console.Write ("Sucesso\r\n");
			}
			catch (ArgumentOutOfRangeException outOfRange)
			{
				Console.Write("Conta digitada não Existe", outOfRange.Message);
			}
			catch (ArgumentNullException outnukk)
			{
				Console.Write("Usuario deixou vazio" + outnukk);
			}
			catch (FormatException erro)
			{
				Console.Write("Errr0" + erro);
			}
			finally
			{
				Console.Write("\r\nDigite alguma tecla para continuar\r\n");
				Console.ReadKey();
				Console.Clear();
			}			
		}

		private static void InserirConta()
		{
			Console.WriteLine("Inserir nova conta");

			Console.Write("Digite 1 para Conta Fisica ou 2 para Juridica: ");
			int entradaTipoConta = int.Parse(Console.ReadLine());

			Console.Write("Digite o Nome do Cliente: ");
			string entradaNome = Console.ReadLine();

			Console.Write("Digite o saldo inicial: ");
			double entradaSaldo = double.Parse(Console.ReadLine());

			Console.Write("Digite o crédito: ");
			double entradaCredito = double.Parse(Console.ReadLine());

			Conta novaConta = new Conta(tipoConta: (TipoConta)entradaTipoConta,
										saldo: entradaSaldo,
										credito: entradaCredito,
										nome: entradaNome);

			listContas.Add(novaConta);
		}

		private static void ListarContas()
		{
			Console.WriteLine("Listar contas");

			if (listContas.Count == 0)
			{
				Console.WriteLine("Nenhuma conta cadastrada.");
				return;
			}

			for (int i = 0; i < listContas.Count; i++)
			{
				Conta conta = listContas[i];
				Console.Write("#{0} - ", i);
				Console.WriteLine(conta);
				Console.WriteLine();
			}
		}

		private static string ObterOpcaoUsuario()
		{
			//Console.WriteLine();
			Console.WriteLine("DIO Bank a seu dispor!!!");
			Console.WriteLine("Informe a opção desejada:");

			Console.WriteLine("1- Listar contas");
			Console.WriteLine("2- Inserir nova conta");
			Console.WriteLine("3- Transferir");
			Console.WriteLine("4- Sacar");
			Console.WriteLine("5- Depositar");
            Console.WriteLine("C- Limpar Tela");
			Console.WriteLine("X- Sair");
			Console.WriteLine();

			string opcaoUsuario = Console.ReadLine().ToUpper();
			Console.WriteLine();
			Console.Clear();
			return opcaoUsuario;
		}

		private static string TestaConta(string origemConta)
		{
			string numeroDigitado = "";
			ConsoleKeyInfo valorDitado;

			
			do
			{
				Console.Write("Digite o número da conta de " + origemConta + ": \r\n");
				Console.Write(numeroDigitado);

				valorDitado = Console.ReadKey();


				if ((valorDitado.Key >= ConsoleKey.D0 && valorDitado.Key <= ConsoleKey.D9) ||
					(valorDitado.Key >= ConsoleKey.NumPad0 && valorDitado.Key <= ConsoleKey.NumPad9) ||
					(valorDitado.Key == ConsoleKey.Decimal))
					
				{					
					numeroDigitado += valorDitado.KeyChar;
					Console.Clear();
				}
				else
				{
					Console.Clear();
				}
			} while ((valorDitado.Key != ConsoleKey.Escape) && (valorDitado.Key != ConsoleKey.Enter)); 

			if (valorDitado.Key == ConsoleKey.Enter)
			{
				return numeroDigitado;
			}else
			{
				return null;
			}
			
		}
	}
}
