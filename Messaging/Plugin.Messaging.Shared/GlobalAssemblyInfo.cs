using System.Resources;
using System.Reflection;

#if DEBUG
[assembly: AssemblyConfiguration("DEBUG")]
#endif
#if RELEASE
[assembly: AssemblyConfiguration("RELEASE")]
#endif

[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("Plugin.Messaging")]
[assembly: AssemblyCopyright("Copyright © Carel Lotz 2014-2016")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: NeutralResourcesLanguage("en")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("3.1.0.0")]
[assembly: AssemblyFileVersion("3.1.0.0")]
[assembly: AssemblyInformationalVersion("3.0.0.0")]
