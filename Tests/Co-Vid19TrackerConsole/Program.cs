using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Co_Vid19TrackerConsole
{
    class Program
    {
        private const string DATA_URL = @"https://raw.githubusercontent.com/CSSEGISandData/COVID-19/master/csse_covid_19_data/csse_covid_19_time_series/time_series_covid19_confirmed_global.csv";
        
        //Этот метод возвращает поток, из которого читаем текстовые данные
        private static async Task<Stream> GetDataStream()
        {
            //Создание клиента
            var client = new HttpClient();
            //Инициализируем запроса к серверу и ответ сервера и если файл большого объёма, не скачивался полностью,
            //а извлекались только те данные, которые необходимы (в данном случае только заголовки).
            var response = await client.GetAsync(DATA_URL, HttpCompletionOption.ResponseHeadersRead);
            //Возвращение потока, которые обеспечит нам процесс чтения данных из сетевой карты.
            return await response.Content.ReadAsStreamAsync();
        }

        //Теперь читая текстовые данные, надо разбить их на строки
        private static IEnumerable<string> GetDataLines()
        {
            //Внутри метода должны получить поток, произойдет запрос и ответ сервера
            using var data_stream = GetDataStream().Result;
            //На его основе основе создать объект, который будет читать строковые данные
            using var data_reader = new StreamReader(data_stream);
            //Далее читаем данные пока не будет конца потока
            while (!data_reader.EndOfStream)
            {
                //До тех пор пока не конец потока, извлекаем очередную строку и помещаем 
                //её в переменную как результат
                var line = data_reader.ReadLine();
                //После этого нужно проверить, что строка не пустая
                //Если строка пустая, то делаем следующий цикл
                if (string.IsNullOrWhiteSpace(line)) continue;
                yield return line.Replace("Korea,", "Korea - ");
            }
        }
        //Метод, выделяющий необходимые нам данные, разпарсим первую
        //строку для извлечения всех дат
        private static DateTime[] GetDateTimes() => GetDataLines()
            .First().Split(',').Skip(5).Select(s => DateTime.Parse(s, CultureInfo.InvariantCulture))
            .ToArray();


       //Теперь необходимо извлечь данные по всем странам
        private static IEnumerable<(string Country, string Province, int[] Counts)> GetData()
        {
            var lines = GetDataLines().Skip(1).Select(line => line.Split(','));
            foreach(var row in lines)
            {
                var province = row[0].Trim();
                var countryName = row[1].Trim(' ', '"');
                var counts = row.Skip(5).Select(int.Parse).ToArray();
                yield return (countryName, province, counts);
            }
        }
        
        static void Main(string[] args)
        {
            //Для старых версий.
            //WebClient client = new WebClient();

            //var client = new HttpClient();
            //var response = client.GetAsync(DATA_URL).Result;
            //var csv_str = response.Content.ReadAsStringAsync().Result;
            //var dates = GetDateTimes();
            //Console.WriteLine(string.Join("\r\n",dates));
            var russia_data = GetData().First(v => v.Country.Equals("Russia", StringComparison.OrdinalIgnoreCase));

            Console.WriteLine(string.Join("\r\n", GetDateTimes().Zip(russia_data.Counts, (date, count) => $"{date:dd:MM:yy} - {count}")));
            Console.ReadLine();
        }
    }
}
