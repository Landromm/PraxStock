using PraxStock.View.Commands.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace PraxStock.View.Commands
{
	class CloseWindowCommand : Command
	{
		public override bool CanExecute(object? p) => p is Window;

		public override void Execute(object? p)
		{
			if(!CanExecute(p)) return;

			var window = (Window)p!;
			window!.Close();
		}
	}

	class CloseDialogCommand : Command
	{
		public bool? DialogResult { get; set; }

		public override bool CanExecute(object? p) => p is Window;

		public override void Execute(object? p)
		{
			if (!CanExecute(p))
				return;

			var window = (Window)p!;
			window.DialogResult = DialogResult;
			window!.Close();
		}
	}
}
