using System;
using System.Collections.Generic;
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

namespace CRUD2
{
	/// <summary>
	/// Interaction logic for Editor.xaml
	/// </summary>
	public partial class Editor : Window
	{

		public Editor()
		{
			BindingGroup = new BindingGroup();
			DataContext = new Artikal();
			InitializeComponent();
		}

		private void Otk(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		private void OK(object sender, RoutedEventArgs e)
		{
			if (BindingGroup.CommitEdit())
			{
				DialogResult = true;
				this.Close();
			}
		}
	}
}
