using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace habbo_api
{
    class HabboPlayer
    {
        public string uniqueID;
        public string name;
        public string motto;
    }
    class Program
    {
        static void Main(string[] args)
        {

            string local= ".com.br";
            string user_name = "leoz_hu3";

            using(WebClient wc = new WebClient()){
                wc.Headers.Add("User-Agent", "Mozilla/5.0 (Linux; U; Android 4.0.3; ko-kr; LG-L160L Build/IML74K) AppleWebkit/534.30 (KHTML, like Gecko) Version/4.0 Mobile Safari/534.30");
                var Json = wc.DownloadString($"http://www.habbo{local}/api/public/users?name={user_name}");        
                JObject obj = JObject.Parse(Json);
                HabboPlayer player = new HabboPlayer() {uniqueID=obj["uniqueId"].ToString(),name=obj["name"].ToString(),motto=obj["motto"].ToString()};
                Console.WriteLine($"Usuario: {player.name}\nMissão: {player.motto}\nUniqueID: {player.uniqueID}");
                Console.WriteLine("=======================================================");
                wc.Headers.Add("User-Agent", "Mozilla/5.0 (Linux; U; Android 4.0.3; ko-kr; LG-L160L Build/IML74K) AppleWebkit/534.30 (KHTML, like Gecko) Version/4.0 Mobile Safari/534.30");
                var JsonFriends = wc.DownloadString($"https://www.habbo{local}/api/public/users/{player.uniqueID}/profile");
                JObject objFriends = JObject.Parse(JsonFriends);
                var friends = objFriends["friends"].ToArray();
                for(int i=0;i<friends.Length;i++){
                    Console.WriteLine($"Amigo: {friends[i]["name"]}");
                }
            }
        }
    }
}
