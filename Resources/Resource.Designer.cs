﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace startup_timer.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resource {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resource() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("startup_timer.Resources.Resource", typeof(Resource).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Startup-Timer.
        /// </summary>
        internal static string appName {
            get {
                return ResourceManager.GetString("appName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 08:00:00.
        /// </summary>
        internal static string dailyTimeSpan {
            get {
                return ResourceManager.GetString("dailyTimeSpan", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to End time: {0}.
        /// </summary>
        internal static string endTime {
            get {
                return ResourceManager.GetString("endTime", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Left: {0}.
        /// </summary>
        internal static string leftTime {
            get {
                return ResourceManager.GetString("leftTime", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to You already worked {0}!.
        /// </summary>
        internal static string overworkAlert {
            get {
                return ResourceManager.GetString("overworkAlert", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Start time: {0}.
        /// </summary>
        internal static string startTime {
            get {
                return ResourceManager.GetString("startTime", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to HH:mm.
        /// </summary>
        internal static string timeFormat {
            get {
                return ResourceManager.GetString("timeFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to WorkTime: {0}.
        /// </summary>
        internal static string workTime {
            get {
                return ResourceManager.GetString("workTime", resourceCulture);
            }
        }
    }
}
