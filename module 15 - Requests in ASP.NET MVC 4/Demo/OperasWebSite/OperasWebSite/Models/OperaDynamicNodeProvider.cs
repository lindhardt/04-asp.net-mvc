using MvcSiteMapProvider.Extensibility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OperasWebSite.Models
{
    public class OperaDynamicNodeProvider : DynamicNodeProviderBase
    {
        OperasDB context = new OperasDB();

        public override IEnumerable<DynamicNode> GetDynamicNodeCollection()
        {
            List<DynamicNode> returnList = new List<DynamicNode>();

            foreach (Opera item in context.Operas)
            {
                DynamicNode newNode = new DynamicNode();
                newNode.Title = item.Title;
                newNode.ParentKey = "AllOperas";
                newNode.RouteValues.Add("id", item.OperaID);
                returnList.Add(newNode);
            }

            return returnList;
        }
    }
}