using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MioBot.Helper
{
    internal class StringHelper
    {
        //搜索字符串(参数1：完整的内容，参数2：左边的内容，参数3：右边的内容)----(获取两个字符串中间的字符串)       
        public static string Search_string(string s, string s1, string s2)  //获取搜索到的数目  
        {
            int n1, n2;
            n1 = s.IndexOf(s1, 0) + s1.Length;   //开始位置  
            n2 = s.IndexOf(s2, n1);               //结束位置    
            return s.Substring(n1, n2 - n1);   //取搜索的条数，用结束的位置-开始的位置,并返回    
        }
        //删除两个指定字符串之间的所有内容 必须保证指定字符唯一
        public static string Delete_string(string s, string s1, string s2)  //删除搜索到的数目  
        {
            int n1, n2;
            n1 = s.IndexOf(s1, 0) + s1.Length;   //开始位置  
            n2 = s.IndexOf(s2, n1);               //结束位置    
            return s.Remove(n1 - 1, n2 - n1 + 2);   //取搜索的条数，用结束的位置-开始的位置,并返回    
        }

    }
}
