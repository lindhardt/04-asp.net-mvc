using BingMapsRESTToolkit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace LocationChecker
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class LocationCheckerService : ILocationCheckerService
    {
        public string GetLocation(string address)
        {
            string key = "AjRSkj6c-Oa0lhoApMZyta1qOzss1RFEgXKFaCfwvXAaKoqzbKMWmiTE9z6-S-Xa";

            var request = new GeocodeRequest
            {
                BingMapsKey = key,
                Query = address
            };

            Response response = Task.Run(() => ServiceManager.GetResponseAsync(request)).Result;

            Location location = response.ResourceSets[0].Resources.FirstOrDefault() as Location;

            string results = String.Format("Success: {0}:{1}",
                        location.GeocodePoints.FirstOrDefault().GetCoordinate().Latitude,
                        location.GeocodePoints.FirstOrDefault().GetCoordinate().Longitude);

            return results;
        }

    }
}

