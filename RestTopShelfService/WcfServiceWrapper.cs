﻿using SwaggerWcf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Web;
using System.ServiceProcess;
using SwaggerWcf.Models;

namespace RestTopShelfService
{
    public class WcfServiceWrapper<TServiceImplementation, TServiceContract> : ServiceBase
        where TServiceImplementation : TServiceContract
    {
        private readonly string _serviceUri;
        private ServiceHost _serviceHost;

        public WcfServiceWrapper(string serviceName, string serviceUri)
        {
            _serviceUri = serviceUri;
            ServiceName = serviceName;
        }

        protected override void OnStart(string[] args)
        {
            Start();
        }

        protected override void OnStop()
        {
            Stop();
        }

        public void Start()
        {
            Console.WriteLine(ServiceName + " starting...");
            bool openSucceeded = false;
            try
            {
                if (_serviceHost != null)
                {
                    _serviceHost.Close();
                }

                _serviceHost = new ServiceHost(typeof(TServiceImplementation));

            }
            catch (Exception e)
            {
                Console.WriteLine("Caught exception while creating " + ServiceName + ": " + e);
                return;
            }

            try
            {
                var webHttpBinding = new WebHttpBinding(WebHttpSecurityMode.None);
                _serviceHost.AddServiceEndpoint(typeof(TServiceContract), webHttpBinding, _serviceUri);

                //ServiceMetadataBehavior metadataBehavior = new ServiceMetadataBehavior();
                //metadataBehavior.HttpGetEnabled = true;
                //_serviceHost.Description.Behaviors.Add(metadataBehavior);

                var webHttpBehavior = new WebHttpBehavior
                {
                    DefaultOutgoingResponseFormat = WebMessageFormat.Json
                };
                _serviceHost.Description.Endpoints[0].Behaviors.Add(webHttpBehavior);

                _serviceHost.Open();

                openSucceeded = true;

                var swaggerHost = new WebServiceHost(typeof(SwaggerWcfEndpoint), new Uri(_serviceUri));
                swaggerHost.Open();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Caught exception while starting " + ServiceName + ": " + ex);
            }
            finally
            {
                if (!openSucceeded)
                {
                    _serviceHost.Abort();
                }
            }

            if (_serviceHost.State == CommunicationState.Opened)
            {
                Console.WriteLine(ServiceName + " started at " + _serviceUri);
            }
            else
            {
                Console.WriteLine(ServiceName + " failed to open");
                bool closeSucceeded = false;
                try
                {
                    _serviceHost.Close();
                    closeSucceeded = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ServiceName + " failed to close: " + ex);
                }
                finally
                {
                    if (!closeSucceeded)
                    {
                        _serviceHost.Abort();
                    }
                }
            }
        }

        public new void Stop()
        {
            Console.WriteLine(ServiceName + " stopping...");
            try
            {
                if (_serviceHost != null)
                {
                    _serviceHost.Close();
                    _serviceHost = null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Caught exception while stopping " + ServiceName + ": " + ex);
            }
            finally
            {
                Console.WriteLine(ServiceName + " stopped...");
            }
        }
    }
}
