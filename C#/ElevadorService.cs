using Prova;
using Newtonsoft.Json;
using ProvaAdmissionalCSharpApisul;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Prova.Services
{
    public class ElevadorService : IElevadorService
    {
        public List<Dados> ListaDados { get; set; }
        public int auxiliar = 0;

        public ElevadorService(string caminho)
        {
            if (!File.Exists(caminho))
            {
                Console.WriteLine("Caminho Invalido");
                Environment.Exit(1);
            }
            else
            {
                using (StreamReader r = new StreamReader(caminho))
                {
                    string json = r.ReadToEnd();
                    ListaDados = JsonConvert.DeserializeObject<List<Dados>>(json);
                }
            }
        }

        /// <summary> Deve retornar uma List contendo o(s) andar(es) menos utilizado(s). </summary>
        public List<int> andarMenosUtilizado()
        {
            var grupos = ListaDados.GroupBy(x => x.Andar);
            grupos = grupos.OrderBy(x => x.Count());
            var firstCount = grupos.FirstOrDefault().Count();

            List<int> resultado = grupos.Where(x => x.Count() == firstCount).OrderBy(x => x.Key).Select(x => x.Key).ToList();
            return resultado;
        }

        /// <summary> Deve retornar uma List contendo o(s) elevador(es) mais frequentado(s). </summary>
        public List<char> elevadorMaisFrequentado()
        {
            var grupos = ListaDados.GroupBy(d => d.Elevador);
            grupos = grupos.OrderByDescending(d => d.Count());
            var firstCount = grupos.FirstOrDefault().Count();

            List<char> resultado = grupos.Where(d => d.Count() == firstCount).OrderBy(d => d.Key).Select(d => d.Key).ToList();
            return resultado;
        }

        /// <summary> Deve retornar uma List contendo o período de maior fluxo de cada um dos elevadores mais frequentados (se houver mais de um). </summary>
        public List<char> periodoMaiorFluxoElevadorMaisFrequentado()
        {
            var ElevadoresFrequentados = elevadorMaisFrequentado();
            var resultado =
                from dados in ListaDados.Where(x => x.Elevador == ElevadoresFrequentados.FirstOrDefault()) group dados by dados.Turno;
            List<char> ListaPeriodoMaiorFluxoElevadorMaisFrequentado = new List<char>();
            foreach (var grupo in resultado)
            {
                if (auxiliar == 0)
                {
                    auxiliar = (int)grupo.Count();
                    ListaPeriodoMaiorFluxoElevadorMaisFrequentado.Add(grupo.Key);
                }
                else
                {
                    if (grupo.Count() >= auxiliar)
                    {
                        if (grupo.Count() == auxiliar)
                        {
                            ListaPeriodoMaiorFluxoElevadorMaisFrequentado.Add(grupo.Key);
                        }
                        else
                        {
                            auxiliar = (int)grupo.Count();
                            ListaPeriodoMaiorFluxoElevadorMaisFrequentado.Clear();
                            ListaPeriodoMaiorFluxoElevadorMaisFrequentado.Add(grupo.Key);
                        }
                    }
                }
            }
            auxiliar = 0;
            return ListaPeriodoMaiorFluxoElevadorMaisFrequentado;
        }

        /// <summary> Deve retornar uma List contendo o(s) elevador(es) menos frequentado(s). </summary>
        public List<char> elevadorMenosFrequentado()
        {
            var grupos = ListaDados.GroupBy(d => d.Elevador);
            grupos = grupos.OrderBy(d => d.Count());
            var firstCount = grupos.FirstOrDefault().Count();

            List<char> resultado = grupos.Where(x => x.Count() == firstCount).OrderBy(d => d.Key).Select(d => d.Key).ToList();
            return resultado;
        }

        /// <summary> Deve retornar uma List contendo o período de menor fluxo de cada um dos elevadores menos frequentados (se houver mais de um). </summary>
        public List<char> periodoMenorFluxoElevadorMenosFrequentado()
        {
            var ElevadoresFrequentados = elevadorMenosFrequentado();
            var resultado =
                from dados in ListaDados.Where(x => x.Elevador == ElevadoresFrequentados.FirstOrDefault()) group dados by dados.Turno;
            List<char> ListaPeriodoMenorFluxoElevadorMenosFrequentado = new List<char>();
            foreach (var grupo in resultado)
            {
                if (auxiliar == 0)
                {
                    auxiliar = (int)grupo.Count();
                    ListaPeriodoMenorFluxoElevadorMenosFrequentado.Add(grupo.Key);
                }
                else
                {
                    if (grupo.Count() >= auxiliar)
                    {
                        if (grupo.Count() == auxiliar)
                        {
                            ListaPeriodoMenorFluxoElevadorMenosFrequentado.Add(grupo.Key);
                        }
                        else
                        {
                            auxiliar = (int)grupo.Count();
                            ListaPeriodoMenorFluxoElevadorMenosFrequentado.Clear();
                            ListaPeriodoMenorFluxoElevadorMenosFrequentado.Add(grupo.Key);
                        }
                    }
                }
            }
            auxiliar = 0;
            return ListaPeriodoMenorFluxoElevadorMenosFrequentado;
        }

        /// <summary> Deve retornar uma List contendo o(s) periodo(s) de maior utilização do conjunto de elevadores. </summary>
        public List<char> periodoMaiorUtilizacaoConjuntoElevadores()
        {
            var grupos = ListaDados.GroupBy(x => x.Turno);
            grupos = grupos.OrderByDescending(x => x.Count());
            var firstCount = grupos.FirstOrDefault().Count();

            List<char> resultado = grupos.Where(x => x.Count() == firstCount).OrderBy(x => x.Key).Select(x => x.Key).ToList();
            return resultado;
        }

        // <summary> Deve retornar um float (duas casas decimais) contendo o percentual de uso do elevador  em relação a todos os serviços prestados. Recebendo qual elevador ele deve analizar</summary>
        public float percentualDeUsoElevador(char elevador)
        {
            var grupos = ListaDados.GroupBy(elevador => elevador.Elevador);
            int totalElevador = grupos.FirstOrDefault(g => g.Key.CompareTo(elevador) == 0).Count();
            float resultado = (float)totalElevador * 100 / (float)ListaDados.Count;
            resultado = (float)Math.Round(resultado * 100f) / 100f;
            return resultado;
        }

        /// <summary> Deve retornar um float (duas casas decimais) contendo o percentual de uso do elevador A em relação a todos os serviços prestados. </summary>
        public float percentualDeUsoElevadorA()
        { return percentualDeUsoElevador('A'); }

        /// <summary> Deve retornar um float (duas casas decimais) contendo o percentual de uso do elevador B em relação a todos os serviços prestados. </summary>
        public float percentualDeUsoElevadorB()
        { return percentualDeUsoElevador('B'); }

        /// <summary> Deve retornar um float (duas casas decimais) contendo o percentual de uso do elevador C em relação a todos os serviços prestados. </summary>
        public float percentualDeUsoElevadorC()
        { return percentualDeUsoElevador('C'); }

        /// <summary> Deve retornar um float (duas casas decimais) contendo o percentual de uso do elevador D em relação a todos os serviços prestados. </summary>
        public float percentualDeUsoElevadorD()
        { return percentualDeUsoElevador('D'); }

        /// <summary> Deve retornar um float (duas casas decimais) contendo o percentual de uso do elevador E em relação a todos os serviços prestados. </summary>
        public float percentualDeUsoElevadorE()
        { return percentualDeUsoElevador('E'); }
    }
}