using PraxStock.View.Commands.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PraxStock.View.Commands;
class CloseWindowCommand : Command
{
	private readonly Action<object> _Execute;
	private readonly Func<object, bool>? _CanExecute;

	public CloseWindowCommand(Action<object> Execute, Func<object, bool>? CanExecute = null)
	{
		_Execute = Execute;
		_CanExecute = CanExecute;
	}
	protected override bool CanExecute(object? parameter) => parameter is Window;

	protected override void Execute(object? parameter)
	{
		if (!CanExecute(parameter))
			return;

		var window = (Window)parameter!;
		window.Close();
	}
}

class CloseDialogCommand : Command
{
	public bool? DialogResult { get; set; }

	protected override bool CanExecute(object? parameter) => parameter is Window;

	protected override void Execute(object? parameter)
	{
		if (!CanExecute(parameter))
			return;

		var window = (Window)parameter!;
		window.DialogResult = DialogResult;
		window.Close();
	}
}
