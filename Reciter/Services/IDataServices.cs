using Reciter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reciter.Services
{
    public interface IDataServices
    {
        public List<WordBook> GetWordBooks();
        public WordBook At(int index);
        public void Save();
        public void RemoveAt(int index);
        public WordBook Add(WordBook wordBook);
    }
}
