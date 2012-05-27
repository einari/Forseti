using System;
using System.Collections.Generic;
using System.Linq;
using MonoMac.Foundation;
using MonoMac.AppKit;
using MonoMac.Growl;

namespace Forseti.OSX.Client
{
	public partial class MainWindowController : MonoMac.AppKit.NSWindowController
	{
		#region Constructors
		
		// Called when created from unmanaged code
		public MainWindowController (IntPtr handle) : base (handle)
		{
			Initialize ();
		}
		
		// Called when created directly from a XIB file
		[Export ("initWithCoder:")]
		public MainWindowController (NSCoder coder) : base (coder)
		{
			Initialize ();
		}
		
		// Call to load from the XIB/NIB file
		public MainWindowController () : base ("MainWindow")
		{
			Initialize ();
		}
		
		// Shared initialization code
		void Initialize ()
		{
		}
		
		#endregion
		
		public override void AwakeFromNib () 
		{
			GrowlApplicationBridge.WeakDelegate = this;
		}
		
		partial void SayHello (NSObject sender)
		{
			GrowlApplicationBridge.Notify (
				"Test result", 
			    "Everything is peachy", 
				"TestRunComplete", 
				null, 
				0, 
				false, 
				null);
			
		}
		
		[Export("registrationDictionaryForGrowl")]
		NSDictionary RegistrationDictionaryForGrowl () {
			var regPath = NSBundle.MainBundle.PathForResource ("GrowlRegistrationTicket", "plist");
			var reg = NSDictionary.FromFile (regPath);
			return reg;
		}
		
		
		//strongly typed window accessor
		public new MainWindow Window {
			get {
				return (MainWindow)base.Window;
			}
		}
	}
}

