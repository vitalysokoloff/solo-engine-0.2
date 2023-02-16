using System;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.Xna.Framework;

namespace Solo.Storage
{
    public static class IO
    {
        static private string _intPattern = @"^.+\s*:\s*\d+\s*$";
        static private string _floatPattern = @"^.+\s*:\s*((\d+,\d+)|(\d+))f\s*$";
        static private string _stringPattern = "^.+\\s*:\\s*\".+\"\\s*$";
        static private string _heapPattern = @"^.+\s*{\s*$";
        static private string _endPattern = @"^\s*}\s*$";
        static private string _boolPattern = @"^.+\s*:\s*\+|true|True|on|On|-|false|False|off|Off\s*$";
        static private string _pointPattern = @"^.+\s*:\s*\d\.\d+\s*$";
        static private string _vectorPattern = @"^.+\s*:\s*((\d+,\d+)|(\d+))f\.((\d+,\d+)|(\d+))f\s*$";

        public static Heap OpenHeap(string path)
        {
            Heap heap = new Heap();
            string fileName = path + ".heap";

            if (!File.Exists(fileName))
            {
                File.Create(fileName);
                return heap;
            }

            using (StreamReader sr = new StreamReader(fileName))
            {               
                heap = ReadHeap(sr);
            }

            return heap;
        }

        public static void SaveHeap(Heap heap, string path)
        {
            using (StreamWriter sw = new StreamWriter(path + ".heap"))
            {
                heap.Save(sw, "");
            }
        }

        private static Heap ReadHeap(StreamReader sr)
        {
            Heap heap = new Heap();

            while (sr.Peek() >= 0)
            {
                string[] data = ParseString(sr.ReadLine());

                switch (data[0])
                {
                    case "int":
                        heap.Add(data[1], Convert.ToInt32(data[2]));
                        break;
                    case "float":
                        heap.Add(data[1], (float)Convert.ToDouble(data[2]));
                        break;
                    case "string":
                        heap.Add(data[1], data[2]);
                        break;
                    case "bool":
                        heap.Add(data[1], data[2] == "+" || data[2] == "true" || data[2] == "True" || data[2] == "on" || data[2] == "On" ? true : false);
                        break;
                    case "point":
                        heap.Add(data[1], new Point(Convert.ToInt32(data[2]), Convert.ToInt32(data[3])));
                        break;
                    case "vector2":
                        heap.Add(data[1], new Vector2((float)Convert.ToDouble(data[2]), (float)Convert.ToDouble(data[3])));
                        break;
                    case "heap":
                        heap.Add(data[1], ReadHeap(sr));                        
                        break;
                    case "end":
                        return heap;
                    default:
                        break;
                }
            }
            return heap;
        }      

        static private string[] ParseString(string str)
        {
            string[] answer;

            str = str.Trim(' ');

            if (Regex.IsMatch(str, _intPattern))
            {
                string[] tmp = str.Split(':');

                answer = new string[3];

                answer[0] = "int";
                answer[1] = tmp[0].Trim(' ', '\t');
                answer[2] = tmp[1].Trim(' ', '\t');

                return answer;    
            }

            if (Regex.IsMatch(str, _floatPattern))
            {
                string[] tmp = str.Split(':');

                answer = new string[3];

                answer[0] = "float";
                answer[1] = tmp[0].Trim(' ', '\t');
                answer[2] = tmp[1].Trim(' ', '\t', 'f');

                return answer;
            }

            if (Regex.IsMatch(str, _stringPattern))
            {
                string[] tmp = str.Split(':');

                answer = new string[3];

                answer[0] = "string";
                answer[1] = tmp[0].Trim(' ', '\t');
                answer[2] = tmp[1].Trim(' ', '\t').Trim('"');

                return answer;
            }

            if (Regex.IsMatch(str, _boolPattern))
            {
                string[] tmp = str.Split(':');

                answer = new string[3];

                answer[0] = "bool";
                answer[1] = tmp[0].Trim(' ', '\t');
                answer[2] = tmp[1].Trim(' ', '\t');

                return answer;
            }

            if (Regex.IsMatch(str, _pointPattern))
            {
                string[] tmp = str.Split(':');

                answer = new string[4];

                answer[0] = "point";
                answer[1] = tmp[0].Trim(' ', '\t');

                string[] tmp2 = tmp[1].Trim(' ', '\t').Split('.');

                answer[2] = tmp2[0];
                answer[3] = tmp2[1];

                return answer;
            }

            if (Regex.IsMatch(str, _vectorPattern))
            {
                string[] tmp = str.Split(':');

                answer = new string[4];

                answer[0] = "vector2";
                answer[1] = tmp[0].Trim(' ', '\t');

                string[] tmp2 = tmp[1].Trim(' ', '\t').Split('.');

                answer[2] = tmp2[0].Trim('f');
                answer[3] = tmp2[1].Trim('f');

                return answer;
            }

            if (Regex.IsMatch(str, _heapPattern))
            {
                answer = new string[2];

                answer[0] = "heap";
                answer[1] = str.Trim(' ', '\t', '{');

                return answer;
            }

            if (Regex.IsMatch(str, _endPattern))
            {
                answer = new string[1];

                answer[0] = "end";

                return answer;
            }

            answer = new string[3];
            return answer;
        }
    }
}
