using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RpgMvc.Models;

namespace RpgMvc.Controllers
{
  //   [Route("[controller]")]
    public class PersonagensController : Controller
    {

        public string uriBase = " http://luizsouza.somee.com/RpgApi/Personagens/";
        public async Task<ActionResult> IndexAsync()
        {
            try 
            {
                string uriComplementar = "GetAll";
                HttpClient httpClient = new HttpClient();
               // string token = HttpContext.Session.GetString("SessionTokenUsuario");
                // httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = await httpClient.GetAsync(uriBase + uriComplementar);
                string serialized = await response.Content.ReadAsStringAsync();

                if(response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    List<PersonagemViewModel> listaPersonagens = await Task.Run(() =>
                    JsonConvert.DeserializeObject<List<PersonagemViewModel>>(serialized));

                    return View(listaPersonagens);
                }
                else
                throw new System.Exception(serialized);
            }
            catch(System.Exception ex)
            {
                TempData["MensagemErro"] = ex.Message;
                return RedirectToAction("Index");
            }

        }

       

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
         [HttpPost]

        public async Task<ActionResult> CreateAsync(PersonagemViewModel p )
        {
            try 
            {
                HttpClient httpClient = new HttpClient ();
                string token = HttpContext.Session.GetString("SessionTokerUsuario");
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer" , token);

                var content = new StringContent(JsonConvert.SerializeObject(p));
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = await httpClient.PostAsync(uriBase, content);
                string serialized = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    TempData["Mensagem"] = string.Format("Personagem {0}, Id {1} Salvo com sucesso!", p.Nome, serialized);
                    return RedirectToAction("Index");
                }
                else 
                throw new System.Exception (serialized);
            }
            catch (System.Exception ex)
            {
                TempData["MensagemErro"] = ex.Message;
                return RedirectToAction("Create");
            }
            
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
    }

    [HttpGet]
    
    public async Task<ActionResult> DetailsAsync(int? id)
    {
        try 
        {
            HttpClient httpClient = new HttpClient();
            string token = HttpContext.Session.GetString("SessionTokenUsuario");
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await httpClient.GetAsync()
        }
    }











}