﻿using PraxStock.View.Commands.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PraxStock.View.Commands;
public class LambdaCommand : Command
{
	private readonly Delegate? _Execute;
	private readonly Delegate? _CanExecute;

	public LambdaCommand(Action<object?> Execute, Func<bool>? CanExecute = null)
	{
		_Execute = Execute;
		_CanExecute = CanExecute;
	}

	public LambdaCommand(Action<object?> Execute, Func<object?, bool>? CanExecute)
	{
		_Execute = Execute;
		_CanExecute = CanExecute;
	}
	public LambdaCommand(Action Execute, Func<bool>? CanExecute = null)
	{
		_Execute = Execute;
		_CanExecute = CanExecute;
	}
	public LambdaCommand(Action Execute, Func<object?, bool>? CanExecute)
	{
		_Execute = Execute;
		_CanExecute = CanExecute;
	}
	public override bool CanExecute(object? p)
	{
		if (!base.CanExecute(p))
			return false;
		return _CanExecute switch
		{
			null => true,
			Func<bool> can_exec => can_exec(),
			Func<object?, bool> can_exec => can_exec(p),
			_ => throw new InvalidOperationException($"Тип делегата {_CanExecute.GetType()} не поддерживается командой")
		};
	}
	public override void Execute(object? p)
	{
		switch (_Execute)
		{
			default:
				throw new InvalidOperationException($"Тип делегата {_Execute.GetType()} не поддерживается командой");
			case null:
				throw new InvalidOperationException("Не указан делегат вызова для команды");
			case Action execute:
				execute();
				break;
			case Action<object?> execute:
				execute(p);
				break;
		}
	}
}

