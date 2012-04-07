using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using Forseti.Files;

namespace Forseti.OSX
{
	// Based on : https://gist.github.com/2164127
	public class FileSystemWatcher : IFileSystemWatcher
	{
        List<FileChanged> _subscribers = new List<FileChanged>();
		
		FSEventStreamCallback _callback;
		
		Thread _runLoop;
		IntPtr _stream;
		
		public FileSystemWatcher ()
		{
			_callback = Callback;
			var currentDirectory = System.IO.Directory.GetCurrentDirectory();			
			IntPtr path = CFStringCreateWithCString (IntPtr.Zero, currentDirectory, 0);
			IntPtr paths = CFArrayCreate (IntPtr.Zero, new IntPtr [1] { path }, 1, IntPtr.Zero);

			_stream = FSEventStreamCreate (IntPtr.Zero, _callback, IntPtr.Zero, paths, FSEventStreamEventIdSinceNow, 0, FSEventStreamCreateFlags.WatchRoot | FSEventStreamCreateFlags.FileEvents);

			CFRelease (paths);
			CFRelease (path);

			_runLoop = new Thread (RunLoop);
			_runLoop.Name = "FSEventStream";
			_runLoop.Start ();
		}
		

		public void SubscribeToChanges (FileChanged changed)
		{
			_subscribers.Add(changed);
		}
		
		void NotifySubscribers(FileChange fileChange, IFile file)
		{
            foreach (var subscriber in _subscribers)
                subscriber(fileChange, file);
		}
		
		
		void RunLoop()
		{
			FSEventStreamScheduleWithRunLoop (_stream, CFRunLoopGetCurrent (), kCFRunLoopDefaultMode);
			FSEventStreamStart (_stream);
			CFRunLoopRun ();
		}
		
		void Callback (IntPtr streamRef, IntPtr clientCallBackInfo, int numEvents, IntPtr eventPaths, IntPtr eventFlags, IntPtr eventIds)
		{
			var paths = new string[numEvents];
			var actions = new FSEventStreamEventFlagItem[numEvents];
			var ids = new UInt64[numEvents];
			unsafe {
				var eventPathsPointer = (char**)eventPaths.ToPointer ();
				var eventFlagsPointer = (uint*)eventFlags.ToPointer ();
				var eventIdsPointer = (ulong*)eventIds.ToPointer ();
				for (var i = 0; i < numEvents; i++) {
					paths [i] = Marshal.PtrToStringAuto (new IntPtr (eventPathsPointer [i]));
					actions[i] = (FSEventStreamEventFlagItem)eventFlagsPointer[i];
					ids [i] = eventIdsPointer [i];
				}
			}
						
			if( numEvents == 2 ) 
			{
				var renameCount = 0;
				for (var i = 0; i < numEvents; i++) {
					if( (actions[i] & FSEventStreamEventFlagItem.Renamed) == FSEventStreamEventFlagItem.Renamed )
						renameCount++;
				}
			
				if( renameCount == 2 ) 
				{
					var before = (File)paths[0];
					var after = (File)paths[1];
					NotifySubscribers(FileChange.Deleted, before);
					NotifySubscribers(FileChange.Added, after);
					return;
				}
			}
			
			
			for (var i = 0; i < numEvents; i++) 
			{
				var action = actions[i];
				var path = paths[i];
				var file = (File)path;
				
				var fileChange = TranslateAction(file, action);
				NotifySubscribers(fileChange, file);
			}
		}
		
		
		
		FileChange TranslateAction(IFile file, FSEventStreamEventFlagItem action)
		{
			if( !System.IO.File.Exists (file.FullPath) ||
				(action & FSEventStreamEventFlagItem.Removed) == FSEventStreamEventFlagItem.Removed )
				return FileChange.Deleted;

			if( (action & FSEventStreamEventFlagItem.Renamed) == FSEventStreamEventFlagItem.Renamed )
				return FileChange.Renamed;
			
			if( (action & FSEventStreamEventFlagItem.Created) == FSEventStreamEventFlagItem.Created )
				return FileChange.Added;
			
			if( (action & FSEventStreamEventFlagItem.Modified ) == FSEventStreamEventFlagItem.Modified )
				return FileChange.Modified;
			
			
			return FileChange.Unknown;
		}
		
		
		
		[DllImport ("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation")]
		extern static IntPtr CFStringCreateWithCString (IntPtr allocator, string value, int encoding);
				
		[DllImport ("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation")]
		extern static IntPtr CFArrayCreate (IntPtr allocator, IntPtr [] values, int numValues, IntPtr callBacks);
				
		[DllImport ("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation")]
		extern static IntPtr CFArrayGetValueAtIndex (IntPtr array, int index);
				
		[DllImport ("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation")]
		extern static void CFRelease (IntPtr cf);
				
		[DllImport ("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation")]
		extern static IntPtr CFRunLoopGetCurrent ();
				
		[DllImport ("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation")]
		extern static IntPtr CFRunLoopGetMain ();
				
		[DllImport ("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation")]
		extern static void CFRunLoopRun ();
				
		[DllImport ("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation")]
		extern static int CFRunLoopRunInMode (IntPtr mode, double seconds, int returnAfterSourceHandled);

		delegate void FSEventStreamCallback (IntPtr streamRef,IntPtr clientCallBackInfo,int numEvents,IntPtr eventPaths,IntPtr eventFlags,IntPtr eventIds);
				
		[DllImport ("/System/Library/Frameworks/CoreServices.framework/CoreServices")]
		extern static IntPtr FSEventStreamCreate (IntPtr allocator, FSEventStreamCallback callback, IntPtr context, IntPtr pathsToWatch, ulong sinceWhen, double latency, FSEventStreamCreateFlags flags);
				
		[DllImport ("/System/Library/Frameworks/CoreServices.framework/CoreServices")]
		extern static int FSEventStreamStart (IntPtr streamRef);
				
		[DllImport ("/System/Library/Frameworks/CoreServices.framework/CoreServices")]
		extern static void FSEventStreamStop (IntPtr streamRef);
				
		[DllImport ("/System/Library/Frameworks/CoreServices.framework/CoreServices")]
		extern static void FSEventStreamRelease (IntPtr streamRef);
				
		[DllImport ("/System/Library/Frameworks/CoreServices.framework/CoreServices")]
		extern static void FSEventStreamScheduleWithRunLoop (IntPtr streamRef, IntPtr runLoop, IntPtr runLoopMode);
				
		[DllImport ("/System/Library/Frameworks/CoreServices.framework/CoreServices")]
		extern static void FSEventStreamUnscheduleFromRunLoop (IntPtr streamRef, IntPtr runLoop, IntPtr runLoopMode);

		const ulong FSEventStreamEventIdSinceNow = ulong.MaxValue;
		private static IntPtr kCFRunLoopDefaultMode = CFStringCreateWithCString (IntPtr.Zero, "kCFRunLoopDefaultMode", 0);
				
		[Flags]
		enum FSEventStreamCreateFlags : uint
		{
			None = 0x00000000,
			UseCFTypes = 0x00000001,
			NoDefer = 0x00000002,
			WatchRoot = 0x00000004,
			IgnoreSelf = 0x00000008,
			FileEvents = 0x00000010
		}
				
		[Flags]
		enum FSEventStreamEventFlag : uint
		{
			None = 0x00000000,
			MustScanSubDirs = 0x00000001,
			UserDropped = 0x00000002,
			KernelDropped = 0x00000004,
			EventIdsWrapped = 0x00000008,
			HistoryDone = 0x00000010,
			RootChanged = 0x00000020,
			FlagMount  = 0x00000040,
			Unmount = 0x00000080
		}
				
		[Flags]
		enum FSEventStreamEventFlagItem : uint
		{
			Created       = 0x00000100,
			Removed       = 0x00000200,
			InodeMetaMod  = 0x00000400,
			Renamed       = 0x00000800,
			Modified      = 0x00001000,
			FinderInfoMod = 0x00002000,
			ChangeOwner   = 0x00004000,
			XattrMod      = 0x00008000,
			IsFile        = 0x00010000,
			IsDir         = 0x00020000,
			IsSymlink     = 0x00040000
		}
		
	}
}

