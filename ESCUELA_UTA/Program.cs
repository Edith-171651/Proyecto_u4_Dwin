using ESCUELA_UTA;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ESCUELA_UTA
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            //builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });


            builder.Services.AddTransient(sp => new HttpClient
            {
                BaseAddress = new Uri("https://localhost:44369/")  //COPIAMOS/PEGAMOS LA URL (SOLO HASTA LA DIAGONAL QUE SIGUE DEL PUERTO) QUE UTILIZAMOS EN EL POSTMAN.
                //                                                   //CONFORME VAYAMOS DESARROLLANDO LE VAMOS INCLUYENDO EN LA URL .../ALUMNO/1 (POR EJEMPLO)

                //BaseAddress = new Uri("http://10.200.3.17:83/WebAPISAI/api/nota")  //COPIAMOS/PEGAMOS LA URL (SOLO HASTA LA DIAGONAL QUE SIGUE DEL PUERTO) QUE UTILIZAMOS EN EL POSTMAN.
            });

            await builder.Build().RunAsync();
        }
    }
}
