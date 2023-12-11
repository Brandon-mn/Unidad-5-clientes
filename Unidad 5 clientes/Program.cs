using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;

class Program
{
    static async Task Main()
    {
        using (HttpClient cliente = new HttpClient())
        {

            cliente.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/xml"));

            string urlServicio = "https://localhost:7077/api/Values";
            HttpResponseMessage respuesta = await cliente.GetAsync(urlServicio);

            if (respuesta.IsSuccessStatusCode)
            {
                // Lee el contenido en formato XML
                string contenidoXml = await respuesta.Content.ReadAsStringAsync();

                // Muestra el contenido XML en la consola
                Console.WriteLine("Respuesta del servicio (XML):");
                Console.WriteLine(contenidoXml);

                // Puedes procesar el contenido XML si es necesario
                ProcesarXml(contenidoXml);
            }
            else
            {
                Console.WriteLine($"Error: {respuesta.StatusCode}");
            }
        }
    }

    static void ProcesarXml(string contenidoXml)
    {
        try
        {
            // Crea un objeto XmlDocument y carga el contenido XML
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(contenidoXml);

            // Realiza cualquier procesamiento adicional según tus necesidades
            // Por ejemplo, podrías extraer valores específicos del XML
            string mensaje = xmlDoc.SelectSingleNode("//Mensaje").InnerText;
            Console.WriteLine($"Mensaje extraído del XML: {mensaje}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al procesar XML: {ex.Message}");
        }
    }
}