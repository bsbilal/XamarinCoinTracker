using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System.Collections.Generic;
using System.Net;
using System.Web;
using Newtonsoft.Json.Linq;
using XamarinCoinTracker.Resources;

namespace XamarinCoinTracker
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
       

        dynamic myStuff = JObject.Parse(CoinMarketCapAPI.makeAPICall());

        List<CryptoCoin> lstMyCoins = new List<CryptoCoin>();
        EditText edtCoinName;
        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            var lstData = FindViewById<ListView>(Resource.Id.lstCoins);
            edtCoinName = FindViewById<EditText>(Resource.Id.edtText);
       


            try
            {
                myStuff = JObject.Parse(CoinMarketCapAPI.makeAPICall());
                lstMyCoins = ListAll();
                var adapter = new CustomAdapter(this, lstMyCoins);
                lstData.Adapter = adapter;
            }
            catch (WebException e)
            {
                Toast.MakeText(this, "API Error - "+e.Message, ToastLength.Short).Show();
            }



          
            

            edtCoinName.TextChanged += delegate {
                if (edtCoinName.Length()>0) 
                {
                    lstMyCoins = ListByText(edtCoinName.Text.ToString());
                }
                else
                {
                    lstMyCoins = ListAll();
                }
                var adapter = new CustomAdapter(this, lstMyCoins);
                lstData.Adapter = adapter;
            };

            lstData.ItemClick += LstData_ItemClick;
        }


        private void LstData_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            Toast.MakeText(this, lstMyCoins[e.Position].Name +" - "+ lstMyCoins[e.Position].Price.ToString(), ToastLength.Short).Show();
           
        }
    
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

       List<CryptoCoin> ListAll()
        {
            List<CryptoCoin> lstCoins = new List<CryptoCoin>();
         

            foreach (var item in myStuff.data)
                {
                try { 
                    CryptoCoin newCoin = new CryptoCoin();
                    newCoin.Name = item.name;
                    newCoin.Symbol = item.symbol;
                    newCoin.Price = item.quote.USD.price;
                    newCoin.UpdateDate = item.quote.USD.last_updated + " UTC";

                    lstCoins.Add(newCoin);
                }
                catch
                {
                    continue;
                }
                }
            return lstCoins;
        }

        List<CryptoCoin> ListByText(string query)
        {
            List<CryptoCoin> lstCoins = new List<CryptoCoin>();
            string Name;
            string Sym;
             foreach (var item in myStuff.data)
                {
                Name = item.name;
                Sym = item.symbol;
                if (Sym.ToLower().Contains(query.ToLower()) || Name.ToLower().Contains(query.ToLower()))
                {
                    CryptoCoin newCoin = new CryptoCoin();
                    newCoin.Name = item.name;
                    newCoin.Symbol = item.symbol;
                    newCoin.Price = item.quote.USD.price;
                    newCoin.UpdateDate = item.quote.USD.last_updated+" UTC";

                    lstCoins.Add(newCoin);
                }
                else
                    continue;
            }
           return lstCoins;

        }
    }
}