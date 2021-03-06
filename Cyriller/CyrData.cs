﻿using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.IO.Compression;
using System.Reflection;

namespace Cyriller
{
    internal class CyrData
    {
        public CyrData()
        { 
        }

        public TextReader GetData(string FileName)
        {
            string[] resourceNames = Assembly.GetExecutingAssembly().GetManifestResourceNames();
            string resourceName = resourceNames.FirstOrDefault(p => p.EndsWith("Cyriller.App_Data." + FileName));

            Stream stream = typeof(CyrData).Assembly.GetManifestResourceStream(resourceName);
            GZipStream gzip = new GZipStream(stream, CompressionMode.Decompress);
            TextReader treader = new StreamReader(gzip);

            return treader;
        }



        /// <summary>
        /// To do. Create a clever search algorithm.
        /// </summary>
        /// <param name="Word"></param>
        /// <param name="Collection"></param>
        /// <returns></returns>
        public string GetSimilar(string Word, List<string> Collection)
        {
            if (Word.IsNullOrEmpty())
            {
                return Word;
            }

            Dictionary<string, int> keys = new Dictionary<string, int>();

            foreach (string s in Collection)
            {
                if (s.EndsWith(Word))
                {
                    keys.Add(s, s.Length);
                }
            }

            if (!keys.Any() && Word.Length > 2)
            {
                return this.GetSimilar(Word.Substring(1), Collection);
            }

            string key = keys.OrderBy(val => val.Value).FirstOrDefault().Key;

            return key;
        }
    }
}
