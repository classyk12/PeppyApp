using Acr.UserDialogs;
using Peppy2.APIServices;
using Peppy2.Helpers;
using Peppy2.Models;
using Plugin.Connectivity;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;

namespace Peppy2.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Both : Xamarin.Forms.TabbedPage
    {
        public UserService services = new UserService();
        public static MediaFile file;
        public Both ()
        {
            InitializeComponent();
            On<Xamarin.Forms.PlatformConfiguration.Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);
            On<Xamarin.Forms.PlatformConfiguration.Android>().SetBarSelectedItemColor(Color.SteelBlue);
            On<Xamarin.Forms.PlatformConfiguration.Android>().SetBarItemColor(Color.Black);

            emailEntry.Completed += (s, e) => usernameEntry.Focus();
            usernameEntry.Completed += (s, e) => passwordEntry.Focus();
            passwordEntry.Completed += (s, e) => passwordEntry2.Focus();
            passwordEntry2.Completed += (s, e) => phonenumberEntry.Focus();
            phonenumberEntry.Completed += (s, e) => SignUpClickEvent(s, e);

            UsernameLoginEntry.Completed += (s, e) => PasswordLoginEntry.Focus();
            PasswordLoginEntry.Completed += (s, e) => LoginClickEvent(s, e);
        }




        //handles user profile picture upload
        private async void ProfileImageTapped (object sender, EventArgs e)
        {

            try
            {
                await CrossMedia.Current.Initialize();//initializes the media plugin
                var dialog = await DisplayActionSheet("Additional Prompt..", "", "", "Take Photo", "Choose from Gallery");
                if (dialog == "Take Photo")
                {

                    if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                    {
                        await DisplayAlert("No Camera", ":( No camera available.", "OK");
                        return;
                    }

                    file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                    {
                        PhotoSize = PhotoSize.Medium,
                        CompressionQuality = 100,
                        Directory = "Sample",
                        Name = "test.jpg",
                        AllowCropping = true

                    });
                }

                else
                {
                    if (!CrossMedia.Current.IsPickPhotoSupported)
                    {
                        await DisplayAlert("No Storage", ":( No storage available.", "OK");
                        return;
                    }

                    file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
                    {
                        PhotoSize = PhotoSize.Medium,
                        CompressionQuality = 100
                    });
                }
                //checks if the returned file is null or not
                if (file == null)
                    return;

                await DisplayAlert("File Location", file.Path, "OK");
                //sets the returned image to the source of the image control
                profilepic.Source = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    profilepic.Aspect = Aspect.AspectFit;
                    return stream;

                });
            }
            catch (Exception)
            {


               await DisplayAlert("Ooops!","Kindly choose some other photo", "ok");
            }
        }

        //handles user Registration
        private async void SignUpClickEvent(object sender, EventArgs e)
        {
            ScaleButton2();
            if (CrossConnectivity.Current.IsConnected)
            {
                using (UserDialogs.Instance.Loading("Please Wait..", null, null, true, MaskType.Gradient))
                {
                    try
                    {
                        var imagearray = Filehelper.Readfully(file.GetStream());

                        var data = new RegisterModel
                        {
                            Id = Guid.NewGuid(),
                            Email = emailEntry.Text,
                            Alias = usernameEntry.Text,
                            Password = passwordEntry.Text,
                            ConfirmPassword = passwordEntry2.Text,
                            PhoneNumber = phonenumberEntry.Text,
                            ImageArray = imagearray,
                            DateJoined = DateTime.Now
                        };
                        //checks for empty entry fields
                        if (string.IsNullOrEmpty(emailEntry.Text) || string.IsNullOrWhiteSpace(usernameEntry.Text) || string.IsNullOrWhiteSpace(passwordEntry.Text)
                            || string.IsNullOrWhiteSpace(passwordEntry2.Text) || string.IsNullOrWhiteSpace(phonenumberEntry.Text))
                        { await DisplayAlert("Error..!!", "Fields marked * cannot be empty", "ok"); }

                        //checks for password fields comparison
                        if (passwordEntry.Text != passwordEntry2.Text)
                        { await DisplayAlert("Error..!!", "Passwords does not match", "ok"); }

                        if (usernameEntry.Text.Length < 2)
                        {
                            errorlbl4.IsVisible = true;
                            errorlbl4.Text = "Username cannot be less 2 charaters";
                            UsernameSignupShake();
                            return;
                        }

                        if (phonenumberEntry.Text.Length != 11)
                        {
                            errorlbl7.IsVisible = true;
                            errorlbl7.Text = "Phone number cannot be less than 11 characters";
                            PhoneSignUpShake();
                            return;
                        }


                        var response = await services.RegisterUser(data);
                        if (!response)
                        {
                            await DisplayAlert("Something went wrong", "Check fields for error details and modify", "ok");
                            errorlbl3.IsVisible = true;
                            errorlbl3.Text = "Enter a valid email; xx@xx.com is an example of a valid email";
                            EmailSignupShake();
                            errorlbl4.IsVisible = true;
                            errorlbl4.Text = "username must be at least 2 characters long";
                            UsernameSignupShake();
                            errorlbl5.IsVisible = true;
                            errorlbl5.Text = "Password must be at least 6 characters and must contain special characters @!?+*$%^ and an Uppercase(A-Z)";
                            PasswordSignUpShake();
                            //errorlbl6.IsVisible = true;
                            errorlbl7.IsVisible = true;
                            errorlbl7.Text = "Enter a valid phone number e.g 12345678890";
                            PhoneSignUpShake();
                        }
                        else
                        {
                            await DisplayAlert("Successful...", "Record saved successfully", "ok");
                            await Navigation.PushAsync(new Both());
                        }
                    }
                    catch (Exception)
                    {

                        await DisplayAlert("Error..", "Make sure Image Field or Text fields are not empty", "ok");
                    }
                }
            }
            else
            {
                await DisplayAlert("Connection Timeout", "Check your Internet Connection and Try again", "ok");
            }
        }
       
              
        


        private async void LoginClickEvent(object sender, EventArgs e)
        {
             ScaleButton();
            if (CrossConnectivity.Current.IsConnected)
            {
                using (UserDialogs.Instance.Loading("Please Wait..", null, null, true, MaskType.Gradient))
                {

                    try
                    {
                        if (string.IsNullOrWhiteSpace(UsernameLoginEntry.Text) || string.IsNullOrWhiteSpace(PasswordLoginEntry.Text))
                        {
                            UsernameLoginShake();
                            PasswordLoginShake();
                            errorlbl.Text =  "username field cannot be empty";                           
                            errorlbl2.Text = "password field cannot be empty";
                            return;
                        }
                        var response = await services.UserLogin(UsernameLoginEntry.Text, PasswordLoginEntry.Text);

                        if (!response)
                        {
                            await DisplayAlert("Login Error...", "Incorrect username or password", "Try again");
                        }
                        else
                        {
                            await DisplayAlert("Successful Login", "Lets take you to your beautiful dashboard", "ok");
                            
                            App.Current.MainPage = new NavigationPage(new PeppyMasterDetail());
                        }
                    }
                    catch (Exception)
                    {
                        await DisplayAlert("Error..", "Something went wrong, try again", "ok");
                    }
                }
            }
            else
            {
                await DisplayAlert("Connection Timeout", "Check your Internet Connection and Try again", "ok");
            }

        }

        private void usernameEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            errorlbl4.IsVisible = false;
        }

        private void UsernameLoginEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            errorlbl.IsVisible = false;
        }

        private void PasswordLoginEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            errorlbl2.IsVisible = false;
        }

        private void emailEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            errorlbl3.IsVisible = false;
        }

        private void passwordEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            errorlbl5.IsVisible = false;
        }

        private void passwordEntry2_TextChanged(object sender, TextChangedEventArgs e)
        {
            errorlbl6.IsVisible = true;
        }

        private void phonenumberEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            errorlbl7.IsVisible = false;
        }

        public async void UsernameLoginShake()
        {
            uint timeout = 50;
            await UsernameLoginEntry.TranslateTo(-15, 0, timeout);

            await UsernameLoginEntry.TranslateTo(15, 0, timeout);

            await UsernameLoginEntry.TranslateTo(-10, 0, timeout);

            await UsernameLoginEntry.TranslateTo(10, 0, timeout);

            await UsernameLoginEntry.TranslateTo(-5, 0, timeout);

            await UsernameLoginEntry.TranslateTo(5, 0, timeout);

            UsernameLoginEntry.TranslationX = 0;

        }

        public async void PasswordLoginShake()
        {
            uint timeout = 50;
            await PasswordLoginEntry.TranslateTo(-15, 0, timeout);

            await PasswordLoginEntry.TranslateTo(15, 0, timeout);

            await PasswordLoginEntry.TranslateTo(-10, 0, timeout);

            await PasswordLoginEntry.TranslateTo(10, 0, timeout);

            await PasswordLoginEntry.TranslateTo(-5, 0, timeout);

            await PasswordLoginEntry.TranslateTo(5, 0, timeout);

            PasswordLoginEntry.TranslationX = 0;

        }

        public async void EmailSignupShake()
        {
            uint timeout = 50;
            await emailEntry.TranslateTo(-15, 0, timeout);

            await emailEntry.TranslateTo(15, 0, timeout);

            await emailEntry.TranslateTo(-10, 0, timeout);

            await emailEntry.TranslateTo(10, 0, timeout);

            await emailEntry.TranslateTo(-5, 0, timeout);

            await emailEntry.TranslateTo(5, 0, timeout);

                  emailEntry.TranslationX = 0;

        }

        public async void UsernameSignupShake()
        {
            uint timeout = 50;
            await usernameEntry.TranslateTo(-15, 0, timeout);

            await usernameEntry.TranslateTo(15, 0, timeout);

            await usernameEntry.TranslateTo(-10, 0, timeout);

            await usernameEntry.TranslateTo(10, 0, timeout);

            await usernameEntry.TranslateTo(-5, 0, timeout);

            await usernameEntry.TranslateTo(5, 0, timeout);

            usernameEntry.TranslationX = 0;

        }

        public async void PasswordSignUpShake()
        {
            uint timeout = 50;
            await passwordEntry.TranslateTo(-15, 0, timeout);

            await passwordEntry.TranslateTo(15, 0, timeout);

            await passwordEntry.TranslateTo(-10, 0, timeout);

            await passwordEntry.TranslateTo(10, 0, timeout);

            await passwordEntry.TranslateTo(-5, 0, timeout);

            await passwordEntry.TranslateTo(5, 0, timeout);

            passwordEntry
.TranslationX = 0;

        }

        public async void PhoneSignUpShake()
        {
            uint timeout = 50;
            await phonenumberEntry.TranslateTo(-15, 0, timeout);

            await phonenumberEntry.TranslateTo(15, 0, timeout);

            await phonenumberEntry.TranslateTo(-10, 0, timeout);

            await phonenumberEntry.TranslateTo(10, 0, timeout);

            await phonenumberEntry.TranslateTo(-5, 0, timeout);

            await phonenumberEntry.TranslateTo(5, 0, timeout);

            phonenumberEntry.TranslationX = 0;

        }

        public async void ScaleButton()
        {
            await signInBtn.ScaleTo(0.95, 50, Easing.BounceOut);
            await signInBtn.ScaleTo(1, 50, Easing.BounceIn);
        }

        public async void ScaleButton2()
        {
            await SignupBtn.ScaleTo(0.95, 50, Easing.BounceOut);
            await SignupBtn.ScaleTo(1, 50, Easing.BounceIn);
        }

    }
}