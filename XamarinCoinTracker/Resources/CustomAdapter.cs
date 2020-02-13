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

namespace XamarinCoinTracker.Resources
{
    public class ViewHolder:Java.Lang.Object
    {
        public TextView txtCoinName { get; set; }
        public TextView txtCoinSymbol { get; set; }
        public TextView txtUpdateDate { get; set; }
        public TextView txtCoinPrice { get; set; }


    }
    public class CustomAdapter : BaseAdapter
    {
        private Activity activity;
        private List<CryptoCoin> coins;

 

        public CustomAdapter(Activity activity,List<CryptoCoin> coins)
        {
            this.activity = activity;
            this.coins = coins;
        }
        public override int Count
        {
            get
            {
               return coins.Count;
            }
        }

        public override Java.Lang.Object GetItem(int position)
        {
               return null;
        }

        public override long GetItemId(int position)
        {
            return coins[position].id;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView ?? activity.
                 LayoutInflater.Inflate(Resource.Layout.ListViewDataTemplate, parent, false);


            var txtName = view.FindViewById<TextView>(Resource.Id.txtCoinName);
            var txtSym = view.FindViewById<TextView>(Resource.Id.txtCoinSymbol);
            var txtPrice = view.FindViewById<TextView>(Resource.Id.txtPrice);
            var txtDate = view.FindViewById<TextView>(Resource.Id.txtUpdateDate);


            txtName.Text = coins[position].Name;
            txtSym.Text = coins[position].Symbol;
            txtPrice.Text ="$"+ coins[position].Price.ToString();
            txtDate.Text = coins[position].UpdateDate;

            return view;
        }
    }
}