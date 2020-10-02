using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace DisplayButtons.Bibliotecas.Helpers
{
    public enum BrowserEmulationVersion
    {
        Default = 0,
        Version7 = 7000,
        Version8 = 8000,
        Version8Standards = 8888,
        Version9 = 9000,
        Version9Standards = 9999,
        Version10 = 10000,
        Version10Standards = 10001,
        Version11 = 11000,
        Version11Edge = 11001
    }
    public static class WBEmulator
    {
        private const string InternetExplorerRootKey = @"Software\Microsoft\Internet Explorer";

        public static int GetInternetExplorerMajorVersion()
        {
            int result;

            result = 0;

            try
            {
                RegistryKey key;

                key = Registry.LocalMachine.OpenSubKey(InternetExplorerRootKey);

                if (key != null)
                {
                    object value;

                    value = key.GetValue("svcVersion", null) ?? key.GetValue("Version", null);

                    if (value != null)
                    {
                        string version;
                        int separator;

                        version = value.ToString();
                        separator = version.IndexOf('.');
                        if (separator != -1)
                        {
                            int.TryParse(version.Substring(0, separator), out result);
                        }
                    }
                }
            }
            catch (SecurityException)
            {
                // The user does not have the permissions required to read from the registry key.
            }
            catch (UnauthorizedAccessException)
            {
                // The user does not have the necessary registry rights.
            }

            return result;
        }
        private const string BrowserEmulationKey = InternetExplorerRootKey + @"\Main\FeatureControl\FEATURE_BROWSER_EMULATION";

        public static BrowserEmulationVersion GetBrowserEmulationVersion()
        {
            BrowserEmulationVersion result;

            result = BrowserEmulationVersion.Default;

            try
            {
                RegistryKey key;

                key = Registry.CurrentUser.OpenSubKey(BrowserEmulationKey, true);
                if (key != null)
                {
                    string programName;
                    object value;

                    programName = Path.GetFileName(Environment.GetCommandLineArgs()[0]);
                    value = key.GetValue(programName, null);

                    if (value != null)
                    {
                        result = (BrowserEmulationVersion)Convert.ToInt32(value);
                    }
                }
            }
            catch (SecurityException)
            {
                // The user does not have the permissions required to read from the registry key.
            }
            catch (UnauthorizedAccessException)
            {
                // The user does not have the necessary registry rights.
            }

            return result;
        }

        public static bool SetBrowserEmulationVersion(BrowserEmulationVersion browserEmulationVersion)
        {
            bool result;

            result = false;

            try
            {
                RegistryKey key;

                key = Registry.CurrentUser.OpenSubKey(BrowserEmulationKey, true);

                if (key != null)
                {
                    string programName;

                    programName = Path.GetFileName(Environment.GetCommandLineArgs()[0]);

                    if (browserEmulationVersion != BrowserEmulationVersion.Default)
                    {
                        // if it's a valid value, update or create the value
                        key.SetValue(programName, (int)browserEmulationVersion, RegistryValueKind.DWord);
                    }
                    else
                    {
                        // otherwise, remove the existing value
                        key.DeleteValue(programName, false);
                    }

                    result = true;
                }
            }
            catch (SecurityException)
            {
                // The user does not have the permissions required to read from the registry key.
            }
            catch (UnauthorizedAccessException)
            {
                // The user does not have the necessary registry rights.
            }

            return result;
        }

        public static bool SetBrowserEmulationVersion()
        {
            int ieVersion;
            BrowserEmulationVersion emulationCode;

            ieVersion = GetInternetExplorerMajorVersion();

            if (ieVersion >= 11)
            {
                emulationCode = BrowserEmulationVersion.Version11;
            }
            else
            {
                switch (ieVersion)
                {
                    case 10:
                        emulationCode = BrowserEmulationVersion.Version10;
                        break;
                    case 9:
                        emulationCode = BrowserEmulationVersion.Version9;
                        break;
                    case 8:
                        emulationCode = BrowserEmulationVersion.Version8;
                        break;
                    default:
                        emulationCode = BrowserEmulationVersion.Version7;
                        break;
                }
            }

            return SetBrowserEmulationVersion(emulationCode);
        }
        public static bool IsBrowserEmulationSet()
        {
            return GetBrowserEmulationVersion() != BrowserEmulationVersion.Default;
        }
    }
    /**
    * Modified from code originally found here: http://support.microsoft.com/kb/326201
    **/


    public class WebBrowserInstanceHelper
    {

        private void SetBrowserFeatureControlKey(string feature, string appName, uint value)
        {
            using (var key = Registry.CurrentUser.CreateSubKey(
                String.Concat(@"Software\Microsoft\Internet Explorer\Main\FeatureControl\", feature),
                RegistryKeyPermissionCheck.ReadWriteSubTree))
            {
                key.SetValue(appName, (UInt32)value, RegistryValueKind.DWord);
            }
        }

        public void SetBrowserFeatureControl()
        {
            // http://msdn.microsoft.com/en-us/library/ee330720(v=vs.85).aspx

            // FeatureControl settings are per-process
            var fileName = System.IO.Path.GetFileName(Process.GetCurrentProcess().MainModule.FileName);

            // make the control is not running inside Visual Studio Designer
            //if (String.Compare(fileName, "devenv.exe", true) == 0 || String.Compare(fileName, "XDesProc.exe", true) == 0)
            //    return;

            SetBrowserFeatureControlKey("FEATURE_BROWSER_EMULATION", fileName, GetBrowserEmulationMode()); // Webpages containing standards-based !DOCTYPE directives are displayed in IE10 Standards mode.
            SetBrowserFeatureControlKey("FEATURE_AJAX_CONNECTIONEVENTS", fileName, 1);
            SetBrowserFeatureControlKey("FEATURE_ENABLE_CLIPCHILDREN_OPTIMIZATION", fileName, 1);
            SetBrowserFeatureControlKey("FEATURE_MANAGE_SCRIPT_CIRCULAR_REFS", fileName, 1);
            SetBrowserFeatureControlKey("FEATURE_DOMSTORAGE ", fileName, 1);
            SetBrowserFeatureControlKey("FEATURE_GPU_RENDERING ", fileName, 1);
            SetBrowserFeatureControlKey("FEATURE_IVIEWOBJECTDRAW_DMLT9_WITH_GDI  ", fileName, 0);
            SetBrowserFeatureControlKey("FEATURE_DISABLE_LEGACY_COMPRESSION", fileName, 1);
            SetBrowserFeatureControlKey("FEATURE_LOCALMACHINE_LOCKDOWN", fileName, 0);
            SetBrowserFeatureControlKey("FEATURE_BLOCK_LMZ_OBJECT", fileName, 0);
            SetBrowserFeatureControlKey("FEATURE_BLOCK_LMZ_SCRIPT", fileName, 0);
            SetBrowserFeatureControlKey("FEATURE_DISABLE_NAVIGATION_SOUNDS", fileName, 1);
            SetBrowserFeatureControlKey("FEATURE_SCRIPTURL_MITIGATION", fileName, 1);
            SetBrowserFeatureControlKey("FEATURE_SPELLCHECKING", fileName, 0);
            SetBrowserFeatureControlKey("FEATURE_STATUS_BAR_THROTTLING", fileName, 1);
            SetBrowserFeatureControlKey("FEATURE_TABBED_BROWSING", fileName, 1);
            SetBrowserFeatureControlKey("FEATURE_VALIDATE_NAVIGATE_URL", fileName, 1);
            SetBrowserFeatureControlKey("FEATURE_WEBOC_DOCUMENT_ZOOM", fileName, 1);
            SetBrowserFeatureControlKey("FEATURE_WEBOC_POPUPMANAGEMENT", fileName, 0);
            SetBrowserFeatureControlKey("FEATURE_WEBOC_MOVESIZECHILD", fileName, 1);
            SetBrowserFeatureControlKey("FEATURE_ADDON_MANAGEMENT", fileName, 0);
            SetBrowserFeatureControlKey("FEATURE_WEBSOCKET", fileName, 1);
            SetBrowserFeatureControlKey("FEATURE_WINDOW_RESTRICTIONS ", fileName, 0);
            SetBrowserFeatureControlKey("FEATURE_XMLHTTP", fileName, 1);
            SetBrowserFeatureControlKey("FEATURE_ENABLE_CLIPCHILDREN_OPTIMIZATION", fileName, 1);
            
            SetBrowserFeatureControlKey("FEATURE_MAXCONNECTIONSPERSERVER", fileName, 200);
        }

        public UInt32 GetBrowserEmulationMode()
        {
            int browserVersion = 7;
            using (var ieKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Internet Explorer",
                RegistryKeyPermissionCheck.ReadSubTree,
                System.Security.AccessControl.RegistryRights.QueryValues))
            {
                var version = ieKey.GetValue("svcVersion");
                if (null == version)
                {
                    version = ieKey.GetValue("Version");
                    if (null == version)
                        throw new ApplicationException("Microsoft Internet Explorer is required!");
                }
                int.TryParse(version.ToString().Split('.')[0], out browserVersion);
            }

            UInt32 mode = 11000; // Internet Explorer 11. Webpages containing standards-based !DOCTYPE directives are displayed in IE11 Standards mode. Default value for Internet Explorer 11.
            switch (browserVersion)
            {
                case 7:
                    mode = 7000; // Webpages containing standards-based !DOCTYPE directives are displayed in IE7 Standards mode. Default value for applications hosting the WebBrowser Control.
                    break;
                case 8:
                    mode = 8000; // Webpages containing standards-based !DOCTYPE directives are displayed in IE8 mode. Default value for Internet Explorer 8
                    break;
                case 9:
                    mode = 9000; // Internet Explorer 9. Webpages containing standards-based !DOCTYPE directives are displayed in IE9 mode. Default value for Internet Explorer 9.
                    break;
                case 10:
                    mode = 10000; // Internet Explorer 10. Webpages containing standards-based !DOCTYPE directives are displayed in IE10 mode. Default value for Internet Explorer 10.
                    break;
                default:
                    // use IE11 mode by default
                    break;
            }

            return mode;
        }

    }

        // Class for deleting the cache.
        public static class WebBrowserHelper
        {
            #region Definitions/DLL Imports
            // For PInvoke: Contains information about an entry in the Internet cache
            [StructLayout(LayoutKind.Explicit, Size = 80)]
            public struct INTERNET_CACHE_ENTRY_INFOA
            {
                [FieldOffset(0)]
                public uint dwStructSize;
                [FieldOffset(4)]
                public IntPtr lpszSourceUrlName;
                [FieldOffset(8)]
                public IntPtr lpszLocalFileName;
                [FieldOffset(12)]
                public uint CacheEntryType;
                [FieldOffset(16)]
                public uint dwUseCount;
                [FieldOffset(20)]
                public uint dwHitRate;
                [FieldOffset(24)]
                public uint dwSizeLow;
                [FieldOffset(28)]
                public uint dwSizeHigh;
                [FieldOffset(32)]
                public System.Runtime.InteropServices.ComTypes.FILETIME LastModifiedTime;
                [FieldOffset(40)]
                public System.Runtime.InteropServices.ComTypes.FILETIME ExpireTime;
                [FieldOffset(48)]
                public System.Runtime.InteropServices.ComTypes.FILETIME LastAccessTime;
                [FieldOffset(56)]
                public System.Runtime.InteropServices.ComTypes.FILETIME LastSyncTime;
                [FieldOffset(64)]
                public IntPtr lpHeaderInfo;
                [FieldOffset(68)]
                public uint dwHeaderInfoSize;
                [FieldOffset(72)]
                public IntPtr lpszFileExtension;
                [FieldOffset(76)]
                public uint dwReserved;
                [FieldOffset(76)]
                public uint dwExemptDelta;
            }
            // For PInvoke: Initiates the enumeration of the cache groups in the Internet cache
            [DllImport(@"wininet",
                SetLastError = true,
                CharSet = CharSet.Auto,
                EntryPoint = "FindFirstUrlCacheGroup",
                CallingConvention = CallingConvention.StdCall)]
            public static extern IntPtr FindFirstUrlCacheGroup(
                int dwFlags,
                int dwFilter,
                IntPtr lpSearchCondition,
                int dwSearchCondition,
                ref long lpGroupId,
                IntPtr lpReserved);
            // For PInvoke: Retrieves the next cache group in a cache group enumeration
            [DllImport(@"wininet",
                SetLastError = true,
                CharSet = CharSet.Auto,
                EntryPoint = "FindNextUrlCacheGroup",
                CallingConvention = CallingConvention.StdCall)]
            public static extern bool FindNextUrlCacheGroup(
                IntPtr hFind,
                ref long lpGroupId,
                IntPtr lpReserved);
            // For PInvoke: Releases the specified GROUPID and any associated state in the cache index file
            [DllImport(@"wininet",
                SetLastError = true,
                CharSet = CharSet.Auto,
                EntryPoint = "DeleteUrlCacheGroup",
                CallingConvention = CallingConvention.StdCall)]
            public static extern bool DeleteUrlCacheGroup(
                long GroupId,
                int dwFlags,
                IntPtr lpReserved);
            // For PInvoke: Begins the enumeration of the Internet cache
            [DllImport(@"wininet",
                SetLastError = true,
                CharSet = CharSet.Auto,
                EntryPoint = "FindFirstUrlCacheEntryA",
                CallingConvention = CallingConvention.StdCall)]
            public static extern IntPtr FindFirstUrlCacheEntry(
                [MarshalAs(UnmanagedType.LPTStr)] string lpszUrlSearchPattern,
                IntPtr lpFirstCacheEntryInfo,
                ref int lpdwFirstCacheEntryInfoBufferSize);
            // For PInvoke: Retrieves the next entry in the Internet cache
            [DllImport(@"wininet",
                SetLastError = true,
                CharSet = CharSet.Auto,
                EntryPoint = "FindNextUrlCacheEntryA",
                CallingConvention = CallingConvention.StdCall)]
            public static extern bool FindNextUrlCacheEntry(
                IntPtr hFind,
                IntPtr lpNextCacheEntryInfo,
                ref int lpdwNextCacheEntryInfoBufferSize);
            // For PInvoke: Removes the file that is associated with the source name from the cache, if the file exists
            [DllImport(@"wininet",
                SetLastError = true,
                CharSet = CharSet.Auto,
                EntryPoint = "DeleteUrlCacheEntryA",
                CallingConvention = CallingConvention.StdCall)]
            public static extern bool DeleteUrlCacheEntry(
                IntPtr lpszUrlName);
            #endregion
            #region Public Static Functions
            /// 
            /// Clears the cache of the web browser
            /// 
            public static void ClearCache()
            {
                // Indicates that all of the cache groups in the user's system should be enumerated
                const int CACHEGROUP_SEARCH_ALL = 0x0;
                // Indicates that all the cache entries that are associated with the cache group
                // should be deleted, unless the entry belongs to another cache group.
                const int CACHEGROUP_FLAG_FLUSHURL_ONDELETE = 0x2;
                // File not found.
                const int ERROR_FILE_NOT_FOUND = 0x2;
                // No more items have been found.
                const int ERROR_NO_MORE_ITEMS = 259;
                // Pointer to a GROUPID variable
                long groupId = 0;
                // Local variables
                int cacheEntryInfoBufferSizeInitial = 0;
                int cacheEntryInfoBufferSize = 0;
                IntPtr cacheEntryInfoBuffer = IntPtr.Zero;
                INTERNET_CACHE_ENTRY_INFOA internetCacheEntry;
                IntPtr enumHandle = IntPtr.Zero;
                bool returnValue = false;
                // Delete the groups first.
                // Groups may not always exist on the system.
                // For more information, visit the following Microsoft Web site:
                // http://msdn.microsoft.com/library/?url=/workshop/networking/wininet/overview/cache.asp   
                // By default, a URL does not belong to any group. Therefore, that cache may become
                // empty even when the CacheGroup APIs are not used because the existing URL does not belong to any group.   
                enumHandle = FindFirstUrlCacheGroup(0, CACHEGROUP_SEARCH_ALL, IntPtr.Zero, 0, ref groupId, IntPtr.Zero);
                // If there are no items in the Cache, you are finished.
                if (enumHandle != IntPtr.Zero && ERROR_NO_MORE_ITEMS == Marshal.GetLastWin32Error())
                {
                    return;
                }
                // Loop through Cache Group, and then delete entries.
                while (true)
                {
                    if (ERROR_NO_MORE_ITEMS == Marshal.GetLastWin32Error() || ERROR_FILE_NOT_FOUND == Marshal.GetLastWin32Error())
                    {
                        break;
                    }
                    // Delete a particular Cache Group.
                    returnValue = DeleteUrlCacheGroup(groupId, CACHEGROUP_FLAG_FLUSHURL_ONDELETE, IntPtr.Zero);
                    if (!returnValue && ERROR_FILE_NOT_FOUND == Marshal.GetLastWin32Error())
                    {
                        returnValue = FindNextUrlCacheGroup(enumHandle, ref groupId, IntPtr.Zero);
                    }
                    if (!returnValue && (ERROR_NO_MORE_ITEMS == Marshal.GetLastWin32Error() || ERROR_FILE_NOT_FOUND == Marshal.GetLastWin32Error()))
                        break;
                }
                // Start to delete URLs that do not belong to any group.
                enumHandle = FindFirstUrlCacheEntry(null, IntPtr.Zero, ref cacheEntryInfoBufferSizeInitial);
                if (enumHandle != IntPtr.Zero && ERROR_NO_MORE_ITEMS == Marshal.GetLastWin32Error())
                {
                    return;
                }
                cacheEntryInfoBufferSize = cacheEntryInfoBufferSizeInitial;
                cacheEntryInfoBuffer = Marshal.AllocHGlobal(cacheEntryInfoBufferSize);
                enumHandle = FindFirstUrlCacheEntry(null, cacheEntryInfoBuffer, ref cacheEntryInfoBufferSizeInitial);
                while (true)
                {
                    internetCacheEntry = (INTERNET_CACHE_ENTRY_INFOA)Marshal.PtrToStructure(cacheEntryInfoBuffer, typeof(INTERNET_CACHE_ENTRY_INFOA));
                    if (ERROR_NO_MORE_ITEMS == Marshal.GetLastWin32Error() || cacheEntryInfoBufferSize == 0)
                    {
                        break;
                    }
                    cacheEntryInfoBufferSizeInitial = cacheEntryInfoBufferSize;
                    returnValue = DeleteUrlCacheEntry(internetCacheEntry.lpszSourceUrlName);
                    if (!returnValue)
                    {
                        returnValue = FindNextUrlCacheEntry(enumHandle, cacheEntryInfoBuffer, ref cacheEntryInfoBufferSizeInitial);
                    }
                    if (!returnValue && ERROR_NO_MORE_ITEMS == Marshal.GetLastWin32Error())
                    {
                        break;
                    }
                    if (!returnValue && cacheEntryInfoBufferSizeInitial > cacheEntryInfoBufferSize)
                    {
                        cacheEntryInfoBufferSize = cacheEntryInfoBufferSizeInitial;
                        cacheEntryInfoBuffer = Marshal.ReAllocHGlobal(cacheEntryInfoBuffer, (IntPtr)cacheEntryInfoBufferSize);
                        returnValue = FindNextUrlCacheEntry(enumHandle, cacheEntryInfoBuffer, ref cacheEntryInfoBufferSizeInitial);
                    }
                }
                Marshal.FreeHGlobal(cacheEntryInfoBuffer);
            }
            #endregion
        }
    }

