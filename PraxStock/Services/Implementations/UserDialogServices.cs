using Microsoft.Extensions.DependencyInjection;
using PraxStock.View.MainViews;
using PraxStock.View.SecondViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PraxStock.Services.Implementations;
internal class UserDialogServices : IUserDialog
{
	private readonly IServiceProvider _service;

	public UserDialogServices(IServiceProvider service)
	{
		_service = service;
	}

	// Открытие главного окна программы.
	private MainWindow? _mainWindow = null;
	public void OpenMainWindow()
	{
		if (_mainWindow is { } window)
		{
			window.Show();
			return;
		}

		window = _service.GetRequiredService<MainWindow>();
		window.Closed += (_, _) => _mainWindow = null;
		_mainWindow = window;
		window.Show();
	}

	// Открытие окна настроек.
	private SettingsWindow? _settingsWindow = null;
	public void OpenSettingsWindow()
	{
		if (_settingsWindow is { } window)
		{
			window.Show();
			return;
		}

		window = _service.GetRequiredService<SettingsWindow>();
		window.Closed += (_, _) => _settingsWindow = null;
		_settingsWindow = window;
		window.Show();
	}

	//Открытие окна просмотра и радактирования списка основных позиций.
	private ItemsListView? _itemsListWindow = null;
	public void OpenItemsListWindow()
	{
		if(_itemsListWindow is { } window) 
		{
			window.Show();
			return;
		}

		window = _service.GetRequiredService<ItemsListView>();
		window.Closed += (_, _) => _itemsListWindow = null;
		_itemsListWindow = window;
		window.Show();
	}

	//Открытие окна добавления поступления.
	private ReceiptAddView? _receiptAddView = null;
	public void OpenAddReceiptWindow()
	{
		if(_receiptAddView is { } window)
		{
			window.ShowDialog();
			return;
		}
		window = _service.GetRequiredService<ReceiptAddView>();
		window.Closed += (_, _) => _receiptAddView = null;
		_receiptAddView = window;
		window.ShowDialog();
	}

	//Открытие окна добавления перемещения позиций.
	private MoveAddView? _moveAddView = null;
	public void OpenMoveAddWindow()
	{
		if (_moveAddView is { } window)
		{
			window.Show();
			return;
		}
		
		window = _service.GetRequiredService<MoveAddView>();
		window.Closed += (_, _) => _moveAddView = null;
		_moveAddView = window;
		window.Show();
	}

	//Открытие окна статистики.
	private StatisticMainView? _statisticMainView = null;
	public void OpenStatisticsMainWindow()
	{
		if(_statisticMainView is { } window)
		{
			window.Show();
			return;
		}
		window = _service.GetRequiredService<StatisticMainView>();
		window.Closed += (_, _) => _statisticMainView = null;
		_statisticMainView = window;
		window.Show();
	}

}
