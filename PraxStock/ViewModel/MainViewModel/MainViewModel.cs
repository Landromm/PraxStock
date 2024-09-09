using PraxStock.Services;
using PraxStock.View.Commands;
using PraxStock.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PraxStock.ViewModel.MainViewModel;
public class MainViewModel : DialogViewModel
{
	private readonly IUserDialog _userDialog = null!;


	public MainViewModel(IUserDialog userDialog)
	{
		_userDialog = userDialog;
	}

	#region Command's - Реализация комманд.
	#region Command OpenSecondFrameCommand - открытие окна настроек приложения.

	/// <summary>открытие окна настроек приложения.</summary>
	private LambdaCommand? _OpenSecondFrameCommand;

	/// <summary>открытие окна настроек приложения.</summary>
	public ICommand OpenSecondFrameCommand => _OpenSecondFrameCommand ??= new(ExecutedOpenSecondFrameCommand);

	/// <summary>Логика выполнения - открытие окна настроек приложения.</summary>
	private void ExecutedOpenSecondFrameCommand()
	{
		Application.Current.Dispatcher.Invoke(() =>
		{
			_userDialog.OpenSettingsWindow();
		});
	}
	#endregion
	#endregion

}