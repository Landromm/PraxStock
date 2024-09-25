using PraxStock.Model.OtherModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PraxStock.Model.Message
{
    public class CurrentlyMainItemList
    {
        public MainListItems _MainListItems { get; set; }

        public CurrentlyMainItemList(MainListItems mainListItems) 
        {
            _MainListItems = mainListItems;
        }
    }
}
