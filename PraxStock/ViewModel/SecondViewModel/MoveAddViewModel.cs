using PraxStock.Model.Message;
using PraxStock.Services.Implementations;
using PraxStock.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PraxStock.ViewModel.SecondViewModel;

class MoveAddViewModel : DialogViewModel
{
	private readonly IMessageBus _messageBus = null!;
	private readonly IDisposable _subscription = null!;


	#region NameItem : string? - Наименование выбранной позиции.

	/// <summary>Наименование выбранной позиции. - поле.</summary>
	private string? _NameItem;

	/// <summary>Наименование выбранной позиции. - свойство.</summary>
	public string? NameItem
	{
		get => _NameItem;
		set
		{
			_NameItem = value;
			OnPropertyChanged(nameof(NameItem));
		}
	}
	#endregion



	public MoveAddViewModel(IMessageBus MessageBus) 
    {
		_messageBus = MessageBus;
		_subscription = MessageBus.RegisterHandler<CurrentlyMainItemList>(OnReceiveMessage);
	}

	private void OnReceiveMessage(CurrentlyMainItemList message)
	{
		NameItem = message._MainListItems.Name;
		Dispose();
	}
	public void Dispose() => _subscription.Dispose();

}

