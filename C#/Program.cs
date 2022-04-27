using ProvaAdmissionalCSharpApisul;
using System;
using Prova;
using static System.Environment;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Prova.Services;
using System.Reflection;

namespace Prova
{
    internal class Program
    {
        private static IElevadorService iElevadorService;

        private static void Main(string[] args)
        {
            string caminho = @"..\input.json";
            string Msg;

            iElevadorService = new ElevadorService(caminho);

            Console.WriteLine(">=================================================================================================================<");
            var menosUsados = iElevadorService.andarMenosUtilizado();

            Msg = "a. Qual é o andar menos utilizado pelos usuários: " + NewLine + "Andar " + menosUsados[0] + NewLine;
            Console.WriteLine(Msg);

            Console.WriteLine(">=================================================================================================================<");

            List<char> elevadorMaisFrequentado = iElevadorService.elevadorMaisFrequentado();
            List<char> periodoMaiorFluxoElevadorMaisFrequentado = iElevadorService.periodoMaiorFluxoElevadorMaisFrequentado();

            Msg = "b. Qual é o elevador mais frequentado e o período que se encontra maior fluxo;" + NewLine;

            for (int index = 0; index < elevadorMaisFrequentado.Count; index++)
                Msg += $"Elevador {elevadorMaisFrequentado[index]} - turno de maior fluxo: {traduzPeriodo(periodoMaiorFluxoElevadorMaisFrequentado[index])}" + NewLine;

            Console.WriteLine(Msg);

            Console.WriteLine(">=================================================================================================================<");

            List<char> elevadorMenosFrequentado = iElevadorService.elevadorMenosFrequentado();
            List<char> periodoMenorFluxoElevadorMenosFrequentado = iElevadorService.periodoMenorFluxoElevadorMenosFrequentado();

            Msg = "c. Qual é o elevador menos frequentado e o período que se encontra menor fluxo;" + NewLine;
            for (int index = 0; index < elevadorMaisFrequentado.Count; index++)
                Msg += $"Elevador {elevadorMenosFrequentado[index]} - turno de menor fluxo: {traduzPeriodo(periodoMenorFluxoElevadorMenosFrequentado[index])}" + NewLine;

            Console.WriteLine(Msg);

            Console.WriteLine(">=================================================================================================================<");

            var turnos = iElevadorService.periodoMaiorUtilizacaoConjuntoElevadores();
            string periodo = new string(turnos.ToArray());
            char chPeriodo = Convert.ToChar(periodo);

            Msg = "d. Qual o período de maior utilização do conjunto de elevadores; " + NewLine + traduzPeriodo(chPeriodo) + NewLine;
            Console.WriteLine(Msg);

            Console.WriteLine(">=================================================================================================================<");

            Msg = "e. Qual o percentual de uso de cada elevador com relação a todos os serviços prestados;";
            Console.WriteLine(Msg);
            Console.WriteLine($"Percentual de uso do elevador A: {iElevadorService.percentualDeUsoElevadorA()} %");
            Console.WriteLine($"Percentual de uso do elevador B: {iElevadorService.percentualDeUsoElevadorB()} %");
            Console.WriteLine($"Percentual de uso do elevador C: {iElevadorService.percentualDeUsoElevadorC()} %");
            Console.WriteLine($"Percentual de uso do elevador D: {iElevadorService.percentualDeUsoElevadorD()} %");
            Console.WriteLine($"Percentual de uso do elevador E: {iElevadorService.percentualDeUsoElevadorE()} %");
            Console.WriteLine(NewLine);

            Console.WriteLine(">=================================================================================================================<");
        }

        private static string traduzPeriodo(char periodo)
        {
            if (periodo == 'M')
            {
                return "M: Matutino";
            }
            else if (periodo == 'V')
            {
                return "V : Vespertino";
            }
            else
            {
                return "N: Noturno";
            }
        }
    }
}