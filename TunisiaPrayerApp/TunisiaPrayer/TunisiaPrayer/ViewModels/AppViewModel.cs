using System;
using System.Collections.Generic;
using System.Text;

namespace TunisiaPrayer.ViewModels
{
    public class AppViewModel
    {
        //load the app's configs
        void LoadConfigs()
        {
            //if the config file doesn't exist then create it
            if (!ConfigExists())
            {
                CreateConfigFile();
            }
            //load the data from the config file
        }

        //checks if the config file exists 
        bool ConfigExists()
        {
            return true;
        }

        //creates the config file for the app
        void CreateConfigFile()
        {

        }
    }
}
