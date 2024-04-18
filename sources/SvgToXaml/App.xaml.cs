// SvgToXaml
// Copyright (C) 2022-2024 Dust in the Wind
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System.Reflection;
using System.Windows;
using Autofac;
using DustInTheWind.SvgToXaml.Application.Transform;
using DustInTheWind.SvgToXaml.FileAccess;
using DustInTheWind.SvgToXaml.Infrastructure;
using DustInTheWind.SvgToXaml.Ports.FileAccess;
using DustInTheWind.SvgToXaml.Ports.UserAccess;
using DustInTheWind.SvgToXaml.UserAccess;
using MediatR.Extensions.Autofac.DependencyInjection;
using MediatR.Extensions.Autofac.DependencyInjection.Builder;

namespace DustInTheWind.SvgToXaml;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : System.Windows.Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        ContainerBuilder containerBuilder = new();
        ConfigureServices(containerBuilder);
        IContainer container = containerBuilder.Build();

        MainWindow = container.Resolve<MainWindow>();
        MainWindow.Show();

        base.OnStartup(e);
    }

    private static void ConfigureServices(ContainerBuilder containerBuilder)
    {
        Assembly useCasesAssembly = typeof(TransformRequest).Assembly;
        MediatRConfiguration mediatRConfiguration = MediatRConfigurationBuilder.Create(useCasesAssembly)
            .WithAllOpenGenericHandlerTypesRegistered()
            .Build();

        containerBuilder.RegisterMediatR(mediatRConfiguration);

        containerBuilder.RegisterType<EventBus>().SingleInstance();
        containerBuilder.RegisterType<RequestBus>().As<IRequestBus>().SingleInstance();

        containerBuilder.RegisterType<UserInteractions>().As<IUserInteractions>();
        containerBuilder.RegisterType<FileSystem>().As<IFileSystem>();

        containerBuilder.RegisterType<OpenFileCommand>();
        containerBuilder.RegisterType<CopyToClipboardCommand>();

        containerBuilder.RegisterType<MainViewModel>();
        containerBuilder.RegisterType<MainWindow>();
    }
}