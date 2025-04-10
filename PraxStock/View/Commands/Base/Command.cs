﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PraxStock.View.Commands.Base;
public abstract class Command : ICommand
{
	event EventHandler? ICommand.CanExecuteChanged
	{
		add => CommandManager.RequerySuggested += value;
		remove => CommandManager.RequerySuggested -= value;
	}

	bool ICommand.CanExecute(object? parameter) => CanExecute(parameter);

	void ICommand.Execute(object? parameter)
	{
		if (((ICommand)this).CanExecute(parameter))
			Execute(parameter);
	}
	public virtual bool CanExecute(object? p) => true;
	public abstract void Execute(object? p);
}
