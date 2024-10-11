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
using PraxStock.Communication.Repositories;
using PraxStock.Model.OtherModel;

namespace PraxStock.ViewModel.SecondViewModel;

class MoveAddViewModel : DialogViewModel
{
	private readonly IMessageBus _messageBus = null!;
	private readonly IDisposable _subscription = null!;
	private readonly IAdminRepositories _repositoriesDB = null!;

	#region IdDateStock : int - Id позиции на складе.

	/// <summary>Id позиции на складе. - поле.</summary>
	private int _IdDateStock;

	/// <summary>Id позиции на складе. - свойство.</summary>
	public int IdDateStock
	{
		get => _IdDateStock;
		set
		{
			_IdDateStock = value;
			OnPropertyChanged(nameof(IdDateStock));
		}
	}
	#endregion

	#region IdItems : int - ID самой позиции.

	/// <summary>ID самой позиции. - поле.</summary>
	private int _IdItems;

	/// <summary>ID самой позиции. - свойство.</summary>
	public int IdItems
	{
		get => _IdItems;
		set
		{
			_IdItems = value;
			OnPropertyChanged(nameof(IdItems));
		}
	}
	#endregion

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

	#region UnitMeasure : string? - Единицы измерения позиции.

	/// <summary>Единицы измерения позиции. - поле.</summary>
	private string? _unitMeasure;

	/// <summary>Единицы измерения позиции. - свойство.</summary>
	public string? UnitMeasure
	{
		get => _unitMeasure;
		set
		{
			_unitMeasure = value;
			OnPropertyChanged(nameof(UnitMeasure));
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

	#region NamePostList : List<string> - Список наименований мест для перемещения.

	/// <summary>Список наименований мест для перемещения. - поле.</summary>
	private List<string> _NamePostList;

	/// <summary>Список наименований мест для перемещения. - свойство.</summary>
	public List<string> NamePostList
	{
		get => _NamePostList;
		set
		{
			_NamePostList = value;
			OnPropertyChanged(nameof(NamePostList));
		}
	}
	#endregion

	#region SelectedNamePost : string? -  Наименование выбранного места перемещения.

	///<summary>Наименование места перемещения. - поле.</summary>
	private string? _SelectedNamePost;

	///<summary>Наименование места перемещения. - свойство.</summary>
	public string? SelectedNamePost
	{
		get => _SelectedNamePost;
		set
		{
			_SelectedNamePost = value;
			OnPropertyChanged(nameof(SelectedNamePost));
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
		_repositoriesDB = new AdminRepositories();
		_subscription = MessageBus.RegisterHandler<CurrentlyMainItemList>(OnReceiveMessage);
		DateMove = DateTime.Now;
		NamePostList = _repositoriesDB.GetAllNamePost();
	}

	private void OnReceiveMessage(CurrentlyMainItemList message)
	{
		IdDateStock = message._MainListItems.IdDataStock;
		IdItems = message._MainListItems.IdItem;
		NameItem = message._MainListItems.Name;
		UnitMeasure = message._MainListItems.UnitMeasure;
		RemainingStock = _repositoriesDB.GetRemainingStock(message._MainListItems.IdDataStock);
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
		SelectedNamePost = "";
		DateMove = DateTime.Now;
	}
	#endregion

	#region Command AddMoveCommand - Подтверждение перемещения при выполнении условий.

	///<summary>Подтверждение перемещения при выполнении условий. - поле.</summary>
	private LambdaCommand? _AddMoveCommand;

	///<summary>Подтверждение перемещения при выполнении условий. - Реализация интерфейса</summary>
	public ICommand AddMoveCommand => _AddMoveCommand ??= new(ExecuteAddMoveCommand, CanExecuteAddMoveCommand);

	private bool CanExecuteAddMoveCommand()
	{
		if (UnitCount > 0 && SelectedNamePost is not null)
			return true;
		return false;
	}
	///<summary>Логикак выполнения - Подтверждение перемещения при выполнении условий</summary>
	private void ExecuteAddMoveCommand()
	{
		if (UnitCount! > RemainingStock)
		{
			MessageBox.Show("Количество перемещаемой позиции, не может быть больше остатка на складе!",
				"Не верное количество!",
				MessageBoxButton.OK,
				MessageBoxImage.Warning);
			UnitCount = 0;
			return;
		}
		var moveListItem = new MoveListItem()
		{
			IdDataStock = IdDateStock,
			IdItem = IdItems,
			Name = NameItem,
			UnitMeasure = UnitMeasure,
			UnitCount = UnitCount,
			NamePost = SelectedNamePost,
			DateMove = DateOnly.FromDateTime(DateMove)
		};
		var resultAdd = _repositoriesDB.AddMoveInPost(moveListItem);
		if (resultAdd)
		{
			MessageBox.Show("Добавление перемещения позиции произведено УСПЕШНО!",
				"Результат операции перемещения",
				MessageBoxButton.OK,
				MessageBoxImage.Information);

			IdDateStock = 0;
			IdItems = 0;
			NameItem = "";
			SelectedNamePost = "";
			UnitMeasure = "";
			RemainingStock = 0;
			UnitCount = 0;
		}
		else
			MessageBox.Show("Ошибка операции перемещения позиции!",
				"Результат операции перемещения",
				MessageBoxButton.OK,
				MessageBoxImage.Warning);
	}
	#endregion



}

