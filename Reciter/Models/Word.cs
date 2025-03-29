using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Reciter.Models
{
    public partial class Word : ObservableObject
    {
        public string Content { get; set; } = string.Empty;
        public string Translation { get; set; } = string.Empty;
        [XmlIgnoreAttribute]
        [ObservableProperty]
        public bool isSelected = false;
    }
}
