// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IoC.cs" company="Web Advanced">
// Copyright 2012 Web Advanced (www.webadvanced.com)
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0

// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


using AlgoTrader.Core.Services;
using AlgoTrader.Core.Services.Interfaces;
using StructureMap;
using StructureMap.Graph;

namespace AlgoTrader.Portfolio.DependencyResolution
{
    /// <summary>
    /// </summary>
    public static class IoC
    {
        /// <summary>
        ///     Initialise the object factory
        /// </summary>
        public static IContainer Initialize()
        {
            ObjectFactory.Initialize(x =>
            {
                x.Scan(scan =>
                {
                    scan.TheCallingAssembly();
                    scan.WithDefaultConventions();
                });

                x.For<IYahooFinanceService>().Use<YahooFinanceService>();
                x.For<IGoogleFinanceService>().Use<GoogleFinanceService>();
                x.For<IFinancialCalculationService>().Use<FinancialCalculationService>();
                x.For<IWebRequestService>().Use<WebRequestService>();
            });

            return ObjectFactory.Container;
        }
    }
}