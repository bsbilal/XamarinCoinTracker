using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace XamarinCoinTracker
{
    public class CryptoCoin
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public string UpdateDate { get; set; }
        public float Price { get; set; }

       
    }
}