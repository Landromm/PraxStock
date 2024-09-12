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


}
