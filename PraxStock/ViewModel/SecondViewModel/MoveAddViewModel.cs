using PraxStock.Model.Message;
using PraxStock.Services.Implementations;
using PraxStock.View.Commands;
using PraxStock.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using PraxStock.View.SecondViews;

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

	#region RemainingStock : double -  Остаток в наличии.

	///<summary>Остаток в наличии. - поле.</summary>
	private double _RemainingStock;

	///<summary>Остаток в наличии. - свойство.</summary>
	public double RemainingStock
	{
		get => _RemainingStock;
		set
		{
			_RemainingStock = value;
			OnPropertyChanged(nameof(RemainingStock));
		}
	}
	#endregion

	#region UnitCount : double -  Количество перемещаемой позиции.

	///<summary>Количество перемещаемой позиции. - поле.</summary>
	private double _UnitCount;

	///<summary>Количество перемещаемой позиции. - свойство.</summary>
	public double UnitCount
	{
		get => _UnitCount;
		set
		{
			_UnitCount = value;
			OnPropertyChanged(nameof(UnitCount));
		}
	}
	#endregion

	#region NamePost : string? -  Наименование места перемещения.

	///<summary>Наименование места перемещения. - поле.</summary>
	private string? _NamePost;

	///<summary>Наименование места перемещения. - свойство.</summary>
	public string? NamePost
	{
		get => _NamePost;
		set
		{
			_NamePost = value;
			OnPropertyChanged(nameof(NamePost));
		}
	}
	#endregion

	#region DateMove : DateTime -  Дата перемещения в соответсвующее место.

	///<summary>Дата перемещения в соответсвующее место. - поле.</summary>
	private DateTime _DateMove;

	///<summary>Дата перемещения в соответсвующее место. - свойство.</summary>
	public DateTime DateMove
	{
		get => _DateMove;
		set
		{
			_DateMove = value;
			OnPropertyChanged(nameof(DateMove));
		}
	}
	#endregion


	public MoveAddViewModel(IMessageBus MessageBus) 
    {
		_messageBus = MessageBus;
		_subscription = MessageBus.RegisterHandler<CurrentlyMainItemList>(OnReceiveMessage);
		DateMove = DateTime.Now;
	}

	private void OnReceiveMessage(CurrentlyMainItemList message)
	{
		NameItem = message._MainListItems.Name;
		RemainingStock = message._MainListItems.UnitCount;
		Dispose();
	}
	public void Dispose() => _subscription.Dispose();


	#region Command CancelCommand - Отмена введенных данных.

	///<summary>Отмена введенных данных. - поле.</summary>
	private LambdaCommand? _CancelCommand;

	///<summary>Отмена введенных данных. - Реализация интерфейса</summary>
	public ICommand CancelCommand => _CancelCommand ??= new(ExecuteCancelCommand);

	///<summary>Логикак выполнения - Отмена введенных данных</summary>
	private void ExecuteCancelCommand()
	{
		UnitCount = 0;
		NamePost = "";
		DateMove = DateTime.Now;
	}
	#endregion

	#region Command AddMoveCommand - Подтверждение перемещения при выполнении условий.

	///<summary>Подтверждение перемещения при выполнении условий. - поле.</summary>
	private LambdaCommand? _AddMoveCommand;

	///<summary>Подтверждение перемещения при выполнении условий. - Реализация интерфейса</summary>
	public ICommand AddMoveCommand => _AddMoveCommand ??= new(ExecuteAddMoveCommand);

	///<summary>Логикак выполнения - Подтверждение перемещения при выполнении условий</summary>
	private void ExecuteAddMoveCommand()
	{

	}
	#endregion



}

