using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Tools
{
    internal class Links
    {
        internal static String loginUrl = "https://www.odnoklassniki.ru/https";
        internal static String musicUrl = "http://wmf1.odnoklassniki.ru/my;jsessionid={0}?count=1000&start=0";
        internal static String trackDetailsUrl = "http://wmf1.odnoklassniki.ru/play;jsessionid={0}?type=10&pid=&tid={1}&position=0";
        internal static String userDetailsUrl = "http://wmf1.odnoklassniki.ru/friends;jsessionid={0}?start=0&count=1000";        
    }
}
