﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD2
{
	public class Artikal : INotifyPropertyChanged
	{
		public int Sifra { get; set; }

		private string _naziv;
		public string Naziv 
		{ 
			get => _naziv; 
			set
			{
				_naziv = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Naziv"));
			}
		}

		private decimal _ucena;
		public decimal Ucena 
		{ 
			get => _ucena; 
			set
			{
				_ucena = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Ucena"));
			}
		}

		private int _marzaProc;
		public int MarzaProc 
		{ 
			get => _marzaProc; 
			set
			{
				_marzaProc = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MarzaProc"));
			}
		}

		public decimal IzlaznaCena { get => Ucena * (1+(decimal)MarzaProc/100);}

		public event PropertyChangedEventHandler PropertyChanged;
	}
}
