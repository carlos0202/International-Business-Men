using International_Business_Men.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace International_Business_Men.DAL.Extensions
{
    public static class DownloadExtensions
    {
        public static async Task<ICollection<T>> GetAsync<T>(this WebClient client, string URI)
            where T : IEntity
        {
            string data = await client.DownloadStringTaskAsync(URI);
            var result = JsonConvert.DeserializeObject<List<T>>(data);

            return result;
        }

        public static ICollection<T> GetSync<T>(this WebClient client, string URI)
            where T : IEntity
        {
            string data = client.DownloadString(URI);
            var result = JsonConvert.DeserializeObject<List<T>>(data);

            return result;
        }
    }
}
