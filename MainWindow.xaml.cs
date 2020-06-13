using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CRUD2
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		//ObservableCollection<Artikal> listaArt = new ObservableCollection<Artikal>();
		BazaXYZ db = new BazaXYZ();

		private string _pretraga;
		public string Pretraga 
		{ 
			get => _pretraga; 
			set
			{
				_pretraga = value;
				if (string.IsNullOrWhiteSpace(_pretraga))
					dg.ItemsSource = db.Artikals.ToList();
				else
					dg.ItemsSource = db.Artikals.Where(a => a.Naziv.Contains(_pretraga.Trim())).ToList();
			}
		}

		public MainWindow()
		{
			InitializeComponent();
			DataContext = this;
			
			dg.ItemsSource = db.Artikals.ToList();
		}

		private void Dodaj(object sender, RoutedEventArgs e)
		{
			Editor ed = new Editor();
			ed.Owner = this;

			if (ed.ShowDialog().Value) //bool?    bool
			{
				db.Artikals.Add(ed.DataContext as Artikal);
				dg.ItemsSource = db.Artikals.ToList();
				db.SaveChanges();
			}
		}

		private void Obrisi(object zklj, RoutedEventArgs kasdf)
		{
			if (dg.SelectedItem != null)
			{
				db.Artikals.Remove(dg.SelectedItem as Artikal);
				dg.ItemsSource = db.Artikals.ToList();
				db.SaveChanges();
			}
		}

		private void Izmena(object o, RoutedEventArgs rea)
		{
			if (dg.SelectedItem != null)
			{
				Editor e = new Editor();
				e.Owner = this;
				e.DataContext = dg.SelectedItem;
				e.ShowDialog();
				db.SaveChanges();
			}
		}
	}
}
