using Advanced.Net6.Interface;
using Advanced.Net6.Service;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advanced.Net6.DemoIOC
{
    public  class ServiceDemoTest
    {
        public static void ServiceTest() 
        {
            {
                ServiceCollection serviceDescriptors = new ServiceCollection();

                serviceDescriptors.AddTransient<IMircPhone, MircPhone>();

                ServiceProvider serviceProvider = serviceDescriptors.BuildServiceProvider();

                IMircPhone mircPhone = serviceProvider.GetService<IMircPhone>();
            }
        }

        /// <summary>
        /// IOC DI 泛型版本
        /// </summary>
        public static void Show()
        {
            {   //瞬时IOC  serviceProvider 每次都是全新的实例
                ServiceCollection serviceDescriptors = new ServiceCollection();

                serviceDescriptors.AddTransient<IMircPhone, MircPhone>();

                ServiceProvider serviceProvider = serviceDescriptors.BuildServiceProvider();

                IMircPhone mircPhone1 = serviceProvider.GetService<IMircPhone>();
                IMircPhone mircPhone2 = serviceProvider.GetService<IMircPhone>();
                IMircPhone mircPhone3 = serviceProvider.GetService<IMircPhone>();
                IMircPhone mircPhone4 = serviceProvider.GetService<IMircPhone>();
                Console.WriteLine($"mircPhone1 和 mircPhone2 {object.ReferenceEquals(mircPhone1, mircPhone2)}");
                Console.WriteLine($"mircPhone1 和 mircPhone3 {object.ReferenceEquals(mircPhone1, mircPhone3)}");
                Console.WriteLine($"mircPhone2 和 mircPhone4 {object.ReferenceEquals(mircPhone2, mircPhone4)}");

            }

            Console.WriteLine("-----------------------------------------");
            {   //IOC  serviceProvider 单例生命周期  使用的是同一个实例
                ServiceCollection serviceDescriptors = new ServiceCollection();

                serviceDescriptors.AddSingleton<IMircPhone, MircPhone>();

                ServiceProvider serviceProvider = serviceDescriptors.BuildServiceProvider();

                IMircPhone mircPhone1 = serviceProvider.GetService<IMircPhone>();
                IMircPhone mircPhone2 = serviceProvider.GetService<IMircPhone>();
                IMircPhone mircPhone3 = serviceProvider.GetService<IMircPhone>();
                IMircPhone mircPhone4 = serviceProvider.GetService<IMircPhone>();
                Console.WriteLine($"mircPhone1 和 mircPhone2 {object.ReferenceEquals(mircPhone1, mircPhone2)}");
                Console.WriteLine($"mircPhone1 和 mircPhone3 {object.ReferenceEquals(mircPhone1, mircPhone3)}");
                Console.WriteLine($"mircPhone2 和 mircPhone4 {object.ReferenceEquals(mircPhone2, mircPhone4)}");

            }
            Console.WriteLine("-----------------------------------------");

            {   //IOC  serviceProvider  作用域生命周期 作用域之间相同
                ServiceCollection serviceDescriptors = new ServiceCollection();

                serviceDescriptors.AddScoped<IMircPhone, MircPhone>();

                ServiceProvider serviceProvider = serviceDescriptors.BuildServiceProvider();

                IMircPhone mircPhone1 = serviceProvider.GetService<IMircPhone>();
                IMircPhone mircPhone2 = serviceProvider.GetService<IMircPhone>();

                ServiceProvider serviceProvider2 = serviceDescriptors.BuildServiceProvider();

                IMircPhone mircPhone3 = serviceProvider2.GetService<IMircPhone>();
                IMircPhone mircPhone4 = serviceProvider2.GetService<IMircPhone>();
                Console.WriteLine($"mircPhone1 和 mircPhone2 {object.ReferenceEquals(mircPhone1, mircPhone2)}");
                Console.WriteLine($"mircPhone1 和 mircPhone3 {object.ReferenceEquals(mircPhone1, mircPhone3)}");
                Console.WriteLine($"mircPhone2 和 mircPhone4 {object.ReferenceEquals(mircPhone2, mircPhone4)}");

            }
        }

        public static void Show2() {


            {   //瞬时IOC  serviceProvider 每次都是全新的实例
                ServiceCollection serviceDescriptors = new ServiceCollection();
                serviceDescriptors.AddTransient<IMircPhone, MircPhone>();
                ServiceProvider serviceProvider = serviceDescriptors.BuildServiceProvider();
                IMircPhone mircPhone1 = serviceProvider.GetService<IMircPhone>();
            }


            {   //瞬时IOC  serviceProvider 每次都是全新的实例
                ServiceCollection serviceDescriptors = new ServiceCollection();
                serviceDescriptors.AddTransient(typeof(IMircPhone), typeof(MircPhone));
                ServiceProvider serviceProvider = serviceDescriptors.BuildServiceProvider();
                IMircPhone mircPhone1 = serviceProvider.GetService<IMircPhone>();
            }

            {   //瞬时IOC  serviceProvider 每次都是全新的实例
                ServiceCollection serviceDescriptors = new ServiceCollection();
                serviceDescriptors.AddTransient(typeof(IMircPhone), mirc => {

                    IMircPhone mircPhone1 = mirc.GetService<IMircPhone>();
                    return mircPhone1;
                });
                ServiceProvider serviceProvider = serviceDescriptors.BuildServiceProvider();
                MircPhone mircPhone1 = serviceProvider.GetService<MircPhone>();
            }

            {   //瞬时IOC  serviceProvider 每次都是全新的实例
                ServiceCollection serviceDescriptors = new ServiceCollection();
                serviceDescriptors.AddTransient<MircPhone>();
                ServiceProvider serviceProvider = serviceDescriptors.BuildServiceProvider();
                MircPhone mircPhone1 = serviceProvider.GetService<MircPhone>();
            }
        }    
    }
}
