using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Criptografia_Cesariana
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Run(async (context) =>
            {
                var publicaDesafio = getArquivoJson("446a2eaa8afd2ce2c569633f29137a66506de5d5");
                await context.Response.WriteAsync(publicaDesafio);
            });
        }

        public string getArquivoJson(string token)
        {
            var client = new WebClient { Encoding = System.Text.Encoding.UTF8 };
            var json = client.DownloadString("https://api.codenation.dev/v1/challenge/dev-ps/generate-data?token=" + token);

            JObject json_object = JObject.Parse(json);

            int numero_casas = (int)json_object["numero_casas"];
            string cifrado = (string)json_object["cifrado"];
            string decifrado = (string)json_object["decifrado"];
            string resumo_criptografico = (string)json_object["resumo_criptografico"];
            string message = string.Empty;

            // monta o resumo sha1 do texto decifrado
            var hash = new SHA1Managed().ComputeHash(Encoding.UTF8.GetBytes(DescriptografiaCesariana(cifrado, Convert.ToInt32(numero_casas))));
            resumo_criptografico = string.Concat(hash.Select(x => x.ToString("x2")));

            // descriptando o texto do json
            decifrado = DescriptografiaCesariana(cifrado, Convert.ToInt32(numero_casas));

            // criptando o texto decifrado
            var cifradoNovo = CriptografiaCesariana(decifrado, Convert.ToInt32(numero_casas));

            if (cifradoNovo == cifrado)
            {
                ArquivoGerado arq = new ArquivoGerado();

                arq.numero_casas = numero_casas;
                arq.token = token;
                arq.cifrado = cifrado;
                arq.decifrado = decifrado;
                arq.resumo_criptografico = resumo_criptografico;

                // buscar o diretorio do appsettings.json
                var diretorio = @"C:\Users\jaqueline.carvalho\source\repos\Criptografia_Cesariana\answer.json";

                SalvarArquivo(arq, diretorio);

                string caminhoJSON = diretorio;
                string postURI = "https://api.codenation.dev/v1/challenge/dev-ps/submit-solution?token=";
                byte[] arquivoByte = File.ReadAllBytes(caminhoJSON);

                using (MultipartFormDataContent tipoConteudo = new MultipartFormDataContent())
                {
                    tipoConteudo.Add(new StreamContent(new MemoryStream(arquivoByte)), "answer", "answer.json");
                    HttpResponseMessage resposta = new HttpClient().PostAsync(postURI + token, tipoConteudo).Result;

                    if (resposta.StatusCode.ToString().ToUpper() != "OK")
                    {
                        message = "Desafio enviado com Sucesso";
                    }
                    else
                    {
                        message = "Erro ao enviar o Desafio";
                    }
                }
            }
            return message;

           
        }
        public  void SalvarArquivo(ArquivoGerado arq, string caminho)
        {
            using (StreamWriter arquivo = File.CreateText(caminho))
            {
                JsonSerializer serializar = new JsonSerializer();
                serializar.Serialize(arquivo, arq);
            }
        }
        public  char cifra(char ch, int key)
        {
            if (!char.IsLetter(ch))
            {

                return ch;
            }
            char d = char.IsUpper(ch) ? 'A' : 'a';
            return (char)((((ch + key) - d) % 26) + d);
        }
        public  string CriptografiaCesariana(string input, int chave)
        {
            string output = string.Empty;

            foreach (char ch in input)
                output += cifra(ch, chave);

            return output;
        }

        public  string DescriptografiaCesariana(string input, int chave)
        {
            return CriptografiaCesariana(input, 26 - chave);
        }

        public class ArquivoGerado
        {
            public int numero_casas { get; set; }
            public string token { get; set; }
            public string cifrado { get; set; }
            public string decifrado { get; set; }
            public string resumo_criptografico { get; set; }

        }
    }
}
