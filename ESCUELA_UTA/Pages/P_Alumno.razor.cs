using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//agregamos las siguientes librerias:
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using ESCUELA_UTA.Models;


namespace ESCUELA_UTA.Pages
{
    public partial class P_Alumno   //se agrega la función -partial-
    {

        [Inject] //Es el quivalente a usar: @inject HttpClient Http

        public HttpClient HttpClient { get; set; }
        //public MarkupString TBody { get; private set; }
        Alumno Alumno = new Alumno();
        List<Alumno> ListAlumno = new List<Alumno>();
        //Almacenar el mensaje de qué es lo que esta pasando.
        string Message;
        MarkupString TBody;
        const string RequestUri = "api/alumno";

        async Task GetAlumno() //
        {
            try
            {
                //Invocar la Web API
                var Response = await HttpClient.GetAsync(RequestUri);
                if (Response.IsSuccessStatusCode)
                {
                    //si la respuesta fue exitosa 
                    //Obtener los alumnos desde el cuerpo de la repsuesta.

                    ListAlumno = await Response.Content.ReadFromJsonAsync<List<Alumno>>();
                    //Generar el código HTML para mostrar los intituciones
                    SetAlumno();
                }
                else
                {
                    //Si la respuesta no fue exitosa, procesar la respuesta
                    await HandleResponse(Response);
                    ListAlumno = new List<Alumno>();
                    SetAlumno();
                };

            }
            catch (Exception ex)
            {
                Message = $"No se pudo obtener la información. {ex.Message}";
                ListAlumno = new List<Alumno>();
                SetAlumno();
            }
        }

        async Task ShowAlumno()
        {
            Message = "Procesando...";
            await GetAlumno();
            if (ListAlumno.Count > 0)
            {
                Message = $"{ListAlumno.Count} registros leídos.";
            }
        }

        async void FindById()  //Nos va permitir recuperar un alumno a través de su ID.
        {
            try
            {
                Message = "Procesando...";
                //Invocar la WebAPI

                var Response =
                    await HttpClient.GetAsync($"{RequestUri}/{Alumno.Id}");
                if (Response.IsSuccessStatusCode)   // Verificamos si la respuesta fue exitosa.
                {
                    //Si la respuesta fue exitosa:
                    //Obtener los datos desde el cuerpo de la respuesta
                    Alumno = await Response.Content.ReadFromJsonAsync<Alumno>();
                    Message = $"Alumno {Alumno.Id} encontrado";
                }
                else
                {
                    //Si no fue exitosa borramos los datos:
                    if (Response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        //El estatus NotFound (HTTP 404) indica que el alumno no fue encontrado
                        Message = $"El Alumno {Alumno.Id} no fue encontrado";
                        Alumno.Nombre = "";
                        Alumno.ApellidoP = "";
                        Alumno.ApellidoM = "";
                        Alumno.Grado = "";
                        Alumno.Grupo = "";
                    }
                    else
                    {
                        //Procesar la respuesta para códigos de estado distinto de 404
                        await HandleResponse(Response);
                    }
                }
            }
            catch (Exception ex)
            {

                Message = $"No se puede obtener la información. {ex.Message}";
            }

            //Indicar que hubo cambios en los datos y es necesario actualizar los cambios
            StateHasChanged();
        }
        //C A M B I O S
        async void UpdateAlumno()  //Nos va permitir recuperar un alumno a través de su ID.
        {
            try
            {
                Message = "Procesando...";
                //Invocar la WeAPI

                var Response =
                    await HttpClient.PutAsJsonAsync($"{RequestUri}/{Alumno.Id}", Alumno);

                if (Response.IsSuccessStatusCode)   // Verificamos si la respuesta fue exitosa.
                {
                    //Si la respuesta fue exitosa:
                    Message = $"Alumno {Alumno.Id} modificado";
                    await GetAlumno();
                }
                else
                {
                    //Procesar la respuesta no exitosa
                    if (Response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        //El estatus NotFound (HTTP 404) indica que el alumno no fue encontrada
                        Message = $"El Alumno {Alumno.Id} no fue encontrado";
                    }
                    else
                    {
                        //Procesar la respuesta para códigos de estado distinto de 404
                        await HandleResponse(Response);
                    }
                }
            }
            catch (Exception ex)
            {
                Message = $"No se puede agregar la información. {ex.Message}";
            }

            //Indicar que hubo cambios en los datos y es necesario actualizar los cambios
            StateHasChanged();
        }

        //B A J A S
        async void DeleteAlumno()  //Nos va permitir recuperar un alumno a través de su ID.
        {
            try
            {
                Message = "Procesando...";
                //Invocar la WeAPI

                var Response =
                    await HttpClient.DeleteAsync($"{RequestUri}/{Alumno.Id}");

                if (Response.IsSuccessStatusCode)   // Verificamos si la respuesta fue exitosa.
                {
                    //Si la respuesta fue exitosa:
                    Message = $"Alumno {Alumno.Id} eliminado";

                    //Actualizar la lista de alumnos
                    await GetAlumno();
                }
                else
                {
                    //Procesar la respuesta no exitosa
                    if (Response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        //El estatus NotFound (HTTP 404) indica que el alumno no fue encontrado
                        Message = $"El alumno {Alumno.Id} no fue encontrado";
                    }
                    else
                    {
                        //Procesar la respuesta para códigos de estado distinto de 404
                        await HandleResponse(Response);
                    }
                }
            }
            catch (Exception ex)
            {

                Message = $"No se puede agregar la información. {ex.Message}";
            }

            //Indicar que hubo cambios en los datos y es necesario actualizar los cambios
            StateHasChanged();
        }

        //A L T A S 
        async void AddAlumno()  //Nos va permitir recuperar un alumno través de su ID.
        {
            try
            {
                Message = "Procesando...";
                //Invocar la WeAPI
                //Alumno.Id = 0;
                var Response =
                    await HttpClient.PostAsJsonAsync(RequestUri, Alumno);
                if (Response.IsSuccessStatusCode)   // Verificamos si la respuesta fue exitosa.
                {
                    //Si la respuesta fue exitosa:
                    //Obtener los datos desde el cuerpo de la respuesta
                    Alumno = await Response.Content.ReadFromJsonAsync<Alumno>();
                    Message = $"Alumno {Alumno.Id} agregado";
                    await GetAlumno();
                }
                else
                {
                    //Procesar la respuesta para códigos de estado distinto de 404
                    await HandleResponse(Response);
                }
            }
            catch (Exception ex)
            {
                Message = $"No se puede agregar la información. {ex.Message}";
            }

            //Indicar que hubo cambios en los datos y es necesario actualizar los cambios
            StateHasChanged();
        }

        void SetAlumno()
        {
            var SB = new StringBuilder();
            foreach (var p in ListAlumno)
            {
                SB.Append("<tr>");
                SB.Append($"<td>{p.Id}</td>");
                SB.Append($"<td>{p.Nombre}</td>");
                SB.Append($"<td>{p.ApellidoP}</td>");
                SB.Append($"<td>{p.ApellidoM}</td>");
                SB.Append($"<td>{p.Grado}</td>");
                SB.Append($"<td>{p.Grupo}</td>");
                SB.Append("</tr>");
            }
            TBody = (MarkupString)SB.ToString();
            StateHasChanged();
        }

        async Task HandleResponse(HttpResponseMessage response)
        {
            switch (response.StatusCode) //Dependiendo del estatus que nos regrese la WebAPI manda el mensaje.
            {
                case System.Net.HttpStatusCode.InternalServerError:
                    //Es un estatus 500. Obtener el detalle del error que viene como texto plano en el cuerpo de la respuesta.
                    var Details =
                        await response.Content.ReadAsStringAsync();
                    Message = $"HTTP 500 Internar Serve error {Details}";
                    break;

                default:

                    //Es un estatus distitno a 500. Obtener los datos del error
                    //que vienen en formato Json según el RFC 7807
                    (int Status, string Tittle, string Details) ProbelmaDetails =
                        await response.Content.ReadFromJsonAsync<(int, string, string)>();
                    Message = $"{ProbelmaDetails.Status}, {ProbelmaDetails.Tittle}," + $"{ProbelmaDetails.Details}";
                    break;
            }
        }
    }
}
