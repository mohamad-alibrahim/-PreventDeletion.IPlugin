using System;
using Microsoft.Xrm.Sdk;



namespace PlugIn
{




public class PreventDeletion : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            IOrganizationServiceFactory serviceFactory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            IOrganizationService service = serviceFactory.CreateOrganizationService(null);

            if (context.MessageName != "Delete" || !context.PreEntityImages.ContainsKey("record")) throw new InvalidPluginExecutionException("Prevent Deletion is not correctly registered.");

             

            Entity contact = context.PreEntityImages["record"];
            
                  if (contact.GetAttributeValue<EntityReference>("createdby").Id != context.InitiatingUserId)
                  {
                            throw new InvalidPluginExecutionException("the contact may only be deleted by the contact Creater"); 
                  }
                  else
                  {
                        return;
                  }
            
            }
             
            
        }
    }
    
    }
