﻿using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace AppServices.Service
{
    public class Service
    {

        public ESalidaCancelacion SolicitarCancelacion(EEntradaCancelacion Parametros)
        {
            ESalidaCancelacion eRespuesta = new ESalidaCancelacion();
            string datos = JsonConvert.SerializeObject(Parametros);
            string json = string.Empty;
            //var usuario = ConfigurationManager.AppSettings["usuario"].ToString();
            //var clave = ConfigurationManager.AppSettings["clave"].ToString();
            var tiempoEspera = ConfigurationManager.AppSettings["tiempoEspera"].ToString();
            var url = ConfigurationManager.AppSettings["url"].ToString();
            url = url.Replace("$", "&");
            byte[] data = UTF8Encoding.UTF8.GetBytes(datos);
            HttpWebRequest peticion;
            peticion = WebRequest.Create(url) as HttpWebRequest;
            peticion.Timeout = Convert.ToInt32(tiempoEspera) * 1000;
            peticion.Method = "POST";
            peticion.ContentLength = data.Length;
            peticion.ContentType = "application/json; charset=utf-8";
            //peticion.Headers.Add("Usuario", usuario);
            //peticion.Headers.Add("Clave", clave);
            Stream postTorrente = peticion.GetRequestStream();
            postTorrente.Write(data, 0, data.Length);
            postTorrente.Close();
            try
            {
                HttpWebResponse respuesta = peticion.GetResponse() as HttpWebResponse;
                StreamReader lectura = new StreamReader(respuesta.GetResponseStream(), Encoding.UTF8);
                json = lectura.ReadToEnd();
                eRespuesta = JsonConvert.DeserializeObject<ESalidaCancelacion>(json);
            }
            catch (Exception ex)
            {
            }
            return eRespuesta;
        }
    }
}
