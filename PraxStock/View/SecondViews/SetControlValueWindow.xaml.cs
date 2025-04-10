 using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PraxStock.View.SecondViews
{
    /// <summary>
    /// Логика взаимодействия для SetControlValueWindow.xaml
    /// </summary>
    public partial class SetControlValueWindow : Window
    {
        #region ValueControl : string - Значение ограничения контроля.

        /// <summary> Значение ограничения контроля. </summary> 
        public static readonly DependencyProperty ValueControlProperty =
            DependencyProperty.Register(
                nameof(ValueControl),
                typeof(string),
                typeof(SetControlValueWindow),
                new PropertyMetadata(default(string)));

        /// <summary> Значение ограничения контроля. /// </summary>
        [Description("Значение ограничения контроля.")]
        public string ValueControl 
        { 
            get => (string)GetValue(ValueControlProperty);
            set => SetValue(ValueControlProperty, value);
        }

		#endregion

		public SetControlValueWindow()
        {
            InitializeComponent();
        }
    }
}
