using System;
using Android.App;
using Android.Content;
using Android.Gms.Common;
using Android.Gms.Common.Apis;
using Android.Gms.Plus;
using Android.OS;
using Android.Support.V7.App;
using Android.Util;
using Android.Views;
using Android.Widget;
using DonorAppMob.Views;
using DonorAppMob.Configure;
using Object = Java.Lang.Object;
#pragma warning disable 618

namespace DonorAppMob
{
	[Activity (MainLauncher = true)]
	public class MainActivity : AppCompatActivity, View.IOnClickListener, 
        GoogleApiClient.IConnectionCallbacks, GoogleApiClient.IOnConnectionFailedListener
	{
		const string Tag = "MainActivity";

		const int RcSignIn = 9001;

		const string KeyIsResolving = "is_resolving";
		const string KeyShouldResolve = "should_resolve";

		GoogleApiClient _mGoogleApiClient;

		TextView _mStatus;

		bool _mIsResolving;

		bool _mShouldResolve;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
			SetContentView (Resource.Layout.activity_main);

			if (savedInstanceState != null) {
				_mIsResolving = savedInstanceState.GetBoolean (KeyIsResolving);
				_mShouldResolve = savedInstanceState.GetBoolean (KeyShouldResolve);
			}

			FindViewById (Resource.Id.sign_in_button).SetOnClickListener (this);
			FindViewById (Resource.Id.sign_out_button).SetOnClickListener (this);
			FindViewById (Resource.Id.disconnect_button).SetOnClickListener (this);

			FindViewById<SignInButton> (Resource.Id.sign_in_button).SetSize (SignInButton.SizeWide);
			FindViewById (Resource.Id.sign_in_button).Enabled = false;

			_mStatus = FindViewById<TextView> (Resource.Id.status);

			_mGoogleApiClient = new GoogleApiClient.Builder (this)
				.AddConnectionCallbacks (this)
				.AddOnConnectionFailedListener (this)
				.AddApi (PlusClass.API)
				.AddScope (new Scope (Scopes.Profile))
				.Build ();
		}

		void UpdateUi (bool isSignedIn)
		{
			if (isSignedIn) {
				var person = PlusClass.PeopleApi.GetCurrentPerson (_mGoogleApiClient);
				var name = string.Empty;
				if (person != null) { 
					name = person.DisplayName;
                    ConfigUser.LoadUserData(person);

                    var intent = new Intent(this, typeof (HomeActivity));
                    StartActivity(intent);
				}
                _mStatus.Text = string.Format(GetString (Resource.String.signed_in_fmt), name);

				FindViewById (Resource.Id.sign_in_button).Visibility = ViewStates.Gone;
				FindViewById (Resource.Id.sign_out_and_disconnect).Visibility = ViewStates.Visible;
			} else {
				_mStatus.Text = GetString (Resource.String.signed_out);

				FindViewById (Resource.Id.sign_in_button).Enabled = true;
				FindViewById (Resource.Id.sign_in_button).Visibility = ViewStates.Visible;
				FindViewById (Resource.Id.sign_out_and_disconnect).Visibility = ViewStates.Gone;
			}
		}

		protected override void OnStart ()
		{
			base.OnStart ();
			_mGoogleApiClient.Connect ();
		}

		protected override void OnStop ()
		{
			base.OnStop ();
			_mGoogleApiClient.Disconnect ();
		}

		protected override void OnSaveInstanceState (Bundle outState)
		{
			base.OnSaveInstanceState (outState);
			outState.PutBoolean (KeyIsResolving, _mIsResolving);
			outState.PutBoolean (KeyShouldResolve, _mIsResolving);
		}

		protected override void OnActivityResult (int requestCode, Result resultCode, Intent data)
		{
			base.OnActivityResult (requestCode, resultCode, data);
			Log.Debug (Tag, "onActivityResult:" + requestCode + ":" + resultCode + ":" + data);

			if (requestCode == RcSignIn) {
				if (resultCode != Result.Ok) {
					_mShouldResolve = false;
				}

				_mIsResolving = false;
				_mGoogleApiClient.Connect ();
			}
		}

		public void OnConnected (Bundle connectionHint)
		{
			Log.Debug (Tag, "onConnected:" + connectionHint);

			UpdateUi (true);
		}

		public void OnConnectionSuspended (int cause)
		{
			Log.Warn (Tag, "onConnectionSuspended:" + cause);
		}

		public void OnConnectionFailed (ConnectionResult result)
		{
			Log.Debug (Tag, "onConnectionFailed:" + result);

			if (!_mIsResolving && _mShouldResolve) {
				if (result.HasResolution) {
					try {
						result.StartResolutionForResult (this, RcSignIn);
						_mIsResolving = true;
					} catch (IntentSender.SendIntentException e) {
						Log.Error (Tag, "Could not resolve ConnectionResult.", e);
						_mIsResolving = false;
						_mGoogleApiClient.Connect ();
					}
				} else {
					ShowErrorDialog (result);
				}
			} else {
				UpdateUi (false);
			}
		}

		class DialogInterfaceOnCancelListener : Object, IDialogInterfaceOnCancelListener
		{
			public Action<IDialogInterface> OnCancelImpl { private get; set; }

			public void OnCancel (IDialogInterface dialog)
			{
				OnCancelImpl (dialog);
			}
		}

		void ShowErrorDialog (ConnectionResult connectionResult)
		{
			var errorCode = connectionResult.ErrorCode;

			if (GooglePlayServicesUtil.IsUserRecoverableError (errorCode)) {
			    var listener = new DialogInterfaceOnCancelListener{
			        OnCancelImpl = dialog => {
			            _mShouldResolve = false;
			            UpdateUi(false);
			        }
			    };
			    GooglePlayServicesUtil.GetErrorDialog (errorCode, this, RcSignIn, listener).Show ();
			} else {
				var errorstring = string.Format(GetString (Resource.String.play_services_error_fmt), errorCode);
				Toast.MakeText (this, errorstring, ToastLength.Short).Show ();

				_mShouldResolve = false;
				UpdateUi (false);
			}
		}

		public async void OnClick (View v)
		{
			switch (v.Id) {
			case Resource.Id.sign_in_button:
				_mStatus.Text = GetString (Resource.String.signing_in);
				_mShouldResolve = true;
				_mGoogleApiClient.Connect ();
				break;
			case Resource.Id.sign_out_button:
				if (_mGoogleApiClient.IsConnected) {
					PlusClass.AccountApi.ClearDefaultAccount (_mGoogleApiClient);
					_mGoogleApiClient.Disconnect ();
				}
				UpdateUi (false);
				break;
			case Resource.Id.disconnect_button:
				if (_mGoogleApiClient.IsConnected) {
					PlusClass.AccountApi.ClearDefaultAccount (_mGoogleApiClient);
					await PlusClass.AccountApi.RevokeAccessAndDisconnect (_mGoogleApiClient);
					_mGoogleApiClient.Disconnect ();
				}
				UpdateUi (false);
				break;
			}
		}
	}

}


