using PraxStock.Model.DBModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PraxStock.Communication.Repositories
{
    interface IAdminRepositories
    {
        ObservableCollection<Item> GetAllItemsList();
        void AddItemsList(string nameItem, string unitMeasure);
        void ChangedItemList(string nameItem, string unitMeasure, int idItem);

	}
}
