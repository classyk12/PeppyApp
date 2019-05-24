using Peppy2.APIServices;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Extended;

namespace Peppy2.Models
{
   public class MainViewModel : INotifyPropertyChanged
    {
        private bool _loading;
        private bool _isbusy;
        //specifies how many data will be shown at a go which is set to 10
        private const int pageSize = 10;
        readonly FeedbackService service = new FeedbackService();

       

        public InfiniteScrollCollection<FeedbackModel> Items { get; set; }


        public bool IsBusy
        {
            get => _isbusy;
            set
            {
                _isbusy = value;
                OnPropertyChanged();
            }
        }

        public bool Load
        {
            get => _loading;
            set
            {
                _loading = value;
                OnPropertyChanged();
            }
        }

        public  MainViewModel()
        {


            try
            {
                Items = new InfiniteScrollCollection<FeedbackModel>
                {

                    OnLoadMore = async () =>
                    {
                        IsBusy = true;
                        var page = Items.Count / pageSize;
                        var items = await service.AllFeedbacks(page, pageSize);
                        IsBusy = false;

                        //returns items to be added to the collection to be shown on the listview
                        return items;
                    },
                    OnCanLoadMore = () =>
                    {
                        return Items.Count == Items.Count;
                    }
                };
                DownloadDataAsync();

            }
            catch (Exception)
            {

                 Application.Current.MainPage.DisplayAlert("Oops!", "Something  went wrong but we got you", "ok");
            }
            
        }



        private async void DownloadDataAsync()
        {
            if(CrossConnectivity.Current.IsConnected)
            {
                try
                {
                    Load = true;
                    //request for the data to be downloaded using the parameters.
                    //i.e 10 items at a go starting at index 0
                    var items = await service.AllFeedbacks(0, pageSize);
                    //casts the downloaded data to observableCollection of the type of the Dataservice i.e string
                    Items.AddRange(items);
                    Load = false;
                }
                catch (Exception)
                {

                    await Application.Current.MainPage.DisplayAlert("Error..", "We could not get feedbacks at the moment, try again later", "ok");
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Connection Timeout", "Check your internet connection and try again", "ok");
            }
                
            
           
         

        }

        private void OnPropertyChanged([CallerMemberName] string propertyname = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
