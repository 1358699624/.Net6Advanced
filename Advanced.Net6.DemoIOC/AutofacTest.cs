using Advanced.Net6.Interface;
using Advanced.Net6.Service;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autofac;
using Advanced.Net6.Service.IOC;
using Advanced.Net6.Interface.IOC;
using AutofacAttribue;

namespace Advanced.Net6.DemoIOC
{
    public class AutofacTest
    {


        public static void Show()
        {
            {
                //ContainerBuilder   containerBuilder = new ContainerBuilder();

                //containerBuilder.RegisterType<MircPhone>().As<IMircPhone>();
                //IContainer container = containerBuilder.Build();    
                //IMircPhone mircPhone = container.Resolve<IMircPhone>();
            }

            //构造函数注入
            //{
            //    ContainerBuilder containerBuilder = new ContainerBuilder();

            //    containerBuilder.RegisterType<MircPhone>().As<IMircPhone>();
            //    //containerBuilder.RegisterType<Power>().UsingConstructor(typeof(IMircPhone)).As<IPower>();
            //    //UsingConstructor //指定构造函数
            //    containerBuilder.RegisterType<Power>().UsingConstructor(typeof(IMircPhone), typeof(IMircPhone)).As<IPower>();
            //    IContainer container = containerBuilder.Build();

            //    IPower mircPhone = container.Resolve<IPower>();
            //}

            ////属性注入
            //{
            //    ContainerBuilder containerBuilder = new ContainerBuilder();

            //    containerBuilder.RegisterType<MircPhone>().As<IMircPhone>();
            //    containerBuilder.RegisterType<Power>().As<IPower>();
            //    containerBuilder.RegisterType<Headphone>().As<IHeadphone>();

            //    //containerBuilder.RegisterType<Power>().UsingConstructor(typeof(IMircPhone)).As<IPower>();
            //    //UsingConstructor //指定构造函数

            //    containerBuilder.RegisterType<Phone>().As<IPhone>()
            //        .PropertiesAutowired(); //表示要支持属性注入： 在对象创建出来以后，自动给属性创建实例，赋值上去
            //    IContainer container = containerBuilder.Build();

            //    IPhone mircPhone = container.Resolve<IPhone>();
            //}

            //属性注入
            {
                ContainerBuilder containerBuilder = new ContainerBuilder();

                containerBuilder.RegisterType<MircPhone>().As<IMircPhone>();
                containerBuilder.RegisterType<Power>().As<IPower>();
                containerBuilder.RegisterType<Headphone>().As<IHeadphone>();

                containerBuilder.RegisterType<Phone>().As<IPhone>()
                    .PropertiesAutowired(new CusotmPropertySelector()); //表示要支持属性注入： 在对象创建出来以后，自动给属性创建实例，赋值上去
                IContainer container = containerBuilder.Build();

                IPhone mircPhone = container.Resolve<IPhone>();
            }


            //方法
            {
                ContainerBuilder containerBuilder = new ContainerBuilder();

                containerBuilder.RegisterType<MircPhone>().As<IMircPhone>();
                containerBuilder.RegisterType<Power>().As<IPower>();
                containerBuilder.RegisterType<Headphone>().As<IHeadphone>();

                //containerBuilder.RegisterType<Power>().UsingConstructor(typeof(IMircPhone)).As<IPower>();
                //UsingConstructor //指定构造函数

                containerBuilder.RegisterType<Phone>().As<IPhone>()
                    .OnActivated(activa => {
                      
                        activa.Instance.Out("tdk");
                    })
                    .PropertiesAutowired(); //表示要支持属性注入： 在对象创建出来以后，自动给属性创建实例，赋值上去
                IContainer container = containerBuilder.Build();

                IPhone mircPhone = container.Resolve<IPhone>();
            }




            //构造函数注入
            {
                ContainerBuilder containerBuilder = new ContainerBuilder();

                containerBuilder.RegisterType<MircPhone>().Keyed<IMircPhone>("MircPhone");

                IContainer container = containerBuilder.Build();

                IEnumerable<IMircPhone> ircPhone = container.Resolve<IEnumerable<IMircPhone>>();

                IMircPhone ircPhone2 = container.ResolveKeyed<IMircPhone>("MircPhone");
            }
        }
    }
}
