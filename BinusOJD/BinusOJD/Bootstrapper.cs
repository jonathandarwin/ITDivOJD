using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc3;
using BinusOJD.Services;

namespace BinusOJD
{
    public static class Bootstrapper
    {
        public static void Initialise()
        {
            var container = BuildUnityContainer();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();
            // e.g. container.RegisterType<ITestService, TestService>(); 
                       
            container.RegisterType<IUserService, UserService>();
            container.RegisterType<IProjectService, ProjectService>();            

            return container;
        }
    }
}