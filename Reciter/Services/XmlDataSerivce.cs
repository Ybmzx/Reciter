using Reciter.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Reciter.Services
{
    public class XmlDataSerivce : IDataServices
    {
        List<WordBook> wordBooks = new();
        string path = "data.xml";
        public XmlDataSerivce(string path)
        {
            this.path = path;
            if (File.Exists(path))
            {
                using (StringReader sr = new StringReader(File.ReadAllText(path)))
                {
                    XmlSerializer serializer = new XmlSerializer(wordBooks.GetType());
                    var temp = (List<WordBook>)serializer.Deserialize(sr);
                    if (temp is not null)
                        wordBooks = temp;
                }
            }
        }

        public WordBook Add(WordBook wordBook)
        {
            wordBooks.Add(wordBook);
            return wordBooks.Last();
        }

        public WordBook At(int index)
        {
            return wordBooks[index];
        }

        public List<WordBook> GetWordBooks()
        {
            return wordBooks;
        }

        public void RemoveAt(int index)
        {
            wordBooks.RemoveAt(index);
        }

        public void Save()
        {
            using (StringWriter sw = new StringWriter())
            {
                XmlSerializer serializer = new XmlSerializer(wordBooks.GetType());
                serializer.Serialize(sw, wordBooks);
                File.WriteAllText(path, sw.ToString());
            }
        }

    }
}
