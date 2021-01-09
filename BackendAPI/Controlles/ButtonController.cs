using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

using Microsoft.AspNetCore.Mvc;


namespace myApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ButtonController : Controller
    {
      
        public PartialViewResult DefinicaoArquitetura()
        {
            return PartialView();
        }
        private static string[] _Definicoes = new string[]
      {
            "SOA (sigla em inglês para Service Oriented " +
            "Architecture) - ou Arquitetura Orientada " +
            "a Serviços, em português - é um modelo de arquitetura " +
            "na área de Sistemas de Informação. " +
            "Tem por objetivo principal o atendimento de " +
            "necessidades de negócios a partir do fornecimento " +
            "de serviços agrupados em componentes (módulos) " +
            " de software. A implantação desta arquitetura é " +
            "feita, de maneira geral, através da utilização " +
            "de Web Services.",
            "REST (sigla em inglês para Representational State " +
            "Transfer) é uma especificação na qual recursos " +
            "são representados por meio de um endereço único, " +
            "através do uso de URLs. Dados consumidos por uma " +
            "aplicação sob a forma de objetos constituem exemplos " +
            "de recursos. Um endereço único, por sua vez, é " +
            "formado por informações de identificação do recurso " +
            "no serviço responsável por prover o mesmo, assim " +
            "como por uma indicação da operação que se deseja " +
            "efetuar sobre tal recurso.",
            "BPM (Business Process Modeling) é um paradigma " +
            "que oferece em suas definições diversas técnicas " +
            "para a modelagem de processos de negócio. Inúmeras " +
            "ferramentas de mercado encapsulam elementos e " +
            "práticas deste conceito, contando inclusive " +
            "com mecanismos para a modelagem gráfica de fluxos " +
            "de atividades.",
            "Acoplamento é um conceito empregado para se " +
            "determinar o grau de relacionamento entre " +
            "diferentes partes que constituem uma aplicação. " +
            "Um alto acoplamento resulta em um nível de " +
            "alta dependência entre os diversos componentes " +
            "envolvidos, fato este que pode levar a dificuldades " +
            "na manutenção futura das funcionalidades " +
            "consideradas. Logo, o ideal é que exista um " +
            "baixo nível de acoplamento entre as estruturas que " +
            "constituem um determinado elemento."
      };

        public static string ObterDefinicao()
        {
            Random r = new Random();
            int posicao = r.Next(0, 3);
            return _Definicoes[posicao];
        }
    }
}