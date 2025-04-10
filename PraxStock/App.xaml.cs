using Microsoft.Extensions.DependencyInjection;
using PraxStock.Model.OtherModel;
using PraxStock.Services;
using PraxStock.Services.Implementations;
using PraxStock.View.MainViews;
using PraxStock.View.SecondViews;
using PraxStock.View.SecondViews.StatisticsWindow;
using PraxStock.ViewModel.MainViewModel;
using PraxStock.ViewModel.SecondViewModel;
using PraxStock.ViewModel.SecondViewModel.StatisticsViewModel;
using System.Configuration;
using System.Data;
using System.Windows;
using static System.Formats.Asn1.AsnWriter;

namespace PraxStock;
/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App
{
	private static IServiceProvider? _servicesProvider;

	public static IServiceProvider Services => _servicesProvider ??= InitializeServices().BuildServiceProvider();

	private static IServiceCollection InitializeServices()
	{
		var services = new ServiceCollection();

		services.AddSingleton<MainViewModel>();
		services.AddScoped<SettingsViewModel>();
		services.AddScoped<ItemsListViewModel>();
		services.AddSingleton<ReceiptAddViewModel>();
		services.AddScoped<MoveAddViewModel>();
		services.AddScoped<StatisticMainViewModel>();
		services.AddScoped<DataStocksStatisticsViewModel>();

		services.AddSingleton<IUserDialog, UserDialogServices>();
		services.AddSingleton<IMessageBus, MessageBusServices>();

		services.AddTransient(
			s =>
			{
				var model = s.GetRequiredService<MainViewModel>();
				var window = new MainWindow { DataContext = model };
				model.DialogComplete += (_, _) => window.Close();

				return window;
			});
		services.AddTransient(
			s =>
			{
				var scope = s.CreateScope();
				var model = scope.ServiceProvider.GetRequiredService<SettingsViewModel>();
				var window = new SettingsWindow { DataContext = model };
				model.DialogComplete += (_, _) => window.Close();
				window.Closed += (_, _) => scope.Dispose();

				return window;
			});
		services.AddTransient(
			s =>
			{
				var scope = s.CreateScope();
				var model = scope.ServiceProvider.GetRequiredService<ItemsListViewModel>();
				var window = new ItemsListView { DataContext = model };
				model.DialogComplete += (_, _) => window.Close();
				window.Closed += (_, _) => scope.Dispose();

				return window;
			});
		services.AddTransient(
			s =>
			{
				var model = s.GetRequiredService<ReceiptAddViewModel>();
				var window = new ReceiptAddView { DataContext = model };
				model.DialogComplete += (_, _) => window.Close();

				return window;
			});
		services.AddTransient(
			s =>
			{
				var model = s.GetRequiredService<MoveAddViewModel>();
				var window = new MoveAddView() { DataContext = model };
					model.DialogComplete += (_, _) => window.Close();

				return window;
			});
		services.AddTransient(
			s =>
			{
				var scope = s.CreateScope();
				var model = scope.ServiceProvider.GetRequiredService<StatisticMainViewModel>();
				var window = new StatisticMainView { DataContext = model };
					model.DialogComplete += (_, _) => window.Close();
					window.Closed += (_, _) => scope.Dispose();

				return window;
			});
		services.AddTransient(
			s =>
			{
				var scope = s.CreateScope();
				var model = scope.ServiceProvider.GetRequiredService<DataStocksStatisticsViewModel>();
				var window = new DataStocksStatisticsView { DataContext = model };
					model.DialogComplete += (_, _) => window.Close();
					window.Closed += (_, _) => scope.Dispose();

				return window;
			});

		return services;
	}

	protected override void OnStartup(StartupEventArgs e)
	{
		base.OnStartup(e);
		Services.GetRequiredService<IUserDialog>().OpenMainWindow();
	}
}

