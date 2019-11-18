using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace HospitalManagement.Helper
{
    public class ConnectivityHelper
    {
        public static bool IsInternetAvailable
        {
            get
            {
                var current = Connectivity.NetworkAccess;

                if (current == NetworkAccess.Internet)
                {
                    return true;
                }

                return false;
            }
        }

    }
}
