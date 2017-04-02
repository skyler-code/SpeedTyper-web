using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using SpeedTyper.LogicLayer;

namespace SpeedTyper.WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;
        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }
        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
        private void AddBindings()
        {
            kernel.Bind<IUserManager>().To<UserManager>();
            kernel.Bind<ITestManager>().To<TestManager>();
            kernel.Bind<IRankManager>().To<RankManager>();
            kernel.Bind<ILevelManager>().To<LevelManager>();
        }
    }
}