using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reciter.Models
{
    public class WordBook
    {
        public string Name { get; set; } = string.Empty;
        public ObservableCollection<Word> Words { get; set; } = new ObservableCollection<Word>();
    }
}
