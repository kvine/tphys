using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NativeLibraryManager;

namespace VerifyServerWebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var accessor = new ResourceAccessor(Assembly.GetExecutingAssembly());
            var libManager = new LibraryManager(
                Assembly.GetExecutingAssembly(),
                //new LibraryItem(Platform.MacOs, Bitness.x64,
                //    new LibraryFile("libTestLib.dylib", accessor.Binary("libTestLib.dylib"))),
                //new LibraryItem(Platform.Windows, Bitness.x64,
                //    new LibraryFile("TestLib.dll", accessor.Binary("TestLib.dll"))),
                //new LibraryItem(Platform.Linux, Bitness.x64,
                //    new LibraryFile("libTestLib.so", accessor.Binary("libTestLib.so")))

                new LibraryItem(Platform.MacOs, Bitness.x64,
                    // new LibraryFile("libVerifyLibrary.dylib", accessor.Binary("libVerifyLibrary.dylib")),
                     new LibraryFile("libVerifyLibrary.so", accessor.Binary("libVerifyLibrary.so"))
                    )
                    );

            libManager.LoadNativeLibrary();
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseUrls("https://*:8861;http://*:8861")
                .UseStartup<Startup>();
    }
}
