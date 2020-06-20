using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CRUD2
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	/// 

	/* Za domaci
	 * 
	 * Artikli treba da sadrze kolicinu.
	 * Izdavanje racuna skida sa kolicine.
	 * Treba mehanizam za dodavanj kolicine.
	 * Treba mehanizam za dupliranje
	 * artikala na racunu (tj protiv :D)
	 * Treba da se sredi onaj nesretni
	 * datagrid za racune.
	 * 
	 * EPIC: Treba nam dinamicna cena za 
	 * artikle. Umesto fiksne cene
	 * artikal treba da ima listu. Idealno,
	 * recnik (datum, cena) no zbog baze, kao
	 * i artkol, drugu klasu. Te kada god citamo
	 * cenu artikla, treba nam ulaz, datum za
	 * koji nas cena intresuje. Ako nema
	 * ulaza za datum onda smatramo da je zadnja.
	 */
	public partial class MainWindow : Window, INotifyPropertyChanged
	{
		//ObservableCollection<Artikal> listaArt = new ObservableCollection<Artikal>();
		BazaXYZ db = new BazaXYZ();

		private int _trenutnaSifra;
		public int TrenutnaSifra 
		{ 
			get => _trenutnaSifra; 
			set
            {
				_trenutnaSifra = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TrenutnaSifra"));
            }
		}
		private int _trenutnaKolicina;
		public int TrenutnaKolicina 
		{ 
			get => _trenutnaKolicina; 
			set
            {
				_trenutnaKolicina = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TrenutnaKolicina"));
            }
		}

		public Racun TrenutniRacun = new Racun();

		private string _pretraga;

        public event PropertyChangedEventHandler PropertyChanged;

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
			UnosRac.BindingGroup = new BindingGroup();
			db.Racuns.ToList();
			dgRacuni.ItemsSource = db.Racuns.Local;
		}

		private void Dodaj(object sender, RoutedEventArgs e)
		{
			Editor ed = new Editor();
			ed.Owner = this;

			if (ed.ShowDialog().Value) //bool?    bool
			{
				db.Artikals.Add(ed.DataContext as Artikal);
				db.SaveChanges();
				dg.ItemsSource = db.Artikals.ToList();
			}
		}

		private void Obrisi(object zklj, RoutedEventArgs kasdf)
		{
			if (dg.SelectedItem != null)
			{
				db.Artikals.Remove(dg.SelectedItem as Artikal);
				db.SaveChanges();
				dg.ItemsSource = db.Artikals.ToList();
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

		private void UnosStavke(object sender, RoutedEventArgs e)
		{
			if (UnosRac.BindingGroup.CommitEdit())
			{
				var art = db.Artikals.Find(TrenutnaSifra);
				if (art != null)
				{
					var ak = new ArtKol(art, TrenutnaKolicina);
					TrenutniRacun.Artikli.Add(ak);
					dgStavke.ItemsSource = null;
					dgStavke.ItemsSource = TrenutniRacun.Artikli;
					TrenutnaKolicina = 0;
					TrenutnaSifra = 0;
				} else
				{
					MessageBox.Show("Artikal ne postoji!");
				}
			}else
			{
				MessageBox.Show("Proverite podatke!");
			}

		}

        private void Izdavanje(object sender, RoutedEventArgs e)
        {
			TrenutniRacun.VremeIzdavanja = DateTime.Now;
			TrenutniRacun.Artikli.ForEach(ak => db.ArtKols.Add(ak));
			db.Racuns.Add(TrenutniRacun);
			db.SaveChanges();
			TrenutniRacun = new Racun();
			dgStavke.ItemsSource = null;
        }
    }
}
