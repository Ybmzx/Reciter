using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reciter.Models
{
    public class PracticeConfig
    {
        public enum OrderType
        {
            [Description("顺序")]
            Ascending,
            [Description("倒序")]
            Descending,
            [Description("乱序")]
            Random,
        }
        public enum SelectionType
        {
            [Description("当前单词本中全部的单词")]
            All,
            [Description("当前单词本中选中的单词")]
            Selected
        }
        public OrderType Order { get; set; } = OrderType.Ascending;
        public SelectionType Selection { get; set; } = SelectionType.All;
    }
}
