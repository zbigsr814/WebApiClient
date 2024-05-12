using Newtonsoft.Json;
using System;
using System.Globalization;
using System.Net.Http.Json;
using System.Text;

namespace WebApiClient // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var httpClient = new HttpClient();

            while (true)
            {
                await Console.Out.WriteLineAsync("Podaj miasto dla którego chcesz wyszukać pogodę\nlub wpisz 'quit' aby zakończyć aplikację");
                var cityToFind = Console.ReadLine();

                if (cityToFind == "quit") break;

                var normalCityToFind = RemoveDiacritics(cityToFind);
                var dataFromSite = await httpClient.GetAsync(string.Format("https://api.weatherapi.com/v1/current.json?key=bd55db8073f645cfa6381817241205&q={0}&aqi=no", normalCityToFind));
                var json = await dataFromSite.Content.ReadAsStringAsync();
                var deserializedData = JsonConvert.DeserializeObject<Weather>(json);     // tworzenie podstawowego obiektu

                if (deserializedData.current != null)
                {
                    await Console.Out.WriteLineAsync("Ciśnienie atmosferyczne: " + deserializedData.current.condition.text.ToString());
                    await Console.Out.WriteLineAsync("Temperatura: " + deserializedData.current.temp_c.ToString() + "°C");
                    await Console.Out.WriteLineAsync("Zachmurzenie: " + deserializedData.current.cloud.ToString() + "%");
                    await Console.Out.WriteLineAsync("Wilgotność: " + deserializedData.current.humidity.ToString() + "%");
                    await Console.Out.WriteLineAsync("Ciśnienie atmosferyczne: " + deserializedData.current.pressure_mb.ToString() + "hPa");
                    await Console.Out.WriteLineAsync("Prędkość wiatru: " + deserializedData.current.wind_mph.ToString() + "m/s");
                    await Console.Out.WriteLineAsync("Kierunek wiatru: " + deserializedData.current.wind_dir.ToString() + "\n\n");
                }
                else
                {
                    await Console.Out.WriteLineAsync("Nie znaleziono takiego miasta\n\n");
                }
            }
            



        }

        static string RemoveDiacritics(string text)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (char c in text)
            {
                switch (c)
                {
                    case 'ą': stringBuilder.Append('a'); break;
                    case 'ć': stringBuilder.Append('c'); break;
                    case 'ę': stringBuilder.Append('e'); break;
                    case 'ł': stringBuilder.Append('l'); break;
                    case 'ń': stringBuilder.Append('n'); break;
                    case 'ó': stringBuilder.Append('o'); break;
                    case 'ś': stringBuilder.Append('s'); break;
                    case 'ź': stringBuilder.Append('z'); break;
                    case 'ż': stringBuilder.Append('z'); break;
                    case 'Ą': stringBuilder.Append('A'); break;
                    case 'Ć': stringBuilder.Append('C'); break;
                    case 'Ę': stringBuilder.Append('E'); break;
                    case 'Ł': stringBuilder.Append('L'); break;
                    case 'Ń': stringBuilder.Append('N'); break;
                    case 'Ó': stringBuilder.Append('O'); break;
                    case 'Ś': stringBuilder.Append('S'); break;
                    case 'Ź': stringBuilder.Append('Z'); break;
                    case 'Ż': stringBuilder.Append('Z'); break;
                    default: stringBuilder.Append(c); break;
                }
            }
            return stringBuilder.ToString();
        }
    }
}